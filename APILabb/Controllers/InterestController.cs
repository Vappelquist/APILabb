using APILabb.DTO;
using APILabb.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace APILabb.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class InterestController : ControllerBase
    {
        private readonly Context _context;

        public InterestController(Context context)
        {
            _context = context;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<InterestDTO>>> GetAllInterests()
        {
            var interests = await _context.Interests
                .Select(i => new InterestDTO
                {
                    ID = i.ID,
                    InterestName = i.InterestName,
                    Description = i.Description
                }).ToListAsync();
            return Ok(interests);
        }
        [HttpPost]
        public async Task<ActionResult<InterestDTO>> CreateInterest(InterestCreateDTO dto)
        {
            var interest = new Interest
            {
                InterestName = dto.InterestName,
                Description = dto.Description
            };

            _context.Interests.Add(interest);
            await _context.SaveChangesAsync();

            return Ok(new InterestDTO
            {
                ID = interest.ID,
                InterestName = interest.InterestName,
                Description = interest.Description
            });
        }
    }
}