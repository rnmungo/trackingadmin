using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using BLL.TrackingAdmin.Contracts;
using Domain.TrackingAdmin.Entities;
using TrackingAdmin.ViewModels;

namespace TrackingAdmin.Controllers
{
    [Route("api/truck")]
    [ApiController]
    public class TruckController : Controller
    {
        private readonly ITruckService _truckService;
        private readonly IMapper _mapper;

        public TruckController(IMapper mapper, ITruckService truckService)
        {
            _mapper = mapper;
            _truckService = truckService;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            List<Truck> trucks = _truckService.GetAll();
            List<TruckResponseViewModel> truckViewModels = _mapper.Map<List<TruckResponseViewModel>>(trucks);
            return Ok(truckViewModels);
        }

        [HttpGet("allowed")]
        public IActionResult GetAllowed()
        {
            List<Truck> trucks = _truckService.GetAllowedTrucks();
            List<TruckResponseViewModel> truckViewModels = _mapper.Map<List<TruckResponseViewModel>>(trucks);
            return Ok(truckViewModels);
        }

        [HttpPost]
        public IActionResult Create([FromBody] TruckCreateRequestViewModel body)
        {
            var truck = _mapper.Map<Truck>(body);
            _truckService.Create(truck);
            return Ok();
        }
    }
}
