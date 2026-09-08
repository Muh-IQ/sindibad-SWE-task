using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sindibad.Domain.Entities
{
    public class Project : BaseEntity
    {

        public string Name { get; set; } = null!;

        public ICollection<Task> Tasks { get; set; } = new List<Task>();
    }
}
