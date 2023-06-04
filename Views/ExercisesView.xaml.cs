using System.Collections.ObjectModel;
using System.Windows.Controls;
using WorkoutApp.Database;
using WorkoutApp.Models;

namespace WorkoutApp
{
    public partial class ExercisesView : UserControl
    {
        public ObservableCollection<Exercise> Exercises { get; set; }
        private AppDbContext dbContext { get; set; }

        public ExercisesView()
        {
            InitializeComponent();
            DataContext = this;

            dbContext = new AppDbContext();
            Exercises = new ObservableCollection<Exercise>(dbContext.Exercises);
        }

    }
}
