using ZiphyTestOnWinUIApp.Core.Contracts.Services;
using ZiphyTestOnWinUIApp.Core.Services;

namespace TestProject;

public class FileServiceTest
{
    [Fact]
    public void FileServiceSaveAndFileExist()
    {
        // Arrange
        IFileService fileService = new FileService();
        var folderPath = Directory.GetCurrentDirectory();
        var fileName = "testFile.txt";
        var content = new Content
        {
            StringValue = "string value",
            IntegerValue = 42
        };

        // Act
        fileService.Save(folderPath, fileName, content);
        var fileExist = File.Exists(Path.Combine(folderPath, fileName));

        // Assert
        Assert.True(fileExist);
    }

    [Fact]
    public void FileServiceRead()
    {
        // Arrange
        IFileService fileService = new FileService();
        var folderPath = Directory.GetCurrentDirectory();
        var fileName = "testFile.txt";
        var contentExpected = new Content
        {
            StringValue = "string value",
            IntegerValue = 42
        };
        fileService.Save(folderPath, fileName, contentExpected);
        
        // Act
        var contentActual = fileService.Read<Content>(folderPath, fileName);


        // Assert
        Assert.True(contentExpected.Equals(contentActual));
    }

    [Fact]
    public void FileServiceRemoveFileAndNotFileExist()
    {
        // Arrange
        IFileService fileService = new FileService();
        var folderPath = Directory.GetCurrentDirectory();
        var fileName = "testFile.txt";

        // Act
        fileService.Delete(folderPath, fileName);
        var fileExist = File.Exists(Path.Combine(folderPath, fileName));

        // Assert
        Assert.False(fileExist);
    }
}

internal class Content
{
    public string StringValue { get; set; }
    public int IntegerValue { get; set; }

    public override bool Equals(object? obj)
    {
        if (obj is not Content content)
            return false;
        
        return Equals(content);
    }

    protected bool Equals(Content other)
    {
        return StringValue == other.StringValue && IntegerValue == other.IntegerValue;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(StringValue, IntegerValue);
    }
}