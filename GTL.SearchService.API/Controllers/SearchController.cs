using GTL.SearchService.API.Extensions;
using GTL.SearchService.API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;

namespace GTL.SearchService.API.Controllers
{
    [Route("api/[controller]")]
   
    public class SearchController : ControllerBase
    {
        private readonly IDistributedCache _distributedCache;
        private readonly ILogger<SearchController> _logger;

        public SearchController(IDistributedCache distributedCache, ILogger<SearchController> logger)
        {
            _distributedCache = distributedCache;
            _logger = logger;
        }

        // GET api/<SearchController>/5
        [HttpGet("{searchPhrase}")]
        //[Route("search-books")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Search(string searchPhrase)
        {

            #region map 10 books in cache with searchPhrase "Learn"
            List<Guid> sampleGuids = new List<Guid>
            {
                Guid.Parse("4f176b23-3a34-4bdf-bb92-8a4ecf7c2c9f"),
                Guid.Parse("62f347bc-d9b8-4d61-8b34-0d5212e56e1a"),
                Guid.Parse("cf3c627b-e447-42b7-bd02-f129f89b6ae2"),
                Guid.Parse("17cdbfdc-5c8e-4d12-91aa-620b2ac9b845"),
                Guid.Parse("2a847573-2ad5-49b3-8659-df8a523f7409"),
                Guid.Parse("580d8cd4-3e56-4943-82d9-1e438ae32490"),
                Guid.Parse("88bc9739-5e33-4050-9210-16d71d8aef2c"),
                Guid.Parse("34096b24-91f7-4e23-bc96-7d4715e6a85a"),
                Guid.Parse("7a41fc2d-6b2d-4298-801e-14c0db2eaff6"),
                Guid.Parse("b52d1138-97c3-4d53-bbc9-99b3924b003e")
            };

            await _distributedCache.SetRecordAsync("search_learn", sampleGuids, TimeSpan.FromMinutes(30));

            var books = new List<Book>
            {
                new Book { Id = Guid.Parse("4f176b23-3a34-4bdf-bb92-8a4ecf7c2c9f"), Title = "Learn Redis", AmountInStock = 10 },
                new Book { Id = Guid.Parse("62f347bc-d9b8-4d61-8b34-0d5212e56e1a"), Title = "Mastering C#", AmountInStock = 15 },
                new Book { Id = Guid.Parse("cf3c627b-e447-42b7-bd02-f129f89b6ae2"), Title = "Distributed Systems", AmountInStock = 8 },
                new Book { Id = Guid.Parse("17cdbfdc-5c8e-4d12-91aa-620b2ac9b845"), Title = "Microservices Architecture", AmountInStock = 12 },
                new Book { Id = Guid.Parse("2a847573-2ad5-49b3-8659-df8a523f7409"), Title = "Clean Code", AmountInStock = 20 },
                new Book { Id = Guid.Parse("580d8cd4-3e56-4943-82d9-1e438ae32490"), Title = "Building Scalable Apps", AmountInStock = 18 },
                new Book { Id = Guid.Parse("88bc9739-5e33-4050-9210-16d71d8aef2c"), Title = "The Art of Scalability", AmountInStock = 5 },
                new Book { Id = Guid.Parse("34096b24-91f7-4e23-bc96-7d4715e6a85a"), Title = "Refactoring", AmountInStock = 10 },
                new Book { Id = Guid.Parse("7a41fc2d-6b2d-4298-801e-14c0db2eaff6"), Title = "Domain-Driven Design", AmountInStock = 7 },
                new Book { Id = Guid.Parse("b52d1138-97c3-4d53-bbc9-99b3924b003e"), Title = "Pro Git", AmountInStock = 25 }
            };

            foreach (var book in books)
            {
                string cacheKeybook = $"book_{book.Id}";
                await _distributedCache.SetRecordAsync(cacheKeybook, book, TimeSpan.FromMinutes(30));
            }
            #endregion

            //Search in cache
            string cacheKey = $"search_{searchPhrase}";

            //Redis cache on searchPhrase contains a list of serialized Json of guids of books
            List<Guid> cachedBooks = await _distributedCache.GetRecordAsync<List<Guid>>(cacheKey);



            if (cachedBooks != null)
            {
                _logger.LogInformation($"Cache hit for search phrase: {searchPhrase}");

                List<Book> booksfromCache = new List<Book>();
                foreach (var bookId in cachedBooks)
                {
                    string cacheKeyBook = $"book_{bookId}";
                    var book = await _distributedCache.GetRecordAsync<Book>(cacheKeyBook);
                    booksfromCache.Add(book);
                }

                return Ok(booksfromCache);
            }


            //Call Warehouse service to get list of books from searchPhrase

            return Ok("Should call warehouse service to get new list from searchPhrase");
        }

       

    }
}
