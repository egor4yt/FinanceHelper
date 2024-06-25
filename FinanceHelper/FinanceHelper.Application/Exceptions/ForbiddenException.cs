namespace FinanceHelper.Application.Exceptions;

public class ForbiddenException(string message) : Exception(message);