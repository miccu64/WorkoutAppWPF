using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace WorkoutApp.Models
{
    public class ExerciseStat
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int Reps { get; set; }
        public float Weight { get; set; }
        public DateTime Date { get; set; }
    }
}
