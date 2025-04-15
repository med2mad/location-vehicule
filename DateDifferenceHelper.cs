namespace RPtest;

// Helpers/DateDifferenceHelper.cs
public static class DateDifferenceHelper
{
    public static (int Years, int Months, int Days) GetDateDifference(DateTime startDate, DateTime endDate)
    {
        // Ensure startDate is before endDate
        //if (startDate > endDate)
        //{
        //    var temp = startDate;
        //    startDate = endDate;
        //    endDate = temp;
        //}

        int years = endDate.Year - startDate.Year;
        int months = 0;
        int days = 0;

        // Check if we need to subtract a year
        if (endDate.Month < startDate.Month || 
            (endDate.Month == startDate.Month && endDate.Day < startDate.Day))
        {
            years--;
            months = (12 - startDate.Month) + endDate.Month;
        }
        else
        {
            months = endDate.Month - startDate.Month;
        }

        // Calculate days
        if (endDate.Day < startDate.Day)
        {
            months--;
            var previousMonth = endDate.AddMonths(-1);
            days = (endDate - previousMonth).Days + (DateTime.DaysInMonth(startDate.Year, startDate.Month) - startDate.Day);
        }
        else
        {
            days = endDate.Day - startDate.Day;
        }

        return (years, months, days);
    }

    public static string FormatDateDifference((int Years, int Months, int Days) diff)
    {
        List<string> parts = new List<string>();
        
        if (diff.Years > 0) parts.Add($"{diff.Years} year{(diff.Years > 1 ? "s" : "")}");
        if (diff.Months > 0) parts.Add($"{diff.Months} month{(diff.Months > 1 ? "s" : "")}");
        if (diff.Days > 0 || parts.Count == 0) parts.Add($"{diff.Days} day{(diff.Days != 1 ? "s" : "")}");
        
        return string.Join(", ", parts);
    }

    public static (int Years, int Months, int Days) f(DateTime LastDepense, int jours, int mois, int annees)
    {
        DateTime nextPaymentDate = LastDepense
            .AddYears(annees)
            .AddMonths(mois)
            .AddDays(jours)
            .AddDays(1).AddTicks(-1);

        int years = 0;
        int months = 0;
        int days = 0;

        DateTime now = DateTime.Now;

        if (now < nextPaymentDate)
        {
            // Calculate difference in years, months, days manually
            years = nextPaymentDate.Year - now.Year;
            months = nextPaymentDate.Month - now.Month;
            days = nextPaymentDate.Day - now.Day;

            // Adjust for negative days or months
            if (days < 0)
            {
                months--;
                days += DateTime.DaysInMonth(now.Year, (now.Month == 12) ? 1 : now.Month + 1);
            }

            if (months < 0)
            {
                years--;
                months += 12;
            }

            return (years, months, days);
        }
        else
        {
            return (0, 0, 0);
        }
    }

}
