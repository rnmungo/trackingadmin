using AutoMapper;
using BLL.TrackingAdmin.Contracts;
using BLL.TrackingAdmin.Exceptions;
using Domain.TrackingAdmin.Entities;
using Microsoft.AspNetCore.Mvc;
using TrackingAdmin.ViewModels;

namespace TrackingAdmin.Controllers
{
    [Route("api/roadmap")]
    [ApiController]
    public class RoadMapController : Controller
    {
        private readonly IRoadMapService _roadMapService;
        private readonly IMapper _mapper;

        public RoadMapController(IMapper mapper, IRoadMapService roadMapService)
        {
            _mapper = mapper;
            _roadMapService = roadMapService;
        }

        [HttpGet("search")]
        public IActionResult Search([FromQuery] RoadMapFiltersRequestViewModel viewModel)
        {
            Paged<RoadMap> result = _roadMapService.Search(
                viewModel.SizeLimit,
                viewModel.CurrentPage,
                viewModel.License,
                viewModel.Status);
            Paged<RoadMapPagedResponseViewModel> response = new Paged<RoadMapPagedResponseViewModel>()
            {
                CurrentPage = result.CurrentPage,
                SizeLimit = result.SizeLimit,
                Total = result.Total,
                Results = _mapper.Map<List<RoadMapPagedResponseViewModel>>(result.Results)
            };
            return Ok(response);
        }

        [HttpPost]
        public IActionResult Create([FromBody] RoadMapCreateRequestViewModel viewModel)
        {
            RoadMap roadMap = _mapper.Map<RoadMap>(viewModel);
            try
            {
                _roadMapService.Create(roadMap);
                return Ok();
            }
            catch (BusinessException ex)
            {
                return BadRequest(new { Code = ex.BusinessCode.ToString(), ex.Message });
            }
        }

        [HttpPut("{id}/move-forward")]
        public IActionResult MoveForward(Guid id)
        {
            try
            {
                _roadMapService.MoveForward(id);
                return Ok();
            }
            catch (BusinessException ex)
            {
                return BadRequest(new { Code = ex.BusinessCode.ToString(), ex.Message });
            }
            
        }
    }
}
