using System.Collections;
using System.Text;

public class Matrix
{
    enum Rotation
    {
        None,
        Clockwise90,
        Clockwise180,
        Clockwise270
    }

    public Matrix(Tower[,] matrix)
    {
        m_matrix = matrix;
        m_rotation = Rotation.None;
    }

    //  the transformation routines
    public void RotateClockwise90()
    {
        m_rotation = (Rotation)(((int)m_rotation + 1) & 3);
    }

    public void Rotate180()
    {
        m_rotation = (Rotation)(((int)m_rotation + 2) & 3);
    }

    public void RotateAnitclockwise90()
    {
        m_rotation = (Rotation)(((int)m_rotation + 3) & 3);
    }

    //  accessor property to make class look like a two dimensional array
    public Tower this[int row, int column]
    {
        get
        {
            Tower
              value = null;

            switch (m_rotation)
            {
                case Rotation.None:
                    value = m_matrix[row, column];
                    break;

                case Rotation.Clockwise90:
                    value = m_matrix[m_matrix.GetUpperBound(0) - column, row];
                    break;

                case Rotation.Clockwise180:
                    value = m_matrix[m_matrix.GetUpperBound(0) - row, m_matrix.GetUpperBound(1) - column];
                    break;

                case Rotation.Clockwise270:
                    value = m_matrix[column, m_matrix.GetUpperBound(1) - row];
                    break;
            }

            return value;
        }

        set
        {
            switch (m_rotation)
            {
                case Rotation.None:
                    m_matrix[row, column] = value;
                    break;

                case Rotation.Clockwise90:
                    m_matrix[m_matrix.GetUpperBound(0) - column, row] = value;
                    break;

                case Rotation.Clockwise180:
                    m_matrix[m_matrix.GetUpperBound(0) - row, m_matrix.GetUpperBound(1) - column] = value;
                    break;

                case Rotation.Clockwise270:
                    m_matrix[column, m_matrix.GetUpperBound(1) - row] = value;
                    break;
            }
        }
    }

    //  creates a string with the matrix values
    public override string ToString()
    {
        int
          num_rows = 0,
          num_columns = 0;

        switch (m_rotation)
        {
            case Rotation.None:
            case Rotation.Clockwise180:
                num_rows = m_matrix.GetUpperBound(0);
                num_columns = m_matrix.GetUpperBound(1);
                break;

            case Rotation.Clockwise90:
            case Rotation.Clockwise270:
                num_rows = m_matrix.GetUpperBound(1);
                num_columns = m_matrix.GetUpperBound(0);
                break;
        }

        string output = string.Empty;

        output += '{';

        for (int row = 0; row <= num_rows; ++row)
        {
            if (row != 0)
            {
                output += ", ";
            }

            output+="{";

            for (int column = 0; column <= num_columns; ++column)
            {
                if (column != 0)
                {
                    output+=", ";
                }
                if (this[row, column] != null)
                {
                    output+=this[row, column].ToString();
                }
                else
                {
                    output+="null";
                }
            }

            output+="}";
        }

        output+="}";

        return output;
    }

    Tower[,]
        //  the original matrix
      m_matrix;

    Rotation
        //  the current view of the matrix
      m_rotation;
}