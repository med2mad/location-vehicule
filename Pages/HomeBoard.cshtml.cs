using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using RPtest.Data;
using RPtest.Models;
using System.Globalization;

namespace RPtest.Pages;
public class HomeBoardModel(ApplicationDbContext _context) : PageModel
{
    [BindProperty(SupportsGet = true)]
    public Paiement Paiement { get; set; }
    public int nombresReservations { get; set; }
    public int vehiculesRestant { get; set; }
    public int reservationsAnnulee { get; set; }

    public void OnGet()
    {
        DateTime now = DateTime.Now;

        nombresReservations = _context.Locations
            .Where(l => l.Date.Year == now.Year && l.Date.Month == now.Month && l.Status != "Annulé")
            .Count();

        int NvehiculesReserves = _context.Locations
            .Where(l => l.DateDepart.Year == now.Year && l.DateDepart.Month == now.Month && l.Status == "Réservé")
            .Count();
        vehiculesRestant = _context.Vehicules.Count() - NvehiculesReserves;

        reservationsAnnulee = _context.Locations
            .Where(l => l.Date.Year == now.Year && l.Date.Month == now.Month && l.Status == "Annulé")
            .Count();
    }

    public IActionResult OnGetPaymentsData()
    {
        DateTime currentMonth = DateTime.Now;
        DateTime startMonth = new DateTime(currentMonth.AddMonths(-11).Year, currentMonth.AddMonths(-11).Month, 1);

        var paymentsData = _context.Paiements
            .Where(p => p.Date >= startMonth)
            .GroupBy(p => new { p.Date.Year, p.Date.Month })
            .Select(g => new { Year = g.Key.Year, Month = g.Key.Month, Total = g.Sum(p => p.Montant) })
            .ToList();

        // Create a list of last 12 months (including previous year)
        var monthlyPayments = Enumerable.Range(0, 12)
            .Select(i => startMonth.AddMonths(i))
            .Select(d => new
            {
                Month = CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(d.Month) + " " + d.Year, // "January 2023"
                Total = paymentsData.FirstOrDefault(p => p.Year == d.Year && p.Month == d.Month)?.Total ?? 0
            })
            .ToList();

        return new JsonResult(monthlyPayments);
    }
}
