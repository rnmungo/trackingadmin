namespace BLL.TrackingAdmin.Extensions
{
    public static class EnumExtensions
    {
        public static T ToEnum<T>(this string value) where T : struct
        {
            try
            {
                return (T)Enum.Parse(typeof(T), value, true);
            }
            catch (ArgumentNullException)
            {
                return default(T);
            }
            catch (ArgumentException)
            {
                return (T)Enum.GetValues(typeof(T)).GetValue(0)!;
            }
        }
    }
}
