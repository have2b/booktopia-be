
using BusinessObject;
using BusinessObject.Model;
using BusinessObject.Model.CSV;
using CsvHelper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Globalization;

namespace BookStoreAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class SeedController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;

        public SeedController(AppDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }


        [HttpPut]
        public async Task<IActionResult> SeedCategory()
        {
            var skippedRows = 0;
            var existingCategories = await _context.Categories.ToDictionaryAsync(c => c.CategoryId);

            // Seed categories
            using var reader = new StreamReader(Path.Combine(_env.ContentRootPath, "Data/categories.csv"));
            using var csv = new CsvReader(reader, CultureInfo.InvariantCulture);

            var categories = csv.GetRecords<Category>().ToList();
            foreach (var category in categories)
            {
                if (existingCategories.ContainsKey(category.CategoryId))
                {
                    skippedRows++;
                    continue;
                }

                await _context.Categories.AddAsync(category);
            }

            using var transaction = await _context.Database.BeginTransactionAsync();
            await _context.Database.ExecuteSqlRawAsync($"SET IDENTITY_INSERT Categories ON");
            await _context.SaveChangesAsync();
            await _context.Database.ExecuteSqlRawAsync($"SET IDENTITY_INSERT Categories OFF");
            await transaction.CommitAsync();

            return Ok(skippedRows);
        }

        [HttpPut]
        public async Task<IActionResult> SeedPublisher()
        {
            var skippedRows = 0;
            var existingPublishers = await _context.Publishers.ToDictionaryAsync(p => p.PublisherId);

            // Seed publishers
            using var reader = new StreamReader(Path.Combine(_env.ContentRootPath, "Data/publishers.csv"));
            using var csv = new CsvReader(reader, CultureInfo.InvariantCulture);

            var publishers = csv.GetRecords<Publisher>().ToList();
            foreach (var publisher in publishers)
            {
                if (existingPublishers.ContainsKey(publisher.PublisherId))
                {
                    skippedRows++;
                    continue;
                }

                await _context.Publishers.AddAsync(publisher);
            }

            using var transaction = await _context.Database.BeginTransactionAsync();
            await _context.Database.ExecuteSqlRawAsync($"SET IDENTITY_INSERT Publishers ON");
            await _context.SaveChangesAsync();
            await _context.Database.ExecuteSqlRawAsync($"SET IDENTITY_INSERT Publishers OFF");
            await transaction.CommitAsync();


            return Ok(skippedRows);
        }

        [HttpPut]
        public async Task<IActionResult> SeedBook()
        {

            var skippedRows = 0;
            var existingBooks = await _context.Books.ToDictionaryAsync(b=> b.BookId);

            // Seed publishers
            using var reader = new StreamReader(Path.Combine(_env.ContentRootPath, "Data/books.csv"));
            using var csv = new CsvReader(reader, CultureInfo.InvariantCulture);

            var books = csv.GetRecords<BookRecord>().ToList();
            foreach (var record in books)
            {
                if (existingBooks.ContainsKey(record.BookId))
                {
                    skippedRows++;
                    continue;
                }
                var mapper = MapperConfig.Init();
                if (record.CostPrice > record.SellPrice)
                {
                    var tmp = record.CostPrice;
                    record.CostPrice = record.SellPrice;
                    record.SellPrice = tmp;
                    
                }
                var book = mapper.Map<Book>(record);

                await _context.Books.AddAsync(book);
            }

            using var transaction = await _context.Database.BeginTransactionAsync();
            await _context.Database.ExecuteSqlRawAsync($"SET IDENTITY_INSERT Books ON");
            await _context.SaveChangesAsync();
            await _context.Database.ExecuteSqlRawAsync($"SET IDENTITY_INSERT Books OFF");
            await transaction.CommitAsync();


            return Ok(skippedRows);
        }
    }
}
