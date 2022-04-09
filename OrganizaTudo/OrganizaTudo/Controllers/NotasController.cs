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
        public static readonly string baseURL = "https://data.mongodb-api.com/app/application-0-mqvuy/endpoint";

        // Retorna uma lista com todas as notas do usuário
        public async Task<List<Nota>> BuscarNotas(string Token)
        {
            try
            {
                HttpClient client = new HttpClient();
                client.DefaultRequestHeaders.Add("Authorization", Token);
                HttpResponseMessage response = await client.GetAsync($"{baseURL}/Notas");

                if (response != null)
                {
                    if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        var result = await response.Content.ReadAsStringAsync();
                        return JsonConvert.DeserializeObject<List<Nota>>(result);
                    }
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

        // Salva uma nova nota no MongoDB
        public async Task<Response> InserirNota(string Token, Nota nota)
        {
            try
            {
                HttpClient client = new HttpClient();
                client.DefaultRequestHeaders.Add("Authorization", Token);

                var body = JObject.Parse("{ nota: { \"titulo\": \"" + nota.titulo + "\" , \"nota\": \"" + nota.nota + "\" } }");

                var data = new StringContent(JsonConvert.SerializeObject(body), Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PostAsync($"{baseURL}/Nota", data);

                if (response != null)
                {
                    if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        var result = await response.Content.ReadAsStringAsync();
                        return JsonConvert.DeserializeObject<Response>(result);
                    }
                }
                return new Response { error = "Ocorreu um erro, tente novamente mais tarde..." };
            }
            catch (Exception)
            {
                throw;
            }
        }

        // Edita as informações de uma nota já existente
        public async Task<Response> EditarNota(string Token, Nota nota, string notaID)
        {
            try
            {
                HttpClient client = new HttpClient();
                client.DefaultRequestHeaders.Add("Authorization", Token);
                var data = new StringContent(JsonConvert.SerializeObject(new { notaNova = nota }), Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PutAsync($"{baseURL}/Nota?id={notaID}", data);

                if (response != null)
                {
                    if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        var result = await response.Content.ReadAsStringAsync();
                        return JsonConvert.DeserializeObject<Response>(result);
                    }
                }
                return new Response { error = "Ocorreu um erro, tente novamente mais tarde..." };
            }
            catch (Exception)
            {
                throw;
            }
        }

        // Exclui (permanentemente) uma nota existente
        public async Task<Response> DeletarNota(string Token, string notaID)
        {
            try
            {
                HttpClient client = new HttpClient();
                client.DefaultRequestHeaders.Add("Authorization", Token);
                HttpResponseMessage response = await client.DeleteAsync($"{baseURL}/Nota?id={notaID}");

                if (response != null)
                {
                    if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        var result = await response.Content.ReadAsStringAsync();
                        return JsonConvert.DeserializeObject<Response>(result);
                    }
                }
                return new Response { error = "Ocorreu um erro, tente novamente mais tarde..." };
            }
            catch (Exception)
            {
                throw;
            }
        }

        // Atualiza a privacidade de uma nota (se uma nota publica for atualizada, passará a ser privada)
        public async Task<Response> AtualizarPrivacidadeNota(string Token, string notaID)
        {
            try
            {
                HttpClient client = new HttpClient();
                client.DefaultRequestHeaders.Add("Authorization", Token);
                HttpResponseMessage response = await client.PutAsync($"{baseURL}/AtualizarPrivacidadeNota?id={notaID}", null);

                if (response != null)
                {
                    if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        var result = await response.Content.ReadAsStringAsync();
                        return JsonConvert.DeserializeObject<Response>(result);
                    }
                }
                return new Response { error = "Ocorreu um erro, tente novamente mais tarde..." };
            }
            catch (Exception)
            {
                throw;
            }
        }

    }
}
