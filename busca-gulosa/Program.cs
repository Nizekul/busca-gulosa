using busca_gulosa;

class OpcoesBusca
{
    public No No { get; set; }
    public List<No> ListaFronteira { get; set; } = new List<No>();
    public Dictionary<string, No> NosExpandido { get; set; } = new Dictionary<string, No>();
    public int Iteracao { get; set; } = 0;
    public int LimiteIteracao { get; set; } = 1000;
    public Action<Exception, OpcoesBusca> Callback { get; set; }
}

class Program
{
    static void Main(string[] args)
    {
        int[,] puzzle = GerarPuzzleAleatorio();

        OitoPuzzle oitoPuzzle = new OitoPuzzle(puzzle);
        oitoPuzzle.Resolver();
    }

    static int[,] GerarPuzzleAleatorio()
    {
        int[] numeros = Enumerable.Range(0, 9).ToArray();

        Random rng = new Random();
        numeros = numeros.OrderBy(x => rng.Next()).ToArray();

        int[,] puzzle = new int[3, 3];
        int k = 0;
        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                puzzle[i, j] = numeros[k++];
            }
        }

        return puzzle;
    }
}
