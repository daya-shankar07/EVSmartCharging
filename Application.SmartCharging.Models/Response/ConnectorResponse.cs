using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.SmartCharging.Models
{
   public class ConnectorResponse: ConnectorRequest
    {
        public string CStationId { get; set; }
    }
}
