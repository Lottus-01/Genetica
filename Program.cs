using System;
using System.Linq;

Console.Clear();

//Codigo de organização de alelos
//Se mexer nisso eu te mato 

string NormalizaAlelo(string alelos)
{
    if (alelos.Contains("A") && alelos.Contains("A".ToLower()))
        return "Aa";
    else if (alelos == "AA" || alelos == "aa")
        return alelos;
    else
        return new string(alelos.OrderByDescending(c => c).ToArray());
}
//Separação de Alelos, preferência para o com Aa
//sub-rotina para separação dos alelos - Gustavo (Não parece ter dado problemas após a alteração, na duvida se der b.o pro resto do programa eu tenho o backup no meu pc)
(string, string) SepararAlelos(string alelos)
{
    return (alelos.Substring(0, 1), alelos.Substring(1, 1));
}
//Sub-rotina GerarCombinações -Gustavo
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

//Combinação e saída
string[] Juncoes = GerarJuncoes(AleloM1, AleloM2, AleloP1, AleloP2);
ImprimirTabela(AleloM1, AleloM2, AleloP1, AleloP2, Juncoes);
