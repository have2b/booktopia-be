using BusinessObject.DTO;
using BusinessObject.Model;

namespace DataAccess.Repository.Interface;

public interface ICategoryRepository
{
    Task<List<Category>> GetCategories(RequestDTO input);
    Task<Category> GetCategoryById(int id);
    Task<Category> AddCategory(CategoryDTO model);
    Task<Category> UpdateCategory(int id, CategoryDTO model);
    Task<Category> DeleteCategory(int id);
}