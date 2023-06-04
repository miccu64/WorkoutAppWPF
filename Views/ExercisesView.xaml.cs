using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using WorkoutApp.Database;
using WorkoutApp.Models;

namespace WorkoutApp
{
    public partial class ExercisesView : UserControl
    {
        public List<Exercise> ExercisesAll { get; set; }
        public ObservableCollection<Exercise> ExercisesFiltered { get; set; }

        private AppDbContext dbContext { get; set; }

        public ExercisesView()
        {
            InitializeComponent();
            DataContext = this;

            dbContext = new AppDbContext();
            ExercisesAll = dbContext.Exercises.OrderBy(exercise => exercise.Name).Include(e => e.BodyPart).ToList();
            ExercisesFiltered = new ObservableCollection<Exercise>(ExercisesAll);
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

        private void ExercisesListBox_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            Exercise selectedExercise = (Exercise)ExercisesListBox.SelectedItem;

        }
    }
}
