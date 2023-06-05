using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using WorkoutApp.Database;
using WorkoutApp.Helpers;
using WorkoutApp.Models;

namespace WorkoutApp.Windows
{
    public partial class ExerciseDetailsWindow : Window
    {
        public Exercise Exercise { get; set; }
        public CollectionViewSource ExerciseStatsViewSource { get; set; }

        private DateTime selectedDate = DateTime.Now;
        public DateTime SelectedDate
        {
            get { return selectedDate; }
            set { selectedDate = value; }
        }

        private AppDbContext dbContext { get; set; }
        private int exerciseId { get; set; }

        public ExerciseDetailsWindow(int _exerciseId)
        {
            exerciseId = _exerciseId;
            InitExerciseStats();

            InitializeComponent();
            DataContext = this;
        }

        public void InitExerciseStats()
        {
            dbContext = new AppDbContext();
            Exercise = dbContext.Exercises.Where(e => e.Id == exerciseId)
                .Include(e => e.BodyPart)
                .Include(e => e.ExerciseStats)
                .Single();

            ExerciseStatsViewSource = new CollectionViewSource();
            ExerciseStatsViewSource.GroupDescriptions.Add(new PropertyGroupDescription("Date", new GroupDateConverter()));
            ExerciseStatsViewSource.SortDescriptions.Add(new SortDescription("Date", ListSortDirection.Descending));
            ExerciseStatsViewSource.Source = Exercise.ExerciseStats;
        }

        private void RefreshExerciseStats()
        {
            dbContext.Dispose();
            InitExerciseStats();
            DataContext = null;
            DataContext = this;
            UpdateLayout();
        }

        private void Reps_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!StaticHelpers.PositiveIntRegex.IsMatch(e.Text))
                e.Handled = true;
        }

        private void Weight_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            TextBox textBox = (TextBox)sender;
            string newText = textBox.Text.Insert(textBox.CaretIndex, e.Text);

            if (!StaticHelpers.UnsignedFloatRegex.IsMatch(newText))
                e.Handled = true;
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            DateTime? date = SelectDatePicker.SelectedDate;
            if (!int.TryParse(Reps.Text, out int reps)
                || !float.TryParse(Weight.Text.Replace(',', '.'), NumberStyles.Float, CultureInfo.InvariantCulture, out float weight)
                || date == null)
            {
                MessageBox.Show("Podano nieprawidłowe wartości", "Błąd");
                return;
            }

            ExerciseStat stat = new()
            {
                Reps = reps,
                Weight = weight,
                Date = date ?? SelectedDate
            };
            Exercise.ExerciseStats.Add(stat);
            dbContext.SaveChanges();

            ExerciseStatsViewSource.View.Refresh();
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            ExerciseStat selectedStat = (ExerciseStat)ListBoxExerciseStats.SelectedItem;
            if (selectedStat == null)
            {
                MessageBox.Show("Nie wybrano pozycji do usunięcia", "Błąd");
                return;
            }

            MessageBoxResult result = MessageBox.Show
            (
                "Czy na pewno chcesz usunąć daną serię?",
                "Potwierdzenie usunięcia",
                MessageBoxButton.YesNo,
                MessageBoxImage.Question
            );

            if (result == MessageBoxResult.No)
                return;

            dbContext.Remove(selectedStat);
            dbContext.SaveChanges();
            RefreshExerciseStats();
        }
    }
}
