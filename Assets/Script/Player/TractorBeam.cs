using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Profiling.HierarchyFrameDataView;

public class TractorBeam : MonoBehaviour
{
    [SerializeField] 
    private Camera playerCamera;
    [SerializeField]
    private float _maxGrabDistance = 40f;
    [SerializeField]
    private float _minGrabDistance = 1f;
    [SerializeField]
    private LineRenderer _pickLine;
    [SerializeField] 
    private LayerMask pickUpLayerMask;

    private Draggable objectGrabbable;
    //private Rigidbody _grabbedObject;
    private float _pickDistance;
    private Vector3 _pickOffset;
    private Quaternion _rotationOffset;
    private Vector3 _pickTargetPosition;
    private Vector3 _pickForce;

    private void Start()
    {
        if (!_pickLine)
        {
            var obj = new GameObject("PhysGun Pick Line");
            _pickLine = obj.AddComponent<LineRenderer>();
            _pickLine.startWidth = 0.02f;
            _pickLine.endWidth = 0.02f;
            _pickLine.useWorldSpace = true;
            _pickLine.gameObject.SetActive(false);
        }
    }


    private void LateUpdate()
    {
        if (objectGrabbable)
        {
            var midpoint = playerCamera.transform.position + playerCamera.transform.forward * _pickDistance * .5f;
            DrawQuadraticBezierCurve(_pickLine, transform.position, midpoint, objectGrabbable.transform.position + objectGrabbable.transform.TransformVector(_pickOffset));
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            if (objectGrabbable == null)
            {
                Grab();
            }
            else
            {
                // Currently carrying something, drop
                Release();
            }
        }

        if(Input.mouseScrollDelta.y != 0)
        {
            OnScrollDelta(Input.mouseScrollDelta.y);
        }
        
    }

    private void FixedUpdate()
    {
        if (objectGrabbable != null)
        {
            var ray = playerCamera.ViewportPointToRay(Vector3.one * .5f);
            _pickTargetPosition = (ray.origin + ray.direction * _pickDistance) - objectGrabbable.transform.TransformVector(_pickOffset);

            var forceDir = _pickTargetPosition - objectGrabbable.transform.position;
            _pickForce = forceDir / Time.fixedDeltaTime * 0.3f / objectGrabbable.objectRigidbody.mass;
            objectGrabbable.objectRigidbody.velocity = _pickForce;
            objectGrabbable.transform.rotation = playerCamera.transform.rotation * _rotationOffset;

        }
    }

    private void Grab()
    {
        var ray = playerCamera.ViewportPointToRay(Vector3.one * .5f);
        if (Physics.Raycast(ray, out RaycastHit hit, _maxGrabDistance, pickUpLayerMask))
        {
            if (hit.transform.TryGetComponent(out objectGrabbable))
            {
                objectGrabbable.Grab();
                _rotationOffset = Quaternion.Inverse(playerCamera.transform.rotation) * hit.rigidbody.rotation;
                _pickOffset = hit.transform.InverseTransformVector(hit.point - hit.transform.position);
                _pickDistance = hit.distance;
                objectGrabbable.objectRigidbody = hit.rigidbody;
                objectGrabbable.objectRigidbody.collisionDetectionMode = CollisionDetectionMode.Continuous;
                objectGrabbable.objectRigidbody.useGravity = false;
                objectGrabbable.objectRigidbody.freezeRotation = true;
                objectGrabbable.objectRigidbody.isKinematic = false;
                _pickLine.enabled = true;
            }
        }
    }

    private void Release()
    {
        objectGrabbable.objectRigidbody.collisionDetectionMode = CollisionDetectionMode.Discrete;
        objectGrabbable.objectRigidbody.useGravity = true;
        objectGrabbable.objectRigidbody.freezeRotation = false;
        objectGrabbable.objectRigidbody.isKinematic = false;
        _pickLine.enabled = false;

        objectGrabbable.Drop();
        objectGrabbable = null;
    }
    private void OnScrollDelta(float delta)
    {
        _pickDistance = Mathf.Clamp(_pickDistance + delta, _minGrabDistance, _maxGrabDistance);
        //ViewModel.Kick(.2f * delta < 0 ? -1 : 1);
    }

    // https://www.codinblack.com/how-to-draw-lines-circles-or-anything-else-using-linerenderer/
    void DrawQuadraticBezierCurve(LineRenderer line, Vector3 point0, Vector3 point1, Vector3 point2)
    {
        line.positionCount = 20;
        float t = 0f;
        Vector3 B = new Vector3(0, 0, 0);
        for (int i = 0; i < line.positionCount; i++)
        {
            B = (1 - t) * (1 - t) * point0 + 2 * (1 - t) * t * point1 + t * t * point2;
            line.SetPosition(i, B);
            t += (1 / (float)line.positionCount);
        }
    }
}
