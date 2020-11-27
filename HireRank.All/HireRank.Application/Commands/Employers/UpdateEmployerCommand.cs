using HireRank.Application.ViewModels;
using MediatR;
using System;

namespace HireRank.Application.Commands.Employers
{
    public class UpdateEmployerCommand : IRequest<EmployerViewModel>
    {
        public Guid Id { get; set; }

        public string CompanyName { get; set; }

        public string CompanyDescription { get; set; }

        public string CompanyAddress { get; set; }

        public string ContactPhoneNumber { get; set; }

        public string SiteUrl { get; set; }
    }
}
