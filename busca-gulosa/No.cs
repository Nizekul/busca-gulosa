﻿namespace busca_gulosa
{
    public class No
    {
        public int[,] Estado { get; set; }
        public int Profundidade { get; set; }
        public int Custo { get; set; }
        public OitoPuzzle Jogo { get; set; }

        public No(int[,] estado, int profundidade)
        {
            Estado = estado;
            Profundidade = profundidade;
            Custo = 0; // Ajuste o cálculo do custo conforme necessário
            Jogo = new OitoPuzzle(estado);
        }

        public List<No> Expandir()
        {
            List<No> nosExpandido = new List<No>();
            int linhaZero = -1, colunaZero = -1;

            // Encontrar a posição do zero (espaço vazio)
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    if (Estado[i, j] == 0)
                    {
                        linhaZero = i;
                        colunaZero = j;
                        break;
                    }
                }
            }

            // Definir movimentos possíveis
            var movimentosPossiveis = new (int, int)[]
            {
                (linhaZero, colunaZero - 1), // Esquerda
                (linhaZero, colunaZero + 1), // Direita
                (linhaZero - 1, colunaZero), // Cima
                (linhaZero + 1, colunaZero)  // Baixo
            };

            // Tentar cada movimento
            foreach (var (novaLinha, novaColuna) in movimentosPossiveis)
            {
                if (EhMovimentoValido(novaLinha, novaColuna))
                {
                    int[,] novoEstado = (int[,])Estado.Clone();
                    // Trocar zero com o número no novo local
                    novoEstado[linhaZero, colunaZero] = novoEstado[novaLinha, novaColuna];
                    novoEstado[novaLinha, novaColuna] = 0;

                    // Criar um novo nó para o estado gerado
                    No novoNo = new No(novoEstado, Profundidade + 1)
                    {
                        Custo = Profundidade + 1 // Para busca gulosa, o custo pode ser a profundidade do nó
                    };

                    nosExpandido.Add(novoNo);
                }
            }

            return nosExpandido;
        }

        private bool EhMovimentoValido(int linha, int coluna)
        {
            // Verifica se a posição está dentro dos limites da matriz
            return linha >= 0 && linha < 3 && coluna >= 0 && coluna < 3;
        }

    }
}
