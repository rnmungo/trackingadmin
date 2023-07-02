using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using BLL.TrackingAdmin.Contracts;
using Domain.TrackingAdmin.Entities;
using TrackingAdmin.ViewModels;

namespace TrackingAdmin.Controllers
{
    [Route("api/distance")]
    [ApiController]
    public class DistanceController : Controller
    {
        private readonly IDistanceService _distanceService;
        private readonly IMapper _mapper;

        public DistanceController(IMapper mapper, IDistanceService distanceService)
        {
            _mapper = mapper;
            _distanceService = distanceService;
        }

        [HttpPost("calibrate")]
        public IActionResult Calibrate([FromBody] DistanceRequestViewModel body)
        {
            List<Distance> distances = _distanceService.GetBestRoute(body.OriginId, body.DestinationIds);
            List<DistanceResponseViewModel> viewModels = _mapper.Map<List<DistanceResponseViewModel>>(distances);
            return Ok(viewModels);
        }
    }
}
