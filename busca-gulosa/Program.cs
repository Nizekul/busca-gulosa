using System;
using System.Collections.Generic;

class EightPuzzle
{
    private int[,] vitoria = { { 1, 2, 3 }, { 4, 5, 6 }, { 7, 8, 0 } };

    private int[,] initialState;

    public EightPuzzle(int[,] initialState)
    {
        this.initialState = initialState;
    }

    public void Solve(EightPuzzle eightPuzzle)
    {
        Console.WriteLine("Solução não implementada ainda.");
        int[] posiicaoDo1 = FindNumber(1);
        int[] posiicaoDo2 = FindNumber(2);
        int[] posiicaoDo3 = FindNumber(3);
        int[] posiicaoDo4 = FindNumber(4);
        int[] posiicaoDo5 = FindNumber(5);
        int[] posiicaoDo6 = FindNumber(6);
        int[] posiicaoDo7 = FindNumber(7);
        int[] posiicaoDo8 = FindNumber(8);
        int[] posiicaoDo0 = FindNumber(0);
        Console.WriteLine($"1 - {posiicaoDo1[0]}, {posiicaoDo1[1]}).");
        Console.WriteLine($"2 - {posiicaoDo2[0]}, {posiicaoDo2[1]}).");
        Console.WriteLine($"3 - {posiicaoDo3[0]}, {posiicaoDo3[1]}).");
        Console.WriteLine($"4 - {posiicaoDo4[0]}, {posiicaoDo4[1]}).");
        Console.WriteLine($"5 - {posiicaoDo5[0]}, {posiicaoDo5[1]}).");
        Console.WriteLine($"6 - {posiicaoDo6[0]}, {posiicaoDo6[1]}).");
        Console.WriteLine($"7 - {posiicaoDo7[0]}, {posiicaoDo7[1]}).");
        Console.WriteLine($"8 - {posiicaoDo8[0]}, {posiicaoDo8[1]}).");
        Console.WriteLine($"0 - {posiicaoDo0[0]}, {posiicaoDo0[1]}).");


    }

    private int[] FindNumber(int number)
    {
        int rows = initialState.GetLength(0);
        int cols = initialState.GetLength(1);

        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < cols; j++)
            {
                if (initialState[i, j] == number)
                {
                    return new int[] { i, j };
                }
            }
        }

        return null;
    }
}

class Program
{
    static void Main(string[] args)
    {
        int[,] puzzle = {
            { 4, 5, 7 },
            { 1, 6, 8 },
            { 2, 3, 0 } // O espaço vazio é representado por 0
        };
        

        EightPuzzle eightPuzzle = new EightPuzzle(puzzle);
        eightPuzzle.Solve(eightPuzzle);
    }
}