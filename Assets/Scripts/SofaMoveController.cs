using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

[RequireComponent(typeof(Rigidbody))]
public class SofaMoveController : MonoBehaviour
{

    [Header("Object Assignments")]
    [SerializeField] private Transform _steeringWheelTransform;
    [Header("Sofa Properties")]
    [SerializeField] private float _sofaMoveSpeed;
    [SerializeField] private float _sofaTurnSpeed;
    [SerializeField] private SteeringWheel _steeringWheel;

    private InputDevice _leftController;
    private InputDevice _rightController;
    private Rigidbody _rb;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        Debug.Assert(_steeringWheelTransform != null, "Steering wheel object has not been assigned!", this);
    }

    private void Start()
    {
        // Initialize the local _rightController variable.
        List<InputDevice> devices = new();
        InputDevices.GetDevicesAtXRNode(XRNode.RightHand, devices);
        _rightController = devices[0];
        InputDevices.GetDevicesAtXRNode(XRNode.LeftHand, devices);
        _leftController = devices[0];
        if (_leftController != null && _rightController != null)
        {
            // A random cube will turn red if all goes well.
            if (CubeDebugger.Instance != null)
            {
                CubeDebugger.Instance.ChangeColor(new Color(255, 0, 0));
            }
        } else
        {
            Debug.Log("Player not playing in VR!");
        }
    }

    private void Update()
    {
        /*
        _rb.centerOfMass = Vector3.zero;
        // If we're in the Unity Editor, take the W/A/D keys.
        if (Input.GetKey(KeyCode.W))
        {
            //_rb.velocity = Vector3.forward * _sofaMoveSpeed;
            _rb.AddForce(transform.forward * _sofaMoveSpeed, ForceMode.Impulse);
        }
        float hor = Input.GetAxisRaw("Horizontal");
        if (hor != 0)
        {
            // _rb.MoveRotation(Quaternion.Euler(new Vector3(0, hor * _sofaTurnSpeed * Time.deltaTime, 0)));
            // _rb.angularVelocity = new Vector3(0, hor * _sofaTurnSpeed, 0);
            // _rb.AddTorque(new Vector3(0, hor * _sofaTurnSpeed, 0));
            transform.Rotate(0, hor * _sofaTurnSpeed * Time.deltaTime, 0);
        }
        */
        HandleAcceleration();
        HandleTurning();
    }

    /// <summary>
    /// Checks if the Trigger button is being pressed on the controller and adds 
    /// a forward acceleration to the object.
    /// </summary>
    private void HandleAcceleration()
    {
        if (_rightController.TryGetFeatureValue(CommonUsages.primaryButton, out bool triggerValue) && triggerValue)
        {
            // Code is run when the trigger button is pressed.
            _rb.AddForce(transform.forward * _sofaMoveSpeed, ForceMode.Impulse);
        }
    }

    /// <summary>
    /// Checks if the steering wheel is rotated and makes the car rotate accordingly.
    /// </summary>
    private void HandleTurning()
    {
        if (_leftController.TryGetFeatureValue(CommonUsages.primary2DAxis, out Vector2 triggerValue))
        {
            // Code is run when the trigger button is pressed.
            float hor = triggerValue.x;
            //_rb.MoveRotation(Quaternion.Euler(new Vector3(0, hor * _sofaTurnSpeed * Time.deltaTime, 0)));
            //_rb.angularVelocity = new Vector3(0, hor * _sofaTurnSpeed, 0);
            //_rb.AddTorque(new Vector3(0, hor * _sofaTurnSpeed, 0));
            transform.Rotate(0, hor * _sofaTurnSpeed * Time.deltaTime, 0);
        }
    }
}
