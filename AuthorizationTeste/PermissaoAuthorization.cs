using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace ApiFolhaPagamento.AuthorizationTeste
{
    public class PermissaoAuthorization : AuthorizationHandler<Permissao>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, Permissao requirement)
        {
            var permissaoClaim = context.User.FindFirst(claim => claim.Type == ClaimTypes.Role);

            if(permissaoClaim is null)
                return Task.CompletedTask;

            if (permissaoClaim.Value == "1")
                context.Succeed(requirement);

            return Task.CompletedTask;

        }
    }
}
