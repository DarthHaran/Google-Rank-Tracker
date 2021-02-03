using System;

namespace GRT.Services
{
    public class EncodeService
    {
        public static string encodeString(string text)
        {
            string[] chars = { "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S",
                "T", "U", "V", "W", "X", "Y", "Z", "a", "b", "c", "d", "e", "f", "g", "h", "i", "j", "k",
                "l", "m", "n", "o", "p", "q", "r", "s", "t", "u", "v", "w", "x", "y", "z", "0", "1", "2",
                "3", "4", "5", "6", "7", "8", "9", "-", "_", "A", "B", "C", "D", "E", "F", "G", "H", "I",
                "J", "M", "T", "L"};

            string character = chars[text.Length - 1];

            var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(text);
            string location = Convert.ToBase64String(plainTextBytes);

            string uule = character + location;

            return uule;
        }
    }
}
