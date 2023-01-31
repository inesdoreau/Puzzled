using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] protected Transform CameraTransform;

    [SerializeField] private float moveChangeFactor = 0.9f;
    //public float mouseSensitivity = 1.0f;
    public float moveSpeed = 2f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        ManageMovements();
        ManageRotation();
    }

    #region Movements

    private Vector3 _velocity;
    private float Forward => Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W) ? 1f : 0f;
    private float Backward => Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S) ? 1f : 0f;
    private float Right => Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D) ? 1f : 0f;
    private float Left => Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A) ? 1f : 0f;
    private float Up => Input.GetKey(KeyCode.E) ? 1f : 0f;
    private float Down => Input.GetKey(KeyCode.Q) ? 1f : 0f;

    private void ManageMovements()
    {
        //Vector3 newVelocity = transform.forward * (Forward - Backward);
        //newVelocity += transform.right * (Right - Left);
        //newVelocity += transform.up * (Up - Down);
        Vector3 newVelocity = CameraTransform.forward * (Forward - Backward);
        newVelocity += CameraTransform.right * (Right - Left);
        newVelocity += CameraTransform.up * (Up - Down);

        if (newVelocity.sqrMagnitude > 1) //cap at 1
        {
            newVelocity.Normalize();
        }

        newVelocity *= moveSpeed;

        float moveChange = 1 - Mathf.Pow(1 - moveChangeFactor, Time.deltaTime * 10); //smoothly go from old vel to new vel
        _velocity = Vector3.Lerp(_velocity, newVelocity, moveChange);

        //transform.position += _velocity * Time.deltaTime;
        CameraTransform.position += _velocity * Time.deltaTime;
        //leftHandTransform.position += _velocity * Time.deltaTime;
        // rightHandTransform.position += _velocity * Time.deltaTime;
    }
    #endregion

    #region Mouse Control
    private void ManageRotation()
    {
        if (!Input.GetKey(KeyCode.Mouse1))
        {
            return;
        }
        //Vector3 rot = transform.localEulerAngles;
        Vector3 rot = CameraTransform.localEulerAngles;
        rot.y = WrapAngle(rot.y + Input.GetAxis("Mouse X"));
        CameraTransform.localEulerAngles = rot;

        Vector3 camRot = CameraTransform.transform.localEulerAngles;
        camRot.x = Mathf.Clamp(WrapAngle(camRot.x - Input.GetAxis("Mouse Y")), -90f, 90f);
        CameraTransform.transform.localEulerAngles = camRot;

    }

    private float WrapAngle(float angle)
    {
        while (angle > 180f)
        {
            angle -= 360f;
        }

        while (angle < -180f)
        {
            angle += 360f;
        }

        return angle;
    }

    #endregion
}
