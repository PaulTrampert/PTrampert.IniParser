using System;

namespace PTrampert.IniUtils;

public class IniSyntaxException : Exception
{
    public IniSyntaxException(int lineNumber, string lineContent, string? filePath)
        : base($"Invalid INI syntax{FileMessasgeClause(filePath)} at line {lineNumber}: '{lineContent}'")
    {
    }

    private static string FileMessasgeClause(string? filePath)
    {
        return filePath == null ? string.Empty : $" in '{filePath}'";
    }
}