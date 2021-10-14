using System.Threading;
using System.Threading.Tasks;

namespace Oak.TaskScheduler
{
    /// <summary>
    /// Implement to define a scheduled task. The task scheduler will use
    /// the implementation to calculate next run time and trigger the run 
    /// method at the calculated time.
    /// </summary>
    public interface IScheduledTask
    {
        /// <summary>
        /// <see cref="IOccurrence"/> implementation. See <see cref="CronOccurrence"/>,
        /// <see cref="EveryXDaysOccurrence"/>, <see cref="EveryXMinutesOccurrence"/>, 
        /// <see cref="EveryXHoursOccurrence"/> for built in implementations. 
        /// </summary>
        IOccurrence Occurrence { get; }
        /// <summary>
        /// Should the task run on start up? If false the task will run on its next occurrence
        /// from the startup time.
        /// </summary>
        bool RunOnStartUp { get; }

        /// <summary>
        /// This method is called by the task scheduler when the 
        /// next occurrence time is met.
        /// </summary>
        Task Run(CancellationToken token = default);
    }
}
