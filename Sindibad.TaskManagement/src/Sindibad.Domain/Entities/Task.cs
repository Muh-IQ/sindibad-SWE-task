using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sindibad.Domain.Entities
{
    public class Task : BaseEntity
    {
        public Guid ProjectId { get; set; }

        public string Title { get; set; } = null!;

        public bool Completed { get; set; } = false;

        public Project Project { get; set; } = null!;
    }
}
