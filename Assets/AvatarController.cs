using UnityEngine;
using System.Collections;

public class AvatarController : MonoBehaviour {

	private Transform leftHand,leftForearm, rightHand, rightForearm;

	// Use this for initialization
	void Start () {
		leftHand = GameObject.FindWithTag("leftHand").transform; 
		rightHand = GameObject.FindWithTag("rightHand").transform; 
		leftForearm = GameObject.FindWithTag("leftForearm").transform; 
		rightForearm = GameObject.FindWithTag("rightForearm").transform; 
	}
	
	// Update is called once per frame
	void Update () {
		rightHand.Rotate(new Vector3(0,PhoneInterface.acceleration.x,-PhoneInterface.acceleration.y),Space.World);
	}

}
