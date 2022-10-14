using System.Text;

var Duplicados = new[] { "abbracadabra", "allottee", "assessee", "kelless", "keenness", "Alfalggo" };
var SemDuplicados = RemoverDuplicados(Duplicados);
foreach (var x in SemDuplicados)
    Console.WriteLine(x);

static List<string> RemoverDuplicados(string[] duplicados)
{
    List<string> NovaLista = new List<string>();
    foreach (var duplicado in duplicados)
    {
        var strResult = new StringBuilder();

        foreach (var element in duplicado.ToCharArray())
        {
            if (strResult.Length == 0 || strResult[strResult.Length - 1] != element)
                strResult.Append(element);
        }

        NovaLista.Add(strResult.ToString());
    }
    return NovaLista;
}