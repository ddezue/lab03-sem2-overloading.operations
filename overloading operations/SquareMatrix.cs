using System;

namespace MatrixCalculator
{
  public class SquareMatrix : ICloneable, IComparable
  {
    private double[,] _data;
    private int _size;
    private static Random _random;

    static SquareMatrix()
    {
      _random = new Random();
    }

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

    public SquareMatrix(int size, bool randomFill) : this(size)
    {
      if (randomFill) {
        for (int rowIndex = 0; rowIndex < size; ++rowIndex) {
          for (int columnIndex = 0; columnIndex < size; ++columnIndex) {
            _data[rowIndex, columnIndex] = _random.Next(-10, 10);
          }
        }
      }
    }

    public SquareMatrix(SquareMatrix other)
    {
      _size = other._size;
      _data = new double[_size, _size];

      for (int rowIndex = 0; rowIndex < _size; ++rowIndex) {
        for (int columnIndex = 0; columnIndex < _size; ++columnIndex) {
          _data[rowIndex, columnIndex] = other._data[rowIndex, columnIndex];
        }
      }
    }

    public double this[int rowIndex, int columnIndex]
    {
      get { return _data[rowIndex, columnIndex]; }
      set { _data[rowIndex, columnIndex] = value; }
    }

    public static SquareMatrix operator +(SquareMatrix left, SquareMatrix right)
    {
      if (left._size != right._size) {
        throw new MatrixDimensionException();
      }

      SquareMatrix result = new SquareMatrix(left._size);

      for (int rowIndex = 0; rowIndex < left._size; ++rowIndex) {
        for (int columnIndex = 0; columnIndex < left._size; ++columnIndex) {
          result[rowIndex, columnIndex] = left[rowIndex, columnIndex] + right[rowIndex, columnIndex];
        }
      }

      return result;
    }

    public static SquareMatrix operator *(SquareMatrix left, SquareMatrix right)
    {
      double sum;
      sum = 0.0;

      if (left._size != right._size) {
        throw new MatrixDimensionException();
      }

      SquareMatrix result = new SquareMatrix(left._size);

      for (int rowIndex = 0; rowIndex < left._size; ++rowIndex) {
        for (int columnIndex = 0; columnIndex < left._size; ++columnIndex) {
          for (int innerIndex = 0; innerIndex < left._size; ++innerIndex) {
            sum += left[rowIndex, innerIndex] * right[innerIndex, columnIndex];
          }

          result[rowIndex, columnIndex] = sum;
        }
      }

      return result;
    }

    public static bool operator >(SquareMatrix left, SquareMatrix right)
    {
      return left.Determinant() > right.Determinant();
    }

    public static bool operator <(SquareMatrix left, SquareMatrix right)
    {
      return left.Determinant() < right.Determinant();
    }

    public static bool operator >=(SquareMatrix left, SquareMatrix right)
    {
      return left.Determinant() >= right.Determinant();
    }

    public static bool operator <=(SquareMatrix left, SquareMatrix right)
    {
      return left.Determinant() <= right.Determinant();
    }

    public static bool operator ==(SquareMatrix left, SquareMatrix right)
    {
      if (ReferenceEquals(left, null) && ReferenceEquals(right, null)) {
        return true;
      }

      if (ReferenceEquals(left, null) || ReferenceEquals(right, null)) {
        return false;
      }

      if (left._size != right._size) {
        return false;
      }

      for (int rowIndex = 0; rowIndex < left._size; ++rowIndex) {
        for (int columnIndex = 0; columnIndex < left._size; ++columnIndex) {
          if (left[rowIndex, columnIndex] != right[rowIndex, columnIndex]) {
            return false;
          }
        }
      }

      return true;
    }

    public static bool operator !=(SquareMatrix left, SquareMatrix right)
    {
      return !(left == right);
    }

    public static implicit operator double[,](SquareMatrix matrix)
    {
      double[,] result;

      result = new double[matrix._size, matrix._size];

      for (int rowIndex = 0; rowIndex < matrix._size; ++rowIndex) {
        for (int columnIndex = 0; columnIndex < matrix._size; ++columnIndex) {
          result[rowIndex, columnIndex] = matrix[rowIndex, columnIndex];
        }
      }

      return result;
    }

    public static implicit operator SquareMatrix(double[,] array)
    {
      int size;

      size = array.GetLength(0);
      SquareMatrix result = new SquareMatrix(size);

      for (int rowIndex = 0; rowIndex < size; ++rowIndex) {
        for (int columnIndex = 0; columnIndex < size; ++columnIndex) {
          result[rowIndex, columnIndex] = array[rowIndex, columnIndex];
        }
      }

      return result;
    }

    public static bool operator true(SquareMatrix matrix)
    {
      return matrix.Determinant() != 0;
    }

    public static bool operator false(SquareMatrix matrix)
    {
      return matrix.Determinant() == 0;
    }

    public double Determinant()
    {
      return CalculateDeterminant(_data, _size);
    }

    private double CalculateDeterminant(double[,] matrix, int size)
    {
      double sign;
      int subRowIndex;
      double determinant;
      double[,] submatrix;
      determinant = 0.0;
      subRowIndex = 0;

      if (size == 1) {
        return matrix[0, 0];
      }

      if (size == 2) {
        return matrix[0, 0] * matrix[1, 1] - matrix[0, 1] * matrix[1, 0];
      }

      submatrix = new double[size - 1, size - 1];

      for (int columnIndex = 0; columnIndex < size; ++columnIndex) {
        for (int rowIndex = 1; rowIndex < size; ++rowIndex) {
          int subColumnIndex;
          subColumnIndex = 0;

          for (int innerColumnIndex = 0; innerColumnIndex < size; ++innerColumnIndex) {
            if (innerColumnIndex == columnIndex) {
              continue;
            }

            submatrix[subRowIndex, subColumnIndex] = matrix[rowIndex, innerColumnIndex];
            ++subColumnIndex;
          }

          ++subRowIndex;
        }

        if (columnIndex % 2 == 0) {
          sign = 1.0;
        }
        else {
          sign = -1.0;
        }

        determinant += sign * matrix[0, columnIndex] * CalculateDeterminant(submatrix, size - 1);
      }
      return determinant;
    }

    public SquareMatrix Inverse()
    {
      int minorSize;
      double determinant;
      double[,] minorMatrix;
      double minorDeterminant;
      int sign;
      minorSize = _size - 1;

      determinant = Determinant();

      if (determinant == 0) {
        throw new MatrixSingularException();
      }

      if (_size == 1) {
        SquareMatrix result = new SquareMatrix(1);
        result[0, 0] = 1.0 / _data[0, 0];

        return result;
      }

      SquareMatrix inverse = new SquareMatrix(_size);
      double[,] adjugate = new double[_size, _size];

      for (int rowIndex = 0; rowIndex < _size; ++rowIndex) {
        for (int columnIndex = 0; columnIndex < _size; ++columnIndex) {
          minorMatrix = GetMinorMatrix(_data, rowIndex, columnIndex, _size);
          minorDeterminant = CalculateDeterminant(minorMatrix, minorSize);
          if ((rowIndex + columnIndex) % 2 == 0) {
            sign = 1;
          }
          else {
            sign = -1;
          }
          adjugate[columnIndex, rowIndex] = sign * minorDeterminant;
        }
      }

      for (int rowIndex = 0; rowIndex < _size; ++rowIndex) {
        for (int columnIndex = 0; columnIndex < _size; ++columnIndex) {
          inverse[rowIndex, columnIndex] = adjugate[rowIndex, columnIndex] / determinant;
        }
      }

      return inverse;
    }

    private double[,] GetMinorMatrix(double[,] matrix, int excludedRow, int excludedColumn, int size)
    {
      int minorSize;
      double[,] minor;
      int minorRowIndex;
      int minorColumnIndex;
      minorRowIndex = 0;
      minorColumnIndex = 0;
      minorSize = _size - 1;

      minor = new double[minorSize, minorSize];

      for (int rowIndex = 0; rowIndex < size; ++rowIndex) {
        if (rowIndex == excludedRow) {
          continue;
        }

        for (int columnIndex = 0; columnIndex < size; ++columnIndex) {
          if (columnIndex == excludedColumn) {
            continue;
          }

          minor[minorRowIndex, minorColumnIndex] = matrix[rowIndex, columnIndex];
          ++minorColumnIndex;
        }

        ++minorRowIndex;
      }

      return minor;
    }

    public object Clone()
    {
      return new SquareMatrix(this);
    }

    int IComparable.CompareTo(object other)
    {
      double thisDeterminant;
      double otherDeterminant;

      if (other is SquareMatrix) {
        SquareMatrix param = other as SquareMatrix;
        thisDeterminant = this.Determinant();
        otherDeterminant = param.Determinant();

        if (otherDeterminant > thisDeterminant) return -1;
        if (otherDeterminant == thisDeterminant) return 0;
        if (otherDeterminant < thisDeterminant) return 1;
      }
      return -1;
    }

    public override bool Equals(object other)
    {
      bool result;

      result = false;
      if (other is SquareMatrix) {
        SquareMatrix param = other as SquareMatrix;
        if (this == param) {
          result = true;
        }
      }
      return result;
    }

    public override int GetHashCode()
    {
      return (int)this.Determinant();
    }

    public override string ToString()
    {
      string result;
      result = "";

      for (int rowIndex = 0; rowIndex < _size; ++rowIndex) {
        for (int columnIndex = 0; columnIndex < _size; ++columnIndex) {
          result += _data[rowIndex, columnIndex].ToString("F2") + " ";
        }
        result += Environment.NewLine;
      }
      return result;
    }
  }
}