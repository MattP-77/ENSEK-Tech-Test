using ENSEK.Classes.Helpers;
using ENSEK.Classes.ResponseClasses;
using Microsoft.AspNetCore.Mvc;

namespace ENSEK.MeterReadings.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FileUploadController : ControllerBase
    {
        public ResultReponseEntity ResponseObject { get; set; }

        [HttpPost]
        [Route("meter-reading-uploads")]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> UploadMeterReading(IFormFile file)
        {
            if (file == null || file.Length == 0)
                return BadRequest("File is not selected.");

            if (!Path.GetExtension(file.FileName).Equals(".xlsx", System.StringComparison.OrdinalIgnoreCase))
                return BadRequest("Only xlsx files are allowed.");

            if (!Utilities.IsFileValidForProcessing(file.FileName, "Meter_Reading"))
                return BadRequest("Invalid filename, expecting 'Meter_Reading'.");

            await ProcessFileForUpload(file);

            return Ok(ResponseObject);
        }

        private async Task ProcessFileForUpload(IFormFile fileForUpload)
        {
            using (MemoryStream memoryStream = new MemoryStream())
            {
                await fileForUpload.CopyToAsync(memoryStream);

                ResponseObject = Utilities.ProcessMeterReadingFile(memoryStream);
            }
        }
    }
}
