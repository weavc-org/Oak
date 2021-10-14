using System.Threading;
using System.Threading.Tasks;

namespace Oak.TaskScheduler.Services
{
    /// <summary>
    /// Singleton service for executing and tracking tasks in this session. 
    /// </summary>
    public interface ITaskHandler
    {
        Task ExecuteTask(IScheduledTask task, CancellationToken token = default);
    }
}
