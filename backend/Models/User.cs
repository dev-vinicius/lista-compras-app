using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using APIListaCompras.Util;

namespace APIListaCompras.Models
{
    [Table("users")]
    public class User : GenericModel
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Column("name")]
        [Required(ErrorMessage="O campo [Nome] é obrigatório.")]
        [StringLength(100, MinimumLength=3, ErrorMessage = "O campo [Nome] deve conter de 3 a 100 caracteres.")]
        public string Name { get; set; }

        [Column("email")]
        [Required(ErrorMessage="O campo [E-mail] é obrigatório.")]
        [DataType(DataType.EmailAddress, ErrorMessage="O [E-mail] informado não é válido")]
        public string Email { get; set; }

        [Column("password")]
        [Required(ErrorMessage="O campo [Senha] é obrigatório.")]
        [StringLength(20, MinimumLength=3, ErrorMessage = "O campo [Senha] deve conter de 3 a 16 caracteres.")]
        public string Password { get; set; }

        [Column("created_at")]
        [JsonPropertyName("created_at")]
        public DateTime? CreatedAt { get; set; }

        [Column("updated_at")]
        [JsonPropertyName("updated_at")]
        public DateTime? UpdatedAt { get; set; }
    }
}