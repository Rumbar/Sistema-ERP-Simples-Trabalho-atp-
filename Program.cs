using System;
using System.IO;

class Produto
{
    public string Nome { get; set; }
    public int Estoque { get; set; }
    public int TotalVendas { get; set; }

    public Produto(string nome, int estoque)
    {
        Nome = nome;
        Estoque = estoque;
        TotalVendas = 0;
    }
}

class Program
{
    static Produto[] produtos = new Produto[4];
    static int[,] vendas = new int[30, 4];

    static void Main(string[] args)
    {
        int opcao;
        do
        {
            Console.WriteLine("Menu:");
            Console.WriteLine("1 – Importar arquivo de produtos");
            Console.WriteLine("2 – Registrar venda");
            Console.WriteLine("3 – Relatório de vendas");
            Console.WriteLine("4 – Relatório de estoque");
            Console.WriteLine("5 – Criar arquivo de vendas");
            Console.WriteLine("6 – Sair");
            Console.Write("Escolha uma opção: ");
            opcao = int.Parse(Console.ReadLine());

            switch (opcao)
            {
                case 1:
                    ImportarProdutos("produtos.txt");
                    break;
                case 2:
                    RegistrarVenda();
                    break;
                case 3:
                    RelatorioVendas();
                    break;
                case 4:
                    RelatorioEstoque();
                    break;
                case 5:
                    CriarArquivoVendas("vendas_totais.txt");
                    break;
                case 6:
                    Console.WriteLine("Saindo...");
                    break;
                default:
                    Console.WriteLine("Opção inválida!");
                    break;
            }
        } while (opcao != 6);
    }

    static void ImportarProdutos(string nomeArquivo)
    {
        string[] linhas = File.ReadAllLines(nomeArquivo);
        for (int i = 0; i < linhas.Length; i++)
        {
            string[] dados = linhas[i].Split(' ');
            produtos[i] = new Produto(dados[0], int.Parse(dados[1]));
        }
        Console.WriteLine("Produtos importados com sucesso!");
    }

    static void RegistrarVenda()
    {
        Console.Write("Digite o número do produto (0 a 3): ");
        int produtoNum = int.Parse(Console.ReadLine());
        Console.Write("Digite o dia do mês (1 a 30): ");
        int dia = int.Parse(Console.ReadLine()) - 1;
        Console.Write("Digite a quantidade vendida: ");
        int quantidade = int.Parse(Console.ReadLine());

        if (quantidade > produtos[produtoNum].Estoque)
        {
            Console.WriteLine("Quantidade de vendas não pode ultrapassar o estoque.");
        }
        else
        {
            produtos[produtoNum].Estoque -= quantidade;
            produtos[produtoNum].TotalVendas += quantidade;
            vendas[dia, produtoNum] += quantidade;
            Console.WriteLine("Venda registrada com sucesso!");
        }
    }

    static void RelatorioVendas()
    {
        Console.WriteLine("Relatório de Vendas:");
        Console.WriteLine("Produto A Produto B Produto C Produto D");
        for (int dia = 0; dia < 30; dia++)
        {
            Console.Write("Dia " + (dia + 1) + ": ");
            for (int produto = 0; produto < 4; produto++)
            {
                Console.Write(vendas[dia, produto] + " ");
            }
            Console.WriteLine();
        }
    }

    static void RelatorioEstoque()
    {
        Console.WriteLine("Relatório de Estoque:");
        foreach (var produto in produtos)
        {
            Console.WriteLine(produto.Nome + " " + produto.Estoque);
        }
    }

    static void CriarArquivoVendas(string nomeArquivo)
    {
        using (StreamWriter writer = new StreamWriter(nomeArquivo))
        {
            foreach (var produto in produtos)
            {
                writer.WriteLine(produto.Nome + " " + produto.TotalVendas);
            }
        }
        Console.WriteLine("Arquivo de vendas criado com sucesso!");
    }
}
