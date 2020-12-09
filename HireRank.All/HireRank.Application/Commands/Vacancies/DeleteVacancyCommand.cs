using MediatR;
using System;

namespace HireRank.Application.Commands.Vacancies
{
    public class DeleteVacancyCommand : IRequest<Guid>
    {
        public Guid Id { get; set; }
    }
}
