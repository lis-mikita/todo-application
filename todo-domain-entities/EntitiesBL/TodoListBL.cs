using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace todo_domain_entities.EntitiesBL
{
    public class TodoListBL
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public bool IsHidden { get; set; }

        public int? UserId { get; set; }
    }
}
