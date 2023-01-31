using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PuzzleController : MonoBehaviour
{
    [InspectorName("Puzzle")]
    public int columns;
    public int rows; 
    float width;
    float height;
    float depth;


    [InspectorName("Pieces")]
    public float pieceSize;
    public PieceController[] pieces;

    
    public event Action OnCompleted;
    public event Action OnFailed;

    private void Awake()
    {
        width = columns * pieceSize;
        height = rows * pieceSize;
    }

    // Get the cell (col, row) given a point in space
    public Vector2 GetCellFromPoint(Vector3 point)
    {
        Vector3 localPoint = transform.InverseTransformPoint(point);
        depth = localPoint.z;

        localPoint = Vector3.Scale(localPoint, transform.localScale);

        // get cell (col, row)
        // assumptions: To change for futur game especially 2
        // 1) the horizontal coordinate is x (not z, z is the depth)
        // 2) the anchor point of the puzzle is in the center
        // 3) size of the puzzle match exactly col * rows * pieceSize (equivalent: no padding)
        // 4) the face of the puzzle is not rotated about X or Z

        float column = Mathf.Floor((localPoint.x + width / 2) / pieceSize);
        float row = Mathf.Floor((localPoint.y + height / 2) / pieceSize);

        return new Vector2(column, row);
    }


    //Check whether a cell is taken or not
    public bool IsTaken(Vector2 cell)
    {
        for (int i = 0; i < pieces.Length; i++)
        {
            if (pieces[i].isPlaced && pieces[i].currCell == cell)
            {
                //print("Cell is taken!");
                return true;
            }
        }
        //print("Cell is free");
        return false;
    }

    // Give a cell's col,row, get the global coordinate of the center of the cell
    public Vector3 GetCellPosition(Vector2 cell)
    {
        // go from cell's col,row --> local point
        float x = (-width / 2 + pieceSize / 2 + cell.x * pieceSize) / transform.localScale.x;
        float y = (-height / 2 + pieceSize / 2 + cell.y * pieceSize) / transform.localScale.y;

        Vector3 localPoint = new Vector3(x, y, depth);

        // go from local point --> global point
        Vector3 globalPoint = transform.TransformPoint(localPoint);

        return globalPoint;
    }

    // Checks for completion of the puzzle
    public void CheckCompletion()
    {
        // keep track of correctness
        bool isCorrect = true;

        for (int i = 0; i < pieces.Length; i++)
        {
            // check that all the pieces are placed
            if (!pieces[i].isPlaced) return;

            // keep track of this "Correctness"
            isCorrect = isCorrect && pieces[i].CheckCorrect();
        }

        // that they are all correct
        if (isCorrect)
        {
            // call puzzle completion event
            if (OnCompleted != null) OnCompleted();
        }
        else
        {
            // call puzzle completion event
            if (OnFailed != null) OnFailed();
        }
    }
}
