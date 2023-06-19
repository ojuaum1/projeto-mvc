using Microsoft.AspNetCore.Mvc;
using ProjetoGamer_MVC.Infra;
using ProjetoGamer_MVC.Models;



namespace ProjetoGamer_MVC.Controllers
{
    [Route("[controller]")]
    public class LoginController : Controller
    {
        private readonly ILogger<LoginController> _logger;

        public LoginController(ILogger<LoginController> logger)
        {
            _logger = logger;
        }
        
        [TempData]
        public string message {get;set;}
       
       
       
        Context c = new Context();

        [Route("Login")]
        public IActionResult index()
        {
            ViewBag.UserName = HttpContext.Session.GetString("UserName");
            return View();
        }

        [Route("Logar")]
        public IActionResult Logar(IFormCollection form)
        {
            string email = form["Email"].ToString();
            string Senha = form["Senha"].ToString();

            Jogador JogadorBuscado = c.Jogador.FirstOrDefault(J => J.Email == email && J.Senha == Senha);

            //Aqui precisamos implementa a secao
            
            if (JogadorBuscado != null)
            {
                HttpContext.Session.SetString("UserName",JogadorBuscado.Nome);
                return LocalRedirect("~/");
            }

            message = "dados invalidos!";
            return LocalRedirect("~/Login/Login");
        }



        [Route("Logout")]
        public IActionResult Logout()
        {
            HttpContext.Session.Remove("UserName");
            return LocalRedirect("~/");
        }







        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View("Error!");
        }


    }
}