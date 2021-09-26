using System;
using System.Collections.Generic;

#nullable disable

namespace Application.SmartCharging.EFCore.Models
{
    public partial class Group
    {
        public Group()
        {
            Cstations = new HashSet<Cstation>();
        }

        public Guid Id { get; set; }
        public string Name { get; set; }
        public int? Capacity { get; set; }

        public virtual ICollection<Cstation> Cstations { get; set; }
    }
}
