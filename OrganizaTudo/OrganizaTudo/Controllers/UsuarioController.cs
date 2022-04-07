using System;
using OrganizaTudo.Models;
using System.Threading.Tasks;
using System.Net.Http;
using Newtonsoft.Json;
using System.Text;

namespace OrganizaTudo.Controllers
{
    public class UsuarioController
    {
        // URL Base da API (MongoDB)
        private static readonly string baseURL = "https://webhooks.mongodb-realm.com/api/client/v2.0/app/organiza-tudo-luhho/service/API/incoming_webhook";

        // Realiza o login do usuário
        public async static Task<Sessao> Login(string apelido, string senha)
        {
            try
            {
                HttpClient client = new HttpClient();
                var data = new StringContent(JsonConvert.SerializeObject(new { apelido, senha }), Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PostAsync($"{baseURL}/Login", data);

                if (response != null && response.IsSuccessStatusCode)
                {
                    Sessao sessao = JsonConvert.DeserializeObject<Sessao>(await response.Content.ReadAsStringAsync());
                    sessao.senha = senha;
                    return sessao;
                }
                return null;
            }
            catch (Exception)
            {
                throw;
            }
        }

        // Realiza a criação de conta, para que o usuário acesse o app
        public async static Task<string> CriarConta(string apelido, string email, string senha)
        {
            try
            {
                HttpClient client = new HttpClient();
                var data = new StringContent(JsonConvert.SerializeObject(new { apelido, email, senha }), Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PostAsync($"{baseURL}/criarConta", data);

                if (response != null && response.IsSuccessStatusCode)
                {
                    string status = await response.Content.ReadAsStringAsync();
                    status = status.Replace("\"", "");

                    switch (status)
                    {
                        case "400.1":
                            return "Apelido em uso!";
                        case "400.2":
                            return "Email em uso!";
                        case "200":
                            return null;
                        default:
                            return "Ocorreu um erro, tente novamente mais tarde...";
                    }
                }
                return null;
            }
            catch (Exception)
            {
                throw;
            }
        }

    }
}
