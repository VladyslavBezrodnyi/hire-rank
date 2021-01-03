using HireRank.Application.ViewModels;
using MediatR;
using System.Collections.Generic;

namespace HireRank.Application.Queries.Admin
{
    public class GetEmployersForConfirmationQuery : IRequest<List<EmployerViewModel>>
    {
    }
}
