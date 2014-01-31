using UnityEngine;
using System.Collections;

public class RingMovement : MonoBehaviour {
		
	public enum Orientation{Pronacion, Flexion, Desviacion }
	public Orientation orientation;
	public float speed = 1;
	public float rotSpeed = 10;
	public bool inverse;



	void Update(){
		Vector3 temp = Vector3.zero;
		switch(orientation){
		case Orientation.Pronacion:
			temp = new Vector3(PhoneInterface.acceleration.z * Mathf.Sign( PhoneInterface.gyroscope.y),0,0);
			break;

		case Orientation.Flexion:
			temp = new Vector3(PhoneInterface.acceleration.y,0,0);
			break;

		case Orientation.Desviacion:
			temp = new Vector3(PhoneInterface.acceleration.x ,0,0);
			break;

		}
		transform.Translate(Vector3.forward * Time.deltaTime * speed);
		transform.Rotate(temp * Time.deltaTime * rotSpeed);
	}
}
