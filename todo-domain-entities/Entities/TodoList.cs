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
        public int Description { get; set; }

        public List<TodoItem> TodoItems { get; set; } 

    }
}
