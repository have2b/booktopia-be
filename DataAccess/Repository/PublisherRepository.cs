using BusinessObject.DTO;
using BusinessObject.Model;
using DataAccess.DAO;
using DataAccess.Repository.Interface;

namespace DataAccess.Repository;

public class PublisherRepository : IPublisherRepository
{
    private readonly PublisherDAO _dao = PublisherDAO.Instance;
    public Task<List<Publisher>> GetPublishers(RequestDTO input) => _dao.GetPublishersAsync(input);
    public Task<Publisher> GetPublisherById(int id) => _dao.GetPublisherByIdAsync(id);
    public Task<Publisher> AddPublisher(PublisherDTO model) => _dao.AddPublisherAsync(model);
    public Task<Publisher> UpdatePublisher(int id, PublisherDTO model) => _dao.UpdatePublisherAsync(id, model);
    public Task<Publisher> DeletePublisher(int id) => _dao.DeletePublisherAsync(id);
}