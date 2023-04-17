namespace Identity.Helper
{
    public static class ExtendedUtility
    {
        public static int ToInt32(this string target)
        {
            try
            {
                int value = Convert.ToInt32(target);
                return value;
            }
            catch (Exception)
            {
                return 0;
            }
        }
    }
}
