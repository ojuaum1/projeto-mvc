using Microsoft.AspNetCore.Mvc;
using ProjetoGamer_MVC.Infra;
using ProjetoGamer_MVC.Models;


namespace projeto_gamer_manha.Controllers
{
    [Route("[controller]")]
    public class JogadorController : Controller
    {
        private readonly ILogger<JogadorController> _logger;

        public JogadorController(ILogger<JogadorController> logger)
        {
            _logger = logger;
        }

        Context c = new Context();

        [Route("Listar")]
        public IActionResult Index()
        {
            ViewBag.UserName = HttpContext.Session.GetString("UserName");
            ViewBag.Jogador = c.Jogador.ToList();
            ViewBag.Equipe = c.Equipe.ToList();


            return View();
        }


        [Route("cadastrar")]
        public IActionResult Cadastrar(IFormCollection form)
        {
            Jogador novoJogador = new Jogador();

            novoJogador.Nome = form["Nome"].ToString();
            novoJogador.Email = form["Email"].ToString();
            novoJogador.Senha = form["Senha"].ToString();
            novoJogador.IdEquipe = int.Parse(form["Equipe"].ToString());

            c.Jogador.Add(novoJogador);
            c.SaveChanges();


            return LocalRedirect("~/Jogador/Listar");
        }

      [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View("Error!");
        }

        [Route("Excluir/{id}")]
        public IActionResult Excluir(int id)
        {
            Jogador jogador = c.Jogador.First(j => j.IdJogador == id);

            c.Jogador.Remove(jogador);
            c.SaveChanges();

            return LocalRedirect("~/jogador/Listar");
        }
        [Route("Editar/{id}")]
        public IActionResult Editar(int id)
        {
            
            ViewBag.UserName = HttpContext.Session.GetString("UserName");
            Jogador jogador = c.Jogador.First(j => j.IdJogador == id);

            ViewBag.Jogador = jogador;
            ViewBag.Equipe = c.Equipe;

            return View ("Edit");
        }

        [Route("Atualizar")]
        public IActionResult Atualizar(IFormCollection form)
        {
            Jogador novoJogador = new Jogador();
            
            novoJogador.Nome = form["Nome"].ToString();
            novoJogador.Email = form["Email"].ToString();
            novoJogador.Senha = form["Senha"].ToString();
            novoJogador.IdEquipe = int.Parse(form["Idequipe"].ToString());
            novoJogador.IdJogador= int.Parse(form["IdJogador"].ToString());

           Jogador jogadorBuscado = c.Jogador.First(J => J.IdJogador == novoJogador.IdJogador);

           jogadorBuscado.Nome = novoJogador.Nome;
           jogadorBuscado.Email= novoJogador.Email;
           jogadorBuscado.Senha = novoJogador.Senha;
           jogadorBuscado.IdJogador = novoJogador.IdJogador;
           jogadorBuscado.IdEquipe = novoJogador.IdEquipe;

            
            c.Jogador.Update(jogadorBuscado);
            c.SaveChanges();

            return LocalRedirect("~/jogador/Listar");
        }
    }
}
