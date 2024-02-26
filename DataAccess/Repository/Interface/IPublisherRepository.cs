using BusinessObject.DTO;
using BusinessObject.Model;

namespace DataAccess.Repository.Interface;

public interface IPublisherRepository
{
    Task<List<Publisher>> GetPublishers(RequestDTO input);
    Task<Publisher> GetPublisherById(int id);
    Task<Publisher> AddPublisher(PublisherDTO model);
    Task<Publisher> UpdatePublisher(int id, PublisherDTO model);
    Task<Publisher> DeletePublisher(int id);
}