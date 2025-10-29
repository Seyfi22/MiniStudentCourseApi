namespace MiniStudentCourseApi.Services.Interfaces
{
    public interface IGenericService<TDto>
    {
        public List<TDto> GetAll();
        public TDto GetById(int id);
        public TDto Add(TDto dto);
        public TDto Update(int id, TDto dto);
        public bool Delete(int id);
    }
}
