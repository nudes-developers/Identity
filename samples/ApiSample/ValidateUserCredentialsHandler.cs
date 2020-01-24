using MediatR;
using Nudes.Identity.Features.Users;
using System.Threading;
using System.Threading.Tasks;

namespace ApiSample
{
    public class ValidateUserCredentialsHandler : IRequestHandler<ValidateUserCredentialsQuery, UserResult>
    {
        public Task<UserResult> Handle(ValidateUserCredentialsQuery request, CancellationToken cancellationToken)
        {
            if (request.Username == "bob" && request.Password == "bob")
            {
                return Task.FromResult(new UserResult()
                {
                     Username = "bob",
                     SubjectId = "1",
                });
            }
            return Task.FromResult<UserResult>(null);
        }
    }
}
