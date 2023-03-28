using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace NewLab1.Pages.OfficeHourSch
{
    public class ViewMeetingModel : PageModel
    {
            
        private readonly IConfiguration _configuration;
        private readonly ILogger<ViewMeetingModel> _logger;

        public ViewMeetingModel(IConfiguration configuration, ILogger<ViewMeetingModel> logger)
        {
            _configuration = configuration;
            _logger = logger;
        }

        [BindProperty]
        public IFormFile ImageFile { get; set; }

        public async Task<IActionResult> OnPostAsync(int studentId)
        {
            if (ImageFile != null && ImageFile.Length > 0)
            {
                try
                {
                    var fileName = Path.GetFileName(ImageFile.FileName);
                    var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images");
                    var filePath = Path.Combine(uploadsFolder, fileName);

                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        await ImageFile.CopyToAsync(fileStream);
                    }

                    string connectionString = _configuration.GetConnectionString("DefaultConnection");
                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        connection.Open();
                        SqlCommand cmd = new SqlCommand("UPDATE Student SET image = @imageName WHERE studentId = @studentId", connection);
                        cmd.Parameters.AddWithValue("@imageName", fileName);
                        cmd.Parameters.AddWithValue("@studentId", studentId);
                        cmd.ExecuteNonQuery();
                        connection.Close();
                    }

                    TempData["Message"] = "Image uploaded successfully!";
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error uploading image.");
                    TempData["ErrorMessage"] = "Error uploading image. Please try again later.";
                }
            }
            else
            {
                TempData["ErrorMessage"] = "Please select a valid image file.";
            }

            return RedirectToPage("/Student/Index");
        }
    }
}
