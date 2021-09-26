using System.Collections.Generic;

namespace Application.SmartCharging.Models
{
    public class CstationResponse : CStationRequest
    {
        public string StationId { get; set; }
        public string GroupId { get; set; }
      
    }
}
