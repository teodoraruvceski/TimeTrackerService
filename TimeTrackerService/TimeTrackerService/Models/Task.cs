using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace TimeTrackerService.Models
{
    public class Task
    {
        public Task(int id, string name, DateTime startTime, DateTime endTime, float duration, string projectName)
        {
            Id = id;
            Name = name;
            StartTime = startTime;
            EndTime = endTime;
            Duration = duration;
            ProjectName = projectName;
        }

        public Task(int id, string name, string projectName)
        {
            Id = id;
            Name = name;
            StartTime = null;
            EndTime = null;
            Duration = null;
            ProjectName = projectName;
        }

        public Task() { }

        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime? StartTime { get; set; }
        public DateTime? EndTime { get; set; }
        public float? Duration { get; set; }
        public string? ProjectName { get; set; }
    }

}
