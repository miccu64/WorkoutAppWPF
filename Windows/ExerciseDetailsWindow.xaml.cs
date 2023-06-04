using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using WorkoutApp.Database;
using WorkoutApp.Models;

namespace WorkoutApp.Windows
{
    public partial class ExerciseDetailsWindow : Window
    {
        public Exercise Exercise { get; set; }
        private AppDbContext dbContext { get; set; }


        public ExerciseDetailsWindow(int exerciseId)
        {
            dbContext = new AppDbContext();
            Exercise = dbContext.Exercises.Where(e => e.Id == exerciseId).Include(e => e.BodyPart).Include(e => e.ExerciseStats).Single();

            InitializeComponent();
            DataContext = this;
        }

        private readonly Regex positiveIntRegex = new("^[1-9]\\d*$");
        private readonly Regex unsignedFloatRegex = new(@"^\d*(\.\d{0,2})?$");

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
            if (!int.TryParse(Reps.Text, out int reps) || !float.TryParse(Weight.Text, out float weight))
            {
                MessageBox.Show("Podano nieprawidłowe wartości", "Błąd");
                return;
            }

            ExerciseStat stat = new()
            {
                Reps = reps,
                Weight = weight,
                Date = DateTime.Now
            };
            Exercise.ExerciseStats.Add(stat);
            dbContext.SaveChanges();
        }
    }
}
