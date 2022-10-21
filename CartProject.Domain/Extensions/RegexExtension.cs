using System.Text.RegularExpressions;

namespace CartProject.Domain.Extensions;

public static class RegexExtension
{
    public static Regex Name() => new (@"^([ \u00c0-\u01ffa-zA-Z'\-\d])+$");
    public static Regex LettersAdnNumbers() => new (@"^([\w\d])+$");
}
