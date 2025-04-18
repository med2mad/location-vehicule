using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;
using System.Threading.Tasks;

namespace RPtest.Pages;

public class EditUserModel(UserManager<IdentityUser> _userManager, SignInManager<IdentityUser> _signInManager) : PageModel
{
    [BindProperty]
    public InputModel Input { get; set; }

    public class InputModel
    {
        public string? Id { get; set; }

        [Required]
        [StringLength(20, ErrorMessage = "Nom utilisateur doit avoir entre {2} et {1} characters", MinimumLength = 6)]
        public string UserName { get; set; }

        [Required]
        public string? Role { get; set; }

        //[EmailAddress]
        //[Display(Name = "Email")]
        //public string Email { get; set; }

        //[Display(Name = "Phone number")]
        //public string PhoneNumber { get; set; }

        [DataType(DataType.Password)]
        public string? NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Compare("NewPassword", ErrorMessage = "Mot de pass et confirmation ne sont pas identiques.")]
        public string? ConfirmPassword { get; set; }

    }

    public async Task<IActionResult> OnGetAsync(string id, string returnUrl = null)
    {
        if (id == null)
        {
            return NotFound();
        }

        var user = await _userManager.FindByIdAsync(id);
        if (user == null)
        {
            return NotFound();
        }

        if (!User.IsInRole("Super Administrateur") &&  User.FindFirstValue(ClaimTypes.NameIdentifier) != id)
        {
            return NotFound();
        }

        Input = new InputModel
        {
            Id = id,
            UserName = user.UserName,
            Role = (await _userManager.GetRolesAsync(user)).FirstOrDefault(),
            //Email = user.Email,
            //PhoneNumber = user.PhoneNumber
        };

        return Page();
    }

    public async Task<IActionResult> OnPostAsync(string id, string returnUrl = null)
    {
        Input.Id = id;

        if (!ModelState.IsValid)
        {
            return Page();
        }

        if (!string.IsNullOrEmpty(Input.NewPassword))
        {
            if (Input.NewPassword.Length < 6 || Input.NewPassword.Length > 20)
            {
                ModelState.AddModelError("Input.NewPassword", "Mot de pass doit avoir entre 6 et 20 characters");
                return Page();
            }
        }

        var user = await _userManager.FindByIdAsync(id);
        if (user == null)
        {
            return NotFound();
        }

        bool exist = _userManager.Users.Any(u => (u.UserName == Input.UserName && u.Id != id));
        if (exist)
        {
            ModelState.AddModelError(string.Empty, "Nom utilisateur déjà existant");
            return Page();
        }

        bool isCurrentUser = user.UserName == User.Identity.Name;

        // 1. First update regular user properties
        user.UserName = Input.UserName;
        //user.Email = Input.Email;
        //user.PhoneNumber = Input.PhoneNumber;

        // 2. Then handle password reset if new password was provided
        if (!string.IsNullOrEmpty(Input.NewPassword))
        {
            var token = await _userManager.GeneratePasswordResetTokenAsync(user);
            var resetResult = await _userManager.ResetPasswordAsync(user, token, Input.NewPassword);

            if (!resetResult.Succeeded)
            {
                foreach (var error in resetResult.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
                return Page();
            }
        }

        //change role 
        var roles = await _userManager.GetRolesAsync(user);
        await _userManager.RemoveFromRolesAsync(user, roles);
        if (!await _userManager.IsInRoleAsync(user, Input.Role))
            await _userManager.AddToRoleAsync(user, Input.Role);


        // 3. Finally save the user updates
        var updateResult = await _userManager.UpdateAsync(user);
        if (!updateResult.Succeeded)
        {
            foreach (var error in updateResult.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }
            return Page();
        }


        if (isCurrentUser)
        {
            await _signInManager.RefreshSignInAsync(user);
        }

        return RedirectToPage(returnUrl);
    }
}