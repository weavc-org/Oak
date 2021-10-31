using System;

namespace Oak.TaskScheduler
{
    /// <summary>
    /// Create an occurrence every x seconds.
    /// </summary>
    public class EveryXSecondsOccurrence : TimespanOccurrenceBase, IOccurrence
    {
        /// <summary>
        /// Set a number of seconds between occurrences
        /// </summary>
        /// <param name="seconds">Number of seconds between occurrences</param>
        public EveryXSecondsOccurrence(int seconds) 
        {
            base.SetValues(new TimeSpan(0, 0, seconds));
        }
    }
}

