static void Jogo()
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
        Console.WriteLine("Lista de numero sorteado");
        foreach (int numero in listNumerosSorteado)
        {
            Console.WriteLine(numero);
        }
    }

    if (listNumerosSorteado.Count == 8)
    {
        listNumerosSorteado.Clear();   
    }

    //Parte do jogo
    string acertou = "nao";
    int tentativa = 0;

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
            acertou = "sim";
        }
    }
}

static int SortearNumero()
{
    Random random = new();

    int numeroAleatorio = random.Next(1, 11);
    
    return (numeroAleatorio);
 
}

static void Menu()
{   Console.WriteLine("Jogo de adivinhar \n");
    Console.WriteLine("Digite 1 para iniciar o jogo");
    Console.WriteLine("Digite 2 para ver o placar do top 10 melhores ");
    Console.WriteLine("Digite 3 para sair \n");

    Console.Write("Qual sua escolha ? ");
    string escolhar = Console.ReadLine()!;

    switch (escolhar)
    {
        case "1":
            Console.WriteLine("Seja Bem-Vindo ao jogo do sorteio");
            Jogo();
            break;
        case "2": 
            Console.WriteLine("Placar");
            break;
        case "3": 
            Console.WriteLine("Tchau");
            break;
        default: 
            Console.WriteLine("Opçao errada ");
            break;
    }

}

Menu();