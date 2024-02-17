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
        public int _Idade { get; set; }
        public double _Saldo { get; set; }

        public Usuario(string nome, int idade, double saldo)
        {
            _Nome = nome;
            _Idade = idade;
            _Saldo = saldo;
        }
    }

    abstract class Banco
    {
        public List <Usuario> usuariosList = new List<Usuario>();

        public void criarConta(string nome, int idade, double saldo)
        {
            Usuario obj = new Usuario(nome, idade, saldo);
            usuariosList.Add(obj);
        }

        public abstract void usarSaldo(int opcao);
    }

    class Conta : Banco
    {
        Usuario usu;

        public Conta(int indexSesao)
        {
            usu = usuariosList[indexSesao]; //Pega da lista do Banco, se eu usar "this", vou estar usando a lista herdada
        }

        public override void usarSaldo(int opcao)
        {
            Operacoes objOpera = new Operacoes(); 

            if (opcao == 0)
            {
                usu._Saldo = objOpera.inserirValor(usu);
            }
            else if (opcao == 1)
            {
                usu._Saldo = objOpera.removerValor(usu);
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
            double resp = double.Parse(Console.ReadLine());
            return usu._Saldo + resp;
        }

        public double removerValor(Usuario usu)
        {
            Console.Write("Qual valor deseja inserir? ");
            double resp = double.Parse(Console.ReadLine());
            return usu._Saldo + resp;
        }
    }


    internal class Program
    {
        static void Main(string[] args)
        {
            Conta conta = new Conta(0);
            Usuario oi = new Usuario("oi", 12, 120);

            while (true) 
            {
                conta.usarSaldo(0);
            }
        }
    }
}