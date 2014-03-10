using UnityEngine;
using System.Collections;

[RequireComponent(typeof(NetworkView))]
public class PhoneInterface : MonoBehaviour {

	public static Vector3 acceleration;
	public static Vector3 gyroscope;
	public static bool rightHanded;
	
	#if UNITY_ANDROID
	void Awake(){
		Input.gyro.enabled = true;
		Screen.sleepTimeout = SleepTimeout.NeverSleep;
	}
	#endif

	void Start(){
		DontDestroyOnLoad(gameObject);
	}

	void Update(){
		if(Network.isClient)
			networkView.RPC("sendVector",RPCMode.Server,Input.acceleration,Input.gyro.rotationRate, rightHanded);
		
		print("Acceleration: "+acceleration + " Gyro: " + gyroscope + " Hand: " + rightHanded);
	}

	[RPC]
	void sendVector(Vector3 acc, Vector3 gyro, bool hand){
		acceleration = acc;
		gyroscope = gyro;
		rightHanded = hand;
	}

	void OnDestroy(){
		Network.Disconnect();
	}
}
