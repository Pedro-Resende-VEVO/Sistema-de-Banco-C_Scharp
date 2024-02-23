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

        public abstract void operacoes(int opcao);
    }

    class Conta : Banco
    {
        private int idSesao;

        public void criarConta(string nome, string senha, double saldo)
        {
            Usuario obj = new Usuario(nome, senha, saldo);
            usuariosList.Add(obj);
        }
        
        public bool logarConta(string nome, string senha)
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

            if (opcao == 2)
            {
                usuariosList[idSesao]._Saldo = objOpera.deposito(usuariosList[idSesao]);
            }
            else if (opcao == 3)
            {
                usuariosList[idSesao]._Saldo = objOpera.saque(usuariosList[idSesao]);
            }

            Console.WriteLine($"Saldo atualizado: {objOpera.valorSaldo(usuariosList[idSesao])}");
        }

        public void perfilVisu()
        {
            Console.WriteLine($"Nome: {usuariosList[idSesao]._Nome}");
            Console.WriteLine($"Senha: {usuariosList[idSesao]._Senha}");
            Console.WriteLine($"Saldo atual: {usuariosList[idSesao]._Saldo}");
        }
    }

    class Operacoes
    {
        public double valorSaldo(Usuario usu)
        {
            return usu._Saldo;
        }

        public double deposito(Usuario usu)
        {
            try
            {
                Console.Write($"Saldo atual: {valorSaldo(usu)}\nQual valor deseja inserir? ");
                double resp = Convert.ToDouble(Console.ReadLine());

                return usu._Saldo + resp;
            }
            catch
            {
                Console.WriteLine("Resposta inválida, favor tentar novamente");
            }

            throw new Exception("Passou do Catch");
        }

        public double saque(Usuario usu)
        {
            try
            {
                while (true)
                {
                    Console.Write($"Saldo atual: {valorSaldo(usu)}\nQual valor deseja retirar?");
                    double resp = Convert.ToDouble(Console.ReadLine());
                    if (resp > usu._Saldo)
                    {
                        Console.WriteLine("Valor passou do limite, favor tentar novamente");
                    }
                    else
                    {
                        return usu._Saldo - resp;
                    }
                }
            }
            catch
            {
                Console.WriteLine("Resposta inválida, favor tentar novamente");
            }

            throw new Exception("Passou do Catch");
        }
    }


    internal class Program
    {
        static void Main(string[] args)
        {
            Conta conta = new Conta();
            bool cond = true;
            bool cond2 = true;

            while (cond == true)
            {
                Console.WriteLine("---Bem vindo ao Banco Sesas---\n");
                Console.WriteLine("-Qual operação deseja fazer ?-\n");
                Console.WriteLine("1) Logar - 2)Cadastrar - 3) Sair");
                Console.WriteLine("--------------------------------");
                Console.Write("RESPOSTA: ");

                try
                {
                    int resp = Convert.ToInt32(Console.ReadLine());

                    switch (resp)
                    {
                        case 1:
                            cond2 = true;
                            while (cond2 == true)
                            {
                                Console.WriteLine("--------------Entrar--------------");
                                Console.WriteLine("Insira as informações solicitadas:\n");
                                Console.WriteLine("Nome: ");
                                string nomeLog = Console.ReadLine()!;
                                Console.WriteLine("Senha: ");
                                string senhaLog = Console.ReadLine()!;
                                if (conta.logarConta(nomeLog, senhaLog) == true)
                                {
                                    Console.WriteLine("Acessando conta, aperte qualquer tecla para voltar ao menu");
                                    Console.ReadLine();
                                    Acesso(conta);          //Acessando a conta do usuário
                                    cond2 = false;
                                }
                                else
                                {
                                    Console.WriteLine("Informações erradas, deseja tentar novamente?\n1) Sim - 2) Não\nRESPOSTA: ");
                                    int continar = Convert.ToInt32(Console.ReadLine());

                                    if (continar == 2)
                                    {
                                        cond2 = false;
                                    }
                                }
                            }
                            break;
                        case 2:
                            Console.WriteLine("-------------Cadastro-------------");
                            Console.WriteLine("Insira as informações solicitadas:\n");
                            Console.WriteLine("Nome: ");
                            string nomeCad = Console.ReadLine()!;
                            Console.WriteLine("Senha: ");
                            string senhaCad = Console.ReadLine()!;
                            conta.criarConta(nomeCad, senhaCad, 0);   //Cria a conta com saldo zerado
                            Console.WriteLine("Cadastro concluído, aperte qualquer tecla para voltar ao menu");
                            Console.ReadLine();
                            break;
                        case 3:
                            cond = false;
                            break;
                        default:
                            Console.WriteLine("Operação inválida, favor tentar novamente");
                            break;
                    }
                }
                catch
                {
                    Console.WriteLine($"Favor inserir o NÚMERO correspondente a operação, tente novamente:");
                }
            }

            Console.WriteLine("Obrigado por usar, vejo você na próximaa ;)");
        }


        static void Acesso(Conta conta)
        {
            bool cond = true;

            while (cond == true)
            {
                Console.WriteLine("------------------Banco Sesas-----------------\n");
                Console.WriteLine($"---------Qual operação deseja fazer?---------\n");
                Console.WriteLine("1) Ver perfil - 2) Deposito - 3) Saque - 4) Sair");
                Console.WriteLine("------------------------------------------------");
                Console.Write("RESPOSTA: ");

                try
                {
                    int resp = Convert.ToInt32(Console.ReadLine());

                    switch (resp)
                    {
                        case 1:
                            Console.WriteLine("--------------Perfil--------------");
                            conta.perfilVisu();
                            Console.WriteLine("----------------------------------");
                            break;
                        case 2:
                            Console.WriteLine("-------------Depósito-------------");
                            conta.operacoes(resp);
                            break;
                        case 3:
                            Console.WriteLine("---------------Saque--------------");
                            conta.operacoes(resp);
                            break;
                        case 4:
                            cond = false;
                            break;
                        default:
                            Console.WriteLine("Operação inválida, favor tentar novamente");
                            break;
                    }

                    Console.WriteLine("Aperte qualquer tecla para voltar a tela inicial");
                    Console.ReadLine();
                }
                catch
                {
                    Console.WriteLine($"Favor inserir o NÚMERO correspondente a operação, tente novamente:");
                }
            }
        }
    }
}
