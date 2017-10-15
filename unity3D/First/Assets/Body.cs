using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Body : MonoBehaviour {
	//下一个身子的引用
	public Body next;

	//使当前身子移动
	public void Move(Vector3 pos){
		//纪录移动前的位置
		Vector3 nextPos = transform.position;
		//移动当前的身子
		transform.position = pos;
		if(next != null){
			next.Move (nextPos);
		}
	}
}
