using AutoMapper;
using HireRank.Application.ViewModels;
using HireRank.Core.Extensions;
using HireRank.Core.Store;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace HireRank.Application.Commands.Students
{
    public class UpdateStudentCommandHandler : IRequestHandler<UpdateStudentCommand, StudentViewModel>
    {
        private readonly IStore _store;
        private IMapper _mapper;

        public UpdateStudentCommandHandler(IStore store, IMapper mapper)
        {
            _store = store;
            _mapper = mapper;
        }

        public async Task<StudentViewModel> Handle(UpdateStudentCommand request, CancellationToken cancellationToken)
        {
            Core.Entities.Student student = await _store.Students.WithIdAsync(request.Id);

            student.FirstName = request.FirstName;
            student.MiddleName = request.MiddleName;
            student.LastName = request.LastName;
            student.DateOfBirth = request.DateOfBirth;
            student.UniversityName = request.UniversityName;
            student.Major = request.Major;

            await _store.SaveChangesAsync();

            StudentViewModel studentViewModel = _mapper.Map<StudentViewModel>(student);
            return studentViewModel;
        }
    }
}
