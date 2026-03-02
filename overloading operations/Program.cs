using System;

namespace MatrixCalculator
{
  public class SquareMatrix
  {
    private double[,] _data;
    private int _size;
    private Random _random = new Random();

    public SquareMatrix()
    {
      _size = 2;
      _data = new double[2, 2];
    }

    public SquareMatrix(int size)
    {
      _size = size;
      _data = new double[size, size];
    }

    public SquareMatrix(int size, bool randomFill)
    {
      _size = size;
      _data = new double[size, size];

      if (randomFill)
      {
        for (int rowIndex = 0; rowIndex < size; ++rowIndex)
        {
          for (int columnIndex = 0; columnIndex < size; ++columnIndex)
          {
            _data[rowIndex, columnIndex] = _random.Next(-10, 10);
          }
        }
      }
    }

    public int Size
    {
      get { return _size; }
    }

    public double this[int rowIndex, int columnIndex]
    {
      get { return _data[rowIndex, columnIndex]; }
      set { _data[rowIndex, columnIndex] = value; }
    }

    public static SquareMatrix operator +(SquareMatrix leftMatrix, SquareMatrix rightMatrix)
    {
      if (leftMatrix._size != rightMatrix._size)
      {
        throw new Exception("Размеры матриц не совпадают");
      }

      SquareMatrix resultMatrix = new SquareMatrix(leftMatrix._size);

      for (int rowIndex = 0; rowIndex < leftMatrix._size; ++rowIndex)
      {
        for (int columnIndex = 0; columnIndex < leftMatrix._size; ++columnIndex)
        {
          resultMatrix[rowIndex, columnIndex] = leftMatrix[rowIndex, columnIndex] + rightMatrix[rowIndex, columnIndex];
        }
      }

      return resultMatrix;
    }


    public double Determinant()
    {

      if (_size == 2)
      {
        return _data[0, 0] * _data[1, 1] - _data[0, 1] * _data[1, 0];
      }


      return 0;
    }


  }

  class Program
  {
    static void Main(string[] args)
    {
      Console.WriteLine("Введите размер матрицы:");
      string userInput = Console.ReadLine();

      if (!int.TryParse(userInput, out int matrixSize))
      {
        Console.WriteLine("Некорректный ввод. Используем размер 2.");
        matrixSize = 2;
      }

      SquareMatrix firstMatrix = new SquareMatrix(matrixSize, true);
      SquareMatrix secondMatrix = new SquareMatrix(matrixSize, true);

      Console.WriteLine("Матрица 1:");


      Console.WriteLine("Матрица 2:");


      Console.WriteLine("Результат сложения:");
      SquareMatrix sumMatrix = firstMatrix + secondMatrix;



    }
  }
}