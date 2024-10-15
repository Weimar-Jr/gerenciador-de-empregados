using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Globalization;
using Microsoft.VisualBasic;
using System.Text.Json;

namespace GerenciadorDeFuncionarios
{
    class Funcionario
    {
        //campos
        private string nome = "Digite";
        private int idade = 0;
        private string cargo = "Digite";
        private decimal salario = 0;

        //propriedades
        public string Nome
        {   get{ return nome;}
            set{ if(!String.IsNullOrEmpty(value))
                {
                     nome = value;
                }
            }
        }

        public int Idade
        {   get{ return idade;}
            set{if(value >=18)
                {
                     idade = value;
                }
            }
        }

        public string Cargo
        {   get{ return cargo;}
            set{ if(!String.IsNullOrEmpty(value))
                {
                     cargo= value;
                }
            }
        }

        public decimal Salario
        {
            get{ return salario;}
            set{
                if(value >0)
                {
                    salario = value;
                }
            }
        }
        //metodo
        public void MonstrarDescricao()
        {
            Console.WriteLine($"Nome:    {nome}");
            Console.WriteLine($"Idade:   {idade}");
            Console.WriteLine($"Cargo:   {cargo}");
            Console.WriteLine($"Salario: {salario}");
        }

        //construtor
        public Funcionario(string nome, int idade, string cargo, decimal salario)
        {
            Nome = nome;
            Idade = idade;
            Cargo = cargo;  
            Salario = salario;
        }
    }

    //classe mexer com documento

    class Program
    {
        static List <Funcionario> Lista = new List<Funcionario>();


        static void Main(string[] args)
        {

            //diretorios
            var caminhoDocumentos = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            var pastaDados = "dados";
            var caminhoCompleto = Path.Combine(caminhoDocumentos, pastaDados);
            var caminhoArquivo = "Funcionarios.JSON";
            var caminhoCompletoArquivo = Path.Combine(caminhoCompleto, caminhoArquivo);

            if (!Directory.Exists(caminhoCompleto))
            {
                Directory.CreateDirectory(caminhoCompleto);
                //criar pasta para o arquivo de texto

            }
            if (!File.Exists(caminhoCompletoArquivo))
            {
                using (FileStream fs = File.Create(caminhoCompletoArquivo))
                {
                    fs.Close(); // Fechar o arquivo para garantir que ele possa ser usado posteriormente.
                }
            }
            //carregar arquivo
            Carregar();

            bool continuar = true;
            do
            {
                try
                {
                    Console.WriteLine("Digite \"1\" para adicionar um novo funcionario.");
                    Console.WriteLine("Digite \"2\" para mostrar funcionarios.");
                    Console.WriteLine("Digite \"3\" para pesquisar informação de um funcionario.");
                    Console.WriteLine("Digite \"4\" para editar informação de um funcionario.");
                    Console.WriteLine("Digite \"5\" para excluir um funcionario.");
                    Console.WriteLine("Digite \"6\" para sair");
                    int resposta = Convert.ToInt32(Console.ReadLine());

                    switch(resposta)
                    {
                        case 1:
                            AdiconarFuncionairo();
                            Gravar();
                        break;

                        case 2:
                            MostrarFuncionarios();
                        break;

                        case 3:
                            ProcurarFuncionario();
                        break;

                        case 4:
                            EditarFuncionario();
                            Gravar();
                        break;

                        case 5:
                            excluirFuncionario();
                            Gravar();
                        break;

                        case 6:
                            continuar = false;
                            Gravar();
                        break;
                    }
                }
                catch(System.FormatException)
                {
                    Console.WriteLine("Formato errado, apenas digite o que o programa pede.");
                }
                catch(Exception ex)
                {
                    Console.WriteLine(ex);
                }
            }while(continuar);
        }

        //metodo adiconar 
        static void AdiconarFuncionairo()
        {
            Console.WriteLine("Digite \"sair\" para sair ");
            Console.WriteLine("Digite o nome do novo funcionario: "); 
            var nome = Console.ReadLine();
            if(!String.IsNullOrEmpty(nome))
            {
                if (nome == "sair")
                {
                    Console.WriteLine("Você saiu com sucesso");

                }else{

                    string nome1 = nome.ToString();
                    VerficarRespostaAdicionarFuncionario(nome1);
                    Console.WriteLine("Digite a idade do funcionario: ");
                    var idade = Convert.ToInt32(Console.ReadLine());
                    string idade2 = idade.ToString();
                    VerficarRespostaAdicionarFuncionario(idade2);
                    Console.WriteLine("Digite o cargo do funcionario");
                    var cargo = Convert.ToString(Console.ReadLine());
                    if (!String.IsNullOrEmpty(cargo))
                    {
                        string cargo1 = cargo.ToString();
                        VerficarRespostaAdicionarFuncionario(cargo1);
                        Console.WriteLine("Digite o salario do funcionario: ");
                        var salario = Convert.ToDecimal(Console.ReadLine());
                        string salario2 = salario.ToString();
                        VerficarRespostaAdicionarFuncionario(salario2);
                        Funcionario funcionario = new Funcionario(nome, idade, cargo, salario);
                        if(funcionario.Idade != 0 && funcionario.Nome != "Digite" && funcionario.Cargo != "Digite" && funcionario.Salario != 0)
                        {
                            Lista.Add(funcionario);
                        }
                    }
                }
            }



        }

        //verificar se esta correto
        public static void VerficarRespostaAdicionarFuncionario(string resposta)
        {
            Console.WriteLine($"\"{resposta}\"\nEsta escrito correto? s/n");
            var resposta2 = Convert.ToString(Console.ReadLine());
            if (resposta2 == "s" || resposta2 == "S")
            {
                //Continua 
            }
            else{AdiconarFuncionairo();}
        }

        //mostrar os funcionarios
        public static void MostrarFuncionarios()
        {
            Console.Clear();
            Lista.ForEach(p => Console.WriteLine($"Nome: {p.Nome} Cargo: {p.Cargo}\nIdade: {p.Idade} Salario: {p.Salario}")); 

        }


        //editar funcionario

        public static void EditarFuncionario()
        {
            Console.WriteLine("Digite \"sair\" para sair ");
            Console.WriteLine("Digite o nome do funcionario que você deseja editar: ");
            var resposta = Console.ReadLine();
            if (!String.IsNullOrEmpty(resposta))
            {
                var funcionarioSelecionado = Lista.FirstOrDefault(p => p.Nome == resposta);
                if(resposta == "sair")
                {
                    Console.WriteLine("Você saiu com sucesso.");
                }
                else{
                    if (funcionarioSelecionado != null)
                    {
                        Console.WriteLine("Digite \"1\" para editar o nome.");
                        Console.WriteLine("Digite \"2\" para editar a idade.");
                        Console.WriteLine("Digite \"3\" para editar o cargo.");
                        Console.WriteLine("Digite \"4\" para editar o salario.");
                        Console.WriteLine("Digite \"5\" para sair.");

                        int resposta2 = Convert.ToInt32(Console.ReadLine());
                        switch(resposta2)
                        {
                            case 1:
                                Console.WriteLine("Digite o novo nome: ");
                                var respostaNome = Convert.ToString(Console.ReadLine());
                                if (!String.IsNullOrEmpty(respostaNome))
                                {
                                    funcionarioSelecionado.Nome = respostaNome;
                                    Console.WriteLine($"Nome alterado para {funcionarioSelecionado.Nome}");
                                }
                                else
                                {
                                    Console.WriteLine("Nome vazio ou invalido.");
                                }
                            break;

                            case 2:
                                Console.WriteLine("Digite a nova idade: ");
                                var respostaIdade = Convert.ToInt32(Console.ReadLine());
                                if(respostaIdade >= 18)
                                {
                                    
                                    funcionarioSelecionado.Idade = respostaIdade;

                                    Console.WriteLine($"Idade alterado para {funcionarioSelecionado.Idade}.");

                                }
                                else
                                {
                                    Console.WriteLine("Idade inferior a 18 anos");
                                }
                            break;

                            case 3:
                                Console.WriteLine("Digite o novo cargo: ");
                                var respostaCargo = Convert.ToString(Console.ReadLine());
                                if (!String.IsNullOrEmpty(respostaCargo))
                                {
                                    funcionarioSelecionado.Cargo = respostaCargo;
                                    Console.WriteLine($"Cargo alterado para: {funcionarioSelecionado.Cargo}.");
                                }
                                else
                                {
                                    Console.WriteLine($"Cargo vazio ou invalido");
                                }
                            break;
                            
                            case 4:
                                Console.WriteLine("Digite o novo salario");
                                var respostaSalario = Convert.ToDecimal(Console.ReadLine());
                                funcionarioSelecionado.Salario = respostaSalario;
                                Console.WriteLine($"Salario alterado para: {funcionarioSelecionado.Salario}");

                            break;

                            case 5:
                                Console.Clear();
                                Console.WriteLine("Você saiu com sucesso.");
                            break;
                            
                            default:
                                Console.WriteLine("Opção invalida.");
                                EditarFuncionario();
                            break;

                        }    
                    }
                    else
                    {
                        Console.WriteLine("Funcionario não encontrado");
                    }
                }
            }
            else{
                Console.WriteLine("Digite um nome valido.");
                EditarFuncionario();
            }
        }

        //excluir funcionario
        public static void excluirFuncionario()
        {
            Console.WriteLine("Digite \"sair\" para sair ");
            Console.WriteLine("Digite o nome do funcionario que você deseja excluir: ");
            var respostaExcluir = Convert.ToString(Console.ReadLine());
            if(!String.IsNullOrEmpty(respostaExcluir))
            {
                if(respostaExcluir == "sair")
                {
                    Console.WriteLine("Você saiu com sucesso.");
                }else{
                    var funcionario = Lista.FirstOrDefault(f => f.Nome == respostaExcluir);
                    if (funcionario != null)
                    {
                        Console.WriteLine($"Nome: {funcionario.Nome}\nIdade: {funcionario.Idade}\nCargo: {funcionario.Cargo}\nSalario: {funcionario.Salario}");
                        Console.WriteLine($"Deseja mesmo excluir o funcionario: {funcionario.Nome} ?\ns/n");
                        var resposta =  Convert.ToString(Console.ReadLine());
                        if(resposta == "s" || resposta == "S")
                        {
                            Lista.Remove(funcionario);
                            Console.WriteLine($"Funcionario: {funcionario.Nome} excluido com sucesso");
                        }
                        else{
                            Console.WriteLine("Cancelado com sucesso.");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Funcionario não encontrado");
                    }
                }
            }
        }

        //procurar funcionario
        public static void ProcurarFuncionario()
        {
            Console.WriteLine("Digite \"sair\" para sair ");
            Console.WriteLine("Digite o nome do funcionario que você deseja procurar: ");
            var nomeFuncionario = Convert.ToString(Console.ReadLine());
            if(!String.IsNullOrEmpty(nomeFuncionario))
            {
                if(nomeFuncionario == "sair")
                {
                    Console.WriteLine("Você saiu com sucesso.");
                }
                else
                {
                    var pesquisaFuncionario = Lista.FirstOrDefault(f => f.Nome == nomeFuncionario);
                    if (pesquisaFuncionario != null)
                    {
                        Console.WriteLine($"Nome: {pesquisaFuncionario.Nome}\nIdade: {pesquisaFuncionario.Idade}\nCargo: {pesquisaFuncionario.Cargo}\nSalario: {pesquisaFuncionario.Salario}");
                    }
                    else
                    {
                        Console.WriteLine("Funcionario não encontrado ou nome invalido.");
                    }
                }
            }
            else
            {
                Console.WriteLine("Nome vazio ou invalido.");
            }
        }

        //metodo de gravar no arquivo de texto
        static void Gravar()
        {
            var caminhoDocumentos = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            var pastaDados = "dados";
            var caminhoCompleto = Path.Combine(caminhoDocumentos, pastaDados);
            var caminhoArquivo = "Funcionarios.JSON";
            var caminhoCompletoArquivo = Path.Combine(caminhoCompleto, caminhoArquivo);
            //seralizar para json
            string jsonSerializado = JsonSerializer.Serialize(Lista);
            //escrever no json
            File.WriteAllText(caminhoCompletoArquivo, jsonSerializado);

        }
        static void Carregar()
        {
            var caminhoDocumentos = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            var pastaDados = "dados";
            var caminhoCompleto = Path.Combine(caminhoDocumentos, pastaDados);
            var caminhoArquivo = "Funcionarios.JSON";
            var caminhoCompletoArquivo = Path.Combine(caminhoCompleto, caminhoArquivo);
            //lendo o arquivo
            string arquivoJSON = File.ReadAllText(caminhoCompletoArquivo);
            if (!string.IsNullOrEmpty(arquivoJSON))
            {
                Lista = JsonSerializer.Deserialize<List<Funcionario>>(arquivoJSON);
            }
        }

    }

}