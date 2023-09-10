using System.Text;

namespace RuTtsInterrop;

public class Utils
{
    private static readonly int[] koi8rMap =
    {
        0xE1, 0xE2, 0xF7, 0xE7, 0xE4, 0xE5, 0xF6, 0xFA,
        0xE9, 0xEA, 0xEB, 0xEC, 0xED, 0xEE, 0xEF, 0xF0,
        0xF2, 0xF3, 0xF4, 0xF5, 0xE6, 0xE8, 0xE3, 0xFE,
        0xFB, 0xFD, 0xFF, 0xF9, 0xF8, 0xFC, 0xE0, 0xF1,
        0xC1, 0xC2, 0xD7, 0xC7, 0xC4, 0xC5, 0xD6, 0xDA,
        0xC9, 0xCA, 0xCB, 0xCC, 0xCD, 0xCE, 0xCF, 0xD0,
        0xD2, 0xD3, 0xD4, 0xD5, 0xC6, 0xC8, 0xC3, 0xDE,
        0xDB, 0xDD, 0xDF, 0xD9, 0xD8, 0xDC, 0xC0, 0xD1
    };

    private static readonly int blank = 0x20;
    private static readonly int koiUC_YO = 0xB3;
    private static readonly int koiLCYO = 0xA3;
    private static readonly int utfUCYO = 0x0401;
    private static readonly int utfLCYO = 0x0451;
    private static readonly int utfCodebase = 0x0410;
    private static readonly int extendedCodebase = 0x80;

    public static string ConvertUtf8ToKoi8r(string utf8Text)
{
    StringBuilder result = new StringBuilder(utf8Text.Length);

    foreach (char utf8Char in utf8Text)
    {
            char convertedChar = ConvertCharacter(utf8Char);
            result.Append(convertedChar);
        }
        return result.ToString();
}

    private static char ConvertCharacter(char c)
    {
        return c switch
        {
            _ when c == (char)utfUCYO => (char)koiUC_YO,
            _ when c == (char)utfLCYO => (char)koiLCYO,
            _ when utfCodebase <= c && c < utfCodebase + koi8rMap.Length => (char)koi8rMap[c - utfCodebase],
            _ when extendedCodebase > c && c > blank => c,
            _ => (char)blank
        };
    }
}
