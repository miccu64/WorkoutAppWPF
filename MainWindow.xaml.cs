using System.ComponentModel;
using System.Windows;
using System.Windows.Controls.Primitives;
using WorkoutApp.Windows;

namespace WorkoutApp
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            Closing += MainWindow_Closing;
            ExerciseDetailsWindow w = new(1);
            w.Show();
        }

        private void MainWindow_Closing(object sender, CancelEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void ToggleButton_Click(object sender, RoutedEventArgs e)
        {
            // Unselect all ToggleButtons in the StackPanel
            foreach (var button in Menu.Children)
            {
                if (button is ToggleButton toggleButton)
                {
                    toggleButton.IsChecked = false;
                }
            }

            // Select the clicked ToggleButton
            if (sender is ToggleButton clickedButton)
            {
                clickedButton.IsChecked = true;

                // Reset the content of the ViewContainer
                ViewContainer.Content = null;

                // Create and set the appropriate view based on the clicked button
                switch (clickedButton.Name)
                {
                    case "Trainings":
                        ViewContainer.Content = new TrainingsView();
                        break;
                    case "Plans":
                        ViewContainer.Content = new PlansView();
                        break;
                    case "Exercises":
                        ViewContainer.Content = new ExercisesView();
                        break;
                }
            }
        }
    }
}
