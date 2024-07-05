﻿using FinanceHelper.Shared;
using MediatR;

namespace FinanceHelper.Application.Commands.Users.Register;

public class RegisterUserCommandRequest : IRequest<RegisterUserCommandResponse>
{
    public string Email { get; set; } = string.Empty;
    public string PasswordHash { get; set; } = string.Empty;
    public string PreferredLocalizationCode { get; set; } = string.Empty;
    public JwtDescriptorDetails JwtDescriptorDetails { get; set; } = null!;
}