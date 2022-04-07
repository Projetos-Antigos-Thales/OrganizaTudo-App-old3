using OrganizaTudo.Config;
using OrganizaTudo.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace OrganizaTudo.Controllers
{
    public class SessaoController
    {
        // Busca a sessao salva no SQLite
        public static async Task<Sessao> BuscarSessaoAsync()
        {
            Repository db = await Repository.Instance;
            Sessao sessaoAtual = await db.GET(1);
            return sessaoAtual;
        }

        // Salva a sessao no SQLite
        public static async Task IniciarSessaoAsync(Sessao sessao)
        {
            Repository db = await Repository.Instance;
            int registros = db.GET().Result.Count;
            sessao.ID = 1;
            await db.SAVE(sessao, registros);
        }

        // Finaliza (exclui) a sessao no SQLite
        public static async void FinalizarSessaoAsync()
        {
            Repository db = await Repository.Instance;
            await db.DELETE(await BuscarSessaoAsync());
        }

    }
}
