using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Shared;
using RPtest.Data;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RPtest.Pages;

public class UserWithRoleViewModel
{
    public string Id { get; set; }
    public string UserName { get; set; }
    public string Role { get; set; }
}

[Authorize(Roles = "Super Administrateur")] // Restrict to super admins only
public class ListUserModel(UserManager<IdentityUser> _userManager) : PageModel
{
    [TempData]
    public string StatusMessage { get; set; }

    public List<UserWithRoleViewModel> UsersWithRoles { get; set; } = new();

    public async Task OnGetAsync()
    {
        var Users = _userManager.Users.ToList();
        foreach (var user in Users)
        {
            var roles = await _userManager.GetRolesAsync(user);
            UsersWithRoles.Add(new UserWithRoleViewModel
            {
                Id = user.Id,
                UserName = user.UserName,
                Role = roles.FirstOrDefault() ?? ""
            });
        }
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