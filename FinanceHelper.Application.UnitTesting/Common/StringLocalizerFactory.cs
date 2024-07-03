﻿using FinanceHelper.Application.Services.Interfaces;

namespace FinanceHelper.Application.UnitTesting.Common;

public class StringLocalizerFactory
{
    public static IStringLocalizer<TCommand> Create<TCommand>()
    {
        var localizer = new UnitTestStringLocalizer<TCommand>();
        return localizer;
    }
}