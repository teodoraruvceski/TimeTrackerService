using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimeTrackerService.Dto
{
    public class TaskDto
    {
       
        public TaskDto(int id, string name, string startTime, string endTime, float duration, string projectName)
        {
            Id = id;
            Name = name;
            StartTime = startTime;
            EndTime = endTime;
            Duration = duration;
            ProjectName = projectName;
        }

        public TaskDto(int id, string name, string projectName)
        {
            Id = id;
            Name = name;
            StartTime = null;
            EndTime = null;
            Duration = null;
            ProjectName = projectName;
        }

        public TaskDto() { }
        public int Id { get; set; }
        public string Name { get; set; }
        public string? StartTime { get; set; }
        public string? EndTime { get; set; }
        public float? Duration { get; set; }
        public string? ProjectName { get; set; }
        
    }
}
