using UnityEditor;
using UnityEngine;
using System.Collections;

public class CustomWindow : EditorWindow {
	float objMass = 1.00f;
	
	[MenuItem ("CustomWindow/test")]
	static void test() {
		Debug.Log ("it worked!");
	}
	
	[MenuItem ("Custom/RigidBody/DoubleMass")]
	static void DoubleMass() {
		GameObject body = Selection.gameObjects[0];
			if (body.GetComponent<Rigidbody>() == null) {
				body.AddComponent<Rigidbody>();
		}
		body.rigidbody.mass = body.rigidbody.mass * 2;
		Debug.Log ("body is doubled to " + body.rigidbody.mass);
	}
		
	[MenuItem ("Window/CustomWindow")]
		public static void ShowWindow() {
		EditorWindow.GetWindow(typeof(CustomWindow));
	}
	
	void OnGUI() {
		GUILayout.Label ("Set Mass" ,EditorStyles.boldLabel);
		objMass = EditorGUILayout.Slider("Slider", objMass, 1, 100);
		if (Selection.activeObject == null) {
			Debug.Log ("Select Object");
		}
		else {
			GameObject body = Selection.gameObjects[0];
			if (body.GetComponent<Rigidbody>() == null) {
				body.AddComponent<Rigidbody>();
			}
			body.rigidbody.mass = objMass;
			Debug.Log("Rigidbody changed to " + body.rigidbody.mass);
		}
	}
}
