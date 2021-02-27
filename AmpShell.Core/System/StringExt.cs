namespace System
{
    public static class StringExt
    {
        public static bool IsNullOrWhiteSpace(string value)
        {
            if (value is null)
            {
                return true;
            }
            for (int i = 0; i < value.Length; i++)
            {
                if (!char.IsWhiteSpace(value[i]))
                {
                    return false;
                }
            }

            return true;
        }
    }
}