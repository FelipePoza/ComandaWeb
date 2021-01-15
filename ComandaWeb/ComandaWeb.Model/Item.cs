using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ComandaWeb.Model
{
    
    public class Item
    {
        public int Id { get; set; }
        [Required]
        [StringLength(250)]
        public string Descricao { get; set; }
        [Required]
        [Column(TypeName = "decimal(10,3)")]
        public decimal Valor { get; set; }
        public ICollection<ComandaItem> Itens { get; set; }
    }
}
