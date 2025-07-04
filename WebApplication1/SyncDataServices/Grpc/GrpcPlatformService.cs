using System.Threading.Tasks;
using AutoMapper;
using Grpc.Core;
using WebApplication1.Data;

namespace WebApplication1.SyncDataServices.Grpc
{
    public class GrpcPlatformService : GrpcPlatform.GrpcPlatformBase
    {
        private readonly IPlatformRepo _repository;
        private readonly IMapper _mapper;

        public GrpcPlatformService(IPlatformRepo repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public override Task<PlatformResponse> GetAllPlatforms(GetAllRequests request, ServerCallContext context)
        {
            var response = new PlatformResponse();
            var platforms = _repository.GetAll();

            foreach (var plat in platforms)
                response.Platform.Add(_mapper.Map<GrpcPlatformModel>(plat));
                
            return Task.FromResult(response);
        }
    }
}