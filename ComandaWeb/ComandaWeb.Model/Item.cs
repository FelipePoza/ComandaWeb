using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ComandaWeb.Model
{
    
    public class Item
    {
        public int Id { get; set; }
        public string Descricao { get; set; }
        public decimal Valor { get; set; }
        public ICollection<ComandaItem> Itens { get; set; }
    }
}
