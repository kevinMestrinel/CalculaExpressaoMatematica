Console.WriteLine("Escreva uma expressão matemática: ");
string expressao = Console.ReadLine();
int resultado = CalcularExpressaoMatematica(expressao);
Console.WriteLine($"Resultado: {resultado}");

int CalcularExpressaoMatematica(string expressao)
{
    Stack<int> numeros = new();
    Stack<char> operadores = new ();


    for (int i = 0; i < expressao.Length; i++)
    {
        char caractere = expressao[i];

        //separa o que é numero e operador.

        if (char.IsDigit(caractere))
        {
            string numero = caractere.ToString();

            while (i + 1 < expressao.Length && char.IsDigit(expressao[i + 1]))
            {
                numero += expressao[i + 1];
                i++;
            }

            numeros.Push(int.Parse(numero));
        }
        else if (caractere == '(')
        {
            operadores.Push(caractere);
        }
        else if (caractere == ')')
        {
            while (operadores.Count > 0 && operadores.Peek() != '(')
            {
                //Realiza a operação dos parentes em primeiro e logo após, remove eles da pilha.
                RealizarOperacao(numeros, operadores);
            }
            operadores.Pop();
        }
        else if (caractere == '+' || caractere == '-' || caractere == '*' || caractere == '/')
        {
            while (operadores.Count > 0 && Prioridade(caractere) <= Prioridade(operadores.Peek()))
            {
                RealizarOperacao(numeros, operadores);
            }
            operadores.Push(caractere);
        }
    }

    while (operadores.Count > 0)
    {
        RealizarOperacao(numeros, operadores);
    }

    return numeros.Peek();
}

int Prioridade(char operador)
{
    //Define a priorida na execucao da expressao matematica.
    if (operador == '+' || operador == '-')
        return 1;
    if (operador == '*' || operador == '/')
        return 2;
    return 0;
}

void RealizarOperacao(Stack<int> numeros, Stack<char> operadores)
{
    //Utiliza os numeros e operadores e logo após remove da fila.
    int direita = numeros.Pop();
    int esquerda = numeros.Pop();
    char operador = operadores.Pop();
    int resultado = 0;

    switch (operador)
    {
        case '+':
            resultado = esquerda + direita;
            break;
        case '-':
            resultado = esquerda - direita;
            break;
        case '*':
            resultado = esquerda * direita;
            break;
        case '/':
            resultado = esquerda / direita;
            break;
    }
    //Adiciona o novo resultado no topo da fila.
    numeros.Push(resultado);
}
