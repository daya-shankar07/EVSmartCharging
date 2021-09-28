using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Application.SmartCharging.Models
{
    public class CStationRequest
    {
        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        [Range(1, 5)]
        public List<ConnectorResponse> Connectors { get; set; }  

    }
}
