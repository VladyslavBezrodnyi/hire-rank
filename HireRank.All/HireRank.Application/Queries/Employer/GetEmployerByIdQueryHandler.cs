using AutoMapper;
using HireRank.Application.ViewModels;
using HireRank.Core.Extensions;
using HireRank.Core.Store;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace HireRank.Application.Queries.Employer
{
    public class GetEmployerByIdQueryHandler : IRequestHandler<GetEmployerByIdQuery, EmployerViewModel>
    {
        private readonly IStore _store;
        private IMapper _mapper;

        public GetEmployerByIdQueryHandler(IStore store, IMapper mapper)
        {
            _store = store;
            _mapper = mapper;
        }

        public async Task<EmployerViewModel> Handle(GetEmployerByIdQuery request, CancellationToken cancellationToken)
        {
            Core.Entities.Employer employer = await _store.Employers.WithIdAsync(request.Id);
            EmployerViewModel employerViewModel = _mapper.Map<EmployerViewModel>(employer);
            return employerViewModel;
        }
    }
}
