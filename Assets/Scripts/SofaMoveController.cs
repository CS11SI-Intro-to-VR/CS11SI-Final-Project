using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

[RequireComponent(typeof(Rigidbody))]
public class SofaMoveController : MonoBehaviour
{

    [Header("Object Assignment")]
    public float SofaMoveSpeed;

    private InputDevice _rightController;
    private Rigidbody _rb;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        // Initialize the local _rightController variable.
        List<InputDevice> devices = new();
        InputDevices.GetDevicesAtXRNode(XRNode.RightHand, devices);
        _rightController = devices[0];
    }

    private void Update()
    {
        HandleAcceleration();

        // TODO: Remove this at the bottom. This is just a test!
        if (Input.GetKey(KeyCode.W))
        {
            _rb.AddForce(Vector3.forward * SofaMoveSpeed);
        }
    }

    /* Function: HandleAcceleration
     * ------------------------------
     * This function checks if the Trigger button is being pressed on the
     * controller and adds a forward acceleration to the object.
     */
    private void HandleAcceleration()
    {
        bool triggerValue;  // Temp variable to hold trigger button status!
        if (_rightController.TryGetFeatureValue(CommonUsages.triggerButton, out triggerValue))
        {
            // Code is run when the trigger button is pressed.
            Debug.Log("I have been triggered!!!");
        }
    }
}
