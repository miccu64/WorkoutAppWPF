﻿using System.Text.RegularExpressions;

namespace WorkoutApp.Helpers
{
    public static class StaticHelpers
    {
        public static readonly Regex PositiveIntRegex = new("^[1-9]\\d*$");
        public static readonly Regex UnsignedFloatRegex = new(@"^\d*([.,]\d{0,2})?$");
    }
}
