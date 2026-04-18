using FlatManage.Domain.Entities;
using FlatManage.Domain.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlatManage.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class TenantController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public TenantController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var tenants = await _unitOfWork.Repository<Tenant>().Query()
                .Include(t => t.User)
                .Include(t => t.Unit)
                .ToListAsync();
            return Ok(tenants);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var tenant = await _unitOfWork.Repository<Tenant>().Query()
                .Include(t => t.User)
                .Include(t => t.Unit)
                .Include(t => t.Agreements)
                .FirstOrDefaultAsync(t => t.Id == id);
            
            if (tenant == null) return NotFound();
            return Ok(tenant);
        }
    }

    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class TicketController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public TicketController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var tickets = await _unitOfWork.Repository<Ticket>().Query()
                .Include(t => t.Tenant.User)
                .Include(t => t.Unit)
                .OrderByDescending(t => t.CreatedAt)
                .ToListAsync();
            return Ok(tickets);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Ticket ticket)
        {
            await _unitOfWork.Repository<Ticket>().AddAsync(ticket);
            await _unitOfWork.SaveChangesAsync();
            return Ok(ticket);
        }

        [HttpPatch("{id}/resolve")]
        public async Task<IActionResult> Resolve(int id)
        {
            var ticket = await _unitOfWork.Repository<Ticket>().GetByIdAsync(id);
            if (ticket == null) return NotFound();
            
            ticket.Status = Domain.Enums.TicketStatus.Resolved;
            ticket.ResolvedAt = System.DateTimeOffset.UtcNow;
            _unitOfWork.Repository<Ticket>().Update(ticket);
            await _unitOfWork.SaveChangesAsync();
            return Ok(ticket);
        }
    }
}
