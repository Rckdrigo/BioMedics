using UnityEngine;
using System.Collections;

public class Block : MonoBehaviour {

	public Sprite[] blocks;
	public bool randomColor;
	private Vector2 coord;

	// Use this for initialization
	void Start () {
		if(blocks.Length > 0 && randomColor)
			GetComponent<SpriteRenderer>().sprite = blocks[Random.Range(0,blocks.Length)];
	}

	public void setCoord(Vector2 coord){
		this.coord = coord;
	}

	public Vector2 getCoord(){
		return coord;
	}


}
