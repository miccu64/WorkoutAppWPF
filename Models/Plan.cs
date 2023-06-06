using System.Collections.Generic;

namespace WorkoutApp.Models
{
    public class Plan
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public List<Exercise> Exercises { get; set; }
    }
}
