    using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Application.SmartCharging.Models
{
    public class GroupRequest
    {
        [Required]
        [StringLength(127)]
        public string Name { get; set; }

        [Range(0.000001, double.MaxValue)]
        public double Capacity { get; set; }

        [Range(0, int.MaxValue)]
        public IEnumerable<CStationRequest> CStations { get; set; }
    }
}
