namespace CIK.News.Framework.Extensions
{
    using CIK.News.Framework.Contants;

    public static class ErrorMessageStringExtensions
    {
         public static string ToNotNullErrorMessage(this string source)
         {
             return string.Format(ConstantMessage.ShouldNotBeNull, source);
         }
    }
}