using Library.Data;
using Library.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace Library.Controllers
{
	[Authorize]
	public class BookController : Controller
	{
		private readonly LibraryDbContext context;

		public BookController(LibraryDbContext _context)
		{
			context = _context;
		}

		[HttpGet]
		public async Task<IActionResult> All()
		{
			var model = await context.Books
			.AsNoTracking()
			.Select(b => new BookInfoViewModel()
			{
				Id = b.Id,
				Author = b.Author,
				Category = b.Category.Name,
				ImageUrl = b.ImageUrl,
				Rating = b.Rating,
				Title = b.Title
			}).ToListAsync();

			return View(model);
		}

		[HttpGet]
		public async Task<IActionResult> Mine()
		{
			var userId = GetUserId();

			var model = await context.Books.Where(b => b.UsersBooks.Any(ub => ub.CollectorId == userId))
				.AsNoTracking()
				.Select(b => new BookMineViewModel()
				{
					Id = b.Id,
					Author = b.Author,
					Category = b.Category.Name,
					ImageUrl = b.ImageUrl,
					Description = b.Description,
					Title = b.Title
				}).ToListAsync();

			if (model == null)
			{
				return BadRequest();
			}

			return View(model);
		}

		[HttpPost]
		public async Task<IActionResult> AddToCollection(int id)
		{
			var book = await context.Books.Where(b => b.Id == id).Include(b => b.UsersBooks).FirstOrDefaultAsync();

			if (book == null)
			{
				return BadRequest();
			}

			var userId = GetUserId();

			if (!book.UsersBooks.Any(ub => ub.CollectorId == userId))
			{
				book.UsersBooks.Add(new IdentityUserBook()
				{
					CollectorId = userId,
					BookId = book.Id
				});

				await context.SaveChangesAsync();
			}

			return RedirectToAction("All");
		}

		[HttpPost]
		public async Task<IActionResult> RemoveFromCollection(int id)
		{
			var book = await context.Books.Where(b => b.Id == id).Include(b => b.UsersBooks).FirstOrDefaultAsync();

			if (book == null)
			{
				return BadRequest();
			}

			var userId = GetUserId();

			var ub = book.UsersBooks.FirstOrDefault(ub => ub.CollectorId == userId);

			if (ub == null)
			{
				return BadRequest();
			}

			book.UsersBooks.Remove(ub);
			await context.SaveChangesAsync();

			return RedirectToAction("Mine");
		}

		[HttpGet]
		public async Task<IActionResult> Add()
		{
			BookFormViewModel model = new BookFormViewModel();

			model.Categories = await context.Categories.AsNoTracking()
				.Select(c => new CategoryViewModel()
				{
					Id = c.Id,
					Name = c.Name
				})
				.ToListAsync();

			return View(model);
		}

		[HttpPost]
		public async Task<IActionResult> Add(BookFormViewModel model)
		{
			Book book = new Book()
			{
				Author = model.Author,
				Title = model.Title,
				Description = model.Description,
				ImageUrl = model.Url,
				Rating = model.Rating,
				CategoryId = model.CategoryId
			};

			await context.Books.AddAsync(book);
			await context.SaveChangesAsync();

			return RedirectToAction("All");
		}

		private string GetUserId()
		{
			return User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? string.Empty;
		}
	}
}
