using Newtonsoft.Json;
using RestSharp;
using RestSharp.Authenticators;
using System;
using System.Collections.Generic;
using System.Text;
using OrganizaTudo.Models;
using System.Threading.Tasks;

namespace OrganizaTudo.Controllers
{
    public class NotasController
    {
        public static readonly string baseURL = "https://webhooks.mongodb-realm.com/api/client/v2.0/app/organiza-tudo-luhho/service/API/incoming_webhook";

        public async Task<List<Nota>> BuscarNotas(string Token)
        {
            try
            {
                RestClient client = new RestClient($"{baseURL}/buscarNotas");
                RestRequest request = new RestRequest(Method.POST);
                request.AddHeader("Authorization", Token);
                //request.AddParameter("Authorization", Token, ParameterType.HttpHeader);

                IRestResponse response = client.Execute<object>(request);
                if (response.IsSuccessful)
                {
                    List<Nota> notas = JsonConvert.DeserializeObject<List<Nota>>(response.Content, new JsonSerializerSettings
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

        // Inserir Nota
        // Pesquisar Nota por ID
        // Pesquisar Notas por Título
        // Buscar Notas
        // Editar Nota
        // Atualizar Privacidade da Nota
        // Deletar Nota

    }
}
