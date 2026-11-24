using System;
using System.Collections.Generic;

const int MAX = 100;

List<int> codigos = new List<int>();
List<string> titulos = new List<string>();
List<string> autores = new List<string>();
List<bool> disponiveis = new List<bool>();

int opcao;

do
{
    Console.WriteLine("===== SISTEMA DA BIBLIOTECA =====");
    Console.WriteLine("1 - Cadastrar livro");
    Console.WriteLine("2 - Listar livros");
    Console.WriteLine("3 - Buscar livro");
    Console.WriteLine("4 - Ordenar livros");
    Console.WriteLine("5 - Listar livros disponíveis");
    Console.WriteLine("6 - Emprestar livro");
    Console.WriteLine("7 - Devolver livro");
    Console.WriteLine("0 - Sair");
    Console.Write("Escolha uma opção: ");

    opcao = LerInteiro();

    Console.Clear();

    if (opcao == 1)
    {
        CadastrarLivro();
    }
    else if (opcao == 2)
    {
        ListarLivros();
    }
    else if (opcao == 3)
    {
        BuscarPorCodigo();
    }
    else if (opcao == 4)
    {
        OrdenarPorTitulo();
    }
    else if (opcao == 5)
    {
        ListarDisponiveis();
    }
    else if (opcao == 6)
    {
        EmprestarLivro();
    }
    else if (opcao == 7)
    {
        DevolverLivro();
    }
    else if (opcao == 0)
    {
        Console.WriteLine("Encerrando sistema... Até logo!");
    }
    else
    {
        Console.WriteLine("Opção inválida! Tente novamente.");
    }

    if (opcao != 0)
    {
        Console.WriteLine("\nPressione ENTER para voltar ao menu...");
        Console.ReadLine();
        Console.Clear();
    }

} while (opcao != 0);


// ===================== FUNÇÕES =====================

// Ler um número inteiro; se o usuário digitar letras, pede de novo
int LerInteiro()
{
    int numero;

    while (int.TryParse(Console.ReadLine(), out numero) == false)
    {
        Console.Write("Valor inválido. Digite apenas números: ");
    }

    return numero;
}

// Retorna a posição do livro na lista, ou -1 se não existir
int ProcurarPosicao(int codigo)
{
    for (int i = 0; i < codigos.Count; i++)
    {
        if (codigos[i] == codigo)
        {
            return i;
        }
    }
    return -1;
}

string TextoStatus(int posicao)
{
    if (disponiveis[posicao] == true)
    {
        return "Disponível";
    }
    return "Emprestado";
}

void CadastrarLivro()
{
    Console.WriteLine("\n===== CADASTRO DE LIVRO =====");

    if (codigos.Count >= MAX)
    {
        Console.WriteLine("Acervo cheio! Não é possível cadastrar mais livros.");
        return;
    }

    Console.Write("Digite o código do livro: ");
    int codigo = LerInteiro();

    if (codigo <= 0)
    {
        Console.WriteLine("\nCódigo inválido! Digite um número maior que zero.");
        return;
    }

    // impedir códigos duplicados
    if (ProcurarPosicao(codigo) != -1)
    {
        Console.WriteLine("ERRO: Já existe um livro com o código " + codigo + ".");
        return;
    }

    Console.Write("Digite o título do livro: ");
    string titulo = Console.ReadLine();

    Console.Write("Digite o autor do livro: ");
    string autor = Console.ReadLine();

    if (titulo == "" || autor == "")
    {
        Console.WriteLine("ERRO: Título e autor não podem ser vazios.");
        return;
    }

    codigos.Add(codigo);
    titulos.Add(titulo);
    autores.Add(autor);
    disponiveis.Add(true);

    Console.WriteLine("\nLivro cadastrado com sucesso!");
}

void ListarLivros()
{
    Console.WriteLine("\n===== LIVROS CADASTRADOS =====");
    Console.WriteLine("Quantidade de livros: " + codigos.Count);

    if (codigos.Count == 0)
    {
        Console.WriteLine("Nenhum livro cadastrado.");
        return;
    }

    for (int i = 0; i < codigos.Count; i++)
    {
        Console.WriteLine("Código: " + codigos[i] + " | Título: " + titulos[i] + " | Autor: " + autores[i] + " | Status: " + TextoStatus(i));
    }
}

void BuscarPorCodigo()
{
    Console.WriteLine("\n===== BUSCA POR CÓDIGO =====");
    Console.Write("Digite o código do livro: ");
    int codigo = LerInteiro();

    int posicao = ProcurarPosicao(codigo);

    if (posicao == -1)
    {
        Console.WriteLine("Livro com código " + codigo + " não encontrado.");
    }
    else
    {
        Console.WriteLine("\nLivro encontrado!");
        Console.WriteLine("Código: " + codigos[posicao]);
        Console.WriteLine("Título: " + titulos[posicao]);
        Console.WriteLine("Autor: " + autores[posicao]);
        Console.WriteLine("Status: " + TextoStatus(posicao));
    }
}

void OrdenarPorTitulo()
{
    Console.WriteLine("\n===== ORDENAR LIVROS POR TÍTULO =====");

    // Bubble sort: compara os títulos e troca as 4 listas juntas
    for (int i = 0; i < codigos.Count - 1; i++)
    {
        for (int j = 0; j < codigos.Count - 1 - i; j++)
        {
           if (string.Compare(titulos[j].ToLower(), titulos[j + 1].ToLower()) > 0)
           //corrigir as diferenças de comparação entre maiúsculas e minúsculas
            {    int tempCodigo = codigos[j];
                codigos[j] = codigos[j + 1];
                codigos[j + 1] = tempCodigo;

                string tempTitulo = titulos[j];
                titulos[j] = titulos[j + 1];
                titulos[j + 1] = tempTitulo;

                string tempAutor = autores[j];
                autores[j] = autores[j + 1];
                autores[j + 1] = tempAutor;

                bool tempDisponivel = disponiveis[j];
                disponiveis[j] = disponiveis[j + 1];
                disponiveis[j + 1] = tempDisponivel;
            }
        }
    }

    Console.WriteLine("Livros ordenados em ordem alfabética por título!");
    ListarLivros();
}

void ListarDisponiveis()
{
    Console.WriteLine("\n===== LIVROS DISPONÍVEIS PARA EMPRÉSTIMO =====");

    bool encontrou = false;

    for (int i = 0; i < codigos.Count; i++)
    {
        if (disponiveis[i] == true)
        {
            Console.WriteLine("Código: " + codigos[i] + " | Título: " + titulos[i] + " | Autor: " + autores[i]);
            encontrou = true;
        }
    }

    if (encontrou == false)
    {
        Console.WriteLine("Nenhum livro disponível no momento.");
    }
}

void EmprestarLivro()
{
    Console.WriteLine("\n===== EMPRÉSTIMO DE LIVRO =====");
    Console.Write("Digite o código do livro: ");
    int codigo = LerInteiro();

    int posicao = ProcurarPosicao(codigo);

    if (posicao == -1)
    {
        Console.WriteLine("ERRO: Código " + codigo + " não existe no acervo.");
    }
    else if (disponiveis[posicao] == false)
    {
        Console.WriteLine("ERRO: O livro \"" + titulos[posicao] + "\" já está emprestado.");
    }
    else
    {
        disponiveis[posicao] = false;
        Console.WriteLine("Empréstimo do livro \"" + titulos[posicao] + "\" realizado com sucesso!");
    }
}

void DevolverLivro()
{
    Console.WriteLine("\n===== DEVOLUÇÃO DE LIVRO =====");
    Console.Write("Digite o código do livro: ");
    int codigo = LerInteiro();

    int posicao = ProcurarPosicao(codigo);

    if (posicao == -1)
    {
        Console.WriteLine("ERRO: Código " + codigo + " não existe no acervo.");
    }
    else if (disponiveis[posicao] == true)
    {
        Console.WriteLine("Este livro já está disponível (não estava emprestado).");
    }
    else
    {
        disponiveis[posicao] = true;
        Console.WriteLine("Livro \"" + titulos[posicao] + "\" devolvido com sucesso!");
    }
}
