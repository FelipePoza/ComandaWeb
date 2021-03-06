﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ComandaWeb.Model
{
    public class Comanda
    {
        public int Id { get; set; }
        [Required]
        [Range(1,9999)]
        public int Codigo { get; set; }
        
        public ICollection<ComandaItem> Itens { get; set; }
    }
    public class ComandaApi
    {
        public int Id { get; set; }
        public int Codigo { get; set; }
    }
}
