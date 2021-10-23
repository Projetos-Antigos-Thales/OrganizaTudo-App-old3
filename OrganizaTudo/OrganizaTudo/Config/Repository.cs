using SQLite;
using System.Collections.Generic;
using System.Threading.Tasks;
using OrganizaTudo.Models;

namespace OrganizaTudo.Config
{
    public class Repository
    {
        static SQLiteAsyncConnection Database;

        // Conexão
        public static readonly Conexao<Repository> Instance = new Conexao<Repository>(async () =>
        {
            var instance = new Repository();
            CreateTableResult result = await Database.CreateTableAsync<Sessao>();
            return instance;
        });

        public Repository()
        {
            Database = new SQLiteAsyncConnection(Constantes.DatabasePath, Constantes.Flags);
        }

        // Repositório
        public Task<List<Sessao>> GET()
        {
            return Database.Table<Sessao>().ToListAsync();
        }

        public Task<List<Sessao>> QUERY()
        {
            // SQL queries are also possible
            return Database.QueryAsync<Sessao>("SELECT * FROM Sessao");
        }

        public Task<Sessao> GET(int id)
        {
            return Database.Table<Sessao>().Where(i => i.ID == id).FirstOrDefaultAsync();
        }

        public Task<int> SAVE(Sessao item, int registros)
        {
            if (registros == 1)
            {
                return Database.UpdateAsync(item);
            }
            else
            {
                return Database.InsertAsync(item);
            }
        }

        public Task<int> DELETE(Sessao item)
        {
            return Database.DeleteAsync(item);
        }
    }
}
