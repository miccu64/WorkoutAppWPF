using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using WorkoutApp.Database;
using WorkoutApp.Models;
using WorkoutApp.Windows;

namespace WorkoutApp
{
    public partial class PlansView : UserControl
    {
        public List<Plan> Plans { get; set; }

        private AppDbContext dbContext { get; set; }
        private ContentControl mainContentControl { get; set; }

        public PlansView(ContentControl _contentControl)
        {
            InitializeComponent();
            mainContentControl = _contentControl;

            DataContext = this;
            SetPlans();
        }

        private void SetPlans()
        {
            dbContext = new AppDbContext();
            Plans = dbContext.Plans.OrderBy(p => p.Name.ToLower()).ToList();
        }

        private void RefreshPlans()
        {
            dbContext.Dispose();
            SetPlans();
            DataContext = null;
            DataContext = this;
            UpdateLayout();
        }

        private void OpenDetails()
        {
            Plan selectedPlan = (Plan)PlansListBox.SelectedItem;
            if (selectedPlan == null)
            {
                MessageBox.Show("Nie wybrano pozycji", "Błąd");
                return;
            }
            selectedPlan = dbContext.Plans.Where(p => p.Id == selectedPlan.Id)
                .Include(p => p.Exercises)
                .Single();

            mainContentControl.Content = new ExercisesView(selectedPlan.Id);
        }

        private void AddNewButton_Click(object sender, RoutedEventArgs e)
        {
            CreateEditPlanWindow w = new();
            w.Closed += (sender, e) =>
            {
                RefreshPlans();
            };
            w.Show();
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            Plan selectedPlan = (Plan)PlansListBox.SelectedItem;
            if (selectedPlan == null)
            {
                MessageBox.Show("Nie wybrano pozycji do usunięcia", "Błąd");
                return;
            }

            MessageBoxResult result = MessageBox.Show
            (
                "Czy na pewno chcesz usunąć plan?",
                "Potwierdzenie usunięcia",
                MessageBoxButton.YesNo,
                MessageBoxImage.Question
            );

            if (result == MessageBoxResult.No)
                return;

            dbContext.Remove(selectedPlan);
            dbContext.SaveChanges();
            RefreshPlans();
        }

        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            Plan selectedPlan = (Plan)PlansListBox.SelectedItem;
            if (selectedPlan == null)
            {
                MessageBox.Show("Nie wybrano pozycji do edycji", "Błąd");
                return;
            }

            Window w = new CreateEditPlanWindow(selectedPlan);
            w.Closed += (sender, e) =>
            {
                RefreshPlans();
            };
            w.Show();
        }

        private void DetailsButton_Click(object sender, RoutedEventArgs e)
        {
            OpenDetails();
        }

        private void PlansListBox_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            OpenDetails();
        }
    }
}
