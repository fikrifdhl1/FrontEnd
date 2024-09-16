using BackendService.Models.DTO;
using FrontEnd.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using WebApplication1.Controllers;
using WebApplication1.Services;

namespace FrontEnd.Controllers
{
    public class CartController : BaseController
    {
        private readonly ICartService _cartService;
        private readonly IAccountService _accountService;
        private readonly ITransactionService _transactionService;

        public CartController(ICartService cartService, ITransactionService transactionService,IAccountService accountService)
        {
            _cartService = cartService;
            _transactionService = transactionService;
            _accountService = accountService;
        }

        public async Task<IActionResult> Index()
        {
            var response = await _cartService.GetAllCartsAsync();
            if (response.Code == 200)
            {
                return View(response.Data);
            }
            TempData["ErrorMessage"] = response.Message;
            return RedirectToAction("Index", "Product");
        }

        public async Task<IActionResult> Details(int id)
        {
            var response = await _cartService.GetCartByIdAsync(id);
            if (response.Code == 200)
            {
                return View(response.Data);
            }
            TempData["ErrorMessage"] = response.ErrorDetails.ToString();
            return RedirectToAction("Index");
        }


        public async Task<IActionResult> Create()
        {
            var users = await _accountService.GetUsers();
            return View(users.Data);
        }

        [HttpPost]
        public async Task<IActionResult> Create(int userId)
        {
            var users = await _accountService.GetUsers();
            if (ModelState.IsValid)
            {
                var cart = new CreateCartDTO
                {
                    UserId = userId
                };
                var response = await _cartService.CreateCartAsync(cart);
                if (response.Code == 200)
                {
                    return RedirectToAction("Index");
                }
                TempData["ErrorMessage"] = response.ErrorDetails.ToString();
            }
            return View(users.Data);
        }


        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var response = await _cartService.DeleteCartAsync(id);
            if (response.Code == 200)
            {
                return RedirectToAction("Index");
            }
            TempData["ErrorMessage"] = response.ErrorDetails.ToString();
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> AddItemToCart([FromForm] CreateCartItemDTO cartItem)
        {
            var response = await _cartService.AddItemToCartAsync(cartItem);
            if (response.Code == 200)
            {
                return RedirectToAction("Details", new { id = cartItem.CartId });
            }
            TempData["ErrorMessage"] = response.ErrorDetails.ToString();
            return RedirectToAction("Details", new { id = cartItem.CartId });
        }

        [HttpGet("{cartId}/items/{id}/edit")]
        public async Task<IActionResult> EditCartItem(int cartId, int id)
        {
            var response = await _cartService.GetCartByIdAsync(cartId);
            var data = response.Data.Items.Where(x => x.Id == id).FirstOrDefault();
            if (response.Code == 200)
            {
                ViewData["CartId"] = cartId;
                return View(data);
            }
            TempData["ErrorMessage"] = response.ErrorDetails.ToString();
            return RedirectToAction("Details", new { id = cartId });
        }

        [HttpPost]
        public async Task<IActionResult> UpdateCartItem([FromForm] UpdateCartItemDTO cartItem)
        {
            var response = await _cartService.UpdateCartItemAsync(cartItem);
            if (response.Code == 200)
            {
                return RedirectToAction("Details", new { id = cartItem.CartId });
            }
            TempData["ErrorMessage"] = response.ErrorDetails.ToString();
            return RedirectToAction("EditCartItem", new { cartId = cartItem.CartId, id = cartItem.Id });
        }

        [HttpPost("{cartId}/items/{id}/delete")]
        public async Task<IActionResult> DeleteCartItem(int cartId, int id)
        {
            var response = await _cartService.UpdateCartItemAsync(new UpdateCartItemDTO
            {
                CartId = cartId,
                Id= id,
                Quantity = 0,
            });
            if (response.Code == 200)
            {
                return RedirectToAction("Details", new { id = cartId });
            }
            TempData["ErrorMessage"] = response.ErrorDetails.ToString();
            return RedirectToAction("Details", new { id = cartId });
        }

        [HttpPost]
        public async Task<IActionResult> Checkout(int id)
        {
            var cart = await _cartService.GetCartByIdAsync(id);
            if (cart.Code == 200)
            {
                var response = await _transactionService.CreateTransactionAsync(new CreateTransactionDTO
                {
                    CartId = id,
                    UserId = cart.Data.UserId,
                });
                if (response.Code == 200)
                {
                    return RedirectToAction("Index");
                }
                TempData["ErrorMessage"] = response.ErrorDetails.ToString();
                return RedirectToAction("Index");
            }
            TempData["ErrorMessage"] = cart.ErrorDetails.ToString();
            return RedirectToAction("Index");

        }

    }
}
