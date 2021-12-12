using Newtonsoft.Json;
using RestSharp;
using RestSharp.Authenticators;
using System;
using System.Collections.Generic;
using System.Text;
using OrganizaTudo.Models;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

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

        public bool InserirNota(string Token, Nota nota)
        {
            try
            {
                RestClient client = new RestClient($"{baseURL}/inserirNota");
                RestRequest request = new RestRequest(Method.POST);
                request.AddHeader("Authorization", Token);
                request.AddParameter("application/json; charset=utf-8", JObject.Parse("{ nota: { \"titulo\": \"" + nota.titulo + "\" , \"nota\": \"" + nota.nota + "\" } }"), ParameterType.RequestBody);

                IRestResponse response = client.Execute<object>(request);

                if (response.IsSuccessful && response.Content.Equals("200")) return true;
                return false;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool EditarNota(string Token, Nota nota, string notaID)
        {
            try
            {
                RestClient client = new RestClient($"{baseURL}/editarNota");
                RestRequest request = new RestRequest(Method.POST);
                request.AddHeader("Authorization", Token);
                request.AddParameter("application/json; charset=utf-8", JObject.Parse("{ notaID: \"" + notaID + "\" , notaNova: { \"titulo\": \"" + nota.titulo + "\" , \"nota\": \"" + nota.nota + "\" } }"), ParameterType.RequestBody);

                IRestResponse response = client.Execute<object>(request);

                if (response.IsSuccessful && response.Content.Equals("200")) return true;
                return false;
            }
            catch (Exception)
            {
                throw;
            }
        }

        // Pesquisar Nota por ID
        // Pesquisar Notas por Título
        // Buscar Notas
        // Editar Nota
        // Atualizar Privacidade da Nota
        // Deletar Nota

    }

}
