namespace SentimentAnalysis.Web.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using SentimentAnalysis.Web.Interfaces;
    using System;
    using System.Threading.Tasks;

    [Route("api/[controller]")]
    public class ClassifyController : Controller
    {
        private readonly IClassifyService _classifyService;
        private readonly IAppInsightsLoggerService _logger;
        public ClassifyController(IClassifyService classifyService, IAppInsightsLoggerService logger)
        {
            _classifyService = classifyService;
            _logger = logger;
            if(_classifyService == null)
            {
                throw new ArgumentException("Classify service cannot be null.");
            }
        }
        
        [HttpGet]
        public async Task<IActionResult> Get([FromQuery(Name = "usersUtterance")] string usersUtterance)
        {
            try
            {
                if(string.IsNullOrEmpty(usersUtterance))
                {
                    return BadRequest("usersUtterance url parameter is missing.");
                }

                var classifedUtterance = await _classifyService.ClassifySentiment(usersUtterance);

                if (!string.IsNullOrEmpty(classifedUtterance))
                {
                    return Json($"Sentiment: {classifedUtterance}");
                }

                return Json($"Error: Could not receive response from Blob Storage.");
            }
            catch (Exception ex)
            {
                _logger.LogException(ex);
                return Json("Exception occurred, please try again later.");
            }
        }
    }
}
