using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace OrganizaTudo.Models
{
    [Table("Sessao")]
    public class Sessao : Response
    {
        [PrimaryKey]
        public int ID { get; set; }
        public string apelido { get; set; }
        public string senha { get; set; }
        public string email { get; set; }
        public string token { get; set; }
        public bool manter { get; set; }
    }
}
