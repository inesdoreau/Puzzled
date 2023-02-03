using System;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleController : MonoBehaviour
{
    [InspectorName("Pieces")]
    //public float pieceSize;
    public List<PuzzlePiece> pieces = new List<PuzzlePiece>();

    
    public event Action OnCompleted;
    public event Action OnFailed;

    private void Awake()
    {
        pieces.Clear();
        for (int i = 0; i < transform.childCount; i++)
        {
            if (transform.GetChild(i).GetComponent<PuzzlePiece>())
            {
                pieces.Add(transform.GetChild(i).GetComponent<PuzzlePiece>());
            }
            else
            {
                Debug.LogError("One children of the transform does not contain PuzzlePiece script", transform.GetChild(i));
            }
        }
    }

    public PuzzlePiece GetPuzzlePieceFromCollider()
    {
        foreach (PuzzlePiece piece in pieces)
        {
            if (piece.selectedDrop && !piece.isTaken)
            {
                return piece;
            }
        }

        return null;
    }


    public PuzzlePiece GetPuzzlePieceFromChild(Transform child)
    {
        foreach (PuzzlePiece piece in pieces)
        {
            if (child.parent == piece.transform)
            {
                return piece;
            }
        }

        return null;
    }


    // Checks for completion of the puzzle
    public void CheckCompletion()
    {
        // keep track of correctness
        bool isCorrect = true;
        foreach (PuzzlePiece piece in pieces)
        {
            isCorrect = isCorrect && piece.CheckCorrect();
        }

        // that they are all correct
        if (isCorrect)
        {
            // call puzzle completion event
            Debug.Log("Puzzle is complete");
            if (OnCompleted != null) OnCompleted();
        }
        else
        {
            // call puzzle completion event
            Debug.Log("Errors is the puzzle");
            if (OnFailed != null) OnFailed();
        }
    }
}
