using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using OrganizaTudo.Models;
using System.Threading.Tasks;
using System.Net.Http;
using System.Text;
using Newtonsoft.Json.Linq;
using System.Linq;

namespace OrganizaTudo.Controllers
{
    public class NotasController
    {
        // URL Base da API (MongoDB)
        public static readonly string baseURL = "https://webhooks.mongodb-realm.com/api/client/v2.0/app/organiza-tudo-luhho/service/API/incoming_webhook";

        // Retorna uma lista com todas as notas do usuário
        public async Task<List<Nota>> BuscarNotas(string Token)
        {
            try
            {
                HttpClient client = new HttpClient();
                client.DefaultRequestHeaders.Add("Authorization", Token);
                HttpResponseMessage response = await client.PostAsync($"{baseURL}/buscarNotas", null);

                if (response != null)
                {
                    List<Nota> notas = JsonConvert.DeserializeObject<List<Nota>>(await response.Content.ReadAsStringAsync(), new JsonSerializerSettings
                    {
                        NullValueHandling = NullValueHandling.Ignore,
                        MissingMemberHandling = MissingMemberHandling.Ignore
                    });
                    return notas;
                }
                return null;
            }
            catch (Exception)
            {
                throw;
            }
        }

        // Realiza uma filtragem na notas do usuário através do título (não acessa a API)
        public List<Nota> PesquisarNotas(List<Nota> notas, string regex)
        {
            try
            {
                IEnumerable<Nota> filtro = notas.Where(x => x.titulo.ToLower().Contains(regex.ToLower()));
                return filtro.ToList();
            }
            catch (Exception)
            {
                throw;
            }
        }

        // Salva uma nova nova no MongoDB
        public async Task<bool> InserirNota(string Token, Nota nota)
        {
            try
            {
                HttpClient client = new HttpClient();
                client.DefaultRequestHeaders.Add("Authorization", Token);

                var body = JObject.Parse("{ nota: { \"titulo\": \"" + nota.titulo + "\" , \"nota\": \"" + nota.nota + "\" } }");

                var data = new StringContent(JsonConvert.SerializeObject(body), Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PostAsync($"{baseURL}/inserirNota", data);

                if (response != null)
                {
                    string result = await response.Content.ReadAsStringAsync();
                    if (result.Equals("\"200\"")) return true;
                }
                return false;
            }
            catch (Exception)
            {
                throw;
            }
        }

        // Edita as informações de uma nota já existente
        public async Task<bool> EditarNota(string Token, Nota nota, string notaID)
        {
            try
            {
                HttpClient client = new HttpClient();
                client.DefaultRequestHeaders.Add("Authorization", Token);
                var data = new StringContent(JsonConvert.SerializeObject(new { notaID, notaNova = nota }), Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PostAsync($"{baseURL}/editarNota", data);

                if (response != null)
                {
                    string result = await response.Content.ReadAsStringAsync();
                    if (result.Equals("\"200\"")) return true;
                }
                return false;
            }
            catch (Exception)
            {
                throw;
            }
        }

        // Exclui (permanentemente) uma nota existente
        public async Task<bool> DeletarNota(string Token, string notaID)
        {
            try
            {
                HttpClient client = new HttpClient();
                client.DefaultRequestHeaders.Add("Authorization", Token);
                var data = new StringContent(JsonConvert.SerializeObject(new { notaID }), Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PostAsync($"{baseURL}/deletarNota", data);

                if (response != null)
                {
                    string result = await response.Content.ReadAsStringAsync();
                    if (result.Equals("\"200\"")) return true;
                }
                return false;
            }
            catch (Exception)
            {
                throw;
            }
        }

        // Atualiza a privacidade de uma nota (se uma nota publica for atualizada, passará a ser privada)
        public async Task<bool> AtualizarPrivacidadeNota(string Token, string notaID, bool publica)
        {
            try
            {
                bool privacidade = !publica;

                HttpClient client = new HttpClient();
                client.DefaultRequestHeaders.Add("Authorization", Token);
                var data = new StringContent(JsonConvert.SerializeObject(new { notaID, privacidade }), Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PostAsync($"{baseURL}/atualizarPrivacidadeNota", data);

                if (response != null)
                {
                    string result = await response.Content.ReadAsStringAsync();
                    if (result.Equals("\"200\"")) return true;
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
