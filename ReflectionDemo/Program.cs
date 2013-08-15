using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ReflectionDemo.Data;
using ReflectionDemo.Models;

namespace ReflectionDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            var repo = new UserRepository();
            var users = repo.All();
            var destination = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "user_data.csv");
            
            var exporter = new Exporter<User>();
            exporter.SetFormat(u => u.CreatedAt, dt => dt.ToString("MM/dd/yyyy"));
            exporter.SetFormat(u => u.Active, active => active ? "Active" : "Inactive");

            var csv = exporter.Export(users);
            File.WriteAllText(destination, csv);
        }
    }
}
