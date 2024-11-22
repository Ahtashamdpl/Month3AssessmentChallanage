using Month3AssessmentCode.Models;

namespace Month3AssessmentCode.services
{
    public interface ITaskService
    {
        Task<TaskModel> Createtask(string userid, TaskModel taskModel);
        Task<List<TaskModel>> GetAllTasks();
        Task<TaskModel> UpdateTask(string userid, int taskId, TaskModel taskModel);
    }
}
