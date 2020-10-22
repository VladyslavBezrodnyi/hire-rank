using HireRank.Core.Entities;
using System.Linq;
using System.Threading.Tasks;

namespace HireRank.Core.Store
{
    public interface IStore
    {
        public IQueryable<User> Users { get; }

        public IQueryable<Admin> Admins { get; }

        public IQueryable<Student> Students { get; }

        public IQueryable<Employer> Employers { get; }

        public IQueryable<Campaign> Campaigns { get;}

        public IQueryable<Vacancy> Vacancies { get; }

        public IQueryable<StudentVacancy> StudentVacancies { get; }

        public IQueryable<VacancyQuestion> VacancyQuestions { get; }

        public IQueryable<Question> Questions { get; }

        public IQueryable<Option> Options { get; }

        Task<TEntity> AddEntity<TEntity>(TEntity entity, bool saveChanges = false) where TEntity : class;

        Task UpdateEntity<TEntity>(TEntity entity, bool saveChanges = false) where TEntity : class;

        Task DeleteEntity<TEntity>(TEntity entity, bool saveChanges = false) where TEntity : class;

        Task SaveChangesAsync();
    }
}
