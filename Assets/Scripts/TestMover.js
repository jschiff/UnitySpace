var speed : int = 1000000; // Relative force applied for standard ws,ad,zx,qe movement.
var mouseSensativity : int = 15; // Speed of mouse movements for X/Y rotation, looking around.
var invertPitch : boolean = false; // Invert the X rotation axis. False: MouseDown=LookDown, True:MouseDown=LookUp
// Whether to use transform.Rotate() or rigidbody.AddRelativeTorque();
enum rotationMethods { Torque = 0, Rotate = 1 };
var rotationMethod = rotationMethods.Rotate;

function FixedUpdate() {
    // This probably could be turned into a constant, instead of checking every update, but doing so lets the user change the setting on the fly without repercussions.
    var invertPitchInt;
    if (invertPitch) invertPitchInt = -1;
    else invertPitchInt = 1;

   // Standard translate/position controls.
    if (Input.GetKey("w")) { rigidbody.AddRelativeForce (Vector3.forward * speed);  }
    if (Input.GetKey("s")) { rigidbody.AddRelativeForce (Vector3.forward * -1 * speed); }
    if (Input.GetKey("a")) { rigidbody.AddRelativeForce (Vector3.left * speed); }
    if (Input.GetKey("d")) { rigidbody.AddRelativeForce (Vector3.right * speed); }
    if (Input.GetKey("z")) { rigidbody.AddRelativeForce (Vector3.down* speed); }
    if (Input.GetKey("x")) { rigidbody.AddRelativeForce (Vector3.up * speed); }

    // Keyboard controls to mimic mouse movements.
    if (Input.GetKey("i")) { // Simulate increasing the X axis, mouse movement up.
        if (rotationMethod == rotationMethods.Torque) { rigidbody.AddRelativeTorque (invertPitchInt * -mouseSensativity * Time.deltaTime, 0, 0); }
        else { transform.Rotate(invertPitchInt *  -mouseSensativity * Time.deltaTime, 0, 0); }
    }
    if (Input.GetKey("k")) { // Simulate decreasing the X axis, mouse movement down.
        if (rotationMethod == rotationMethods.Torque) { rigidbody.AddRelativeTorque (invertPitchInt * mouseSensativity * Time.deltaTime, 0, 0); }
        else { transform.Rotate(invertPitchInt * mouseSensativity * Time.deltaTime,0,0); }
    }
    if (Input.GetKey("j")) { // Simulate increasing the Y axis, mouse movement left.
        if (rotationMethod == rotationMethods.Torque) { rigidbody.AddRelativeTorque (0, -mouseSensativity * Time.deltaTime, 0); }
        else { transform.Rotate(0, -mouseSensativity * Time.deltaTime, 0); }
    }
    if (Input.GetKey("l")) { // Simulate decreasing the Y axis, mouse movement right.
        if (rotationMethod == rotationMethods.Torque) { rigidbody.AddRelativeTorque (0, mouseSensativity * Time.deltaTime, 0); }
        else { transform.Rotate(0, mouseSensativity * Time.deltaTime, 0); }
    }

    // Actual mouse movement controls
    if (Input.GetAxis("Mouse X")) {
        if (rotationMethod == rotationMethods.Torque) { rigidbody.AddRelativeTorque( 0, Input.GetAxis("Mouse X") * mouseSensativity, 0); }
        else { transform.Rotate(0, Input.GetAxis("Mouse X") * mouseSensativity, 0); }
    }
    if (Input.GetAxis("Mouse Y")) {
        if (rotationMethod == rotationMethods.Torque) { rigidbody.AddRelativeTorque( invertPitchInt * Input.GetAxis("Mouse Y") * -mouseSensativity, 0, 0); }
        else { transform.Rotate(invertPitchInt * Input.GetAxis("Mouse Y") * -mouseSensativity, 0, 0); }
    }

    // Roll controls
    if (Input.GetKey("q")) {
        if (rotationMethod == rotationMethods.Torque) { rigidbody.AddRelativeTorque(0, 0, speed * Time.deltaTime); }
        else { transform.Rotate( 0, 0, -speed * Time.deltaTime); }
    }
    if (Input.GetKey("e")) {
        if (rotationMethod == rotationMethods.Torque) { rigidbody.AddRelativeTorque(0, 0, -speed * Time.deltaTime); }
        else { transform.Rotate( 0, 0, speed * Time.deltaTime); }
    }
}