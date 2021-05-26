using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace Projeto2
{
    class Program
    {
        [System.Serializable]
        struct Cliente
        {
            public string nome;
            public string email;
            public string cpf;
        }
        static List<Cliente> clientes = new List<Cliente>();

        enum Menu { Listagem = 1, Adicionar = 2, Remover = 3, Sair = 4}

        static void Main(string[] args)
        {
            Carregar();

            bool escolheuSair = false;
            while(!escolheuSair)
            {
                Console.WriteLine("    SISTEMA DE CLIENTES    ");
                Console.WriteLine("                       Bem Vindos!!");
                Console.WriteLine("======================================================");
                Console.WriteLine("  1-Listagem\n  2-Adicionar\n  3-Remover\n  4-Sair");
                int intOp = int.Parse(Console.ReadLine());
                Menu opcao = (Menu)intOp;

                switch (opcao)
                {
                    case Menu.Adicionar:
                        Adicionar();
                        break;
                    case Menu.Listagem:
                        Listagem();
                        break;
                    case Menu.Remover:
                        Remover();
                        break;
                    case Menu.Sair:
                        escolheuSair = true;
                        break;
                }
                Console.Clear();
            }
        }

        static void Adicionar()
        {
            Cliente cliente = new Cliente();
            Console.WriteLine("Cadastro de Cliente; ");
            Console.WriteLine("Nome do Cliente: ");
            cliente.nome = Console.ReadLine();
            Console.WriteLine("Email do Cliente: ");
            cliente.email = Console.ReadLine();
            Console.WriteLine("CPF do Cliente: ");
            cliente.cpf = Console.ReadLine();

            clientes.Add(cliente);
            Salvar();

            Console.WriteLine("Cadastro concluído, aperte ENTER para sair.");
            Console.ReadLine();
        }
        static void Listagem()
        {
            if (clientes.Count > 0)
            {
                Console.WriteLine("Lista de Clientes: ");
                int i = 0;
                foreach(Cliente cliente in clientes)
                {
                    Console.WriteLine($"ID: {i}");
                    Console.WriteLine($"Nome: {cliente.nome}");
                    Console.WriteLine($"E-mail: {cliente.email}");
                    Console.WriteLine($"CPF: {cliente.cpf}");
                    Console.WriteLine("*============================================*");
                    i++;
                }
            }
            else
            {
                Console.WriteLine("Nenhum cliente cadastrado!\n Adicione um novo cliente!");
            }
            Console.WriteLine("Aperte ENTER para sair.");
            Console.ReadLine();
        }
        static void Remover()
        {
            Listagem();
            Console.WriteLine("Digite o ID do cliente que você quer remover: ");
            int id = int.Parse(Console.ReadLine());
            if (id >= 0 && id < clientes.Count)
            {
                clientes.RemoveAt(id);
                Salvar();
            }
            else
            {
                Console.WriteLine("Id digitado é inválido, tente novamente!");
                Console.ReadLine();
            }
        }
        static void Salvar()
        {
            FileStream stream = new FileStream("clients.dat",FileMode.OpenOrCreate);
            BinaryFormatter enconder = new BinaryFormatter();

            enconder.Serialize(stream, clientes);
            stream.Close();
        }
        static void Carregar()
        {
                FileStream stream = new FileStream("clients.dat", FileMode.OpenOrCreate);
            try
            {
                BinaryFormatter enconder = new BinaryFormatter();

                clientes = (List<Cliente>)enconder.Deserialize(stream);

                if (clientes == null)
                {
                    clientes = new List<Cliente>();
                }

            }
            catch(Exception e)
            {
                clientes = new List<Cliente>();
            }
            
                stream.Close();
        }
    }
}
