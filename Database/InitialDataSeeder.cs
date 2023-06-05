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
                new BodyPart() { Id = 1, Name = "Klatka piersiowa" },
                new BodyPart() { Id = 2, Name = "Nogi" },
                new BodyPart() { Id = 3, Name = "Plecy" },
                new BodyPart() { Id = 4, Name = "Pośladki" },
                new BodyPart() { Id = 5, Name = "Biceps" },
                new BodyPart() { Id = 6, Name = "Barki" },
                new BodyPart() { Id = 7, Name = "Triceps" },
                new BodyPart() { Id = 8, Name = "Brzuch" },
                new BodyPart() { Id = 9, Name = "Inne" }
            });

            context.Exercises.AddRange(new List<Exercise>()
            {
                new Exercise { Id = 1, Name = "Wyciskanie sztangi na ławce płaskiej", BodyPart = context.BodyParts.Local.Single(bp => bp.Id == 1) },
                new Exercise { Id = 2, Name = "Przysiady", BodyPart = context.BodyParts.Local.Single(bp => bp.Id == 2) },
                new Exercise { Id = 3, Name = "Deska", BodyPart = context.BodyParts.Local.Single(bp => bp.Id == 8) },
                new Exercise { Id = 4, Name = "Wiosłowanie sztangą", BodyPart = context.BodyParts.Local.Single(bp => bp.Id == 3) },
                new Exercise { Id = 5, Name = "Martwy ciąg", BodyPart = context.BodyParts.Local.Single(bp => bp.Id == 2) },
                new Exercise { Id = 6, Name = "Podciąganie na drążku", BodyPart = context.BodyParts.Local.Single(bp => bp.Id == 3) },
                new Exercise { Id = 7, Name = "Martwy ciąg rumuński", BodyPart = context.BodyParts.Local.Single(bp => bp.Id == 2) },
                new Exercise { Id = 8, Name = "Wyciskanie hantli na ławce płaskiej", BodyPart = context.BodyParts.Local.Single(bp => bp.Id == 1) },
                new Exercise { Id = 9, Name = "Wyciskanie żołnierskie", BodyPart = context.BodyParts.Local.Single(bp => bp.Id == 6) }
            });

            context.Workouts.AddRange(new List<Workout>()
            {
                new Workout()
                {
                    Id = 1,
                    Name = "Góra",
                    Description = "Trening górnych partii",
                    Exercises = context.Exercises.Local.Where(exercise => new int[]{ 1, 4, 6 }.Contains(exercise.Id)).ToList()
                },
                new Workout()
                {
                    Id = 2,
                    Name = "Dół",
                    Description = "Trening dolnych partii",
                    Exercises = context.Exercises.Local.Where(exercise => new int[]{ 2, 3, 5 }.Contains(exercise.Id)).ToList()
                },
            });

            context.Plans.Add(new Plan()
            {
                Id = 1,
                Name = "Góra/dół",
                Remarks = "4x w tygodniu",
                Workouts = context.Workouts.Local.ToList(),
                IsDefault = true
            });

            context.SaveChanges();
        }
    }
}
