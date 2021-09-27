    using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Application.SmartCharging.Models
{
    public class GroupResponse :GroupRequest
    {
        public string Id { get; set; }
        public IEnumerable<CstationResponse> CStations { get; set; }

    }
}
