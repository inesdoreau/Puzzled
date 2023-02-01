using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Draggable))]
public class PieceController : MonoBehaviour
{
    public PuzzleController puzzle;

    public bool isPlaced;

    //To Do : Activate animation ? 
    //public Animation pieceAnimation;

    Draggable drag;
    Rigidbody _rigidbody;

    private void Awake()
    {
        drag = GetComponent<Draggable>();
        _rigidbody= GetComponent<Rigidbody>();
    }

    void OnEnable()
    {
        drag.OnDrag += HandleDrag;
        drag.OnDrop += HandlePuzzleDrop;
        //puzzle.OnCompleted += HandleCompletion;
        //puzzle.OnFailed += HandleFailure;
    }

    void OnDisable()
    {
        drag.OnDrag -= HandleDrag;
        drag.OnDrop -= HandlePuzzleDrop;
        puzzle.OnCompleted -= HandleCompletion;
        //puzzle.OnFailed -= HandleFailure;
    }

    void HandleFailure()
    {
        // Set the flag to false
        isPlaced = false;
    }

    // handle a successful puzzle completion
    void HandleCompletion()
    {
        // block the piece
        drag.ToggleBlock(true);
    }


    void HandleDrag()
    {
        // Set the flag to false
        _rigidbody.isKinematic = false;

        PuzzlePiece cell = puzzle.GetPuzzlePieceFromChild(transform);

        if (cell != null)
        {
            cell.PieceIsPlaced(false);
        }

        transform.SetParent(null);
    }

    // Takes care of what happens when you drop the piece on the puzzle
    void HandlePuzzleDrop()
    {
        PuzzlePiece cell = puzzle.GetPuzzlePieceFromCollider(GetComponent<Collider>());

        if(cell != null && !cell.isTaken)
        {
            _rigidbody.isKinematic = true;
            cell.PieceIsPlaced(true);
            transform.SetParent(cell.transform);

            transform.localPosition = Vector3.zero;
            transform.localRotation = Quaternion.identity;

            puzzle.CheckCompletion();
        }
    }

}
