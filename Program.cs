using System;

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

 //Requisitos
Console.WriteLine("Alelos da Mãe (AA, Aa ou aa): ");
    String AleloM = Console.ReadLine()!;

Console.WriteLine("Alelos do Pai (AA, Aa ou aa): ");
    String AleloP = Console.ReadLine()!;

Console.WriteLine("Tipo de dominância (C/I): ");
    String domi = Console.ReadLine()!;


 //Separação de Alelos, preferencia para o com A>a
string AleloM1 = AleloM.Substring(0, 1);
string AleloM2 = AleloM.Substring(1, 1);
string AleloP1 = AleloP.Substring(0, 1);
string AleloP2 = AleloP.Substring(1, 1);

string Junção1 = NormalizaAlelo($"{AleloM1}{AleloP1}");
string Junção2 = NormalizaAlelo($"{AleloM2}{AleloP1}");
string Junção3 = NormalizaAlelo($"{AleloM1}{AleloP2}");
string Junção4 = NormalizaAlelo($"{AleloM2}{AleloP2}");


//Tabela>>
Console.WriteLine(@$"

    | {AleloM1} | {AleloM2} 
---------------------------------------
{AleloP1} | {Junção1} | {Junção2}
---------------------------------------
{AleloP2} | {Junção3} | {Junção4}

");

//Teste
