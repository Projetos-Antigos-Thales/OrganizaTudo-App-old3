using System;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace OrganizaTudo.Config
{
    public class Conexao<T>
    {
        readonly Lazy<Task<T>> instance;

        public Conexao(Func<T> factory)
        {
            instance = new Lazy<Task<T>>(() => Task.Run(factory));
        }

        public Conexao(Func<Task<T>> factory)
        {
            instance = new Lazy<Task<T>>(() => Task.Run(factory));
        }

        public TaskAwaiter<T> GetAwaiter()
        {
            return instance.Value.GetAwaiter();
        }
    }
}
