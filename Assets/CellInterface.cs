using UnityEngine;
using System.Collections;

[RequireComponent(typeof(NetworkView))]
public class CellInterface : MonoBehaviour {

	Vector3 acc, gyro;
	
	[RPC]
	void sendVector(Vector3 acc, Vector3 gyro){
		this.acc = acc;
		this.gyro = gyro;
	}

	void OnGUI(){
		print(Network.peerType);
		GUILayout.Space(50);

		if(Network.isClient){
			if(GUILayout.Button("Enviar vector"))
				networkView.RPC("sendVector",RPCMode.Server,Input.acceleration,Input.gyro.rotationRate);
		}else if(Network.isServer)
			GUILayout.Label("Acceleration: "+acc + " Gyro: " + gyro);
	}
}
