﻿using System;
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
        private static readonly string baseURL = "https://data.mongodb-api.com/app/application-0-mqvuy/endpoint";

        // Realiza o login do usuário
        public async static Task<Sessao> Login(string apelido, string senha)
        {
            try
            {
                HttpClient client = new HttpClient();
                HttpContent data = new StringContent(JsonConvert.SerializeObject(new { apelido, senha }), Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PostAsync($"{baseURL}/Login", data);

                if (response != null)
                {
                    string result = await response.Content.ReadAsStringAsync();
                    Sessao login = JsonConvert.DeserializeObject<Sessao>(result);
                    login.senha = senha;
                    return login;
                }

                return new Sessao { error = "Ocorreu um erro, tente novamente mais tarde..." };
            }
            catch (Exception e)
            {
                throw new Exception("Erro ao Efetuar Login", e);
            }
        }

        // Realiza a criação de conta, para que o usuário acesse o app
        public async static Task<Response> CriarConta(string apelido, string email, string senha)
        {
            try
            {
                HttpClient client = new HttpClient();
                var data = new StringContent(JsonConvert.SerializeObject(new { apelido, email, senha }), Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PostAsync($"{baseURL}/CriarConta", data);

                if (response != null)
                {
                    string result = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<Response>(result);
                }
                return new Response { error = "Ocorreu um erro, tente novamente mais tarde..." };
            }
            catch (Exception)
            {
                throw;
            }
        }

        // Edita o Apelido e E-mail do Usuário
        public async Task<Response> AtualizarPerfil(string Token, string apelido, string email, string senha)
        {
            try
            {
                HttpClient client = new HttpClient();
                client.DefaultRequestHeaders.Add("Authorization", Token);
                var data = new StringContent(JsonConvert.SerializeObject(new { dados = new { apelido, email, senha } }), Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PutAsync($"{baseURL}/Perfil", data);

                if (response != null)
                {
                    var result = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<Response>(result);
                }
                return new Response { error = "Ocorreu um erro, tente novamente mais tarde..." };
            }
            catch (Exception)
            {
                throw;
            }
        }

        // Retorna o perfil atualizado
        public async Task<Usuario> BuscarPerfil(string Token)
        {
            try
            {
                HttpClient client = new HttpClient();
                client.DefaultRequestHeaders.Add("Authorization", Token);
                HttpResponseMessage response = await client.GetAsync($"{baseURL}/Perfil");

                if (response != null)
                {
                    if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        var result = await response.Content.ReadAsStringAsync();
                        return JsonConvert.DeserializeObject<Usuario>(result);
                    }
                }
                return new Usuario { error = "Ocorreu um erro, tente novamente mais tarde..." };
            }
            catch (Exception)
            {
                throw;
            }
        }

    }
}
