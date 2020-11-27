using AutoMapper;
using HireRank.Application.ViewModels;
using HireRank.Core.Extensions;
using HireRank.Core.Store;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace HireRank.Application.Commands.Employers
{
    public class UpdateEmployerCommandHandler : IRequestHandler<UpdateEmployerCommand, EmployerViewModel>
    {
        private readonly IStore _store;
        private IMapper _mapper;

        public UpdateEmployerCommandHandler(IStore store, IMapper mapper)
        {
            _store = store;
            _mapper = mapper;
        }

        public async Task<EmployerViewModel> Handle(UpdateEmployerCommand request, CancellationToken cancellationToken)
        {
            Core.Entities.Employer employer = await _store.Employers.WithIdAsync(request.Id);

            employer.CompanyName = request.CompanyName;
            employer.CompanyDescription = request.CompanyDescription;
            employer.CompanyAddress = request.CompanyAddress;
            employer.ContactPhoneNumber = request.ContactPhoneNumber;
            employer.SiteUrl = request.SiteUrl;

            await _store.SaveChangesAsync();

            EmployerViewModel employerViewModel = _mapper.Map<EmployerViewModel>(employer);
            return employerViewModel;
        }
    }
}
