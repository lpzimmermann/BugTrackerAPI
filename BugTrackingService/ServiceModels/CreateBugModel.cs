using System.ComponentModel.DataAnnotations;
using Abstractions;

namespace BugTrackingService.ServiceModels
{
    public class CreateBugModel
    {
        [Required]
        public string Title { get; set; }
        [Required]
        public Bug.BugState State { get; set; }
    }
}