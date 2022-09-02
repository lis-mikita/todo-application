using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System;
using todo_domain_entities.EntitiesBL;

namespace todo_aspnetmvc_ui.Models
{
    public class TodoItemModel
    {
        private DateTime _createdDate;
        private DateTime _duetoDate;

        public int Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public TodoItemStatus Status { get; set; }

        public DateTime CreatedDate
        {
            get => _createdDate.ToUniversalTime();
            set => _createdDate = DateTime.SpecifyKind(value, DateTimeKind.Utc);
        }

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
