using System;
using System.Collections.Generic;
using System.Text;

namespace todo_domain_entities.EntitiesBL
{
    public class UserBL
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
