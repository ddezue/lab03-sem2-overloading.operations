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
    public MatrixDimensionException() : base("Error: matrix dimensions do not match") { }
  }

  public class MatrixSingularException : MatrixException
  {
    public MatrixSingularException() : base("Error: matrix is singular") { }
  }
}