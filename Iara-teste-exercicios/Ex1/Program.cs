Console.WriteLine("Digite um número");
int num = Convert.ToInt32(Console.ReadLine());

while (num <= 0)
{
    Console.WriteLine("O número deve ser maior que zero, digite novamente");
    num = Convert.ToInt32(Console.ReadLine());
}

Console.WriteLine(Soma(num));
Console.ReadKey();

static int Soma(int num)
{
    var total = 0;
    for (var i = 0; i <= num; i++)
    {
        total = total + i;
    }

    return total;
}