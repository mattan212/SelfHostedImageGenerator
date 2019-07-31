using System.Web.Http;

namespace SelfHostedImageGenerator
{
    [RoutePrefix("api/ImageBank")]
    public class ImageBankController : ApiController
    {
        private readonly ImageBankDataService _imageBankService;

        public ImageBankController()
        {
            _imageBankService = new ImageBankDataService();
        }

        [Route("Get")]
        [HttpGet]
        public IHttpActionResult Get()
        {
            return Ok("Hello world");
        }

        [Route("StoreImage")]
        [HttpPost]
        public IHttpActionResult StoreImage(ImageModel model)
        {
            try
            {
                var res = _imageBankService.SaveImage(model);

                return Ok(res);
            }
            catch
            {
                return BadRequest();
            }
        }
    }
}