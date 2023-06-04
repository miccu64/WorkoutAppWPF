using System.Windows;
using WorkoutApp.Database;

namespace WorkoutApp
{
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            AppDbContext context = new AppDbContext();
            InitialDataSeeder.Seed(context);
        }
    }
}
