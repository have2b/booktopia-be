using BusinessObject;
using BusinessObject.DTO;
using BusinessObject.Model;
using DataAccess.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.DAO;

public class PublisherDAO
{
    private static readonly Lazy<PublisherDAO> _instance =
        new Lazy<PublisherDAO>(() => new PublisherDAO(new AppDbContext()));

    private readonly AppDbContext _context;

    private PublisherDAO(AppDbContext context)
    {
        _context = context;
    }

    public static PublisherDAO Instance => _instance.Value;

    // Get all publisher
    public async Task<List<Publisher>> GetPublishersAsync(RequestDTO input)
    {
        return await _context.Publishers
            .Skip(input.PageIndex * input.PageSize)
            .Take(input.PageSize)
            .ToListAsync();
    }

    // Get a publisher by Id
    public async Task<Publisher> GetPublisherByIdAsync(int id)
    {
        var publisher = await _context.Publishers.FirstOrDefaultAsync(c => c.PublisherId == id);
        if (publisher == null)
        {
            throw new PublisherNotFoundException(id);
        }

        return publisher;
    }

    // Add new publisher
    public async Task<Publisher> AddPublisherAsync(PublisherDTO model)
    {
        var mapper = MapperConfig.Init();
        var publisher = mapper.Map<Publisher>(model);
        _context.Publishers.Add(publisher);
        await _context.SaveChangesAsync();
        return publisher;
    }

    // Update a publisher
    public async Task<Publisher> UpdatePublisherAsync(int id, PublisherDTO model)
    {
        var publisher = await _context.Publishers.FirstOrDefaultAsync(c => c.PublisherId == id);
        if (publisher == null)
        {
            throw new PublisherNotFoundException(id);
        }

        var mapper = MapperConfig.Init();
        publisher = mapper.Map(model, publisher);
        _context.Publishers.Update(publisher);
        await _context.SaveChangesAsync();
        return publisher;
    }

    // Delete a publisher
    public async Task<Publisher> DeletePublisherAsync(int id)
    {
        var publisher = await _context.Publishers.FirstOrDefaultAsync(c => c.PublisherId == id);
        if (publisher == null)
        {
            throw new PublisherNotFoundException(id);
        }

        _context.Publishers.Remove(publisher);
        await _context.SaveChangesAsync();
        return publisher;
    }
}