﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TataGamedomWebAPI.Infrastructure;
using TataGamedomWebAPI.Infrastructure.Data;
using TataGamedomWebAPI.Models.DTOs.Cart;
using TataGamedomWebAPI.Models.DTOs.Shop;
using TataGamedomWebAPI.Models.EFModels;

namespace TataGamedomWebAPI.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class CartsController : ControllerBase
	{
		private readonly AppDbContext _context;

		public CartsController(AppDbContext context)
		{
			_context = context;
		}

		// GET: api/Carts
		[HttpGet]
		public async Task<ActionResult<CartDTO>> GetCart()
		{
			var account = HttpContext.User.FindFirstValue(ClaimTypes.Name);
			var user = await _context.Members.FirstOrDefaultAsync(m => m.Account == account);

			if (user == null)
			{
				//未登入取得購物車的方法
				//todo
				return NotFound("請先登入會員");
			}
			var currentTime = DateTime.Now;
			List<CartItemDTO> cartItems = await GetCartItems(user, currentTime);
			if (cartItems.Count == 0)
			{
				// 購物車沒東西的話
				return NotFound("您的購物車是空的");
			}

			var cart = new CartDTO
			{
				Id = user.Id,
				MemberId = user.Id,
				CartItems = cartItems
			};
			return cart;
		}
		private async Task<List<CartItemDTO>> GetCartItems(Member? user, DateTime currentTime)
		{
			return await _context.Carts
								.Where(c => c.MemberId == user.Id)
								.Select(c => new CartItemDTO
								{
									Product = new ProductsDTO
									{
										Id = c.Product.Id,
										Index = c.Product.Index,
										IsVirtual = c.Product.IsVirtual,
										Price = c.Product.Price,
										SpecialPrice = c.Product.CouponsProducts
											.Any(cp => currentTime >= cp.Coupon.StartTime && currentTime <= cp.Coupon.EndTime && cp.ProductId == c.ProductId)
											? (int)Math.Round(c.Product.CouponsProducts
														.Where(cp => currentTime >= cp.Coupon.StartTime && currentTime <= cp.Coupon.EndTime && cp.ProductId == c.ProductId)
														.Select(cp => cp.Coupon.DiscountTypeId == 1
														? c.Product.Price * (cp.Coupon.Discount / 100.0)
														: (cp.Coupon.DiscountTypeId == 2
														? c.Product.Price - cp.Coupon.Discount
														: c.Product.Price))
														.FirstOrDefault(), 1)
											: c.Product.Price,
										GamePlatformName = c.Product.GamePlatform.Name,
										SaleDate = c.Product.SaleDate,
										ChiName = c.Product.Game.ChiName,
										GameCoverImg = c.Product.Game.GameCoverImg,
									},
									Qty = c.Quantity
								})
								.ToListAsync();
		}
            return cart;
        }

		// PUT: api/Carts/5
		// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
		[HttpPut]
		public async Task<IActionResult> UpdateItem(int productId, int newQty)
		{
			newQty = newQty <= 0 ? 0 : newQty;

			await UpdateItemQty(productId, newQty);
			return Ok();
		}

		private async Task<ActionResult<CartDTO>> UpdateItemQty(int productId, int newQty)
		{
			// 取得目前購物車主檔
			var account = HttpContext.User.FindFirstValue(ClaimTypes.Name);
			var user = await _context.Members.FirstOrDefaultAsync(m => m.Account == account);

			List<CartItemDTO> cartItems = await GetCartItems(user, DateTime.Now);

			foreach(var item in cartItems)
			{
				if (newQty == 0)
				{
					var cartItem = await _context.Carts.FindAsync(item.Product.Id);
					if (cartItem != null)
					{
						_context.Carts.Remove(cartItem);
					}
				}
				else
				{
					var cartItemInDb = await _context.Carts.FindAsync(item.Product.Id);
					cartItemInDb.Quantity = newQty;
				}
			}
			await _context.SaveChangesAsync();
			return Ok();
		}

		// POST: api/Carts
		// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
		[HttpPost]
		public async Task<ApiResult> PostCart(CartItemCreateDTO cartItemCreateDTO)
		{
			if (_context.Carts == null)
			{
				return ApiResult.Fail("Entity set 'AppDbContext.Carts'  is null.");
			}
			var account = HttpContext.User.FindFirstValue(ClaimTypes.Name);
			var user = await _context.Members.FirstOrDefaultAsync(m => m.Account == account);
			Cart cart = new Cart
			{
				MemberId = user.Id,
				ProductId = cartItemCreateDTO.ProductId,
				Quantity = cartItemCreateDTO.Qty
			};

			try
			{
				await _context.Carts.AddAsync(cart);
				await _context.SaveChangesAsync();
			}
			catch (Exception ex)
			{
				return ApiResult.Fail("加入購物車失敗！");
			}

			return ApiResult.Success("已成功加入購物車！");
		}

		// DELETE: api/Carts/5
		[HttpDelete("{id}")]
		public async Task<IActionResult> DeleteCart(int id)
		{
			if (_context.Carts == null)
			{
				return NotFound();
			}
			var cart = await _context.Carts.FindAsync(id);
			if (cart == null)
			{
				return NotFound();
			}

			_context.Carts.Remove(cart);
			await _context.SaveChangesAsync();

			return NoContent();
		}

		private bool CartExists(int id)
		{
			return (_context.Carts?.Any(e => e.Id == id)).GetValueOrDefault();
		}
	}
}
