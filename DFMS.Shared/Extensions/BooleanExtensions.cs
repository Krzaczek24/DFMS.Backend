namespace DFMS.Shared.Extensions
{
    public static class BooleanExtensions
    {
        public static bool IsTrue(this bool? obj)
        {
            return obj.HasValue && obj.Value;
        }

        public static bool IsNotTrue(this bool? obj)
        {
            return !IsTrue(obj);
        }

        public static bool IsFalse(this bool? obj)
        {
            return obj.HasValue && !obj.Value;
        }

        public static bool IsNotFalse(this bool? obj)
        {
            return !IsFalse(obj);
        }
    }
}
