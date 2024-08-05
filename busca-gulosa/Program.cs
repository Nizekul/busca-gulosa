using busca_gulosa;

class OpcoesBusca
{
    public No No { get; set; }
    public List<No> ListaFronteira { get; set; } = new List<No>();
    public Dictionary<string, No> NosExpandido { get; set; } = new Dictionary<string, No>();
    public int Iteracao { get; set; } = 0;
    public int LimiteIteracao { get; set; } = 3000;
    public Action<Exception, OpcoesBusca> Callback { get; set; }
}

class Program
{
    static void Main(string[] args)
    {
        int[,] puzzle = GerarPuzzleAleatorio();
        int[,] puzzle2 = new int[,] { { 1, 2, 3 }, { 4, 8, 7 }, { 5, 6, 0 } };
        int[,] puzzle3 = new int[,] { { 4, 1, 0 }, { 8, 5, 3 }, { 7, 2, 6 } };
        int[,] puzzle4 = new int[,] { { 7, 2, 6 }, { 1, 5, 0 }, { 4, 8, 3 } };

        OitoPuzzle oitoPuzzle = new OitoPuzzle(puzzle4);
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
