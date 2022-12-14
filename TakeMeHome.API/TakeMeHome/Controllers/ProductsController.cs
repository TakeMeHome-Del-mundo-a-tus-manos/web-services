using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using TakeMeHome.API.Shared.Extensions;
using TakeMeHome.API.TakeMeHome.Domain.Models;
using TakeMeHome.API.TakeMeHome.Domain.Services;
using TakeMeHome.API.TakeMeHome.Domain.Services.Communication;
using TakeMeHome.API.TakeMeHome.Resources;

namespace TakeMeHome.API.TakeMeHome.Controllers;
//Method Controller for Products
[Route("/api/v1/[controller]")]
public class ProductsController : ControllerBase
{
    private readonly IProductService _productService;
    private readonly IMapper _mapper;

    public ProductsController(IProductService productService, IMapper mapper)
    {
        _productService = productService;
        _mapper = mapper;
    }
    
    [HttpGet]
    public async Task<IEnumerable<ProductResource>> GetAllAsync()
    {
        var products = await _productService.ListAsync();
        var resources = _mapper.Map<IEnumerable<Product>, IEnumerable<ProductResource>>(products);
        return resources;
    }
    
    [HttpGet]
    [Route("{userId}/{statusId}")]
    public async Task<IEnumerable<ProductResource>> GetByOrderIdAndStatusIdAsync(int userId, int statusId)
    {
        var products = await _productService.ListByUserIdAndStatusIdAsync(userId, statusId);
        var resources = _mapper.Map<IEnumerable<Product>, IEnumerable<ProductResource>>(products);
        return resources;
    }
    
    [HttpGet("order/{id}")]
    public async Task<IActionResult> GetByOrderIdAsync(int id)
    {
        var products = await _productService.FindByOrderIdAsync(id);
        var resources = _mapper.Map<Product, ProductResource>(products);
        return Ok(resources);
    }
    
    [HttpPost]
    public async Task<IActionResult> PostAsync([FromBody] SaveProductResource resource)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState.GetErrorMessages());
        var product = _mapper.Map<SaveProductResource, Product>(resource);
        var result = await _productService.SaveAsync(product);
        if (!result.Success)
            return BadRequest(result.Message);
        var productResource = _mapper.Map<Product, ProductResource>(result.Resource);
        return Ok(productResource);
    }
}