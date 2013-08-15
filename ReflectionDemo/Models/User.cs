using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ReflectionDemo.Attributes;

namespace ReflectionDemo.Models
{
    public class User
    {
        public override string ToString()
        {
            return string.Format("{0} {1}", FirstName, LastName);
        }

        [Export(DisplayName = "First Name", Order = 1)]
        public string FirstName { get; set; }

        [Export(DisplayName = "Last Name", Order = 2)]
        public string LastName { get; set; }

        [Export(DisplayName = "Email Address", Order = 3)]
        public string Email { get; set; }

        [Export(DisplayName = "Join Date", Order = 4)]
        public DateTime CreatedAt { get; set; }

        [Export(DisplayName = "State", Order = 5)]
        public bool Active { get; set; }

        [Export(false)]
        public string Password { get; set; }
    }
}