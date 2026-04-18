using FlatManage.Domain.Entities;
using FlatManage.Domain.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FlatManage.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class BuildingController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public BuildingController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var buildings = await _unitOfWork.Repository<Building>().GetAllAsync();
            return Ok(buildings);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var building = await _unitOfWork.Repository<Building>().Query()
                .Include(b => b.Floors)
                .ThenInclude(f => f.Units)
                .FirstOrDefaultAsync(b => b.Id == id);
            
            if (building == null) return NotFound();
            return Ok(building);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create([FromBody] Building building)
        {
            await _unitOfWork.Repository<Building>().AddAsync(building);
            await _unitOfWork.SaveChangesAsync();
            return CreatedAtAction(nameof(GetById), new { id = building.Id }, building);
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            var building = await _unitOfWork.Repository<Building>().GetByIdAsync(id);
            if (building == null) return NotFound();
            _unitOfWork.Repository<Building>().Delete(building);
            await _unitOfWork.SaveChangesAsync();
            return NoContent();
        }
    }
}
