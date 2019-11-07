public static class StringMethods
{
    public static string ProperCase(this string sender)
    {
        System.Globalization.TextInfo TI = (new System.Globalization.CultureInfo("en-US", false)).TextInfo;
        return TI.ToTitleCase(sender.ToLower());
    }
}