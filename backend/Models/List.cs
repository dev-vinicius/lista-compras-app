using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using APIListaCompras.Util;

namespace APIListaCompras.Models
{
    [Table("lists")]
    public class List : GenericModel {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Column("user_id")]
        [Required(ErrorMessage="O campo [Usuário] é obrigatório.")]
        [JsonPropertyName("user_id")]
        public int? UserId { get; set; }

        [Required(ErrorMessage="O campo [Título] é obrigatório.")]
        [Column("title")]
        public string Title { get; set; }

        [Column("subtitle")]
        public string Subtitle { get; set; }

        [Column("active")]
        [Required(ErrorMessage="O campo [Ativo] é obrigatório.")]
        public bool? Active { get; set; }

        [Column("created_at")]
        [JsonPropertyName("created_at")]
        public DateTime CreatedAt { get; set; }

        [Column("updated_at")]
        [JsonPropertyName("updated_at")]
        public DateTime UpdatedAt { get; set; }

        public List<Item> Itens { get; set; }
    }
}
