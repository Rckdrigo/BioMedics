using UnityEngine;
using System.Collections;

public class ColorPicker : MonoBehaviour {

	public Color color;

	void Awake(){
		renderer.material.color = color;
	}

}
