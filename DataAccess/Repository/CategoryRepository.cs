using BusinessObject.DTO;
using BusinessObject.Model;
using DataAccess.DAO;
using DataAccess.Repository.Interface;

namespace DataAccess.Repository;

public class CategoryRepository : ICategoryRepository
{
    private readonly CategoryDAO _dao = CategoryDAO.Instance;
    public Task<List<Category>> GetCategories() => _dao.GetCategoriesAsync();
    public Task<Category> GetCategoryById(int id) => _dao.GetCategoryByIdAsync(id);
    public Task<Category> AddCategory(CategoryDTO model) => _dao.AddCategoryAsync(model);
    public Task<Category> UpdateCategory(int id, CategoryDTO model) => _dao.UpdateCategoryAsync(id, model);
    public Task<Category> DeleteCategory(int id) => _dao.DeleteCategoryAsync(id);
}