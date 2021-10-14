using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;

namespace Oak.TaskScheduler.Services
{
    /// <summary>
    /// A new <see cref="ITaskScope"/> is created every iteration of the 
    /// scheduler. Its job is to handle the lifetime of the <see cref="IServiceScope"/> 
    /// and tasks.
    /// </summary>
    public interface ITasksScope
    {
        /// <summary>
        /// Handles execution of a new task set in a new service scope.
        /// This will run through each of the tasks & pass them through to the TaskHandler
        /// </summary>
        Task Handle(IServiceScope scope, CancellationToken token = default);
    }
}
