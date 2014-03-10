using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LevelBuilder : MonoBehaviour {

	public Transform prefab;
	public Vector2 startPos  = new Vector2(22,0);
	
	public int blockSize = 2;
	public int blocksPerLine = 8;
	public int nLines = 5;

	private struct BlockLine{
		public List<Transform> blocks;
	};

	List<BlockLine> lineStructure;

	// Use this for initialization
	void Start () {
		Physics2D.gravity = new Vector2(4.0f,0);
		lineStructure = new List<BlockLine>();
		CreateLevel();
	}

	public void CreateLevel(){
		for(int i = 0; i < nLines; i++){
			BlockLine temp;
			Vector2 pos = new Vector2(startPos.x - blockSize*i,startPos.y);

			temp.blocks = Line.CreateLine(transform,prefab,blocksPerLine,pos);
			lineStructure.Add(temp);
		}

 	}
	public void AddLine () {
		transform.Translate(Vector3.left * blockSize);

		BlockLine temp;
		temp.blocks = Line.CreateLine(transform,prefab,blocksPerLine,startPos);
		lineStructure.Add(temp);
	}

	void Update(){
		print(lineStructure.Count);

		if(Input.GetKeyDown(KeyCode.A))
			AddLine();
	}
}
