using System;

namespace MatrixCalculator
{
  class Program
  {
    static void Main()
    {
      string input;

      try
      {
        Console.Write("Enter matrix size: ");
        input = Console.ReadLine();

        if (!int.TryParse(input, out int matrixSize) || matrixSize <= 0) {
          Console.WriteLine("Invalid size. Using size 2.");
          matrixSize = 2;
        }

        SquareMatrix firstMatrix = new SquareMatrix(matrixSize, true);
        SquareMatrix secondMatrix = new SquareMatrix(matrixSize, true);

        Console.WriteLine("\nMatrix 1:");
        Console.WriteLine(firstMatrix);

        Console.WriteLine("Matrix 2:");
        Console.WriteLine(secondMatrix);

        Console.WriteLine("Addition:");
        Console.WriteLine(firstMatrix + secondMatrix);

        Console.WriteLine("Multiplication:");
        Console.WriteLine(firstMatrix * secondMatrix);

        Console.WriteLine($"Determinant of matrix 1: {firstMatrix.Determinant():F4}");
        Console.WriteLine($"Determinant of matrix 2: {secondMatrix.Determinant():F4}");

        if (firstMatrix) {
          Console.WriteLine("Matrix 1 is non-singular");
        }
        else {
          Console.WriteLine("Matrix 1 is singular");
        }

        Console.WriteLine($"Matrix 1 > Matrix 2: {firstMatrix > secondMatrix}");
        Console.WriteLine($"Matrix 1 < Matrix 2: {firstMatrix < secondMatrix}");
        Console.WriteLine($"Matrix 1 >= Matrix 2: {firstMatrix >= secondMatrix}");
        Console.WriteLine($"Matrix 1 <= Matrix 2: {firstMatrix <= secondMatrix}");
        Console.WriteLine($"Matrix 1 == Matrix 2: {firstMatrix == secondMatrix}");
        Console.WriteLine($"Matrix 1 != Matrix 2: {firstMatrix != secondMatrix}");

        SquareMatrix clonedMatrix = (SquareMatrix)firstMatrix.Clone();
        Console.WriteLine("Clone of matrix 1:");
        Console.WriteLine(clonedMatrix);

        Console.WriteLine($"Original and clone are equal: {firstMatrix == clonedMatrix}");

        try
        {
          SquareMatrix inverseMatrix = firstMatrix.Inverse();
          Console.WriteLine("Inverse matrix:");
          Console.WriteLine(inverseMatrix);
        }
        catch (MatrixSingularException exception)
        {
          Console.WriteLine(exception.Message);
        }
      }
      catch (MatrixDimensionException exception)
      {
        Console.WriteLine($"Dimension error: {exception.Message}");
      }
      catch (MatrixSingularException exception)
      {
        Console.WriteLine($"Error: {exception.Message}");
      }
      catch (Exception exception)
      {
        Console.WriteLine($"Unknown error: {exception.Message}");
      }

      Console.ReadKey();
    }
  }
}