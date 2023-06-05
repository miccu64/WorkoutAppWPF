using System.Collections.Generic;
using System.Windows.Controls;
using WorkoutApp.Models;

namespace WorkoutApp
{
    public partial class PlansView : UserControl
    {
        public PlansView()
        {
            InitializeComponent();

            DataContext = this;
        }

        public List<Exercise> Exercises { get; set; }

        private void AddNewButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {

        }

        private void DeleteButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {

        }

        private void EditButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {

        }

        private void DetailsButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {

        }

        private void ExercisesListBox_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {

        }
    }
}
