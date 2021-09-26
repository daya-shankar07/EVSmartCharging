using Application.SmartCharging.EFCore.Models;
using Application.SmartCharging.Models;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.SmartCharging.Common
{
    public class DomainProfile : Profile
    {
        public DomainProfile()
        {
            CreateMap<Group, GroupResponse>();
            CreateMap<Cstation, CstationResponse>();
            CreateMap<Connector, ConnectorResponse>();
            CreateMap<GroupRequest, Group>();
            CreateMap<GroupRequest, Group>();
            CreateMap<CStationRequest, Cstation>();
            CreateMap<ConnectorRequest, Connector>();

        }
    }
}
