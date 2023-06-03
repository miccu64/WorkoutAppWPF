using System.Collections.Generic;
using System.Windows.Controls;
using WorkoutApp.Models;

namespace WorkoutApp
{
    public partial class ExercisesView : UserControl
    {
        public ExercisesView()
        {
            InitializeComponent();
            InitializeExercises();

            DataContext = this;
        }

        public List<Exercise> Exercises { get; set; }

        private void InitializeExercises()
        {
            Exercises = new List<Exercise>
            {
                new Exercise { Id=1, Name = "Wyciskanie sztangi na ławce płaskiej", BodyPart = "Chest" },
                new Exercise { Id=2,Name = "Przysiady", BodyPart = "Legs" },
                new Exercise { Id=3,Name = "Deska", BodyPart = "Core" },
            };
        }
    }
}
