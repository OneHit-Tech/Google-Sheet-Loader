public static class Extensions
{
    public static string Color(this string str, string color)
    {
        return $"<color={color}>{str}</color>";
        
    }
}