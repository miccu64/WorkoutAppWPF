using System.Collections.Generic;
using System.Windows.Controls;
using WorkoutApp.Models;

namespace WorkoutApp
{
    public partial class TrainingsView : UserControl
    {
        public TrainingsView()
        {
            InitializeComponent();

            DataContext = this;
        }

        public List<Exercise> Exercises { get; set; }
    }
}
