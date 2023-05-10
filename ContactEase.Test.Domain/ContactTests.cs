namespace ContactEase.Test.Domain;

public class ContactTests
{
    [Fact]
    public void Create_ContactWithValidData_ShouldCreateContact()
    {
        // Arrange
        var personId = new Guid();
        var type = "Whatssap";
        var value = "48991262481";

        // Act
        var result = Contact.Create(personId, type, value);

        // Assert
        Assert.False(result.IsError);
        Assert.Equal(personId, result.Value.PersonId);
        Assert.Equal(type, result.Value.Type);
        Assert.Equal(value, result.Value.Value);
    }

    [Theory]
    [InlineData("")]
    [InlineData("A very long type with more than 50 characters that should not be accepted")]
    public void Create_ContactWithInvalidType_ShouldReturnError(string type)
    {
        // Arrange
        var personId = new Guid();
        var value = "48991262481";

        // Act
        var result = Contact.Create(personId, type, value);

        // Assert
        Assert.True(result.IsError);
        Assert.Contains(result.Errors, error => error.Code == "Contact.InvalidType");
        Assert.Contains(result.Errors, error => error.Type == ErrorType.Validation);
    }

    [Theory]
    [InlineData("")]
    [InlineData("1234")]
    [InlineData("A very long value with more than 50 characters that should not be accepted")]
    public void Create_ContactWithInvalidValue_ShouldReturnError(string value)
    {
        // Arrange
        var personId = new Guid();
        var type = "Telefone";

        // Act
        var result = Contact.Create(personId, type, value);

        // Assert
        Assert.True(result.IsError);
        Assert.Contains(result.Errors, error => error.Type == ErrorType.Validation);
        Assert.Contains(result.Errors, error => error.Code == "Contact.InvalidValue");
    }

    [Fact]
    public void Update_ContactWithValidData_ShouldUpdateContact()
    {
        // Arrange
        var contact = Contact.Create(new Guid(), "Email", "john@email.com").Value;
        var type = "Whatssap";
        var value = "48992262527";

        // Act
        var result = contact.Update(type, value);

        // Assert
        Assert.False(result.IsError);
        Assert.Equal(type, result.Value.Type);
        Assert.Equal(value, result.Value.Value);
    }

    [Theory]
    [InlineData("")]
    [InlineData("A very long type with more than 50 characters that should not be accepted")]
    public void Update_ContactWithInvalidType_ShouldReturnError(string type)
    {
        // Arrange
        var contact = Contact.Create(new Guid(), "Fone", "+1 (555) 123-4567").Value;
        var value = "+1 (555) 123-25478";

        // Act
        var result = contact.Update(type, value);

        // Assert
        Assert.True(result.IsError);
        Assert.Contains(result.Errors, error => error.Code == "Contact.InvalidType");
        Assert.Contains(result.Errors, error => error.Type == ErrorType.Validation);
    }

    [Theory]
    [InlineData("")]
    [InlineData("1234")]
    [InlineData("A very long value with more than 50 characters that should not be accepted")]
    public void Update_ContactWithInvalidValue_ShouldReturnError(string value)
    {
        // Arrange
        var contact = Contact.Create(new Guid(), "Email", "john.doe@example.com").Value;
        var type = "e-mail";

        // Act
        var result = contact.Update(type, value);

        // Assert
        Assert.True(result.IsError);
        Assert.Contains(result.Errors, error => error.Type == ErrorType.Validation);
        Assert.Contains(result.Errors, error => error.Code == "Contact.InvalidValue");
    }
}
