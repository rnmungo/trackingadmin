using AutoMapper;
using BLL.TrackingAdmin.Contracts;
using Domain.TrackingAdmin.Entities;
using Microsoft.AspNetCore.Mvc;
using TrackingAdmin.ViewModels;

namespace TrackingAdmin.Controllers
{
    [Route("api/location")]
    [ApiController]
    public class LocationController : Controller
    {
        private readonly ILocationService _locationService;
        private readonly IMapper _mapper;

        public LocationController(IMapper mapper, ILocationService locationService)
        {
            _mapper = mapper;
            _locationService = locationService;
        }

        public IActionResult GetAll()
        {
            List<Location> locations = _locationService.GetAll();
            List<LocationResponseViewModel> locationViewModels = _mapper.Map<List<LocationResponseViewModel>>(locations);
            return Ok(locationViewModels);
        }
    }
}
