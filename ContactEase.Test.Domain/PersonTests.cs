namespace ContactEase.Test.Domain;

public class PersonTests
{
    [Fact]
    public void Create_PersonWithValidData_ShouldCreatePerson()
    {
        // Arrange
        var name = "John Doe";
        var nickname = "JD";
        var notes = "A person with valid data";

        // Act
        var result = Person.Create(name, nickname, notes);

        // Assert
        Assert.False(result.IsError);
        Assert.Equal(name, result.Value.Name);
        Assert.Equal(nickname, result.Value.Nickname);
        Assert.Equal(notes, result.Value.Notes);
    }

    [Theory]
    [InlineData("")]
    [InlineData("A")]
    [InlineData("A very long name with more than 50 characters that should not be accepted")]
    public void Create_PersonWithInvalidName_ShouldReturnError(string name)
    {
        // Arrange
        var nickname = "JD";
        var notes = "A person with an invalid name";

        // Act
        var result = Person.Create(name, nickname, notes);

        // Assert
        Assert.True(result.IsError);
        Assert.Contains(result.Errors, error => error.Type == ErrorType.Validation);
    }

    [Theory]
    [InlineData("")]
    [InlineData("A")]
    [InlineData("A very long nickname with more than 50 characters that should not be accepted")]
    public void Create_PersonWithInvalidNickname_ShouldReturnError(string nickname)
    {
        // Arrange
        var name = "John Doe";
        var notes = "A person with an invalid nickname";

        // Act
        var result = Person.Create(name, nickname, notes);

        // Assert
        Assert.True(result.IsError);
        Assert.Contains(result.Errors, error => error.Type == ErrorType.Validation);
    }

    [Fact]
    public void Update_PersonWithValidData_ShouldUpdatePerson()
    {
        // Arrange
        var person = Person.Create("John Doe", "JD", "Old notes").Value;
        var name = "John Updated";
        var nickname = "JU";
        var notes = "New notes";

        // Act
        var result = person.Update(name, nickname, notes);

        // Assert
        Assert.False(result.IsError);
        Assert.Equal(name, result.Value.Name);
        Assert.Equal(nickname, result.Value.Nickname);
        Assert.Equal(notes, result.Value.Notes);
    }

    [Theory]
    [InlineData("")]
    [InlineData("A")]
    [InlineData("A very long name with more than 50 characters that should not be accepted")]
    public void Update_PersonWithInvalidName_ShouldReturnError(string name)
    {
        // Arrange
        var person = Person.Create("John Doe", "JD", "Old notes").Value;
        var nickname = "JU";
        var notes = "A person with an invalid name";

        // Act
        var result = person.Update(name, nickname, notes);

        // Assert
        Assert.True(result.IsError);
        Assert.Contains(result.Errors, error => error.Type == ErrorType.Validation);
    }

    [Theory]
    [InlineData("")]
    [InlineData("A")]
    [InlineData("A very long nickname with more than 50 characters that should not be accepted")]
    public void Update_PersonWithInvalidNickname_ShouldReturnError(string nickname)
    {
        // Arrange
        var person = Person.Create("John Doe", "JD", "Old notes").Value;
        var name = "John Doe";
        var notes = "A person with an invalid name";

        // Act
        var result = Person.Create(name, nickname, notes);

        // Assert
        Assert.True(result.IsError);
        Assert.Contains(result.Errors, error => error.Type == ErrorType.Validation);
    }
}