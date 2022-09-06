using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace todo_domain_entities.Entities
{
    public class TodoList
    {
        [Required, Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        [MinLength(3)]
        public string Title { get; set; }

        [MaxLength(500)]
        public string Description { get; set; }

        public bool IsHidden { get; set; }

        public List<TodoItem> TodoItems { get; set; }

        public TodoList()
        {
            TodoItems = new List<TodoItem>();
        }

        public int? UserId { get; set; }

        public User User { get; set; }
    }
}
