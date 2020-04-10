using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using APIListaCompras.Util;

namespace APIListaCompras.Models
{
    [Table("itens")]
    public class Item : GenericModel {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Column("list_id")]
        [JsonPropertyName("list_id")]
        [Required(ErrorMessage="A [Lista] deve ser informada.")]
        public int? ListId { get; set; }

        [Column("description")]
        [Required(ErrorMessage="A [Descrição] deve ser informada.")]
        public string Description { get; set; }

        [Column("note")]
        public string Note { get; set; }

        [Column("quantity")]
        [Range(1, int.MaxValue, ErrorMessage="A [Quantidade] tem que ser maior que zero.")]
        public int Quantity { get; set; }

        [Column("price")]
        [Range(double.MinValue, double.MaxValue, ErrorMessage="O [Valor] deve ser maior que zero.")]
        public Decimal Price { get; set; }

        [Column("active")]
        [Required(ErrorMessage="O campo [Ativo] deve ser informado.")]
        public bool? Active { get; set; }

        [Column("created_at")]
        [JsonPropertyName("created_at")]
        public DateTime CreatedAt { get; set; }

        [Column("updated_at")]
        [JsonPropertyName("updated_at")]
        public DateTime UpdatedAt { get; set; }

        [NotMapped]
        public Decimal Total {
            get {
                return (Quantity > Decimal.Zero && Price > Decimal.Zero) ? Quantity * Price : Decimal.Zero;
            }
        }
    }
}
