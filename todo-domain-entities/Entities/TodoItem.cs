using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace todo_domain_entities.Entities
{
    public class TodoItem
    {
        private DateTime _createdDate;
        private DateTime _duetoDate;

        [Required, Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        [MinLength(3)]
        public string Title { get; set; }

        [MaxLength(500)]
        public string Description { get; set; }

        public TodoItemStatus Status { get; set; }


        [DataType(DataType.DateTime)]
        public DateTime CreatedDate 
        {
            get => _createdDate.ToUniversalTime();
            set => _createdDate = DateTime.SpecifyKind(value, DateTimeKind.Utc);
        }

        [DataType(DataType.DateTime)]
        [Column("DueTo")]
        public DateTime DuetoDateTime 
        {
            get => _duetoDate.ToUniversalTime();
            set => _duetoDate = DateTime.SpecifyKind(value, DateTimeKind.Utc);
        }

        public int? ToDoListId { get; set; }

    }

    public enum TodoItemStatus
    {
        Completed,
        InProgress,
        NotStarted
    }
}
