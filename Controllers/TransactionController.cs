using BackendService.Models.DTO;
using FrontEnd.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace WebApplication1.Controllers
{
    public class TransactionController : BaseController
    {
        private readonly ITransactionService _transactionService;

        public TransactionController(ITransactionService transactionService)
        {
            _transactionService = transactionService;
        }

        public async Task<IActionResult> Index()
        {
            var response = await _transactionService.GetTransactionsAsync();
            if (response.Code != 200)
            {
                TempData["ErrorMessage"] = response.ErrorDetails.ToString();
                return RedirectToAction(nameof(Index));
            }
            return View(response.Data);
        }

        public async Task<IActionResult> Details(int id)
        {
            var response = await _transactionService.GetTransactionByIdAsync(id);
            if (response.Code != 200)
            {
                TempData["ErrorMessage"] = response.ErrorDetails.ToString();
                return RedirectToAction(nameof(Index));
            }
            return View(response.Data);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateTransactionDTO transaction)
        {
            if (ModelState.IsValid)
            {
                var response = await _transactionService.CreateTransactionAsync(transaction);
                if (response.Code != 200)
                {
                    TempData["ErrorMessage"] = response.ErrorDetails.ToString();
                    return RedirectToAction(nameof(Index));
                }
                return RedirectToAction(nameof(Index));
            }
            return View(transaction);
        }
    }
}
