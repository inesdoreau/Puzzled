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
    [SerializeField] private float scrollSpeed = 1f;

    [SerializeField] private LineRenderer lineRenderer;

    private void Start()
    {
        // Initialize line renderer
        //objectGrabPointTransform.position = new Vector3(objectGrabPointTransform.position.x,
        //                                    objectGrabPointTransform.position.y,
        //                                    objectGrabPointTransform.position.z + distanceFromGrabPoint);
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
                        objectGrabPointTransform.position = raycastHit.transform.position;
                        objectGrabbable.Grab(objectGrabPointTransform);
                    }

                }
            }
            else
            {
                // Currently carrying something, drop
                objectGrabbable.Drop();
                objectGrabbable = null;

                lineRenderer.enabled = false;
            }
        }

        if (objectGrabbable != null)
        {

            // Set line renderer positions to visualize raycast
            lineRenderer.enabled = true;
            lineRenderer.SetPosition(0, transform.position);
            lineRenderer.SetPosition(1, objectGrabPointTransform.position);

            Vector3 moveDirection =  Vector3.Cross(transform.position, objectGrabPointTransform.position);

            // Adjust movement direction based on scroll button input
            float scrollInput = Input.GetAxis("Mouse ScrollWheel");
            if (scrollInput > 0)
            {
                moveDirection += transform.forward * scrollSpeed;
                objectGrabPointTransform.position += moveDirection.normalized;
            }
            else if (scrollInput < 0)
            {
                moveDirection -= transform.forward * scrollSpeed;
                objectGrabPointTransform.position -= moveDirection.normalized;
            }

        }
    }

    private void OnMouseDown()
    {
       
    }
}
