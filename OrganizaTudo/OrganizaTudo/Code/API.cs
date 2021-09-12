using System;
using System.Collections.Generic;
using System.Text;
using RestSharp;
using RestSharp.Authenticators;
using OrganizaTudo.Models;
using Newtonsoft.Json;

namespace OrganizaTudo.Code
{
    public class API
    {

        public readonly string baseURL = "https://webhooks.mongodb-realm.com/api/client/v2.0/app/organiza-tudo-luhho/service/API/incoming_webhook";

        public Sessao Login(Usuario usuario)
        {
            try
            {
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
