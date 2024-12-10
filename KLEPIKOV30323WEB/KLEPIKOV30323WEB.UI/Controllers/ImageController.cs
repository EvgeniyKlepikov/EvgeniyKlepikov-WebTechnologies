using KLEPIKOV30323WEB.UI.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace KLEPIKOV30323WEB.UI.Controllers
{
    public class ImageController(UserManager<AppUser> userManager) : Controller
    {
        public async Task<IActionResult> GetAvatar()
        {
            //if(User.Identity.IsAuthenticated)
            //{
            //    var email = User.FindFirst(ClaimTypes.Email).Value;
            //    var user = await userManager.FindByEmailAsync(email);
            //    if (user.Avatar is not null)
            //        return File(user.Avatar, "image/*");
            //}
            //return File("user.Avatar", "image/*");
            var email = User.FindFirst(ClaimTypes.Email)!.Value;
            var user = await userManager.FindByEmailAsync(email);
            if (user == null)
            {
                return NotFound();
            }
            if (user.Avatar != null)
                return File(user.Avatar, user.MimeType);
            var imagePath = Path.Combine("Images", "login-user.png");
            return File(imagePath, "image/png");
        }
    }
}
