using System;
using System.Collections.Generic;
using System.Text;
using RestSharp;
using RestSharp.Authenticators;
using OrganizaTudo.Models;
using Newtonsoft.Json;

namespace OrganizaTudo.Controllers
{
    public class UsuarioController
    {
        public static readonly string baseURL = "https://webhooks.mongodb-realm.com/api/client/v2.0/app/organiza-tudo-luhho/service/API/incoming_webhook";

        public static Sessao Login(string apelido, string senha)
        {
            try
            {
                Usuario usuario = new Usuario() { apelido = apelido, senha = senha };
                RestClient client = new RestClient($"{baseURL}/Login");
                RestRequest request = new RestRequest(Method.POST);
                request.AddParameter("application/json; charset=utf-8", JsonConvert.SerializeObject(usuario), ParameterType.RequestBody);
                // client.Authenticator = new HttpBasicAuthenticator("Autentication", "Bearer password");

                IRestResponse response = client.Execute<object>(request);

                if (response.IsSuccessful && !response.Content.Equals("\"404\"") && !response.Content.Equals("\"500\""))
                {
                    Sessao sessao = JsonConvert.DeserializeObject<Sessao>(response.Content);
                    return sessao;
                }
                return null;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static string CriarConta(string apelido, string email, string senha)
        {
            try
            {
                Usuario usuario = new Usuario() { apelido = apelido, email = email, senha = senha };
                RestClient client = new RestClient($"{baseURL}/criarConta");
                RestRequest request = new RestRequest(Method.POST);
                request.AddParameter("application/json; charset=utf-8", JsonConvert.SerializeObject(usuario), ParameterType.RequestBody);
                // client.Authenticator = new HttpBasicAuthenticator("Autentication", "Bearer password");

                IRestResponse response = client.Execute<object>(request);
                string status = response.Content.Replace("\"","");

                switch (status)
                {
                    case "400.1":
                        return "Apelido em uso!";
                        break;
                    case "400.2":
                        return "Email em uso!";
                        break;
                    case "500":
                        return "Ocorreu um erro, tente novamente mais tarde...";
                        break;
                    default:
                        return null;
                        break;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool request(Usuario usuario)
        {
            try
            {
                RestClient client = new RestClient($"{baseURL}/request");
                RestRequest request = new RestRequest(Method.POST);
                request.AddParameter("application/json; charset=utf-8", JsonConvert.SerializeObject(usuario), ParameterType.RequestBody);
                // client.Authenticator = new HttpBasicAuthenticator("Autentication", "Bearer password");

                IRestResponse response = client.Execute<object>(request);
                if (response.IsSuccessful)
                {
                    Sessao sessao = JsonConvert.DeserializeObject<Sessao>(response.Content, new JsonSerializerSettings
                    {
                        NullValueHandling = NullValueHandling.Ignore,
                        MissingMemberHandling = MissingMemberHandling.Ignore
                    });
                    return true;
                }
                return false;
            }
            catch (Exception)
            {
                throw;
            }
        }

    }
}
