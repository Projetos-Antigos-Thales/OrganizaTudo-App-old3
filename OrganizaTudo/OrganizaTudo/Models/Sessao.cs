using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace OrganizaTudo.Models
{
    [Table("Sessao")]
    public class Sessao
    {
        [PrimaryKey]
        public int ID { get; set; }
        public string apelido { get; set; }
        public string token { get; set; }
        public bool manter { get; set; }
    }
}
