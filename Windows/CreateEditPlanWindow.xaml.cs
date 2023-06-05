using System.Collections.Generic;
using System.Linq;
using System.Windows;
using WorkoutApp.Database;
using WorkoutApp.Models;

namespace WorkoutApp.Windows
{
    public partial class CreateEditExerciseWindow : Window
    {
        public List<BodyPart> BodyParts { get; set; }

        private AppDbContext dbContext { get; set; }
        private Exercise exerciseToEdit { get; set; }

        public CreateEditExerciseWindow()
        {
            Init();
        }

        public CreateEditExerciseWindow(Exercise exercise)
        {
            Init();
            exerciseToEdit = exercise;
            ExerciseName.Text = exerciseToEdit.Name;
            BodyPartComboBox.SelectedValue = exerciseToEdit.BodyPart.Id;
            AddButton.Content = "Edytuj";
            AddWindow.Title = "Edytowanie ćwiczenia";
        }

        private void Init()
        {
            dbContext = new AppDbContext();
            BodyParts = dbContext.BodyParts.ToList();

            InitializeComponent();
            DataContext = this;
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            string? text = ExerciseName.Text;
            int? partId = (int)BodyPartComboBox.SelectedValue;
            if (string.IsNullOrEmpty(text) || !(partId > 0))
            {
                MessageBox.Show("Nie uzupełniono wszystkich pól", "Błąd");
                return;
            }

            if (exerciseToEdit == null)
            {
                dbContext.Add(new Exercise()
                {
                    BodyPart = dbContext.BodyParts.First(bp => bp.Id == partId),
                    Name = text
                });
            }
            else
            {
                Exercise? exercise = dbContext.Exercises.Find(exerciseToEdit.Id);
                if (exercise == null)
                    return;

                exercise.Name = text;
                exercise.BodyPart = dbContext.BodyParts.Find(partId) ?? exercise.BodyPart;
            }

            dbContext.SaveChanges();
            Close();
        }
    }
}
