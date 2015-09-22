using UnityEngine;
using System.Collections;

public class Array2D
{
    int rowNum = 5;
    int colNum = 5;

    public object[][] Array;

    public Array2D(int row, int col)
    {
        if (row < 1)
        {
            row = 1;
        }
        if (col < 1)
        {
            col = 1;
        }
        Array = new object[row][];
        for (int i = 0; i < row; i++)
        {
            Array[i] = new object[col];
        }
    }

    //Working
    public void TransposeClockwise()
    {
        object[][] tempArray = new object[rowNum][];
        for (int i = 0; i < rowNum; i++)
        {
            tempArray[i] = new object[colNum];
        }
        for (int i = 0; i < rowNum; i++)
        {
            for (int j = colNum -1; j >= 0; j--)
            {
                tempArray[i][colNum - j -1] = Array[j][i];
            }
        }
        Array = tempArray;
    }

    //Working too
    public void TransposeAntiClockwise()
    {
        object[][] tempArray = new object[rowNum][];
        for (int i = 0; i < rowNum; i++)
        {
            tempArray[i] = new object[colNum];
        }
        for (int i = 0; i < rowNum; i++)
        {
            for (int j = colNum -1; j >= 0; j--)
            {
                tempArray[j][i] = Array[i][colNum - j - 1];
            }
        }
        Array = tempArray;
    }
}