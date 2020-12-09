using AutoMapper;
using HireRank.Application.ViewModels;
using HireRank.Core.Extensions;
using HireRank.Core.Store;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace HireRank.Application.Queries.Student
{
    public class GetStudentByIdQueryHandler : IRequestHandler<GetStudentByIdQuery, StudentViewModel>
    {
        private readonly IStore _store;
        private IMapper _mapper;

        public GetStudentByIdQueryHandler(IStore store, IMapper mapper)
        {
            _store = store;
            _mapper = mapper;
        }

        public async Task<StudentViewModel> Handle(GetStudentByIdQuery request, CancellationToken cancellationToken)
        {
            Core.Entities.Student student = await _store.Students.WithIdAsync(request.Id);
            StudentViewModel studentViewModel = _mapper.Map<StudentViewModel>(student);
            return studentViewModel;
        }
    }
}
