using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjetoGamer_MVC.Models
{
    public class Jogador
    {
        [Key] // DATA ANNOTATION - IdJOGADOR
        public int IdJogador { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Senha { get; set; }

        [ForeignKey("Equipe")] // DATA ANNOTATION - IdEquipe
        public int IdEquipe { get; set; }
        public Equipe Equipe { get; set; }
    }
}