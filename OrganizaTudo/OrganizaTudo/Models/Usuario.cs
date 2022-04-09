using System;
using System.Collections.Generic;
using System.Text;

namespace OrganizaTudo.Models
{
    public class Usuario : Response
    {
        public string apelido { get; set; }
        public string senha { get; set; }
        public string email { get; set; }
    }
}
