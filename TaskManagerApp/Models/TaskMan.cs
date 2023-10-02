using System.ComponentModel.DataAnnotations;

namespace TaskManagerApp.Models
{
    public class TaskMan
    {
        // creating columns for model table (we will push this to database)
        [Key]
        public int TaskNumber { get; set; }

        [Required]
        public string AssignedUser { get; set; }
        public string TaskDetails { get; set; }
        public DateTime DueDate { get; set; }   // remember to get from user and convert to date object
        public DateTime DateAdded { get; set; } = DateTime.Now;
        public bool CompletionStatus { get; set; } = false;     // by default every task added is incomplete
    }
}
