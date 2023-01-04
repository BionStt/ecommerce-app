using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.Server.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CategoryController : ControllerBase
{
    private readonly ICategoryService _categoryService;

    public CategoryController(ICategoryService categoryService)
    {
        _categoryService = categoryService;
    }

    [HttpGet]
    public async Task<ActionResult<ServiceResponse<List<Category>>>> GetCategories()
    {
        var result = await _categoryService.GetCategories();
        return Ok(result);
    }

    [HttpGet("admin"), Authorize(Roles = "Admin")]
    public async Task<ActionResult<ServiceResponse<List<Category>>>> GetAdminCategories()
    {
        var result = await _categoryService.GetAdminCategories();
        return Ok(result);
    }

    [HttpDelete("admin"), Authorize(Roles = "Admin")]
    public async Task<ActionResult<ServiceResponse<List<Category>>>> DeleteCategory(int categoryId)
    {
        var result = await _categoryService.DeleteCategory(categoryId);
        return Ok(result);
    }

    [HttpPost("admin"), Authorize(Roles = "Admin")]
    public async Task<ActionResult<ServiceResponse<List<Category>>>> AddCategory()
    {
        var result = await _categoryService.AddCategories();
        return Ok(result);
    }
}
