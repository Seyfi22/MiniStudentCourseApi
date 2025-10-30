using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MiniStudentCourseApi.Data;
using MiniStudentCourseApi.Services.Interfaces;

namespace MiniStudentCourseApi.Services.Implementations
{
    public class GenericService<TDto, TEntity> : IGenericService<TDto>
        where TEntity : class
    {
        protected readonly StudentCourseDbContext _context;
        protected readonly IMapper _mapper;
        protected readonly DbSet<TEntity> _dbSet;

        public GenericService(StudentCourseDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
            _dbSet = _context.Set<TEntity>();
        }

        public virtual List<TDto> GetAll()
        {
            var entities = _dbSet.ToList();
            return _mapper.Map<List<TDto>>(entities);
        }

        public virtual TDto GetById(int id)
        {
            var entity = _dbSet.Find(id);
            if(entity == null)
            {
                throw new KeyNotFoundException($"Entity with id {id} not found");
            }

            return _mapper.Map<TDto>(entity);
        }

        public virtual TDto Add(TDto dto)
        {
            if(dto == null)
            {
                throw new ArgumentNullException(nameof(dto), "Dto can not be null");
            }

            var entity = _mapper.Map<TEntity>(dto);
            _dbSet.Add(entity);
            _context.SaveChanges();

            return _mapper.Map<TDto>(entity);
        }

        public virtual TDto Update(int id, TDto dto)
        {
            if(dto == null)
            {
                throw new ArgumentNullException(nameof(dto), "Dto can not be null");
            }

            var entity = _dbSet.Find(id);

            if(entity == null)
            {
                throw new KeyNotFoundException($"Entity with id {id} not found");
            }

            _mapper.Map(dto, entity);
            _context.SaveChanges();

            return _mapper.Map<TDto>(entity);
        }

        public virtual bool Delete(int id)
        {
            var entity = _dbSet.Find(id);

            if(entity == null)
            {
                throw new KeyNotFoundException($"Entity with id {id} not found");
            }

            _dbSet.Remove(entity);
            _context.SaveChanges();

            return true;
        }
    }
}
