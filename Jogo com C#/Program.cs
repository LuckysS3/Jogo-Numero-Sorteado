using System.ComponentModel.Design;

Dictionary<string, int> placar = new Dictionary<string, int>();
string caminhoArquivo = "placar.txt";
CarregarPlacar();
Menu();

void Menu()
{
    Console.WriteLine("Jogo de adivinhar \n");
    Console.WriteLine("Digite 1 para iniciar o jogo");
    Console.WriteLine("Digite 2 para ver o placar do top 10 melhores ");
    Console.WriteLine("Digite 0 para sair \n");
    Console.Write("Qual sua escolha ? ");
    string escolhar = Console.ReadLine()!;

    switch (escolhar)
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
            Console.WriteLine("Opçao errada ");
            break;
    }
}

void Jogo()
{
    //Parte do numero sorteado
    List<int> listNumerosSorteado = [];

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

    //Parte do jogo
    string acertou = "nao";
    int tentativa = 0;

    Console.WriteLine($"O numero e {numeroSorteado}");

    while (acertou != "sim")
    {
        Console.WriteLine("O numero foi sorteado");
        Console.Write("Chute um numero ");
        string numeroChutado = Console.ReadLine()!;
        int numeroChutadoInt = int.Parse(numeroChutado);

        if (numeroChutadoInt > numeroSorteado)
        {
            Console.WriteLine("Numero chutado e maior");
            tentativa++;
        }
        else if (numeroChutadoInt < numeroSorteado)
        {
            Console.WriteLine("Numero chutado e menor");
            tentativa++;
        }
        else
        {
            Console.WriteLine($"Parabens voce acertou o numero que era {numeroSorteado}");
            Console.Write("Digite seu nome");
            string nome = Console.ReadLine()!;
            AtualizarPlacar(nome, tentativa);

            acertou = "sim";
        }
    }

    Console.WriteLine("\n \nVoce quer jogar de novo? ");
    Console.WriteLine("Digite 1 para jogar de novo");
    Console.WriteLine("Digite 2 para voltar para o menu");
    string resposta = Console.ReadLine()!;

    if (resposta == "1")
    {
        Console.WriteLine("De novo entao \n \n");
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

    return (numeroAleatorio);

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
        if (tentativa > placar[nome])
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
}

void ExibirOPlacar()
{
    Console.Clear();
    Console.WriteLine("-----------------------");
    Console.WriteLine("         Placar");
    Console.WriteLine("-----------------------");

    var top10 = placar.OrderBy(p => p.Value).Take(10);

    foreach (KeyValuePair <string, int> entrada in placar)
    {
        Console.WriteLine($"Jogador: {entrada.Key} | Tentativas: {entrada.Value}");
    }
    Console.WriteLine("\nDigite qualquer tecla para voltar para o menu principal");
    Console.ReadLine();
    Console.Clear();
}