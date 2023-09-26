using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
namespace TimeTrackerService.Models
{
    public class Project
    {
        public Project() { }
        public Project(string name)
        {
            Name = name;
            Id = 0;
        }

        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
