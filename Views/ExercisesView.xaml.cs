using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using WorkoutApp.Database;
using WorkoutApp.Models;
using WorkoutApp.Windows;

namespace WorkoutApp
{
    public partial class ExercisesView : UserControl
    {
        public List<Exercise> ExercisesAll { get; set; }
        public ObservableCollection<Exercise> ExercisesFiltered { get; set; }

        private AppDbContext dbContext { get; set; }
        private Plan Plan { get; set; }

        public ExercisesView()
        {
            InitializeComponent();
            DataContext = this;

            SetExercises(null);
        }

        public ExercisesView(int _planId)
        {
            InitializeComponent();
            DataContext = this;
            PlanButtonsGrid.Visibility = Visibility.Visible;

            SetExercises(_planId);
        }

        private void SetExercises(int? planId)
        {
            dbContext = new AppDbContext();
            SearchTextBox.Text = "";

            ExercisesAll = dbContext.Exercises.OrderBy(exercise => exercise.Name.ToLower()).Include(e => e.BodyPart).ToList();
            if (planId != null)
            {
                Plan = dbContext.Plans.Find(planId);
                List<int> exerciseToShowIds = Plan.Exercises.Select(e => e.Id).ToList();
                ExercisesAll = ExercisesAll.Where(e => exerciseToShowIds.Contains(e.Id)).ToList();
            }

            ExercisesFiltered = new ObservableCollection<Exercise>(ExercisesAll);
        }

        private void RefreshExercises()
        {
            dbContext.Dispose();
            SetExercises(Plan?.Id);
            DataContext = null;
            DataContext = this;
            UpdateLayout();
        }

        private void OpenDetails()
        {
            Exercise selectedExercise = (Exercise)ExercisesListBox.SelectedItem;
            if (selectedExercise == null)
            {
                MessageBox.Show("Nie wybrano pozycji", "Błąd");
                return;
            }

            ExerciseDetailsWindow w = new(selectedExercise.Id);
            w.Show();
        }

        private void SearchTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            ExercisesFiltered.Clear();

            string? text = SearchTextBox.Text?.ToLower();
            List<Exercise> exercisesToShow = string.IsNullOrEmpty(text)
                ? ExercisesAll
                : ExercisesAll.Where(exercise => exercise.Name.ToLower().Contains(text) || exercise.BodyPart.Name.ToLower().Contains(text)).ToList();

            foreach (Exercise exercise in exercisesToShow)
            {
                ExercisesFiltered.Add(exercise);
            }
        }

        private void AddNewButton_Click(object sender, RoutedEventArgs e)
        {
            Window w = new CreateEditExerciseWindow();
            w.Closed += (sender, e) =>
            {
                RefreshExercises();
            };
            w.Show();
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            Exercise selectedExercise = (Exercise)ExercisesListBox.SelectedItem;
            if (selectedExercise == null)
            {
                MessageBox.Show("Nie wybrano pozycji do usunięcia", "Błąd");
                return;
            }

            MessageBoxResult result = MessageBox.Show
            (
                "Czy na pewno chcesz usunąć ćwiczenie? Spowoduje to także usunięcie jego statystyk!",
                "Potwierdzenie usunięcia",
                MessageBoxButton.YesNo,
                MessageBoxImage.Question
            );

            if (result == MessageBoxResult.No)
                return;

            selectedExercise = dbContext.Exercises.Include(e => e.ExerciseStats).Where(e => e.Id == selectedExercise.Id).First();
            foreach (ExerciseStat stat in selectedExercise.ExerciseStats)
                dbContext.Remove(stat);

            dbContext.Remove(selectedExercise);
            dbContext.SaveChanges();
            RefreshExercises();
        }

        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            Exercise selectedExercise = (Exercise)ExercisesListBox.SelectedItem;
            if (selectedExercise == null)
            {
                MessageBox.Show("Nie wybrano pozycji do edycji", "Błąd");
                return;
            }

            Window w = new CreateEditExerciseWindow(selectedExercise);
            w.Closed += (sender, e) =>
            {
                RefreshExercises();
            };
            w.Show();
        }

        private void DetailsButton_Click(object sender, RoutedEventArgs e)
        {
            OpenDetails();
        }

        private void ExercisesListBox_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            OpenDetails();
        }

        private void AddToPlanButton_Click(object sender, RoutedEventArgs e)
        {
            List<int> exerciseToAvoidIds = Plan.Exercises.Select(e => e.Id).ToList();
            AddExerciseToPlanWindow w = new(exerciseToAvoidIds, Plan.Id);
            w.Closed += (sender, e) =>
            {
                RefreshExercises();
            };
            w.Show();
        }

        private void DeleteFromPlanButton_Click(object sender, RoutedEventArgs e)
        {
            Exercise selectedExercise = (Exercise)ExercisesListBox.SelectedItem;
            if (selectedExercise == null)
            {
                MessageBox.Show("Nie wybrano pozycji", "Błąd");
                return;
            }

            MessageBoxResult result = MessageBox.Show
            (
                "Czy na pewno chcesz usunąć ćwiczenie z planu?",
                "Potwierdzenie usunięcia",
                MessageBoxButton.YesNo,
                MessageBoxImage.Question
            );

            if (result == MessageBoxResult.No)
                return;

            Plan plan = dbContext.Plans.Find(Plan.Id);
            selectedExercise = dbContext.Exercises.Find(selectedExercise.Id);
            plan.Exercises.Remove(selectedExercise);

            dbContext.SaveChanges();
            RefreshExercises();
        }
    }
}
