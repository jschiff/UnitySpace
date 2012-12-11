using UnityEngine;
using System.Collections;

[RequireComponent (typeof(Rigidbody))]
public class ShipMover : MonoBehaviour {
	
	// Use this for initialization
	void Start () {
		cursor = new CrosshairCursor(cursorImage);
	}
	
	// Called when GUI elements are drawn
	void OnGUI() {
		cursor.OnGUI();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		updateKeys();
		
		Vector3 push = rigidbody.transform.forward * speed;
		
		handleTranslate();
		handleRotate();
		stabilize();
		
		cursor.simulate();
	}
	
	private void handleTranslate() {
		Vector3 thrust = Vector3.zero;
		
		if(A) {
			thrust += (Vector3.left * speed * Time.deltaTime);
		}
		if (W) {
			thrust += (Vector3.forward * speed * Time.deltaTime);
		}
		if (S) {
			thrust += (Vector3.back * speed * Time.deltaTime);
		}
		if (D) {
			thrust += (Vector3.right * speed * Time.deltaTime);	
		}
		if (Q) {
			thrust += (Vector3.down * speed * Time.deltaTime);
		}
		if (E) {
			thrust += (Vector3.up * speed * Time.deltaTime);
		}
		
		rigidbody.AddRelativeForce(thrust);
		
		// Apply speed limit
		if (rigidbody.velocity.sqrMagnitude > speedLimit) {
			rigidbody.drag = emergencyDrag;
		}
		else {
			rigidbody.drag = normalDrag;
		}
	
	}
	
	private void handleRotate() {
		Vector3 current = rigidbody.transform.forward;
		Vector3 target = cursor.cursorVector;
		Vector3 axisOfRotation = Vector3.Cross(current, target);
		
		Vector3 torque = axisOfRotation * rotSpeed;
		// torque.z = 0;
		
		// Want to get this effect but as a torque?
		/* rigidbody.transform.eulerAngles = new Vector3(
			rigidbody.transform.eulerAngles.x,
			rigidbody.transform.eulerAngles.y,
			0); */
		
		// torque += zLockAdjustment;
		
		if (rigidbody.angularVelocity.sqrMagnitude < rotSpeedLimit)
			rigidbody.AddTorque(torque * Time.deltaTime);
	}
	
	// Stabilize z axis (roll)
	private void stabilize() {
		        Vector3 predictedUp = Quaternion.AngleAxis(
            rigidbody.angularVelocity.magnitude * Mathf.Rad2Deg * .01f / speed,
            rigidbody.angularVelocity
        ) * transform.up;

        Vector3 torqueVector = Vector3.Cross(predictedUp, Vector3.up);
		torqueVector = Vector3.Project(torqueVector, transform.forward);
        rigidbody.AddTorque(torqueVector * stabSpeed * stabSpeed * Time.deltaTime);
	}
	
	private void updateKeys() {
		A = Input.GetKey(KeyCode.A);
		W = Input.GetKey(KeyCode.W);
		S = Input.GetKey(KeyCode.S);
		D = Input.GetKey(KeyCode.D);
		Q = Input.GetKey(KeyCode.Q);
		E = Input.GetKey(KeyCode.E);
		
		if (Input.GetKey(KeyCode.Escape)) Application.Quit();
	}
	
	static float speed = 1000000f;
	static float speedLimit = 30000f;
	static float rotSpeed = 60f;
	static float stabSpeed = 3f; // Rotation stabilizer
	static float rotSpeedLimit = 1f;
	bool A = false;
	bool W = false;
	bool S = false;
	bool D = false;
	bool Q = false;
	bool E = false;
	static float normalDrag = .02f;
	static float emergencyDrag = 5f;
	private CrosshairCursor cursor;
	public Texture2D cursorImage;
}
