using Microsoft.AspNetCore.Mvc;

namespace CRMUNI.UI.Api.Controllers;

public class UsuarioController : Controller
{
    // GET
    public IActionResult Index()
    {
        return View();
    }
}