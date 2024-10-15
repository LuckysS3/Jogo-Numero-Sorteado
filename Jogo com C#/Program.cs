
static void SortearNumero(List<int> listNumerosSorteado)
{
    Random random = new();


    int numeroAleatorio = random.Next(1, 11);


    if (listNumerosSorteado.Contains(numeroAleatorio))
    {
        SortearNumero(listNumerosSorteado);
    }
    else
    {
        listNumerosSorteado.Add(numeroAleatorio);
    }

    if(listNumerosSorteado.Count == 8)
    {
        listNumerosSorteado.Clear();
    }

    Console.WriteLine("Número aleatório gerado: " + numeroAleatorio);

    Console.WriteLine("Repeti");
    string sim = Console.ReadLine()!;
    Console.WriteLine($"A lista dos numero sorteado");
    foreach (int numero in listNumerosSorteado)
    {
        Console.WriteLine(numero);
    }

    if (sim == "s")
    {
        SortearNumero(listNumerosSorteado);
    }
}

static void Menu()
{
    List<int> listNumerosSorteado = [];

    Console.WriteLine("Jogo de adivinhar \n");
    Console.WriteLine("Digite 1 para iniciar o jogo");
    Console.WriteLine("Digite 2 para ver o placar do top 10 melhores ");
    Console.WriteLine("Digite 3 para sair \n");

    Console.Write("Qual sua escolha ? ");
    string escolhar = Console.ReadLine()!;

    switch (escolhar)
    {
        case "1":
            SortearNumero(listNumerosSorteado);
            break;
        case "2": Console.WriteLine("Placar");
            break;
        case "3": Console.WriteLine("Tchau");
            break;
        default: Console.WriteLine("Opçao errada ");
            break;
    }

}

Menu();