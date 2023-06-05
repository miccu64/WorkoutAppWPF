using System.Collections.Generic;

namespace WorkoutApp.Models
{
    public class Plan
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Remarks { get; set; }
        public List<Workout> Workouts { get; set; }
        public bool IsDefault { get; set; }
    }
}
