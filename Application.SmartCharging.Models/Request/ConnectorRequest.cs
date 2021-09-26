using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.SmartCharging.Models
{
   public class ConnectorRequest
    {
        [Range(1, 5)]
        public int Id { get; set; }

        [Range(0.000001, double.MaxValue)]
        public double MaxCurrent { get; set; }
    }
}
