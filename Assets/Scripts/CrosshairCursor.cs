using UnityEngine;
using System.Collections;

public class CrosshairCursor {
	public CrosshairCursor(Texture2D image, Rigidbody shipBody) {
		this.cursorImage = image;	
		this.shipBody = shipBody;
	}
	
	public void OnGUI() {
		// Crosshair
		Rect crossPos = center(Camera.main.ViewportToScreenPoint(new Vector3(.5f, .5f, 0)), crosshairTex);
		GUI.Label(crossPos, crosshairTex);
	}
	
	/**
	 * Center a rectangle around the moue position with the size of the mouse texture
	 **/
	private Rect center(Vector2 mousePos, Texture tex) {
		Rect ret = new Rect(mousePos.x - tex.width / 2,
							mousePos.y - tex.height / 2,
							tex.width, tex.height);
		return ret;
	}
	
	// Create the crosshair texture
	private static Texture2D createCrossHair(Color color) {
		int width = 99, height = 99;
		int midX = width / 2 + 1;
		int midY = height / 2 + 1;
		Texture2D tex = new Texture2D(width, height);
		Color clear = new Color(0, 0, 0, 0);
	
		// This is a double loop just for clarity's sake
		for (int i = 0; i < height; i++) {
			for	(int j = 0; j < width; j++) {
				if (i == midY || j == midX) {
					tex.SetPixel(j, i, color);
				}
				else {
					tex.SetPixel(j, i, clear);
				}
			}
		}
		tex.Apply();
		return tex;
	}
	
	private Texture2D crosshairTex = createCrossHair(Color.green);
	private Rigidbody shipBody;
	public Vector3 cursorVector;
	public Vector3 relativecVec;
	public static CrosshairCursor current;
	public Vector3 cameraLocation;
	public Texture2D cursorImage;
}
