using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

Dictionary<string, int> placar = new Dictionary<string, int>();

// Obtém o caminho para a pasta Documentos do usuário e cria o caminho completo para o arquivo placar.txt
string caminhoArquivo = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "Jogo Sorteio", "placar.txt");

CarregarPlacar();
Menu();

void Menu()
{
    Console.WriteLine("Jogo de adivinhar \n");
    Console.WriteLine("Digite 1 para iniciar o jogo");
    Console.WriteLine("Digite 2 para ver o placar do top 10 melhores ");
    Console.WriteLine("Digite 0 para sair \n");
    Console.Write("Qual sua escolha? ");
    string escolha = Console.ReadLine()!;

    switch (escolha)
    {
        case "1":
            Console.WriteLine("\n \nSeja Bem-Vindo ao jogo do sorteio");
            Jogo();
            break;
        case "2":
            ExibirOPlacar();
            break;
        case "0":
            Console.WriteLine("Tchau");
            break;
        default:
            Console.WriteLine("Opção errada");
            break;
    }
}

void Jogo()
{
    List<int> listNumerosSorteado = new();
    int numeroSorteado = SortearNumero();

    if (listNumerosSorteado.Contains(numeroSorteado))
    {
        SortearNumero();
    }
    else
    {
        listNumerosSorteado.Add(numeroSorteado);
    }

    if (listNumerosSorteado.Count == 8)
    {
        listNumerosSorteado.Clear();
    }

    string acertou = "nao";
    int tentativa = 0;

    Console.WriteLine($"O número é {numeroSorteado}");

    while (acertou != "sim")
    {
        Console.WriteLine("O número foi sorteado");
        Console.Write("Chute um número: ");
        string numeroChutado = Console.ReadLine()!;
        int numeroChutadoInt = int.Parse(numeroChutado);

        if (numeroChutadoInt > numeroSorteado)
        {
            Console.WriteLine("Número chutado é maior");
            tentativa++;
        }
        else if (numeroChutadoInt < numeroSorteado)
        {
            Console.WriteLine("Número chutado é menor");
            tentativa++;
        }
        else
        {
            Console.WriteLine($"Parabéns, você acertou! O número era {numeroSorteado}");
            Console.Write("Digite seu nome: ");
            string nome = Console.ReadLine()!;
            AtualizarPlacar(nome, tentativa);
            acertou = "sim";
        }
    }

    Console.WriteLine("\n \nVocê quer jogar de novo? ");
    Console.WriteLine("Digite 1 para jogar de novo");
    Console.WriteLine("Digite 2 para voltar ao menu");
    string resposta = Console.ReadLine()!;

    if (resposta == "1")
    {
        Console.Clear();
        Jogo();
    }
    else
    {
        Console.Clear();
        Menu();
    }
}

static int SortearNumero()
{
    Random random = new();
    int numeroAleatorio = random.Next(1, 11);
    return numeroAleatorio;
}

void CarregarPlacar()
{
    if (File.Exists(caminhoArquivo))
    {
        string[] linhas = File.ReadAllLines(caminhoArquivo);
        foreach (string linha in linhas)
        {
            string[] partes = linha.Split(':');
            string nome = partes[0];
            int tentativas = int.Parse(partes[1]);
            placar[nome] = tentativas;
        }
    }
}

void AtualizarPlacar(string nome, int tentativa)
{
    if (placar.ContainsKey(nome))
    {
        if (tentativa < placar[nome])
        {
            placar[nome] = tentativa; 
        }
    }
    else
    {
        placar[nome] = tentativa;
    }

    if (placar.Count > 10)
    {
        var piorJogador = placar.OrderByDescending(p => p.Value).First();
        placar.Remove(piorJogador.Key);
    }

    SalvarPlacar();  
}

void SalvarPlacar()
{
    try
    {
        // Cria o diretório "Jogo Sorteio" em Documentos se não existir
        string pasta = Path.GetDirectoryName(caminhoArquivo);
        if (!Directory.Exists(pasta))
        {
            Directory.CreateDirectory(pasta);
        }

        // Escreve cada entrada do placar no arquivo
        List<string> linhas = new List<string>();
        foreach (var entrada in placar)
        {
            linhas.Add($"{entrada.Key}:{entrada.Value}");
        }
        File.WriteAllLines(caminhoArquivo, linhas);
        Console.WriteLine("Placar salvo com sucesso.");
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Erro ao salvar o placar: {ex.Message}");
    }
}

void ExibirOPlacar()
{
    Console.Clear();
    Console.WriteLine("-----------------------");
    Console.WriteLine("         Placar");
    Console.WriteLine("-----------------------");

    var top10 = placar.OrderBy(p => p.Value).Take(10);
    foreach (var entrada in top10)
    {
        Console.WriteLine($"Jogador: {entrada.Key} | Tentativas: {entrada.Value}");
    }

    Console.WriteLine("\nDigite qualquer tecla para voltar ao menu principal");
    Console.ReadLine();
    Console.Clear();
    Menu();
}
