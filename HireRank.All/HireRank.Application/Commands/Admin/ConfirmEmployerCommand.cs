using MediatR;
using System;

namespace HireRank.Application.Commands.Admin
{
    public class ConfirmEmployerCommand : IRequest<bool>
    {
        public Guid Id { get; set;}
    }
}
