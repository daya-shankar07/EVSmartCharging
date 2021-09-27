using Application.SmartCharging.EFCore.Models;
using Application.SmartCharging.Models;
using AutoMapper;

namespace Application.SmartCharging.Common
{
    /// <summary>
    /// for automapper
    /// </summary>
    public class DomainProfile : Profile
    {
        public DomainProfile()
        {
            CreateMap<Group, GroupResponse>();
            //CreateMap<CStat>
            CreateMap<Cstation, CstationResponse>();
            CreateMap<Connector, ConnectorResponse>();
            CreateMap<GroupRequest, Group>();
            CreateMap<GroupRequest, Group>();
            CreateMap<CStationRequest, Cstation>();
            CreateMap<ConnectorRequest, Connector>();

        }
    }
}
