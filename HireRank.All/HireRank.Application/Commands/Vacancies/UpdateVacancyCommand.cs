using MediatR;
using System;

namespace HireRank.Application.Commands.Vacancies
{
    public class UpdateVacancyCommand : IRequest<Guid>
    {
        public Guid Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public int TestSize { get; set; }
    }
}
