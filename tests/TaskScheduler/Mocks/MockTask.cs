using NUnit.Framework;
using Moq;
using System.Threading;
using System.Threading.Tasks;
using System;
using Oak.TaskScheduler;

namespace Oak.Tests.TaskScheduler
{
    public class MockTasks
    {
        public Mock<IScheduledTask> Default() 
        {
            var mock = new Mock<IScheduledTask>();

            mock.Setup(m => m.RunOnStartUp).Returns(false);
            mock.Setup(m => m.Run(It.IsNotNull<CancellationToken>())).Returns(Task.CompletedTask);
            mock.Setup(m => m.Occurrence.Next(It.IsAny<DateTime>())).Returns((DateTime d) => { return d.AddMinutes(1); });
            return mock;
        }

        public Mock<IScheduledTask> Daily() 
        {
            var mock = new Mock<IScheduledTask>();

            mock.Setup(m => m.RunOnStartUp).Returns(false);
            mock.Setup(m => m.Run(It.IsNotNull<CancellationToken>())).Returns(async (CancellationToken c) => { await Task.Delay(2000); return; });
            mock.Setup(m => m.Occurrence.Next(It.IsAny<DateTime>())).Returns((DateTime d) => { return d.Date.AddDays(1).AddHours(4); });
            return mock;
        }

        public Mock<IScheduledTask> Startup() 
        {
            var mock = new Mock<IScheduledTask>();

            mock.Setup(m => m.RunOnStartUp).Returns(true);
            mock.Setup(m => m.Run(It.IsNotNull<CancellationToken>())).Returns(Task.CompletedTask);
            mock.Setup(m => m.Occurrence.Next(It.IsAny<DateTime>())).Returns((DateTime d) => { return d.AddMinutes(1); });
            return mock;
        }
    }
}