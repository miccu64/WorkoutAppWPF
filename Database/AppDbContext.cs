﻿using Microsoft.EntityFrameworkCore;
using WorkoutApp.Models;

namespace WorkoutApp.Database
{
    public class AppDbContext : DbContext
    {
        public DbSet<BodyPart> BodyParts { get; set; }
        public DbSet<Exercise> Exercises { get; set; }
        public DbSet<Plan> Plans { get; set; }
        public DbSet<ExerciseStat> ExerciseStats { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite(@"DataSource=database.db;");
        }
    }
}
