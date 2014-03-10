using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Line : MonoBehaviour{

	public static List<Transform> CreateLine(Transform parent,Transform prefab, int blocksPerLine, Vector2 pos){
		List<Transform> list = new List<Transform>();

		for (int i = 0; i < blocksPerLine; i++){
			Transform temp = (Transform)Instantiate(prefab,new Vector2(pos.x,pos.y - blocksPerLine + (2*i)),Quaternion.identity);
			temp.parent = parent;
			list.Add(temp);
		}
		int temporal = Random.Range(0,list.Count);
		Destroy(list[temporal].gameObject);
		list.RemoveAt(temporal);

		return list;
	}
}
