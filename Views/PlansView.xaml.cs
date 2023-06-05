using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Controls;
using WorkoutApp.Database;
using WorkoutApp.Models;

namespace WorkoutApp
{
    /// <summary>
    /// Interaction logic for TrainingView.xaml
    /// </summary>
    public partial class PlansView : UserControl
    {
        public List<Plan> Plans { get; set; }

        private AppDbContext dbContext { get; set; }

        public PlansView()
        {
            InitializeComponent();
            DataContext = this;

            dbContext = new AppDbContext();
            Plans = dbContext.Plans.ToList();
        }

        private void PlansListBox_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {

        }
    }
}
