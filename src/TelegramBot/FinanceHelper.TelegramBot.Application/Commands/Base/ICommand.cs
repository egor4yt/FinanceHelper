﻿using FinanceHelper.TelegramBot.Application.Services.Localization.Interfaces;
using Telegram.Bot.Types;

namespace FinanceHelper.TelegramBot.Application.Commands.Base;

public interface ICommand
{
    string? Command { get; }
    Task ExecuteAsync(Update update, IStringLocalizer stringLocalizer);
}