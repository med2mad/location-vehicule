using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Shared;
using RPtest.Data;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RPtest.Pages;

[Authorize] // Restrict to logged-in users
public class ListUserModel(UserManager<IdentityUser> _userManager) : PageModel
{
    public IList<IdentityUser> Users { get; set; }
    [TempData]
    public string StatusMessage { get; set; }

    public async Task OnGetAsync()
    {
        Users = _userManager.Users.ToList();
    }

    public async Task<IActionResult> OnPostDeleteAsync(string id)
    {
        if (string.IsNullOrEmpty(id))
        {
            return NotFound();
        }

        var user = await _userManager.FindByIdAsync(id);
        if (user == null)
        {
            return NotFound();
        }

        if (user.UserName == User.Identity.Name)
        {
            StatusMessage = "Vous ne pouvez pas supprimer votre propre compte.";
            return RedirectToPage();
        }

        var result = await _userManager.DeleteAsync(user);
        if (result.Succeeded)
        {
            StatusMessage = $"{user.UserName} Supprimé.";
        }
        else
        {
            StatusMessage = "Echec de la suppression.";
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }
        }

        return RedirectToPage();
    }
}