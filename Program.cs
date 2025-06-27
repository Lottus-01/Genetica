Console.Clear();

// Requisitos
(string AleloM, string AleloP, string domi) Requisitos()
{
    Console.WriteLine("Alelos da Mãe (AA, Aa ou aa): ");
    string AleloM = Console.ReadLine()!;
    Console.WriteLine("Alelos do Pai (AA, Aa ou aa): ");
    string AleloP = Console.ReadLine()!;
    Console.WriteLine("Tipo de dominância (C/I): ");
    string domi = Console.ReadLine()!;
    return (AleloM, AleloP, domi);
}

// Combinações
string[] GerarJuncoes(string AleloM1, string AleloM2, string AleloP1, string AleloP2) =>
    new string[]
    {
        NormalizaAlelo($"{AleloM1}{AleloP1}"),
        NormalizaAlelo($"{AleloM2}{AleloP1}"),
        NormalizaAlelo($"{AleloM1}{AleloP2}"),
        NormalizaAlelo($"{AleloM2}{AleloP2}")
    };

// Codigo de definição de Dominancia
(string nome, string descricao) InterpretarFenotipo(string genotipo, string tipoDominancia) =>
    tipoDominancia.ToUpper() switch
    {
        "C" => genotipo switch
        {
            "AA" => ("AA", "Não apresenta a característica recessiva"),
            "Aa" => ("Aa", "Não apresenta a característica recessiva"),
            "aa" => ("aa", "Apresenta a característica recessiva"),
            _ => ("Inválido", "Genótipo inválido")
        },
        "I" => genotipo switch
        {
            "AA" => ("A", "Apresenta a característica de `A`"),
            "aa" => ("a", "Apresenta a característica de `a`"),
            _ => ("Intermediário", "Apresenta característica distinta de `A` e de `a`")
        },
        _ => ("Inválido", "Tipo de dominância inválido")
    };

// Tabela
void ImprimirTabela(string AleloM1, string AleloM2, string AleloP1, string AleloP2, string[] Juncoes)
{
    Console.WriteLine("\n--- Genética Mendeliana ---");
    Console.WriteLine($"    | {AleloM1}  | {AleloM2} ");
    Console.WriteLine("--------------------");
    Console.WriteLine($" {AleloP1} | {Juncoes[0]} | {Juncoes[1]}");
    Console.WriteLine("--------------------");
    Console.WriteLine($" {AleloP2} | {Juncoes[2]} | {Juncoes[3]}");
}

var (AleloM, AleloP, domi) = Requisitos();

// Normalização e separação dos alelos
string NormalizaAlelo(string alelos)
{
    if (alelos.Contains("A") && alelos.Contains("a"))
        return "Aa";
    else if (alelos == "AA" || alelos == "aa")
        return alelos;
    else
        return new string(alelos.OrderByDescending(c => c).ToArray());
}

    (string, string) SepararAlelos(string alelos) =>
        (alelos.Substring(0, 1), alelos.Substring(1, 1));

string AleloM_norm = NormalizaAlelo(AleloM);
string AleloP_norm = NormalizaAlelo(AleloP);

(string AleloM1, string AleloM2) = SepararAlelos(AleloM_norm);
(string AleloP1, string AleloP2) = SepararAlelos(AleloP_norm);

// Agrupamento de Alelos
    string[] Juncoes = GerarJuncoes(AleloM1, AleloM2, AleloP1, AleloP2);
    var contagem = Juncoes.GroupBy(j => j).ToDictionary(g => g.Key, g => g.Count());
    int total = Juncoes.Length;

//Tabela dos fenótipos
ImprimirTabela(AleloM1, AleloM2, AleloP1, AleloP2, Juncoes);

Console.WriteLine("\n-------------------------------------");

Console.WriteLine("\nFrequência dos genótipos e fenótipos:");

foreach (var item in contagem)
{
    double porcentagem = (item.Value / (double)total) * 100;
    var (nome, descricao) = InterpretarFenotipo(item.Key, domi);
    Console.WriteLine($"{item.Key}: {porcentagem:0}% - {descricao}");
}
