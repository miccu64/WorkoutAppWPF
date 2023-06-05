using System.Collections.Generic;
using System.Linq;
using System.Windows;
using WorkoutApp.Database;
using WorkoutApp.Models;

namespace WorkoutApp.Windows
{
    public partial class CreateEditPlanWindow : Window
    {
        public List<Plan> Plans { get; set; }

        private AppDbContext dbContext { get; set; }
        private Plan planToEdit { get; set; }

        public CreateEditPlanWindow()
        {
            Init();
        }

        public CreateEditPlanWindow(Plan plan)
        {
            Init();
            planToEdit = plan;
            NameTextbox.Text = planToEdit.Name;
            DescriptionTextbox.Text = planToEdit.Description;
            AddButton.Content = "Edytuj";
            AddWindow.Title = "Edytowanie ćwiczenia";
        }

        private void Init()
        {
            dbContext = new AppDbContext();
            Plans = dbContext.Plans.ToList();

            InitializeComponent();
            DataContext = this;
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            string? name = NameTextbox.Text;
            string? description = DescriptionTextbox.Text;
            if (string.IsNullOrEmpty(name))
            {
                MessageBox.Show("Nie uzupełniono pola z nazwą", "Błąd");
                return;
            }

            if (planToEdit == null)
            {
                dbContext.Add(new Plan()
                {
                    Name = name,
                    Description = description
                });
            }
            else
            {
                Plan? plan = dbContext.Plans.Find(planToEdit.Id);
                if (plan == null)
                    return;

                plan.Name = name;
                plan.Description = description;
            }

            dbContext.SaveChanges();
            Close();
        }
    }
}
