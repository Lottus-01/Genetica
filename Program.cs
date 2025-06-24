using System;
using System.Linq;

Console.Clear();

//Codigo de organização de alelos
//Se mexer nisso eu te mato 

string NormalizaAlelo(string alelos)
{
    if (alelos.Contains("A") && alelos.Contains("a"))
        return "Aa";
    else if (alelos == "AA" || alelos == "aa")
        return alelos;
    else
        return new string(alelos.OrderByDescending(c => c).ToArray());
}
//Separação de Alelos
(string, string) SepararAlelos(string alelos)
{
    return (alelos.Substring(0, 1), alelos.Substring(1, 1));
}
//Gerar combinações
string[] GerarJuncoes(string AleloM1, string AleloM2, string AleloP1, string AleloP2)
{
    return new string[]
    {
        NormalizaAlelo($"{AleloM1}{AleloP1}"),
        NormalizaAlelo($"{AleloM2}{AleloP1}"),
        NormalizaAlelo($"{AleloM1}{AleloP2}"),
        NormalizaAlelo($"{AleloM2}{AleloP2}")
    };
}

void ImprimirTabela(string AleloM1, string AleloM2, string AleloP1, string AleloP2, string[] Juncoes)
{
    Console.WriteLine("\n--- Genética Mendeliana ---\n");
    Console.WriteLine($"  | {AleloM1} | {AleloM2}");
    Console.WriteLine("--------------");
    Console.WriteLine($"{AleloP1} | {Juncoes[0]} | {Juncoes[1]}");
    Console.WriteLine("--------------");
    Console.WriteLine($"{AleloP2} | {Juncoes[2]} | {Juncoes[3]}");
}

// Interpretar fenótipo baseado na dominância
string InterpretarFenotipo(string genotipo, string tipoDominancia)
{
    return tipoDominancia.ToUpper() switch
    {
        "C" => genotipo.Contains('A')
                ? "Não apresenta a característica recessiva"
                : "Apresenta a característica recessiva",
        "I" => genotipo switch
        {
            "AA" => "Fenótipo dominante",
            "aa" => "Fenótipo recessivo",
            _ => "Fenótipo intermediário (mistura)"
        },
        _ => "Tipo de dominância inválido"
    };
}

//Requisitos
Console.WriteLine("Alelos da Mãe (AA, Aa ou aa): ");
string AleloM = Console.ReadLine()!;
Console.WriteLine("Alelos do Pai (AA, Aa ou aa): ");
string AleloP = Console.ReadLine()!;
Console.WriteLine("Tipo de dominância (C/I): ");
string domi = Console.ReadLine()!;

//Separação e normalização
string AleloM_norm = NormalizaAlelo(AleloM);
string AleloP_norm = NormalizaAlelo(AleloP);

string AleloM1, AleloM2;
string AleloP1, AleloP2;
(AleloM1, AleloM2) = SepararAlelos(AleloM_norm);
(AleloP1, AleloP2) = SepararAlelos(AleloP_norm);

//Combinação
string[] Juncoes = GerarJuncoes(AleloM1, AleloM2, AleloP1, AleloP2);

// Contar frequências dos genótipos gerados
var contagem = Juncoes
    .GroupBy(j => j)
    .ToDictionary(g => g.Key, g => g.Count());

// Calcular total
int total = Juncoes.Length;

Console.WriteLine("\nFrequência dos genótipos e fenótipos:");
Console.WriteLine("-------------------------------------");

foreach (var item in contagem)
{
    double porcentagem = (item.Value / (double)total) * 100;
    string fenotipo = InterpretarFenotipo(item.Key, domi);
    Console.WriteLine($"{item.Key}: {porcentagem:0}% - {fenotipo}");
}

ImprimirTabela(AleloM1, AleloM2, AleloP1, AleloP2, Juncoes);
