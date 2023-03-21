using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupObject : MonoBehaviour
{
    [SerializeField] private Transform playerCameraTransform;
    [SerializeField] private Transform objectGrabPointTransform;
    [SerializeField] private LayerMask pickUpLayerMask;

    private Draggable objectGrabbable;

    // max distance for grabbing 
    [SerializeField] private float maxGrabDistance = 100.0f;
    [SerializeField] private float distanceFromGrabPoint = 3.0f;

    private void Start()
    {
        objectGrabPointTransform.position = new Vector3(objectGrabPointTransform.position.x,
                                            objectGrabPointTransform.position.y,
                                            objectGrabPointTransform.position.z + distanceFromGrabPoint);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            Debug.Log("KeyDown");

            if (objectGrabbable == null)
            {
                // Not carrying an object, try to grab
                if (Physics.Raycast(playerCameraTransform.position, playerCameraTransform.forward, out RaycastHit raycastHit, maxGrabDistance, pickUpLayerMask))
                {
                    if (raycastHit.transform.TryGetComponent(out objectGrabbable))
                    {
                        objectGrabbable.Grab(objectGrabPointTransform);
                    }
                }
            }
            else
            {
                // Currently carrying something, drop
                objectGrabbable.Drop();
                objectGrabbable = null;
            }
        }
    }

    private void OnMouseDown()
    {
       
    }
}
