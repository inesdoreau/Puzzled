using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class PuzzlePiece : MonoBehaviour
{
    public Collider pieceCollider;
    public MeshRenderer pieceRenderer;

    public bool isTaken = false;
    public PieceController correctPiece;


    private void Awake()
    {
        pieceCollider = GetComponent<Collider>();
        pieceRenderer = GetComponent<MeshRenderer>();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PieceIsPlaced(bool _isTaken)
    {
        isTaken = _isTaken;
        if(isTaken)
        {
            pieceRenderer.enabled = false;
        }
        else
        {
            pieceRenderer.enabled = true;
        }
    }

    public bool CheckCorrect()
    {
        if(correctPiece.transform.position == transform.position)
        {
            return true;
        }
        
        return false;
    }
}
