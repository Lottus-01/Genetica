using System;
using System.Linq;

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

// Normalização 
//se mexer nisso eu te mato-
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
string InterpretarFenotipo(string genotipo, string tipoDominancia) =>
    tipoDominancia.ToUpper() switch
    {
        "C" => genotipo.Contains('A')
                ? "Não apresenta a característica recessiva"
                : "Apresenta a característica recessiva",
        "I" => genotipo switch
        {
            "AA" => "Fenótipo dominante",
            "aa" => "Fenótipo recessivo",
            _ => "Fenótipo intermediário"
        },
        _ => "Tipo de dominância inválido"
    };

// Tudo enfiado em void, pra gerar no final
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

// Normalização e separação
string AleloM_norm = NormalizaAlelo(AleloM);
string AleloP_norm = NormalizaAlelo(AleloP);

(string AleloM1, string AleloM2) = SepararAlelos(AleloM_norm);
(string AleloP1, string AleloP2) = SepararAlelos(AleloP_norm);

// Agrupamento de Alelos
    string[] Juncoes = GerarJuncoes(AleloM1, AleloM2, AleloP1, AleloP2);
    var contagem = Juncoes.GroupBy(j => j).ToDictionary(g => g.Key, g => g.Count());
    int total = Juncoes.Length;

//
ImprimirTabela(AleloM1, AleloM2, AleloP1, AleloP2, Juncoes);

Console.WriteLine("\n-------------------------------------");

Console.WriteLine("\nFrequência dos genótipos e fenótipos:");

foreach (var item in contagem)
{
    double porcentagem = (item.Value / (double)total) * 100;
    string fenotipo = InterpretarFenotipo(item.Key, domi);
    Console.WriteLine($"{item.Key}: {porcentagem:0}% - {fenotipo}");
}
