namespace busca_gulosa
{
    public class OitoPuzzle
    {
        private int[,] vitoria = { { 1, 2, 3 }, { 4, 5, 6 }, { 7, 8, 0 } };
        private int[,] estadoInicial;

        public OitoPuzzle(int[,] estadoInicial)
        {
            this.estadoInicial = estadoInicial;
        }

        public bool EstaFinalizado()
        {
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    if (estadoInicial[i, j] != vitoria[i, j])
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        public void Resolver()
        {
            int distancia = ObterDistanciaManhattan();
            Console.WriteLine($"Distância Manhattan: {distancia}");

            No noInicial = new No(estadoInicial, 0);
            OpcoesBusca opcoes = new OpcoesBusca
            {
                No = noInicial,
                Callback = (erro, opcoesResultado) =>
                {
                    if (erro != null)
                    {
                        Console.WriteLine($"Erro: {erro.Message}");
                    }
                    else
                    {
                        Console.WriteLine("Solução encontrada!");
                    }
                }
            };

            Buscar(opcoes);
        }

        private void Buscar(OpcoesBusca opcoes)
        {
            if (opcoes.No.Jogo.EstaFinalizado())
            {
                opcoes.Callback(null, opcoes);
                return;
            }

            var listaExpandida = opcoes.No.Expandir();
            opcoes.NosExpandido[ObterStringEstado(opcoes.No.Estado)] = opcoes.No;
            opcoes.MaxNosExpandidoComprimento = Math.Max(opcoes.MaxNosExpandidoComprimento, opcoes.NosExpandido.Count);

            var listaExpandidaNaoExplorada = listaExpandida.Where(no =>
            {
                if (opcoes.NosExpandido.TryGetValue(ObterStringEstado(no.Estado), out No noJaExpandido) && noJaExpandido.Custo <= no.Custo)
                    return false;

                var noAlternativo = opcoes.ListaFronteira.FirstOrDefault(n => ObterStringEstado(n.Estado) == ObterStringEstado(no.Estado));
                if (noAlternativo != null)
                {
                    if (noAlternativo.Custo <= no.Custo)
                        return false;
                    else
                        opcoes.ListaFronteira.Remove(noAlternativo);
                }

                return true;
            }).ToList();

            opcoes.ListaFronteira.AddRange(listaExpandidaNaoExplorada);
            opcoes.MaxListaFronteiraComprimento = Math.Max(opcoes.MaxListaFronteiraComprimento, opcoes.ListaFronteira.Count);

            if (opcoes.ExpandirOtimizacaoCheck)
            {
                var noDesejado = listaExpandidaNaoExplorada.FirstOrDefault(noNaoExplorado => noNaoExplorado.Jogo.EstaFinalizado());
                if (noDesejado != null)
                {
                    opcoes.Callback(null, new OpcoesBusca { No = noDesejado });
                    return;
                }
            }

            var proximoNo = ObterProximoNo(opcoes);
            if (proximoNo == null)
            {
                opcoes.Callback(new Exception("Lista fronteira está vazia"), opcoes);
                return;
            }

            opcoes.Iteracao++;
            if (opcoes.LimiteIteracao != 0 && opcoes.Iteracao > opcoes.LimiteIteracao)
            {
                opcoes.Callback(new Exception("Limite de iteração alcançado"), opcoes);
                return;
            }


            // Logar o estado atual, custo e profundidade
            Console.WriteLine($"Iteração: {opcoes.Iteracao}");
            Console.WriteLine($"Estado atual com custo {proximoNo.Custo} e profundidade {proximoNo.Profundidade}:");
            ImprimirEstado(proximoNo.Estado);

            opcoes.No = proximoNo;
            Buscar(opcoes);
        }

        private No ObterProximoNo(OpcoesBusca opcoes)
        {
            var melhorNo = opcoes.ListaFronteira.OrderBy(no => no.Jogo.ObterDistanciaManhattan()).FirstOrDefault();
            if (melhorNo != null)
                opcoes.ListaFronteira.Remove(melhorNo);
            return melhorNo;
        }

        private int[] EncontrarNumero(int numero)
        {
            int linhas = estadoInicial.GetLength(0);
            int colunas = estadoInicial.GetLength(1);

            for (int i = 0; i < linhas; i++)
            {
                for (int j = 0; j < colunas; j++)
                {
                    if (estadoInicial[i, j] == numero)
                    {
                        return new int[] { i, j };
                    }
                }
            }

            return null;
        }

        private int ObterDistanciaManhattan()
        {
            int distancia = 0;
            for (int numero = 1; numero <= 8; numero++)
            {
                int[] posicaoAtual = EncontrarNumero(numero);
                int[] posicaoObjetivo = EncontrarNumeroObjetivo(numero);
                distancia += Math.Abs(posicaoAtual[0] - posicaoObjetivo[0]) + Math.Abs(posicaoAtual[1] - posicaoObjetivo[1]);
            }
            return distancia;
        }

        private int[] EncontrarNumeroObjetivo(int numero)
        {
            int linhas = vitoria.GetLength(0);
            int colunas = vitoria.GetLength(1);

            for (int i = 0; i < linhas; i++)
            {
                for (int j = 0; j < colunas; j++)
                {
                    if (vitoria[i, j] == numero)
                    {
                        return new int[] { i, j };
                    }
                }
            }

            return null;
        }

        private string ObterStringEstado(int[,] estado)
        {
            int linhas = estado.GetLength(0);
            int colunas = estado.GetLength(1);
            string estadoString = "";

            for (int i = 0; i < linhas; i++)
            {
                for (int j = 0; j < colunas; j++)
                {
                    estadoString += estado[i, j].ToString();
                }
            }

            return estadoString;
        }

        private void ImprimirEstado(int[,] estado)
        {
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    Console.Write(estado[i, j] + " ");
                }
                Console.WriteLine();
            }
        }


    }
}