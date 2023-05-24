using Microsoft.AspNetCore.Mvc;
using Shopping.Application.Interfaces;

namespace ProductWebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ApiBaseController : ControllerBase
{
    protected readonly IWebHostEnvironment _hostEnviroment;
    protected readonly IJwtService _jwtService;
    protected readonly IUserTokenService _userTokenService;
    protected readonly IConfiguration _configuration;
    protected readonly ICategoryService _categoryService;
    protected readonly IPermissionService _permissionService;
    protected readonly IRoleService _roleService;
    protected readonly IRolePermissionService _rolePermissionService;
    protected readonly IOrderRepository _orderRepository;
    protected readonly IUserService _userService;
    protected readonly IProductService _productService;

    
    public ApiBaseController
    (
        IWebHostEnvironment hostEnviroment,
        IJwtService jwtService ,
        IUserTokenService userTokenService,
        IConfiguration configuration,
        ICategoryService categoryService,
        IPermissionService permissionService,
        IRoleService roleService,
        IRolePermissionService rolePermissionService,
        IOrderRepository orderRepository,
        IUserService userService,
        IProductService productService
    )
    {
        _categoryService = categoryService;
        _permissionService = permissionService;
        _roleService = roleService;
        _orderRepository = orderRepository;
        _userService = userService;
        _productService = productService;
        _rolePermissionService = rolePermissionService;
        _orderRepository = orderRepository;
        _hostEnviroment = hostEnviroment;
        _jwtService = jwtService;
        _userTokenService = userTokenService;
        _configuration = configuration;

    }






}

