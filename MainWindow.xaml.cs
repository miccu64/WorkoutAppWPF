﻿using System.Windows;
using System.Windows.Controls.Primitives;

namespace WorkoutApp
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
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
