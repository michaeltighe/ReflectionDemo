using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ReflectionDemo.Models;

namespace ReflectionDemo.Data
{
    public class UserRepository
    {
        public IEnumerable<User> All()
        {
            return new List<User>
            {
                new User{FirstName = "Michael", LastName = "Tighe", Active = true, CreatedAt = DateTime.Now.AddDays(-60), Email = "mtighe@nerdery.com", Password="79054025255fb1a26e4bc422aef54eb4"},
                new User{FirstName = "Adam", LastName = "Witter", Active = true, CreatedAt = DateTime.Now.AddDays(-45), Email = "awitter@nerdery.com", Password="9e107d9d372bb6826bd81d3542a419d6"},
                new User{FirstName = "Andrew", LastName = "Tongen", Active = true, CreatedAt = DateTime.Now.AddDays(-10), Email = "atongen@nerdery.com", Password="e4d909c290d0fb1ca068ffaddf22cbd0"},
                new User{FirstName = "Jordan", LastName = "Cox", Active = false, CreatedAt = DateTime.Now.AddDays(-90), Email = "jcox@nerdery.com", Password="d41d8cd98f00b204e9800998ecf8427e"},
                new User{FirstName = "Mark", LastName = "Spooner", Active = false, CreatedAt = DateTime.Now.AddDays(-30), Email = "mspooner@nerdery.com", Password="098f6bcd4621d373cade4e832627b4f6"},
            };
        }
    }
}
