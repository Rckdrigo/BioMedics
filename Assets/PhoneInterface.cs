using UnityEngine;
using System.Collections;

[RequireComponent(typeof(NetworkView))]
public class PhoneInterface : MonoBehaviour {

	public static Vector3 acceleration, gyroscope;

	void Update(){
		if(Network.isClient)
			networkView.RPC("sendVector",RPCMode.Server,Input.acceleration,Input.gyro.rotationRate);
		//acc = new Vector3(Input.GetAxis("Horizontal"),Input.GetAxis("Vertical"),0);
		//gyro = new Vector3(Input.GetAxisRaw("Horizontal"),Input.GetAxisRaw("Vertical"),0);

	}

	[RPC]
	void sendVector(Vector3 acc, Vector3 gyro){
		acceleration = acc;
		gyroscope = gyro;
	}

	void OnGUI(){
		print(Network.peerType);
		GUILayout.Space(50);

		GUILayout.Label("Acceleration: "+acceleration + " Gyro: " + gyroscope);
	}
}
