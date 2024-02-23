using BusinessObject;
using BusinessObject.DTO;
using DataAccess.Exceptions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace DataAccess.DAO;

using BusinessObject.Model;

public class CategoryDAO
{
    private static readonly Lazy<CategoryDAO> _instance =
        new Lazy<CategoryDAO>(() => new CategoryDAO(new AppDbContext()));

    private readonly AppDbContext _context;

    private CategoryDAO(AppDbContext context)
    {
        _context = context;
    }

    public static CategoryDAO Instance => _instance.Value;

    // Get all categories
    public async Task<List<Category>> GetCategoriesAsync()
    {
        return await _context.Categories.ToListAsync();
    }

    // Get a category by Id
    public async Task<Category> GetCategoryByIdAsync(int id)
    {
        var category = await _context.Categories.FirstOrDefaultAsync(c => c.CategoryId == id);
        if (category == null)
        {
            throw new CategoryNotFoundException(id);
        }

        return category;
    }

    // Add new category
    public async Task<Category> AddCategoryAsync(CategoryDTO model)
    {
        var mapper = MapperConfig.Init();
        var category = mapper.Map<Category>(model);
        _context.Categories.Add(category);
        await _context.SaveChangesAsync();
        return category;
    }

    // Update a category
    public async Task<Category> UpdateCategoryAsync(int id, CategoryDTO model)
    {
        var category = await _context.Categories.FirstOrDefaultAsync(c => c.CategoryId == id);
        if (category == null)
        {
            throw new CategoryNotFoundException(id);
        }

        var mapper = MapperConfig.Init();
        category = mapper.Map(model, category);
        _context.Categories.Update(category);
        await _context.SaveChangesAsync();
        return category;
    }

    // Delete a category
    public async Task<Category> DeleteCategoryAsync(int id)
    {
        var category = await _context.Categories.FirstOrDefaultAsync(c => c.CategoryId == id);
        if (category == null)
        {
            throw new CategoryNotFoundException(id);
        }

        _context.Categories.Remove(category);
        await _context.SaveChangesAsync();
        return category;
    }
}