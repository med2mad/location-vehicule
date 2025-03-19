using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace RPtest.Pages;
public class ContactModel : PageModel
{
    [BindProperty]
    public string nom { get; set; }
    [BindProperty]
    public string prenom { get; set; }
    [BindProperty]
    public string email { get; set; }
    [BindProperty]
    public string message { get; set; }
    [BindProperty]
    public string tel { get; set; }

    public void OnGet()
    {
    }

    public void OnPost()
    {
        var x = new EmailSender();
        x.SendEmailAsync("mohamed.leghdaich@gmail.com", "Contacté par " + nom + " " + prenom, message + "\n\n" + email + "\n" + tel);
    }
}

