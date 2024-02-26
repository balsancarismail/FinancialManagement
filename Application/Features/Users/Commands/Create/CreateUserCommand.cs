using Application.Features.Auth.Constants;
using Application.Features.Users.Rules;
using AutoMapper;
using Core.Application.Pipelines.Transaction;
using Core.CrossCuttingConcerns.Exceptions.Types;
using Core.Security.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Users.Commands.Create;

public class CreateUserCommand : IRequest<CreatedUserResponse>, ITransactionalRequest
{

    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public string Role { get; set; }

    //public string[] Roles => new[] { Admin, Write, Add };

    public class CreateUserCommandHandler(
        IMapper mapper,
        UserBusinessRules userBusinessRules,
        UserManager<AppUser> userManager)
        : IRequestHandler<CreateUserCommand, CreatedUserResponse>
    {
        public async Task<CreatedUserResponse> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            await userBusinessRules.UserEmailShouldNotExistsWhenInsert(request.Email);
            var user = mapper.Map<AppUser>(request);
            var identityResult = await userManager.CreateAsync(user, request.Password);

            if (identityResult.Succeeded)
            {
                var createdUser =
                    await userManager.Users.SingleOrDefaultAsync(x => x.Email == request.Email.Trim().Normalize(),
                        cancellationToken);
                await userBusinessRules.UserShouldBeExistsWhenSelected(createdUser);
                await userBusinessRules.RoleMustBeExists(request.Role);

                await userManager.AddToRoleAsync(createdUser, request.Role);
                var response = mapper.Map<CreatedUserResponse>(createdUser);
                return response;
            }
            throw new BusinessException(AuthMessages.RegistrationFailed);
        }
    }
}