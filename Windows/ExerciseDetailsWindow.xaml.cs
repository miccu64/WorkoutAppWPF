using Microsoft.EntityFrameworkCore;
using System;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using WorkoutApp.Database;
using WorkoutApp.Models;

namespace WorkoutApp.Windows
{
    public partial class ExerciseDetailsWindow : Window
    {
        public Exercise Exercise { get; set; }
        private AppDbContext dbContext { get; set; }

        public CollectionViewSource ExerciseStatsViewSource { get; set; }

        public ExerciseDetailsWindow(int exerciseId)
        {
            dbContext = new AppDbContext();
            Exercise = dbContext.Exercises.Where(e => e.Id == exerciseId).Include(e => e.BodyPart).Include(e => e.ExerciseStats).Single();

            InitializeComponent();
            DataContext = this;
           // ListBoxExerciseStats.ItemsSource = Exercise.ExerciseStats;
        }

        private readonly Regex positiveIntRegex = new Regex("^[1-9]\\d*$");
        private readonly Regex unsignedFloatRegex = new Regex(@"^\d*([.,]\d{0,2})?$");

        private void Reps_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!positiveIntRegex.IsMatch(e.Text))
            {
                e.Handled = true;
            }
        }

        private void Weight_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            TextBox textBox = (TextBox)sender;
            string newText = textBox.Text.Insert(textBox.CaretIndex, e.Text);

            if (!unsignedFloatRegex.IsMatch(newText))
            {
                e.Handled = true;
            }
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            if (!int.TryParse(Reps.Text, out int reps) || !float.TryParse(Weight.Text.Replace(',', '.'), NumberStyles.Float, CultureInfo.InvariantCulture, out float weight))
            {
                MessageBox.Show("Podano nieprawidłowe wartości", "Błąd");
                return;
            }

            ExerciseStat stat = new ExerciseStat()
            {
                Reps = reps,
                Weight = weight,
                Date = DateTime.Now
            };
            Exercise.ExerciseStats.Add(stat);
            dbContext.SaveChanges();

            ListBoxExerciseStats.ItemsSource = null;
            ListBoxExerciseStats.ItemsSource = Exercise.ExerciseStats;
        }

    }
}
