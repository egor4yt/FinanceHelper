namespace FinanceHelper.Application.Exceptions;

public class BadRequestException(string message) : Exception(message);