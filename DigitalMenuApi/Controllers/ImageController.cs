using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DigitalMenuApi.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace DigitalMenuApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImageController : ControllerBase
    {
        private readonly IConfiguration _config;

        public ImageController(IConfiguration config)
        {
            _config = config;
        }

        [Authorize]
        [HttpPost]
        public ActionResult<string> UploadImage( IFormFile image)
        {
            string imageUrl = string.Empty;

            if(image != null && image.Length > 0)
            {
                imageUrl = FirebaseService.UploadFileToFirebaseStorage(image.OpenReadStream(), DateTime.Now.ToString("dd-MM-yyyy-HH-mm-ss-ff_") + image.FileName, "Images").Result;
            }

            return Ok(imageUrl);

        }
    }
}
