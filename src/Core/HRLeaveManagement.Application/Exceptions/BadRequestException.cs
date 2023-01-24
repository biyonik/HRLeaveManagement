using FluentValidation.Results;

namespace HRLeaveManagement.Application.Exceptions;

public class BadRequestException: Exception
{
    private List<string> ValidationErrors { get; set; }
    public BadRequestException(string  message) : base(message)
    {
        
    }
    public BadRequestException(string message, ValidationResult validationResult): this(message)
    {
        ValidationErrors = new();
        foreach (var error in validationResult.Errors)       
        {
            ValidationErrors.Add(error.ErrorMessage);
        }
    }
}