using System;
using System.Collections.Generic;
using System.Text;

namespace ComandaWeb.Model
{
    public class ComandaItem
    {
        public int Id { get; set; }
        public int IdComanda { get; set; }
        public int IdItem { get; set; }
        public Comanda Comanda { get; set; }
        public Item Item { get; set; }
    }
}
