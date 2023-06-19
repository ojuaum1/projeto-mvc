using System.ComponentModel.DataAnnotations;

namespace ProjetoGamer_MVC.Models
{
    public class Equipe
    {
        [Key] //DATA ANNOTATION - IdEquipe
        public int IdEquipe { get; set; }

        [Required]
        public string Nome { get; set; }
        public string Imagem { get; set; }

        //REFERENCIA QUE A CLASSE EQUIPE VAI TER ACESSO
        // A COLLECTION "JOGADOR"
        public ICollection<Jogador> Jogador {get; set;}
    }
}