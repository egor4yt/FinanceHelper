using FinanceHelper.Application.Commands.Users.Register;
using FinanceHelper.MessageBroker.Messages.Base;

namespace FinanceHelper.MessageBroker.Messages.Telegram.Registration;

public class TelegramRegistrationMessage : IMessage
{
    public long ChatId { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? PreferredLocalizationCode { get; set; }

    public object ToApplicationCommand()
    {
        var request = new RegisterUserCommandRequest
        {
            LastName = LastName,
            FirstName = FirstName,
            PreferredLocalizationCode = PreferredLocalizationCode,
            TelegramChatId = ChatId
        };
        return request;
    }
}