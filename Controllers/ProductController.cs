using BackendService.Models.DTO;
using FrontEnd.Services;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Controllers;

public class ProductController : BaseController
{
    private readonly IProductService _productService;

    public ProductController(IProductService productService)
    {
        _productService = productService;
    }

    public async Task<IActionResult> Index()
    {
        var response = await _productService.GetProductsAsync();
        if (response.Code != 200)
        {
            TempData["ErrorMessage"] = response.ErrorDetails.ToString();
            return RedirectToAction(nameof(Index));
        }
        return View(response.Data);
    }

    public async Task<IActionResult> Details(int id)
    {
        var response = await _productService.GetProductByIdAsync(id);
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
    public async Task<IActionResult> Create(CreateProductDTO product)
    {
        if (ModelState.IsValid)
        {
            var response = await _productService.CreateProductAsync(product);
            if (response.Code != 200)
            {
                TempData["ErrorMessage"] = response.ErrorDetails.ToString();
                return RedirectToAction(nameof(Index));
            }
            return RedirectToAction(nameof(Index));
        }
        return View(product);
    }

    public async Task<IActionResult> Edit(int id)
    {
        var product = await _productService.GetProductByIdAsync(id);
        if (product.Code != 200)
        {
            TempData["ErrorMessage"] = product.ErrorDetails.ToString();
            return RedirectToAction(nameof(Index));
        }
        return View(new UpdateProductDTO
        {
            Id= product.Data.Id,
            Description= product.Data.Description,
            Name= product.Data.Name,
            Price= product.Data.Price,
            Stock = product.Data.Stock
        });
    }

    [HttpPost]
    public async Task<IActionResult> Edit(int id, UpdateProductDTO product)
    {
        if (ModelState.IsValid)
        {
            var response = await _productService.UpdateProductAsync(id, product);
            if (response.Code != 200)
            {
                TempData["ErrorMessage"] = response.ErrorDetails.ToString(); ;
                return RedirectToAction(nameof(Index));
            }
            return RedirectToAction(nameof(Index));
        }
        return View(product);
    }


    public async Task<IActionResult> Delete(int id)
    {
        var product = await _productService.GetProductByIdAsync(id);
        if (product.Code != 200)
        {
            TempData["ErrorMessage"] = product.ErrorDetails.ToString();
            return RedirectToAction(nameof(Index));
        }
        return View(product.Data);
    }

    [HttpPost, ActionName("DeleteConfirmed")]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var response = await _productService.DeleteProductAsync(id);
        if (response.Code != 200)
        {
            TempData["ErrorMessage"] = response.ErrorDetails.ToString();
        }
        return RedirectToAction(nameof(Index));
    }


}
