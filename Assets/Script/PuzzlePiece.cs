using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class PuzzlePiece : MonoBehaviour
{
    public Collider pieceCollider;
    public List<MeshRenderer> pieceRenderers;

    public bool isTaken = false;
    public PieceController correctPiece;


    private void Awake()
    {
        pieceCollider = GetComponent<Collider>();

        pieceRenderers.Clear();

        if(transform.childCount == 0)
        {
            pieceRenderers.Add(transform.GetComponent<MeshRenderer>());
        }
        else
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                if (transform.GetChild(i).GetComponent<MeshRenderer>())
                {
                    pieceRenderers.Add(transform.GetChild(i).GetComponent<MeshRenderer>());
                }
                else
                {
                    Debug.LogError("One children of the transform does not contain Mesh Renderer", transform.GetChild(i));
                }
            }

        }
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
        pieceCollider.enabled = !_isTaken;
        pieceRenderers.ForEach(r => r.enabled = !_isTaken);
        isTaken = _isTaken;
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
