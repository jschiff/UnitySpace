using UnityEngine;
using System.Collections;

[RequireComponent (typeof(Rigidbody))]
public class ShipMover : MonoBehaviour {
	
	// Use this for initialization
	void Start () {
		cursor = new CrosshairCursor(cursorImage, rigidbody);
		
		rigidbody.drag = normalDrag;
		rigidbody.angularDrag = normalAngularDrag;
		// Screen.showCursor = false;
		Screen.lockCursor = true;
	}
	
	// Called when GUI elements are drawn
	void OnGUI() {
		cursor.OnGUI();
		dealWithSystemCursor();
		handleMouse();
	}
	
	// 	
	private void dealWithSystemCursor() {
	}
	
	// Update is called once per frame
	void update() {
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		handleThrust();
		handleMouse();
	}
	
	private void handleThrust() {
		updateKeys();
		
		applySpeedLimits();
		
		Vector3 thrust = Vector3.zero;
		if (W) {
			thrust += (Vector3.forward * speed * Time.deltaTime);
		}
		if (S) {
			thrust += (Vector3.back * speed * Time.deltaTime);
		}
		if (A) {
			thrust += (Vector3.left * speed * Time.deltaTime);
		}
		if (D) {
			thrust += (Vector3.right * speed * Time.deltaTime);
		}
		
		rigidbody.AddRelativeForce(thrust);
		

	
	}
	
	private void applySpeedLimits() {
		// Apply speed limit
		if (rigidbody.velocity.sqrMagnitude > speedLimit) {
			rigidbody.drag = emergencyDrag;
		}
		else {
			rigidbody.drag = normalDrag;
		}
		
		// Apply rotation limit
		if (rigidbody.angularVelocity.sqrMagnitude > angSpeedLimitSqr) {
			rigidbody.angularDrag = emergAngularDrag;	
		}
		else {
			rigidbody.angularDrag = normalAngularDrag;	
		}
	}
	
	private void stabilize() {
		
        Vector3 torqueVector = Vector3.Cross(rigidbody.transform.up, Vector3.up);
		torqueVector = Vector3.Project(torqueVector, transform.forward);
		
		rigidbody.AddTorque(torqueVector * Time.deltaTime * 150);
	}
	
	private void handleMouse() {	
		float roll = Input.GetAxis("Mouse X");
		float pitch = Input.GetAxis("Mouse Y");
	
		rigidbody.AddRelativeTorque(Vector3.up * roll * rollConstant);
		rigidbody.AddRelativeTorque(Vector3.left * pitch * pitchConstant);
		
		if (Q) {
			rigidbody.AddRelativeTorque(Vector3.forward * 500 * Time.deltaTime);
		}
		if (E) {
			rigidbody.AddRelativeTorque(Vector3.back * 500 * Time.deltaTime);
		}
		
		stabilize();
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
	
	public float speed = 10000f;
	public float speedLimit = 80000f;
	public float rollConstant = 100f;
	public float pitchConstant = 100f;
	public float normalAngularDrag = 2f;
	public float emergAngularDrag = 10f;
	public float angSpeedLimitSqr = 10f;
	bool A = false;
	bool W = false;
	bool S = false;
	bool D = false;
	bool Q = false;
	bool E = false;
	public float normalDrag = 1f;
	public float emergencyDrag = 5f;
	private CrosshairCursor cursor;
	public Texture2D cursorImage;
}
