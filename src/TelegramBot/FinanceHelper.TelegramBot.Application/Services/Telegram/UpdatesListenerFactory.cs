using System.Collections.Concurrent;
using FinanceHelper.TelegramBot.Application.Services.Localization.Interfaces;
using FinanceHelper.TelegramBot.Application.Services.Telegram.Interfaces;
using Telegram.Bot;

namespace FinanceHelper.TelegramBot.Application.Services.Telegram;

/// <inheritdoc />
public class UpdatesListenerFactory(ITelegramBotClient botClient, IStringLocalizerFactory localizationFactory) : IUpdatesListenerFactory
{
    /// <summary>
    ///     User id : listener
    /// </summary>
    private readonly ConcurrentDictionary<long, IUpdatesListener> _listenersCache = new ConcurrentDictionary<long, IUpdatesListener>();

    public IUpdatesListener Create(long chatId)
    {
        var listener = _listenersCache.GetValueOrDefault(chatId);
        if (listener is null)
        {
            listener = new BaseUpdateListener(botClient, localizationFactory);
            _listenersCache.GetOrAdd(chatId, listener);
        }

        return listener;
    }
}