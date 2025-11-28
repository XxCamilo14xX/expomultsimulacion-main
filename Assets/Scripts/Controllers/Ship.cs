using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PlayerInput))]
public class Ship : GravityObject {

    public Transform hatch;
    public float hatchAngle;
    public Transform camViewPoint;
    public Transform pilotSeatPoint;
    public LayerMask groundedMask;

    [Header ("Handling")]
    public float thrustStrength = 20;
    public float rotSpeed = 5;
    public float rollSpeed = 30;
    public float rotSmoothSpeed = 10;
    public bool lockCursor;

    [Header ("Interact")]
    public Interactable flightControls;

    Rigidbody rb;
    Quaternion targetRot;
    Quaternion smoothedRot;

    Vector3 thrusterInput;
    PlayerController pilot;
    bool shipIsPiloted;
    int numCollisionTouches;
    bool hatchOpen;

    // Legacy keys
    KeyCode ascendKey = KeyCode.Space;
    KeyCode descendKey = KeyCode.LeftShift;
    KeyCode rollCounterKey = KeyCode.Q;
    KeyCode rollClockwiseKey = KeyCode.E;
    KeyCode forwardKey = KeyCode.W;
    KeyCode backwardKey = KeyCode.S;
    KeyCode leftKey = KeyCode.A;
    KeyCode rightKey = KeyCode.D;

    // Input System values
    Vector2 inputMove;
    float inputVertical;
    Vector2 inputLook;
    float inputRoll;

    void Awake () {
        InitRigidbody ();
        targetRot = transform.rotation;
        smoothedRot = transform.rotation;

        if (lockCursor) {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }

    void OnEnable() {
        var pInput = GetComponent<PlayerInput>();
        if (pInput != null) {
            pInput.onActionTriggered += HandleInput;
        }
    }

    void OnDisable() {
        var pInput = GetComponent<PlayerInput>();
        if (pInput != null) {
            pInput.onActionTriggered -= HandleInput;
        }
    }

    void HandleInput(InputAction.CallbackContext context) {
        // Debug.Log($"Ship Input: {context.action.name}");

        switch (context.action.name) {
            case "Move": 
                if (shipIsPiloted) inputMove = context.ReadValue<Vector2>(); 
                break;
            case "Vertical": 
                if (shipIsPiloted) inputVertical = context.ReadValue<float>(); 
                break;
            case "Look": 
                if (shipIsPiloted) inputLook = context.ReadValue<Vector2>(); 
                break;
            case "Roll": 
                if (shipIsPiloted) inputRoll = context.ReadValue<float>(); 
                break;
            case "ToggleHatch": 
                if (context.performed) ToggleHatch(); 
                break;
            case "ExitSeat": 
                if (context.performed && shipIsPiloted) StopPilotingShip(); 
                break;
            case "SelectPlanet": 
                if (context.performed && shipIsPiloted) OnSelectPlanet(); 
                break;
            case "ExitToDesktop":
                if (context.performed) OnExitToDesktop();
                break;
        }
    }

    void OnSelectPlanet() {
        var shipHUD = FindObjectOfType<ShipHUD>();
        if (shipHUD != null) {
            shipHUD.ToggleLock();
        }
    }

    void OnExitToDesktop() {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Windowsescritorio");
    }

    void Update () {
        if (shipIsPiloted) {
            HandleMovement ();
        }

        // Animate hatch
        float hatchTargetAngle = (hatchOpen) ? hatchAngle : 0;
        hatch.localEulerAngles = Vector3.right * Mathf.LerpAngle (hatch.localEulerAngles.x, hatchTargetAngle, Time.deltaTime);
    }

    void HandleMovement () {
        DebugHelper.HandleEditorInput (lockCursor);
        
        // Thruster input
        // Combine legacy keyboard with new input system
        float thrustX = inputMove.x;
        if (thrustX == 0) thrustX = GetInputAxis (leftKey, rightKey);

        float thrustY = inputVertical;
        if (thrustY == 0) thrustY = GetInputAxis (descendKey, ascendKey);

        float thrustZ = inputMove.y;
        if (thrustZ == 0) thrustZ = GetInputAxis (backwardKey, forwardKey);

        thrusterInput = new Vector3 (thrustX, thrustY, thrustZ);

        // Rotation input
        float yawInput = inputLook.x * rotSpeed;
        if (yawInput == 0) yawInput = Input.GetAxisRaw ("Mouse X") * rotSpeed;

        float pitchInput = inputLook.y * rotSpeed;
        if (pitchInput == 0) pitchInput = Input.GetAxisRaw ("Mouse Y") * rotSpeed;

        float rollInput = inputRoll * rollSpeed * Time.deltaTime;
        if (rollInput == 0) rollInput = GetInputAxis (rollCounterKey, rollClockwiseKey) * rollSpeed * Time.deltaTime;

        // Calculate rotation
        if (numCollisionTouches == 0) {
            var yaw = Quaternion.AngleAxis (yawInput, transform.up);
            var pitch = Quaternion.AngleAxis (-pitchInput, transform.right);
            var roll = Quaternion.AngleAxis (-rollInput, transform.forward);

            targetRot = yaw * pitch * roll * targetRot;
            smoothedRot = Quaternion.Slerp (transform.rotation, targetRot, Time.deltaTime * rotSmoothSpeed);
        } else {
            targetRot = transform.rotation;
            smoothedRot = transform.rotation;
        }
    }

    void FixedUpdate () {
        // Gravity
        Vector3 gravity = NBodySimulation.CalculateAcceleration (rb.position);
        rb.AddForce (gravity, ForceMode.Acceleration);

        // Thrusters
        Vector3 thrustDir = transform.TransformVector (thrusterInput);
        rb.AddForce (thrustDir * thrustStrength, ForceMode.Acceleration);

        if (numCollisionTouches == 0) {
            rb.MoveRotation (smoothedRot);
        }
    }

    int GetInputAxis (KeyCode negativeAxis, KeyCode positiveAxis) {
        int axis = 0;
        if (Input.GetKey (positiveAxis)) {
            axis++;
        }
        if (Input.GetKey (negativeAxis)) {
            axis--;
        }
        return axis;
    }

    void InitRigidbody () {
        rb = GetComponent<Rigidbody> ();
        rb.interpolation = RigidbodyInterpolation.Interpolate;
        rb.useGravity = false;
        rb.isKinematic = false;
        rb.centerOfMass = Vector3.zero;
        rb.collisionDetectionMode = CollisionDetectionMode.ContinuousSpeculative;
    }

    public void ToggleHatch () {
        hatchOpen = !hatchOpen;
    }

    public void TogglePiloting () {
        if (shipIsPiloted) {
            StopPilotingShip ();
        } else {
            PilotShip ();
        }
    }

    public void PilotShip () {
        pilot = FindObjectOfType<PlayerController> ();
        shipIsPiloted = true;
        pilot.Camera.transform.parent = camViewPoint;
        pilot.Camera.transform.localPosition = Vector3.zero;
        pilot.Camera.transform.localRotation = Quaternion.identity;
        pilot.gameObject.SetActive (false);
        hatchOpen = false;

        var pInput = GetComponent<PlayerInput>();
        if (pInput != null && pInput.actions != null) {
            pInput.SwitchCurrentActionMap("Spaceship");
        }
    }

    void StopPilotingShip () {
        shipIsPiloted = false;
        pilot.transform.position = pilotSeatPoint.position;
        pilot.transform.rotation = pilotSeatPoint.rotation;
        pilot.Rigidbody.linearVelocity = rb.linearVelocity;
        pilot.gameObject.SetActive (true);
        pilot.ExitFromSpaceship ();
        
        var pInput = GetComponent<PlayerInput>();
        if (pInput != null && pInput.actions != null) {
            pInput.SwitchCurrentActionMap("Plane");
        }
    }

    void OnCollisionEnter (Collision other) {
        if (groundedMask == (groundedMask | (1 << other.gameObject.layer))) {
            numCollisionTouches++;
        }
    }

    void OnCollisionExit (Collision other) {
        if (groundedMask == (groundedMask | (1 << other.gameObject.layer))) {
            numCollisionTouches--;
        }
    }

    public void SetVelocity (Vector3 velocity) {
        rb.linearVelocity = velocity;
    }

    public bool ShowHUD {
        get {
            return shipIsPiloted;
        }
    }
    public bool HatchOpen {
        get {
            return hatchOpen;
        }
    }

    public bool IsPiloted {
        get {
            return shipIsPiloted;
        }
    }

    public Rigidbody Rigidbody {
        get {
            return rb;
        }
    }

}