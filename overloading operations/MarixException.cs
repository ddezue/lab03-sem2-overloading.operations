using System;

namespace MatrixCalculator
{
  public class MatrixException : Exception
  {
    public MatrixException() : base() { }
    public MatrixException(string message) : base(message) { }
  }

  public class MatrixDimensionException : MatrixException
  {
    public MatrixDimensionException() : base("Ошибка: несовпадение размерностей матриц") { }
  }

  public class MatrixSingularException : MatrixException
  {
    public MatrixSingularException() : base("Ошибка: матрица вырождена") { }
  }
}