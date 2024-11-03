namespace FinanceHelper.MessageBroker.Messages.Base;

public interface IMessage
{
    object ToApplicationCommand();
}