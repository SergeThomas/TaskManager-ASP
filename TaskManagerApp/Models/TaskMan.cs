﻿using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TaskManagerApp.Models
{
    public class TaskMan
    {
        // creating columns for model table (we will push this to database)
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int TaskNumber { get; set; }

        [Required]
        [DisplayName("User Name")]
        public string AssignedUser { get; set; }

        [DisplayName("Task Details")]
        public string TaskDetails { get; set; }

        [DisplayName("Due Date")]
        public DateTime DueDate { get; set; }   // remember to get from user and convert to date object

        [DisplayName("Date Added")]
        public DateTime DateAdded { get; set; } = DateTime.Now;
        public bool CompletionStatus { get; set; } = false;     // by default every task added is incomplete
    }
}
