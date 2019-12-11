using GroupProject.Services;
using Microsoft.AspNetCore.Mvc;

namespace GroupProject.Controllers
{
    // Controller to register a user as an admin allowing them to perform actions assigned to admin only.
    // Currently disabled for security.
    /*
    [Route("api/[controller]")]
    [ApiController]
    public class AdminTokenController : ControllerBase
    {
        private readonly AdminRegistrationTokenService _adminService;

        public AdminTokenController(AdminRegistrationTokenService adminService)
        {
            _adminService = adminService;
        }

        [HttpGet]
        public ActionResult<ulong> Get() =>
            _adminService.CreationKey;
}*/
}
 