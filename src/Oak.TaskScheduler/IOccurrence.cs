using System;

namespace Oak.TaskScheduler
{
    /// <summary>
    /// Calculate next occurrence for a scheduled task. See <see cref="CronOccurrence"/>,
    /// <see cref="EveryXDaysOccurrence"/>, <see cref="EveryXMinutesOccurrence"/>, <see cref="EveryXHoursOccurrence"/>
    /// for implemtation examples.
    /// </summary>
    public interface IOccurrence
    {
        /// <summary>
        /// Get next occurrence after provided date.
        /// </summary>
        /// <returns>DateTime of next occurrence.</returns>
        DateTime Next(DateTime from);
    }
}
