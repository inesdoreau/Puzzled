using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PuzzleSlot : MonoBehaviour
{
    
    [SerializeField] private PuzzlePiece correctPiece;
    [SerializeField] private GameObject puzzlePiece;

    private Collider pieceCollider;
    private Outline outline;

    private Color correctColor = Color.green;
    private Color wrongColor = Color.red;

    public bool isTaken { get; private set; }
    public bool selectedDrop { get; private set; }


    private void Awake()
    {       
        pieceCollider = GetComponent<Collider>();
        outline = GetComponent<Outline>();
        outline.enabled = false;
    }

    // Start is called before the first frame update
    void Start()
    {
        if(correctPiece == null)
        {
            Debug.LogError("No correct piece set for the slot", this);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PieceIsPlaced(bool _isTaken)
    {
        outline.enabled = !_isTaken;
        pieceCollider.enabled = !_isTaken;
        isTaken = _isTaken;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag.Equals("PuzzlePiece"))
        {
            other.gameObject.TryGetComponent(out PuzzlePiece piece);
            if(piece != null )
            {
                if(piece.isPlaced)
                {
                    return;
                }
                if(piece == correctPiece)
                {
                    outline.OutlineColor = correctColor;
                }
                else
                {
                    outline.OutlineColor = wrongColor;
                }
                outline.enabled = true;
                selectedDrop = true;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {        
        outline.enabled = false;
        selectedDrop = false;
    }

    public bool CheckCorrect()
    {
        if(correctPiece.transform.position == transform.position)
        {
            puzzlePiece.SetActive(false);
            return true;
        }
        
        return false;
    }
}
