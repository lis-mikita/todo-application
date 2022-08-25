using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System;

namespace todo_aspnetmvc_ui.Models
{
    public class TodoItemModel
    {
        [Required, Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(500)]
        public string TodoListId { get; set; }

        [Required]
        [MaxLength(50)]
        [MinLength(3)]
        public string Title { get; set; }

        [MaxLength(500)]
        public string Description { get; set; }

        public TodoItemStatus Status { get; set; }

        [Obsolete("Property only used for EF-serialization purposes")]
        [DataType(DataType.DateTime)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public DateTime CreatedDate
        {
            get => CreatedDate.ToUniversalTime();
            set => CreatedDate = DateTime.SpecifyKind(value, DateTimeKind.Utc);
        }

        [DataType(DataType.DateTime)]
        [Column("DueTo")]
        public DateTime DuetoDateTime
        {
            get => DuetoDateTime.ToUniversalTime();
            set => DuetoDateTime = DateTime.SpecifyKind(value, DateTimeKind.Utc);
        }
    }

    public enum TodoItemStatus
    {
        Completed,
        InProgress,
        NotStarted
    }
}
