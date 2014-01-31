using UnityEngine;
using System.Collections;

public class PauseMenu : MonoBehaviour {

	Vector3 initPosition;
	Quaternion initRotation;
	bool onPause;

	// Use this for initialization
	void Start () {
		initPosition = GameObject.FindWithTag("Player").transform.position;
		initRotation = GameObject.FindWithTag("Player").transform.rotation;
	}

	void Update(){
		if(Input.GetKeyDown(KeyCode.P) || Input.GetKeyDown(KeyCode.Escape)){
			onPause = !onPause;
			Time.timeScale = onPause ? 0 : 1;
		}
	}

	void Restart(){
		GameObject.FindWithTag("Player").transform.position = initPosition;
		GameObject.FindWithTag("Player").transform.rotation = initRotation;
		onPause = false;
		Time.timeScale = 1;
	}
	
	void OnGUI(){
		GUILayout.BeginArea(new Rect(0,0, Screen.width, Screen.height));
		if(onPause){
			GUILayout.Label("PAUSE");
			if(GUILayout.Button("Pronacion / Supinacion"))
				GameObject.FindWithTag("Player").GetComponent<RingMovement>().orientation = RingMovement.Orientation.Pronacion;
			if(GUILayout.Button("Flexion / Extension"))
				GameObject.FindWithTag("Player").GetComponent<RingMovement>().orientation = RingMovement.Orientation.Flexion;
			if(GUILayout.Button("Desviacion Radial / Cubital"))
				GameObject.FindWithTag("Player").GetComponent<RingMovement>().orientation = RingMovement.Orientation.Desviacion;
			if(GUILayout.Button("RESTART")){
				Restart();
			}
		}
		GUILayout.EndArea();
	}
	
	void OnDestroy(){
		Network.Disconnect();
	}
}

