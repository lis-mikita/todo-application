using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using todo_domain_entities.Entities;

namespace todo_aspnetmvc_ui.Models
{
    public class TodoListModel
    {
        [Required, Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        [MinLength(3)]
        public string Title { get; set; }

        [MaxLength(500)]
        public string Description { get; set; }

        public List<TodoItem> TodoItems { get; set; }
    }
}
