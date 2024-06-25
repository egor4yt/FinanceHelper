﻿using FinanceHelper.Application.Exceptions;
using FinanceHelper.Domain.Entities;
using FinanceHelper.Persistence;
using FinanceHelper.Shared;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FinanceHelper.Application.Commands.Users.Register;

public class RegisterUserCommandHandler(ApplicationDbContext applicationDbContext) : IRequestHandler<RegisterUserCommandRequest, RegisterUserCommandResponse>
{
    public async Task<RegisterUserCommandResponse> Handle(RegisterUserCommandRequest request, CancellationToken cancellationToken)
    {
        var response = new RegisterUserCommandResponse();

        var userExists = await applicationDbContext.Users.AnyAsync(x => x.Email == request.Email, cancellationToken);
        if (userExists) throw new ConflictException("User", request.Email);

        var newUser = new User
        {
            Email = request.Email,
            PasswordHash = SecurityHelper.ComputeSha256Hash(request.Password)
        };

        await applicationDbContext.Users.AddAsync(newUser, cancellationToken);
        await applicationDbContext.SaveChangesAsync(cancellationToken);

        response.Id = newUser.Id;
        return response;
    }
}