using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Programa_Banco
{
    class Usuario
    {
        public string _Nome { get; set; }
        public string _Senha { get; set; }
        public double _Saldo { get; set; }

        public Usuario(string nome, string senha, double saldo)
        {
            _Nome = nome;
            _Senha = senha;
            _Saldo = saldo;
        }
    }

    abstract class Banco
    {
        public List<Usuario> usuariosList = new List<Usuario>();

        public void criarConta(string nome, string senha, double saldo)
        {
            Usuario obj = new Usuario(nome, senha, saldo);
            usuariosList.Add(obj);
        }

        public abstract bool logarConta(string nome, string senha);

        public abstract void operacoes(int opcao);
    }

    class Conta : Banco
    {
        private int idSesao;

        public override bool logarConta(string nome, string senha)
        {
            for (int i = 0; i < usuariosList.Count; i++)
            {
                if (usuariosList[i]._Nome == nome && usuariosList[i]._Senha == senha)
                {
                    idSesao = i;
                    return true;
                }
            }
            return false;
        }

        public override void operacoes(int opcao)
        {
            Operacoes objOpera = new Operacoes();

            if (opcao == 0)
            {
                usuariosList[idSesao]._Saldo = objOpera.inserirValor(usuariosList[idSesao]);
            }
            else if (opcao == 1)
            {
                usuariosList[idSesao]._Saldo = objOpera.removerValor(usuariosList[idSesao]);
            }
        }
    }

    class Operacoes
    {
        public double valorSaldo(Usuario usu)
        {
            return usu._Saldo;
        }

        public double inserirValor(Usuario usu)
        {
            Console.Write("Qual valor deseja inserir? ");
            double resp = Convert.ToDouble(Console.ReadLine());
            return usu._Saldo + resp;
        }

        public double removerValor(Usuario usu)
        {
            Console.Write("Qual valor deseja inserir? ");
            double resp = Convert.ToDouble(Console.ReadLine());
            return usu._Saldo + resp;
        }
    }


    internal class Program
    {
        static void Main(string[] args)
        {
            Conta conta = new Conta();

            conta.criarConta("Sese", "123", 120);

            Console.WriteLine(conta.usuariosList[0]._Nome);

            while (true)
            {
                //conta.usarSaldo(0);
                int i = Convert.ToInt32(Console.ReadLine());

                if (i == 0)
                {
                    break;
                }

            }
        }
    }
}
