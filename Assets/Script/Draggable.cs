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

    private void Awake()
    {
        currentState = State.Ready;
    }

    private void Update()
    {
        if (currentState == State.Dragging)
        {
            // Get the object to face the camera 
            Transform cameraTransform = Camera.main.transform;
            transform.position = cameraTransform.position + cameraTransform.forward * cameraDistance;
            transform.LookAt(cameraTransform.position);
        }
    }

    private void OnMouseDown()
    {
        // if it's ready, start dragging
        if (currentState == State.Ready)
        {
            // Check if the object is not too far 
            float distanceFromCamera = Vector3.Distance(transform.position, Camera.main.transform.position);

            if (distanceFromCamera > maxGrabDistance)
            {
                return;
            }
            currentState = State.Dragging;
            isDragging = true;

            print("Start Dragging");

            // Execute event
            if (OnDrag != null)
                OnDrag();
        }
        //else if (currentState == State.Dragging)
        //{
        //    // Set the state to Ready
        //    currentState = State.Ready;
        //    isDragging = false;

        //    // Execute event
        //    if (OnDrop != null)
        //        OnDrop();
        //}
    }

    private void OnMouseUp()
    {
        if (currentState == State.Dragging)
        {
            // Set the state to Ready
            currentState = State.Ready;
            isDragging = false;

            // Execute event
            if (OnDrop != null)
                OnDrop();
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
