using Microsoft.EntityFrameworkCore;
using Month3AssessmentCode.ApplDbCont;
using Month3AssessmentCode.Models;

namespace Month3AssessmentCode.services
{
    public class TaskService:ITaskService
    {
        public TaskService(ApplicationDBContext applicationDBContext)
        {
            this.applicationDBContext = applicationDBContext;
        }
        private readonly ApplicationDBContext applicationDBContext;


        public async Task<TaskModel> Createtask(string userid,TaskModel taskModel)
        {
            var task= new TaskModel{ Title=taskModel.Title,Description=taskModel.Description};
            applicationDBContext.Add(task);
            await applicationDBContext.SaveChangesAsync();
            return task;
        }

        public async Task<TaskModel> UpdateTask(string userid, int taskId,TaskModel taskModel)
        {

            var task = await applicationDBContext.Tasks.FindAsync(taskId);
            if (task == null)
                return null;

            task.Title = taskModel.Title;
            task.Description = taskModel.Description;

            await applicationDBContext.SaveChangesAsync();
            return task;
        }

        public async Task<List<TaskModel>> GetAllTasks()
        {

            var task = await applicationDBContext.Tasks.ToListAsync();
            if (task == null)
                return null;


            return task;
        }

    }
}
