namespace HireRank.Application.Commands.Account
{
    public class EmployerRegisterCommand: RegisterCommand
    {
        public string CompanyName { get; set; }

        public string CompanyDescription { get; set; }

        public string CompanyAddress { get; set; }

        public string ContactPhoneNumber { get; set; }

        public string SiteUrl { get; set; }
    }
}
