using System;
using System.Collections.Generic;
using System.Text;
using OrganizaTudo.Code;
using OrganizaTudo.Models;

namespace OrganizaTudo.Controllers
{
    public class UsuarioController
    {
        private static API api = new API();

        public static string Login(string apelido, string senha)
        {
            try
            {
                Usuario usuario = new Usuario() { apelido = apelido, senha = senha };
                return api.Login(usuario).Token;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
