using Microsoft.AspNetCore.Mvc;

namespace Task_1.Controllers
{
    [ApiController]
    [Route("/")]
    
    public class StatusController : ControllerContext
    {
        [HttpGet]
        public ActionResult<string> CheckStatus()
        {
            return "Ihor Volokhovych, PM_HW_10, PM_HW_10.Task_1";
        }
    }
}