using UnityEngine;
using System.Collections;

public class RingMovement : MonoBehaviour {
		
	public enum Orientation{Pronacion, Flexion, Desviacion }
	public Orientation orientation;
	public float speed = 1;
	public float rotSpeed = 10;
	public bool inverse;

	void Start(){
		if(orientation == Orientation.Pronacion)
			Input.gyro.enabled = true;
	}

	void Update(){
		Vector3 temp = Vector3.zero;
		switch(orientation){
		case Orientation.Pronacion:
			temp = new Vector3(Input.acceleration.z * Mathf.Sign( Input.gyro.rotationRate.y),0,0);
			break;

		case Orientation.Flexion:
			temp = new Vector3(Input.acceleration.y,0,0);
			break;

		case Orientation.Desviacion:
			temp = new Vector3(Input.acceleration.z ,0,0);
			break;

		}
		transform.Translate(Vector3.forward * Time.deltaTime * speed);
		transform.Rotate(temp * Time.deltaTime * rotSpeed);
	}
}
