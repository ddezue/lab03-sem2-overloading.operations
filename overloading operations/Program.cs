using System;

namespace MatrixCalculator
{
  class Program
  {
    static void Main(string[] args)
    {
      try
      {
        Console.Write("Введите размер матриц: ");
        string input = Console.ReadLine();

        if (!int.TryParse(input, out int matrixSize) || matrixSize <= 0)
        {
          Console.WriteLine("Некорректный размер. Используется размер 2.");
          matrixSize = 2;
        }

        SquareMatrix firstMatrix = new SquareMatrix(matrixSize, true);
        SquareMatrix secondMatrix = new SquareMatrix(matrixSize, true);

        Console.WriteLine("\nМатрица 1:");
        Console.WriteLine(firstMatrix);

        Console.WriteLine("Матрица 2:");
        Console.WriteLine(secondMatrix);

        Console.WriteLine("Сложение:");
        Console.WriteLine(firstMatrix + secondMatrix);

        Console.WriteLine("Умножение:");
        Console.WriteLine(firstMatrix * secondMatrix);

        Console.WriteLine($"Детерминант матрицы 1: {firstMatrix.Determinant():F4}");
        Console.WriteLine($"Детерминант матрицы 2: {secondMatrix.Determinant():F4}");

        if (firstMatrix)
        {
          Console.WriteLine("Матрица 1 невырожденная");
        }
        else
        {
          Console.WriteLine("Матрица 1 вырожденная");
        }

        Console.WriteLine($"Матрица 1 > Матрица 2: {firstMatrix > secondMatrix}");
        Console.WriteLine($"Матрица 1 < Матрица 2: {firstMatrix < secondMatrix}");
        Console.WriteLine($"Матрица 1 >= Матрица 2: {firstMatrix >= secondMatrix}");
        Console.WriteLine($"Матрица 1 <= Матрица 2: {firstMatrix <= secondMatrix}");
        Console.WriteLine($"Матрица 1 == Матрица 2: {firstMatrix == secondMatrix}");
        Console.WriteLine($"Матрица 1 != Матрица 2: {firstMatrix != secondMatrix}");

        SquareMatrix clonedMatrix = (SquareMatrix)firstMatrix.Clone();
        Console.WriteLine("Клон матрицы 1:");
        Console.WriteLine(clonedMatrix);

        Console.WriteLine($"Оригинал и клон равны: {firstMatrix == clonedMatrix}");

        try
        {
          SquareMatrix inverseMatrix = firstMatrix.Inverse();
          Console.WriteLine("Обратная матрица:");
          Console.WriteLine(inverseMatrix);
        }
        catch (MatrixSingularException exception)
        {
          Console.WriteLine(exception.Message);
        }
      }
      catch (MatrixDimensionException exception)
      {
        Console.WriteLine($"Ошибка размерности: {exception.Message}");
      }
      catch (MatrixSingularException exception)
      {
        Console.WriteLine($"Ошибка: {exception.Message}");
      }
      catch (Exception exception)
      {
        Console.WriteLine($"Неизвестная ошибка: {exception.Message}");
      }

      Console.ReadKey();
    }
  }
}