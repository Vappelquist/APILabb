using APILabb.DTO;
using APILabb.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;

namespace APILabb.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly Context _context;
        public UserController(Context context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserCreateDTO>>> GetAllUsers()
        {
            var users = await _context.Users
                .Select(u => new UserCreateDTO
                {
                    ID = u.ID,
                    Name = u.Name,
                    PhoneNumber = u.PhoneNumber,
                    Interests = _context.Connections
                    .Where(c => c.UserID == u.ID)
                    .Select(c => c.Interest)
                    .Distinct()
                    .Select(i => new InterestViewDTO
                    {
                        ID = i.ID,
                        InterestName = i.InterestName,
                        Description = i.Description,
                        Links = _context.Connections
                        .Where(c => c.UserID == u.ID && c.InterestID == i.ID)
                        .Select(c => new LinkDTO
                        {
                            ID = c.ID,
                            URL = c.URL
                        }).ToList()
                    }).ToList()
                }).ToListAsync();
            return Ok(users);
        }

        [HttpGet("{userID}/Interests")]
        public async Task<ActionResult<UserCreateDTO>> GetInterestFromId(int userID)
        {
            var userExists = await _context.Users.AnyAsync(u => u.ID == userID);
            if (!userExists)
                return NotFound($"User with ID {userID} not found.");
            var user = await _context.Users
                .Where(u => u.ID == userID)
                .Select(u => new UserCreateDTO
                {
                    
                    Interests = _context.Connections
                    .Where(c => c.UserID == u.ID)
                    .Select(c => c.Interest)
                    .Distinct()
                    .Select(i => new InterestViewDTO
                    {
                        ID = i.ID,
                        InterestName = i.InterestName,
                        Description = i.Description,
                        Links = _context.Connections
                        .Where(c => c.UserID == u.ID && c.InterestID == i.ID)
                        .Select(c => new LinkDTO
                        {
                            ID = c.ID,
                            URL = c.URL
                        }).ToList()
                    }).ToList()
                }).FirstOrDefaultAsync();
            return Ok(user);
        }

        [HttpGet("{userID}/links")]
        public async Task<ActionResult<UserCreateDTO>> GetJustLinksFromId(int userID)
        {
            var userExists = await _context.Users.AnyAsync(u => u.ID == userID);
            if (!userExists)
                return NotFound($"User with ID {userID} not found.");

            var user = await _context.Users
                .Where(u => u.ID == userID)
                .Select(u => new UserWithLinksDTO
                {
                    
                    Links = _context.Connections
                        .Where(c => c.UserID == u.ID && c.URL != null)
                        .Select(c => new LinkDTO
                        {
                            ID = c.ID,
                            URL = c.URL
                        }).ToList()
                }).FirstOrDefaultAsync();

            return Ok(user);
        }
        [HttpPost]
        public async Task<ActionResult<User>> CreateUser(UserMakerDTO dto)
        {
            var user = new User
            {
                Name = dto.Name,
                PhoneNumber = dto.PhoneNumber
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return Ok(new UserCreateDTO
            {
                ID = user.ID,
                Name = user.Name,
                PhoneNumber = user.PhoneNumber,
            });
        }
        [HttpPost("{userID}/interest/{interestID}")]
        public async Task<ActionResult<User>> AssignInterestToUser(int userID, int interestID)
        {
            var userExists = await _context.Users.AnyAsync(u => u.ID == userID);
            if (!userExists)
                return NotFound($"User with ID {userID} not found.");

            var interestExists = await _context.Interests.AnyAsync(i => i.ID == interestID);
            if (!interestExists)
                return NotFound($"Interest with ID {interestID} not found.");

            var alreadyAssigned = await _context.Connections.AnyAsync(c => c.UserID == userID && c.InterestID == interestID);
            if (alreadyAssigned)
                return BadRequest($"User with ID {userID} already has interest with ID {interestID} assigned.");

            var connection = new Connection
            {
                UserID = userID,
                InterestID = interestID
            };

            _context.Connections.Add(connection);
            await _context.SaveChangesAsync();

            return Ok($"Interest {interestID} assigned to user {userID}");
        }

        [HttpPost("{userID}/interest/{interestID}/link")]
        public async Task<ActionResult<User>> AssignLink(int userID, int interestID, CreateLinkDTO dto)
        {
            var userExist = await _context.Users.AnyAsync(u => u.ID == userID);
            if (!userExist)
                return NotFound($"User with ID {userID} not found");
            //check if user has the interest assigned before allowing to add a link
            var connectionExist = await _context.Connections.AnyAsync(c => c.UserID == userID && c.InterestID == interestID);
            if (!connectionExist)
                return NotFound($"User with ID {userID} does not have interest with ID {interestID} assigned, cannot add link");

            var connection = new Connection
            {
                UserID = userID,
                InterestID = interestID,
                URL = dto.URL
            };

            _context.Connections.Add(connection);
            await _context.SaveChangesAsync();

            return Ok($"Link assigned to user {userID} for interest {interestID}");
        }
    }
}
