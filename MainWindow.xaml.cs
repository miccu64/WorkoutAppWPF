using System.ComponentModel;
using System.Windows;
using System.Windows.Controls.Primitives;

namespace WorkoutApp
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            Closing += MainWindow_Closing;
            ViewContainer.Content = new ExercisesView();
            ExercisesButton.IsChecked = true;
        }

        private void MainWindow_Closing(object? sender, CancelEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void ToggleButton_Click(object sender, RoutedEventArgs e)
        {
            foreach (var button in Menu.Children)
            {
                if (button is ToggleButton toggleButton)
                    toggleButton.IsChecked = false;
            }

            if (sender is ToggleButton clickedButton)
            {
                clickedButton.IsChecked = true;
                ViewContainer.Content = null;

                switch (clickedButton.Name)
                {
                    case "PlansButton":
                        ViewContainer.Content = new PlansView(ViewContainer);
                        break;
                    case "ExercisesButton":
                        ViewContainer.Content = new ExercisesView();
                        break;
                }
            }
        }
    }
}
