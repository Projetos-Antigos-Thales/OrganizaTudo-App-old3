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
        public static async Task<Sessao> BuscarSessaoAsync()
        {
            Repository db = await Repository.Instance;
            // Busca a sessao salva no SQLite
            return await db.GET(1);
        }

        public static async Task IniciarSessaoAsync(Sessao sessao)
        {
            Repository db = await Repository.Instance;
            int registros = db.GET().Result.Count;
            sessao.ID = 1;
            // Salvar a sessao no SQLite
            await db.SAVE(sessao, registros);
        }

        public static async void FinalizarSessaoAsync(Sessao sessao)
        {
            Repository db = await Repository.Instance;
            // Finaliza a sessao no SQLite
            await db.DELETE(sessao);
        }

    }
}
