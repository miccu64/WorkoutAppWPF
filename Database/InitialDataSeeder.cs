using System.Collections.Generic;
using System.Linq;
using WorkoutApp.Models;

namespace WorkoutApp.Database
{
    public static class InitialDataSeeder
    {
        public static void Seed(AppDbContext context)
        {
            if (!context.Database.EnsureCreated())
                return;

            context.BodyParts.AddRange(new List<BodyPart>()
            {
                new BodyPart() { Id=1, Name="Klatka piersiowa" },
                new BodyPart() { Id=2, Name="Nogi" },
                new BodyPart() { Id=3, Name="Plecy" },
                new BodyPart() { Id=4, Name="Pośladki" },
                new BodyPart() { Id=5, Name="Biceps" },
                new BodyPart() { Id=6, Name="Barki" },
                new BodyPart() { Id=7, Name="Triceps" },
                new BodyPart() { Id=8, Name="Brzuch" }
            });

            context.Exercises.AddRange(new List<Exercise>()
            {
                new Exercise { Id=1, Name = "Wyciskanie sztangi na ławce płaskiej", BodyPart = context.BodyParts.Local.Single(bp => bp.Id == 1) },
                new Exercise { Id=2, Name = "Przysiady", BodyPart = context.BodyParts.Local.Single(bp => bp.Id == 2) },
                new Exercise { Id=3, Name = "Deska", BodyPart = context.BodyParts.Local.Single(bp => bp.Id == 8) }
            });

            context.SaveChanges();
        }
    }
}
