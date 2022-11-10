using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.ModelBinding;

namespace WebSite_FormValidacao.Models
{
    public class QuestionarioModel
    {
        [Required] // requisita esse dado não deixa a gente terminar sem ter esse valor inserido, NÃO PODE FICAR VAZIO
        [DisplayName("Tipo de Projeto")] //nome para ficar mais amigavel para o usuario
        public String TipoProjeto { get; set; }

        [Required(ErrorMessage = "O CEP deve ser informado.")]
        [RegularExpression(@"^\d{8}$|^\d{5}-\d{3}$", ErrorMessage = "O código postal deve ser no formato 00000000 ou 00000-000")]
        [DisplayName("CEP")] /* @"^\d{8}$|^\d{5}-\d{3}$" */
        public String Cep { get; set; }

        [Required(ErrorMessage = "O nome do contato deve ser informado.")]
        [StringLength(50,MinimumLength = 5)]
        //[DisplayName("Contato")]
        public String Contato { get; set; }

        [Required(ErrorMessage = "O email deve ser informado.")]
        [DataType(DataType.EmailAddress)]//para fins de teste a mensagem padrão que ira aparecer é do visual studio
        //[DisplayName("Email")]
        public String Email { get; set; }

        [Required(ErrorMessage = "O numero de telefone/celular deve ser informado.")]
        [DataType(DataType.PhoneNumber,ErrorMessage = "O número de telefone deve ser informado no formato (000) 000-0000")]
        [DisplayName("Número do Telefone")]
        public String Telefone { get; set; }

        [Required(ErrorMessage = "Informe a descrição do projeto.")]
        [StringLength(5000, MinimumLength = 20)] // aqui também pode colocar um erro como errorMessage mas para fins de teste nao colocaremos para ver o que o visua studio vai falar para a gente
        [DisplayName("Descrição do Projeto")]
        [DataType(DataType.MultilineText)]// basicamente da para fazer um texto com varias linhas
        public String Descricao { get; set; }
    }
}