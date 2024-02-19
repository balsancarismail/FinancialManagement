using Application.Features.Auth.Constants;
using Core.CrossCuttingConcerns.Exceptions.Types;
using Core.Security.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Users.Rules;

public class UserBusinessRules(UserManager<AppUser> userManager)
{
    public Task UserShouldBeExistsWhenSelected(AppUser? user)
    {
        if (user == null)
            throw new BusinessException(AuthMessages.UserDontExists);
        return Task.CompletedTask;
    }

    public async Task UserIdShouldBeExistsWhenSelected(int id)
    {
        var doesExist = await userManager.FindByIdAsync(id.ToString());

        if (doesExist is null)
            throw new BusinessException(AuthMessages.UserDontExists);
    }

    public Task UserPasswordShouldBeMatched(AppUser user, string password)
    {
        var result = userManager.CheckPasswordAsync(user, password).Result;

        if (!result) throw new BusinessException(AuthMessages.PasswordDontMatch);

        return Task.CompletedTask;
    }

    public async Task UserEmailShouldNotExistsWhenInsert(string email)
    {
        var doesExists = await userManager.Users.AnyAsync(u => u.Email == email);

        if (doesExists)
            throw new BusinessException(AuthMessages.UserMailAlreadyExists);
    }

    public async Task UserEmailShouldNotExistsWhenUpdate(int id, string email)
    {
        var doesExists = await userManager.Users.AnyAsync(u => u.Id != id && u.Email == email);
        if (doesExists)
            throw new BusinessException(AuthMessages.UserMailAlreadyExists);
    }

    public async Task RoleMustBeExists(string role)
    {
        
        if (!ConstantRoles.Roles.Contains(role))
            throw new BusinessException(AuthMessages.RoleDontExists);
    }
}