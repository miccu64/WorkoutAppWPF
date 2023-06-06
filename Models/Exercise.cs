using System.Collections.Generic;

namespace WorkoutApp.Models
{
    public class Exercise
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public BodyPart BodyPart { get; set; }
        public List<ExerciseStat> ExerciseStats { get; set; }
    }
}
