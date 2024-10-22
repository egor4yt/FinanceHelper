namespace FinanceHelper.TelegramBot.MessageBroker.Messages.Registration;

public class RegisterUser
{
    public long ChatId { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
}