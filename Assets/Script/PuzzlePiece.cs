using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(Draggable))]
public class PuzzlePiece : MonoBehaviour
{
    [SerializeField] private PuzzleController puzzle;

    public bool isPlaced { get; set; }

    //To Do : Activate animation ? 
    //public Animation pieceAnimation;

    Draggable drag;
    Rigidbody _rigidbody;
    //public List<Collider> pieceColliders;

    private void Awake()
    {
        drag = GetComponent<Draggable>();
        _rigidbody= GetComponent<Rigidbody>();

        if(GetComponent<Collider>() == null)
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                if (transform.GetChild(i).GetComponent<Collider>())
                {
                    MeshFilter childMesh = transform.GetChild(i).GetComponent<MeshFilter>();
                    MeshCollider newCollider = transform.AddComponent<MeshCollider>();
                    newCollider.sharedMesh = childMesh.sharedMesh;
                    newCollider.convex = true;
                }
               
            }
        }
        
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

        PuzzleSlot slot = puzzle.GetPuzzlePieceFromChild(transform);

        if (slot != null)
        {
            slot.PieceIsPlaced(false);
        }

        transform.SetParent(null);
        isPlaced = false;
    }

    // Takes care of what happens when you drop the piece on the puzzle
    void HandlePuzzleDrop()
    {
        PuzzleSlot slot = puzzle.GetPuzzlePieceFromCollider();

        if(slot != null && !slot.isTaken)
        {
            slot.PieceIsPlaced(true);
            isPlaced = true;
            _rigidbody.isKinematic = true;
            transform.SetParent(slot.transform);

            transform.localPosition = Vector3.zero;
            transform.localRotation = Quaternion.identity;

            puzzle.CheckCompletion();
        }
    }


}
