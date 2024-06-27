using busca_gulosa;

class OpcoesBusca
{
    public No No { get; set; }
    public List<No> ListaFronteira { get; set; } = new List<No>();
    public Dictionary<string, No> NosExpandido { get; set; } = new Dictionary<string, No>();
    public int Iteracao { get; set; } = 0;
    public int LimiteIteracao { get; set; } = 1000;
    public bool ExpandirOtimizacaoCheck { get; set; } = false;
    public Action<Exception, OpcoesBusca> Callback { get; set; }
    public int MaxListaFronteiraComprimento { get; set; } = 0;
    public int MaxNosExpandidoComprimento { get; set; } = 0;
}

class Program
{
    static void Main(string[] args)
    {
        int[,] puzzle = {
            { 1, 2, 3 },
            { 4, 5, 7 },
            { 6, 0, 8 } // O espaço vazio é representado por 0
        };

        OitoPuzzle oitoPuzzle = new OitoPuzzle(puzzle);
        oitoPuzzle.Resolver();
    }
}
