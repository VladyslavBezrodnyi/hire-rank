using HireRank.Core.Entities;
using HireRank.Core.Store;
using HireRank.Infrastructure.Context;
using System.Linq;
using System.Threading.Tasks;

namespace HireRank.Infrastructure
{
    public class EntityFrameworkStore : IStore
    {
        private readonly HireRankContext _context;

        public EntityFrameworkStore(HireRankContext context)
        {
            _context = context;
        }

        public IQueryable<User> Users => _context.Users;

        public IQueryable<Admin> Admins => _context.Admins;

        public IQueryable<Student> Students => _context.Students;

        public IQueryable<Employer> Employers => _context.Employers;

        public IQueryable<Campaign> Campaigns => _context.Campaigns;

        public IQueryable<Vacancy> Vacancies => _context.Vacancies;

        public IQueryable<StudentVacancy> StudentVacancies => _context.StudentVacancies;

        public IQueryable<VacancyQuestion> VacancyQuestions => _context.VacancyQuestions;

        public IQueryable<Question> Questions => _context.Questions;

        public IQueryable<Option> Options => _context.Options;

        public async Task<TEntity> AddEntity<TEntity>(TEntity entity, bool saveChanges = false) where TEntity : class
        {
            await _context.Set<TEntity>().AddAsync(entity);

            if (saveChanges)
                await SaveChangesAsync();

            return entity;
        }

        public async Task UpdateEntity<TEntity>(TEntity entity, bool saveChanges = false) where TEntity : class
        {
            _context.Set<TEntity>().Update(entity);

            if (saveChanges)
                await SaveChangesAsync();
        }

        public async Task DeleteEntity<TEntity>(TEntity entity, bool saveChanges = false) where TEntity : class
        {
            _context.Set<TEntity>().Remove(entity);

            if (saveChanges)
                await SaveChangesAsync();
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
