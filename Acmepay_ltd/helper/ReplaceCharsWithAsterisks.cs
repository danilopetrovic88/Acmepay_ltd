namespace Acmepay_ltd.helper
{
    public class ReplaceCharsWithAsterisks
    {
        public static string Replace(string input)
        {
            if (input.Length != 18)
            {
                throw new ArgumentException("Input string must be 18 characters long.");
            }
            char[] chars = input.ToCharArray();

            for (int i = 6; i < 14; i++)
            {
                chars[i] = '*';
            }

            return new string(chars);
        }
    }
}
