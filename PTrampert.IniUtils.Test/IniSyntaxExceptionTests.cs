namespace PTrampert.IniUtils.Test;

public class IniSyntaxExceptionTests
{
    [Test]
    public void IniSyntaxException_MessageIncludesLineNumberAndContent_WithoutFilePath()
    {
        // Arrange
        int lineNumber = 42;
        string lineContent = "invalid_line";

        // Act
        var ex = new IniSyntaxException(lineNumber, lineContent, null);

        // Assert
        Assert.That(ex.Message, Is.EqualTo("Syntax error at line 42: 'invalid_line'"));
    }
    
    [Test]
    public void IniSyntaxException_MessageIncludesLineNumberContentAndFilePath()
    {
        // Arrange
        int lineNumber = 7;
        string lineContent = "another_invalid_line";
        string filePath = "config.ini";
        
        // Act
        var ex = new IniSyntaxException(lineNumber, lineContent, filePath);
        
        // Assert
        Assert.That(ex.Message, Is.EqualTo("Syntax error in 'config.ini' at line 7: 'another_invalid_line'"));
    }
}