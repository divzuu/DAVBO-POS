namespace DAVBO_POS
{
    internal static class Session
    {
        public static int? UserId { get; set; }
        public static string Username { get; set; }
        public static string FullName { get; set; }

        public static void Clear()
        {
            UserId = null;
            Username = null;
            FullName = null;
        }
    }
}
