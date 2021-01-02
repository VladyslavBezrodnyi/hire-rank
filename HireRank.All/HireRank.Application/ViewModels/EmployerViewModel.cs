using System;

namespace HireRank.Application.ViewModels
{
    public class EmployerViewModel
    {
        public string Email { get; set; }

        public Guid Id { get; set; }

        public string CompanyName { get; set; }

        public string CompanyDescription { get; set; }

        public string CompanyAddress { get; set; }

        public string ContactPhoneNumber { get; set; }

        public string SiteUrl { get; set; }

        public bool IsConfirmed { get; set; }
    }
}
