using System;

namespace Oak.TaskScheduler
{
    public abstract class TimespanOccurrenceBase
    {
        protected TimespanOccurrenceBase()
        {
        }

        protected virtual TimeSpan Timespan { get; private set; } = default;
        protected virtual TimeSpan Offset { get; private set; } = default;

        protected virtual void SetValues(TimeSpan timespan, TimeSpan offset = default)
        {
            if (offset >= timespan)
                throw new Exception("Offset cannot exceed (or equal) the timespan between scheduled occurrences.");

            this.Timespan = timespan;
            this.Offset = offset;
        }

        public virtual DateTime Next(DateTime from)
        {
            return this.calculate(from);
        }

        protected virtual DateTime calculate(DateTime from)
        {
            // Convert to ticks
            var dateTicks = from.Ticks;
            var timespanTicks = this.Timespan.Ticks;

            // Find the last matching value by taking the remainder of from / timespan
            // and substracting it from the from date
            var last = dateTicks - (dateTicks % timespanTicks);
            
            // Add the occurrence timespan to the last value to get the next occurrence value
            var next = last + timespanTicks;

            var date = new DateTime(next);

            // Add the offset to the next occurrence
            date = date + Offset;

            // If the offset has caused the timespan between runs to be greater
            // than the scheduled occurrence, offset the value by the timespan
            if ((date - from) > this.Timespan)
                date = date.Add(-this.Timespan);

            return date;
        }
    }
}

