using Microsoft.AspNetCore.Mvc;
using ProjetoGamer_MVC.Infra;
using ProjetoGamer_MVC.Models;

namespace ProjetoGamer_MVC.Controllers
{
    [Route("[controller]")]
    public class EquipeController : Controller
    {
        private readonly ILogger<EquipeController> _logger;

        public EquipeController(ILogger<EquipeController> logger)
        {
            _logger = logger;
        }

        //INSTANCIA DO CONTEXTO PARA ACESSAR O BANCO DE DADOS
        Context c = new Context();

        [Route("Listar")] //http://localhost/Equipe/Listar:
        public IActionResult Index()
        {
            ViewBag.UserName = HttpContext.Session.GetString("UserName");
            //variavel que armazena as equipes listadas do banco de dados
            ViewBag.Equipe = c.Equipe.ToList();
            //retorna a view de equipe(TELA)
            return View();
        }

        [Route("Cadastrar")]
        public IActionResult Cadastrar(IFormCollection form)
        {
            //instancia do objeto equipe
            Equipe novaEquipe = new Equipe();

            //atribuição de valores recebidos do formulário
            novaEquipe.Nome = form["Nome"].ToString();

            //aqui estava chegando como string
            // novaEquipe.Imagem = form["Imagem"].ToString();


            //início da lógica de upload da imagem
            if (form.Files.Count > 0)
            {

                var file = form.Files[0];

                var folder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/img/Equipes");

                if (!Directory.Exists(folder))
                {
                    Directory.CreateDirectory(folder);
                }

                //gera o caminho completo até o caminho do arquivo(imagem - nome da extensão)
                var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwoot/img/", folder, file.FileName);

                using (var stream = new FileStream(path, FileMode.Create))
                {
                    file.CopyTo(stream);
                }

                novaEquipe.Imagem = file.FileName;

            }
            else
            {
                novaEquipe.Imagem = "padrao.png";
            }
            //fim da lógica de upload

            //adiciona objeto na tabela do BD
            c.Equipe.Add(novaEquipe);

            //salva as alterações feitas do BD
            c.SaveChanges();

            //Retorna para o local chamando a rota de listar(método index)
            return LocalRedirect("~/Equipe/Listar");
        }

        [Route("Excluir/{id}")]
        public IActionResult Excluir(int id)
        {
            Equipe equipeBuscada = c.Equipe.First(e => e.IdEquipe == id);

            c.Remove(equipeBuscada);

            c.SaveChanges();

            return LocalRedirect("~/Equipe/Listar");
        }


        [Route("Editar/{id}")]
        public IActionResult Editar(int id)
        {
            ViewBag.UserName = HttpContext.Session.GetString("UserName");
            Equipe equipe = c.Equipe.First(x => x.IdEquipe == id);

            ViewBag.Equipe = equipe;

            return View("Edit");
        }

        [Route("Atualizar")]
        public IActionResult Atualizar(IFormCollection form)
        {
            Equipe equipe = new Equipe();

            equipe.IdEquipe = int.Parse(form["IdEquipe"].ToString());

            equipe.Nome = form["Nome"].ToString();

            if (form.Files.Count > 0)
            {

                var file = form.Files[0];

                var folder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/img/Equipes");

                if (!Directory.Exists(folder))
                {
                    Directory.CreateDirectory(folder);
                }

                //gera o caminho completo até o caminho do arquivo(imagem - nome da extensão)
                var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwoot/img/", folder, file.FileName);

                using (var stream = new FileStream(path, FileMode.Create))
                {
                    file.CopyTo(stream);
                }

                equipe.Imagem = file.FileName;

            }
            else
            {
                equipe.Imagem = "padrao.png";
            }

           Equipe equipeBuscada = c.Equipe.First(x => x.IdEquipe == equipe.IdEquipe);

           equipeBuscada.Nome = equipe.Nome;
           equipeBuscada.Imagem = equipe.Imagem;

           c.Equipe.Update(equipeBuscada);

           c.SaveChanges();
           
           return LocalRedirect("~/Equipe/Listar");
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View("Error!");
        }
    }
}