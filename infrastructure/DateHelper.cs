namespace infrastructure;

public class DateHelper
{
    public static String GetDate()
    {
        DateTime date = DateTime.Now;
        return date.ToString("dd/MM/yyyy");
    }
}