using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PuzzleController : MonoBehaviour
{
    [InspectorName("Pieces")]
    //public float pieceSize;
    public List<PuzzleSlot> pieces = new List<PuzzleSlot>();

    
    public event Action OnCompleted;
    public event Action OnFailed;

    private void Awake()
    {
        pieces.Clear();
        for (int i = 0; i < transform.childCount; i++)
        {
            if (transform.GetChild(i).GetComponent<PuzzleSlot>())
            {
                pieces.Add(transform.GetChild(i).GetComponent<PuzzleSlot>());
            }
            else
            {
                Debug.Log("One children of the transform does not contain PuzzlePiece script", transform.GetChild(i));
            }
        }
    }

    public PuzzleSlot GetPuzzlePieceFromCollider()
    {
        foreach (PuzzleSlot piece in pieces)
        {
            if (piece.selectedDrop && !piece.isTaken)
            {
                return piece;
            }
        }

        return null;
    }


    public PuzzleSlot GetPuzzlePieceFromChild(Transform child)
    {
        foreach (PuzzleSlot piece in pieces)
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
        foreach (PuzzleSlot piece in pieces)
        {
            isCorrect = isCorrect && piece.CheckCorrect();
        }

        // that they are all correct
        if (isCorrect)
        {
            // call puzzle completion event
            Debug.Log("Puzzle is complete");
            PlayerPrefs.SetInt("currentScene", SceneManager.GetActiveScene().buildIndex) ;
            SceneManager.LoadScene(0);
            if (OnCompleted != null) OnCompleted();
        }
        else
        {
            // call puzzle completion event
            Debug.Log("Errors in the puzzle");
            if (OnFailed != null) OnFailed();
        }
    }
}
