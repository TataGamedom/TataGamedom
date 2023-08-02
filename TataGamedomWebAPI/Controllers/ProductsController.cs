﻿using System;
using System.Collections.Generic;
using System.Drawing.Printing;
using System.Linq;
using System.Text.Json.Serialization;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TataGamedomWebAPI.Infrastructure.Data;
using TataGamedomWebAPI.Models.DTOs;
using TataGamedomWebAPI.Models.EFModels;
using static Microsoft.Extensions.Logging.EventSource.LoggingEventSource;
using Dapper;
using Microsoft.Data.SqlClient;
using TataGamedomWebAPI.Models.Dtos;

namespace TataGamedomWebAPI.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class ProductsController : ControllerBase
	{
		private readonly AppDbContext _context;

		public ProductsController(AppDbContext context)
		{
			_context = context;
		}

		// GET: api/Products
		[HttpGet]
		public async Task<ActionResult<IEnumerable<ProductsDTO>>> GetProducts(string? keyword, int page = 1, int pageSize = 9)
		{
			if (_context.Products == null)
			{
				return NotFound();
			}

			var products = await _context.Products
				.Include(p => p.Game)
				.ThenInclude(g => g.GameClassificationGames)
				.ThenInclude(gc => gc.GameClassification)
				.Include(p => p.GamePlatform) 
				.Where(p => p.Game.ChiName.Contains(keyword ?? string.Empty) || p.Game.EngName.Contains(keyword ?? string.Empty))
				.Select(p => new ProductsDTO
				{
					Id = p.Id,
					Index = p.Index,
					IsVirtual = p.IsVirtual,
					Price = p.Price,
					GamePlatformName = p.GamePlatform.Name,
					SaleDate = p.SaleDate,
					ChiName = p.Game.ChiName,
					EngName = p.Game.EngName,
					IsRestrict = p.Game.IsRestrict,
					GameCoverImg = p.Game.GameCoverImg,
					Classification = p.Game.GameClassificationGames.Select(gc => gc.GameClassification.Name).ToList()
				})
				.Skip((page - 1) * pageSize)
				.Take(pageSize)
				.ToListAsync();

			return Ok(products);
		}
		//public async Task<ActionResult<ProductsIndexDTO>> GetProducts(string? keyword, int page = 1, int pageSize = 9)
		//{
		//	if (_context.Products == null)
		//	{
		//		return NotFound();
		//	}
		//	var products = _context.Products.Include(p=>p.Game).AsQueryable();
		//	var classification = _context.GameClassificationGames.Include(c=>c.)

		//	using (var conn = _context.Database.GetDbConnection())
		//	{
		//		string sql = @"
		//	 SELECT P.Id, P.[Index], P.IsVirtual, P.Price, GPC.Name AS GamePlatformName, P.SaleDate, G.ChiName, G.EngName, G.IsRestrict, G.GameCoverImg
		//	 FROM Products AS P
		//	 JOIN Games AS G ON G.Id = P.GameId
		//	 JOIN CouponsProducts AS CP ON CP.ProductId = P.Id
		//	 JOIN Coupons AS C ON C.Id = CP.CouponId
		//	 JOIN GamePlatformsCodes AS GPC ON GPC.Id = P.GamePlatformId ";

		//		if (!string.IsNullOrEmpty(keyword))
		//		{
		//			sql += $@"WHERE G.ChiName LIKE '%{keyword}%' OR G.EngName LIKE '%{keyword}%'";
		//		}
		//		var products = await conn.QueryAsync<ProductsDTO>(sql);

		//		//分頁
		//		int totalCount = products.Count();
		//		int totalPages = (int)Math.Ceiling(totalCount / (double)pageSize);
		//		products = products.Skip(pageSize * (page - 1)).Take(pageSize);

		//		ProductsIndexDTO productsDTO = new ProductsIndexDTO
		//		{
		//			Products = products.ToList(),
		//			TotalPages = totalPages
		//		};
		//		return productsDTO;
		//	}
		//	return null;
		//}

		// GET: api/Products/5
		[HttpGet("{id}")]
		public async Task<ActionResult<Product>> GetProduct(int id)
		{
			if (_context.Products == null)
			{
				return NotFound();
			}
			var product = await _context.Products.FindAsync(id);

			if (product == null)
			{
				return NotFound();
			}

			return product;
		}

		// PUT: api/Products/5
		// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
		[HttpPut("{id}")]
		public async Task<IActionResult> PutProduct(int id, Product product)
		{
			if (id != product.Id)
			{
				return BadRequest();
			}

			_context.Entry(product).State = EntityState.Modified;

			try
			{
				await _context.SaveChangesAsync();
			}
			catch (DbUpdateConcurrencyException)
			{
				if (!ProductExists(id))
				{
					return NotFound();
				}
				else
				{
					throw;
				}
			}

			return NoContent();
		}

		// POST: api/Products
		// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
		[HttpPost]
		public async Task<ActionResult<Product>> PostProduct(Product product)
		{
			if (_context.Products == null)
			{
				return Problem("Entity set 'AppDbContext.Products'  is null.");
			}
			_context.Products.Add(product);
			await _context.SaveChangesAsync();

			return CreatedAtAction("GetProduct", new { id = product.Id }, product);
		}

		// DELETE: api/Products/5
		[HttpDelete("{id}")]
		public async Task<IActionResult> DeleteProduct(int id)
		{
			if (_context.Products == null)
			{
				return NotFound();
			}
			var product = await _context.Products.FindAsync(id);
			if (product == null)
			{
				return NotFound();
			}

			_context.Products.Remove(product);
			await _context.SaveChangesAsync();

			return NoContent();
		}

		private bool ProductExists(int id)
		{
			return (_context.Products?.Any(e => e.Id == id)).GetValueOrDefault();
		}
	}
}