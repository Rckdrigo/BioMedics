using UnityEngine;
using System.Collections;

public class PathCollision : MonoBehaviour {

	void OnCollisionEnter(Collision collision){
		renderer.material.color = Color.red;
	}

}
