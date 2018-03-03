using System;
using System.Collections.Generic;

namespace HelperMethods
{
    public static class Helper
    {
       
        
        public static string PassworGenerator(int passwordLength = 13)
        {
            var random = new Random();

            var buffer = new char[passwordLength];
            var password = "";
            for (var i = 0; i < passwordLength; i++)
            {
                buffer[i] = (char)('a' + random.Next(0, 26)); //"a - z"
                password = new string(buffer);
            }
            return password;
        }


        public static string TextSummerizer(string text, int maxChars = 20)
        {
            if (text.Length < maxChars)
                return text;
            
            var words = text.Split(' ');
            var totalChars = 0;
            var summarizedWords = new List<string>();
            
            foreach (var word in words)
            {
                summarizedWords.Add(word);
                totalChars += word.Length + 1; // 1 denotes "space"
                if (totalChars > maxChars)
                    break;
            }
            
            return String.Join(" ", summarizedWords) + "...";

        }
    }
}

