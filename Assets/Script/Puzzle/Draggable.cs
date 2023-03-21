using System;
using UnityEngine;

public class Draggable : MonoBehaviour
{
    // distance from the camera when dragging 
    [SerializeField] private float cameraDistance = 10.0f;
    // max distance for grabbing 
    [SerializeField] private float maxGrabDistance = 100.0f;

    // current state 
    State currentState;
    static bool isDragging = false;

    // all actions
    public event Action OnOver;
    public event Action OnClick;
    public event Action OnDrag;
    public event Action OnDrop;

    // states 
    enum State
    {
        Ready,
        Dragging,
        Blocked
    }

    private Rigidbody objectRigidbody;
    private Transform objectGrabPointTransform;

    private void Awake()
    {
        objectRigidbody = GetComponent<Rigidbody>();
        currentState = State.Ready;
    }

    public void Grab(Transform objectGrabPointTransform)
    {
        this.objectGrabPointTransform = objectGrabPointTransform;
        objectRigidbody.useGravity = false;
        objectRigidbody.isKinematic = true;

        if (OnDrag != null)
            OnDrag();
    }

    public void Drop()
    {
        this.objectGrabPointTransform = null;
        objectRigidbody.useGravity = true;
        objectRigidbody.isKinematic = false;

        if (OnDrop != null)
            OnDrop();
    }

    private void FixedUpdate()
    {
        if (objectGrabPointTransform != null)
        {
            float lerpSpeed = 10f;
            Vector3 newPosition = Vector3.Lerp(transform.position, objectGrabPointTransform.position, Time.deltaTime * lerpSpeed);
            objectRigidbody.MovePosition(newPosition);
        }
    }
        
    // block a draggable item
    public void ToggleBlock(bool isBlock)
    {
        if (isBlock)
        {
            currentState = State.Blocked;
        }
        else
        {
            currentState = State.Ready;
        }
    }
}
