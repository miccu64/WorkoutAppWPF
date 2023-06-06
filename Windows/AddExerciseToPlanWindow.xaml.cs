using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using WorkoutApp.Database;
using WorkoutApp.Models;

namespace WorkoutApp.Windows
{
    public partial class AddExerciseToPlanWindow : Window
    {
        public List<Exercise> Exercises { get; set; }
        public Exercise SelectedExercise { get; set; }

        private AppDbContext dbContext { get; set; }
        private int planId { get; set; }

        public AddExerciseToPlanWindow(List<int> _exercisesToAvoidIds, int _planId)
        {
            dbContext = new AppDbContext();
            Exercises = dbContext.Exercises.Where(e => !_exercisesToAvoidIds.Contains(e.Id))
                .OrderBy(e => e.Name.ToLower())
                .Include(e => e.BodyPart)
                .ToList();
            planId = _planId;

            InitializeComponent();
            DataContext = this;
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            Exercise? exercise = (Exercise)ExerciseComboBox.SelectedItem;
            if (exercise == null)
            {
                MessageBox.Show("Nie wybrano ćwiczenia", "Błąd");
                return;
            }

            Plan plan = dbContext.Plans.Where(p => p.Id == planId)
                .Include(p => p.Exercises)
                .Single();
            exercise = dbContext.Exercises.Find(exercise.Id);
            if (plan == null || exercise == null)
                return;

            plan.Exercises.Add(exercise);
            dbContext.SaveChanges();
            Close();
        }

        private void ExerciseComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            SelectedExercise = (Exercise)ExerciseComboBox.SelectedItem;
            BodyPartLabel.Content = SelectedExercise.BodyPart.Name;
        }
    }
}
