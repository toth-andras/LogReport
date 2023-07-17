namespace LogReport.Utils;

using System.Text;
using System.Text.RegularExpressions;

public static class EmailAnonymizerService
{
    /// <summary>
    /// Adds '*' symbols to the given text to anonymize it.
    /// </summary>
    /// <returns>Ciphered text.</returns>
    private static string AddСipher(string text)
    {
        if (text.Length is 0 or 1)
        {
            return "*";
        }

        var sb = new StringBuilder(text);
        for (var i = 1; i < sb.Length; i+=2)
        {
            sb[i] = '*';
        }

        return sb.ToString();
    }
    
    /// <summary>
    /// Anonymizes all emails in the given text.
    /// </summary>
    /// <param name="text">Text to process.</param>
    public static string AnonymizeEmails(string text)
    {
        var pattern = @"([\w\-\.]+)(@[\w\-]+\.[\w\-]+)";
        return Regex.Replace(text, pattern, match => AddСipher(match.Groups[1].Value) + match.Groups[2].Value);
    }
}