using System;

Console.Clear();

//Código de organização de alelos
//Se mexer nisso eu te mato
//Fiz uma alteração no código a baixo ,porém os alelos parecem da um erro na parte inferior ficando "a aa a" ao invez de "a Aa a", mas na minha cabeça da pra corrigir quando for verificar a Dominancia.
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
string aleloM = Console.ReadLine()!;

Console.WriteLine("Alelos do Pai (AA, Aa ou aa): ");
string aleloP = Console.ReadLine()!;

Console.WriteLine("Tipo de dominância (C/I): ");
string domi = Console.ReadLine()!;

string aleloM_norm = NormalizaAlelo(aleloM);
string aleloP_norm = NormalizaAlelo(aleloP);

(string aleloM1, string aleloM2) = SepararAlelos(aleloM_norm);
(string aleloP1, string aleloP2) = SepararAlelos(aleloP_norm);

string[] juncos = GerarCombinacoes(aleloM1, aleloM2, aleloP1, aleloP2);
ImprimirTabela(aleloM1, aleloM2, aleloP1, aleloP2, juncos);

//Separação de Alelos, preferência para o com Aa
//sub-rotina para separação dos alelos - Gustavo (Não parece ter dado problemas após a alteração, na duvida se der b.o pro resto do programa eu tenho o backup no meu pc)
(string, string) SepararAlelos(string alelos)
{
    return (alelos.Substring(0, 1), alelos.Substring(1, 1));
}
//Sub-rotina GerarCombinações -Gustavo
string[] GerarCombinacoes(string aleloM1, string aleloM2, string aleloP1, string aleloP2)
{
    return new string[]
    {
        NormalizaAlelo(aleloM1[0].ToString() + aleloP1[0].ToString()),
        NormalizaAlelo(aleloM1[0].ToString() + aleloP2[0].ToString()),
        NormalizaAlelo(aleloM2[0].ToString() + aleloP1[0].ToString()),
        NormalizaAlelo(aleloM2[0].ToString() + aleloP2[0].ToString())
    };
}

//Sub-rotina tabela -Gustavo
void ImprimirTabela(string aleloM1, string aleloM2, string aleloP1, string aleloP2, string[] juncoes)
{
    Console.WriteLine($"\n      | {aleloP1} | {aleloP2} |");
    Console.WriteLine("-------------------");
    Console.WriteLine($"  {aleloM1}  | {juncoes[0]} | {juncoes[1]} ");
    Console.WriteLine($"  {aleloM2}  | {juncoes[2]} | {juncoes[3]} ");
}