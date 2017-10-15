using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum HeadDir{
	Up, Down, Left, Right,
}

public class HeadCtrl : MonoBehaviour {
	//食物
	public GameObject foodPrefab;
	//身体的与社体
	public GameObject bodyPrefab;
	//移动速度，米/秒
	public float speed;
	// 计时器
	private float _Timer=0f;
	//now dir
	private HeadDir _CurrentDir= HeadDir.Up;
	private HeadDir _NextDir= HeadDir.Up;
	//游戏是否结束
	private bool _IsOver = false;
	//第一节身体的引用
	private Body _FirstBody;
	private Body _LastBody;

	public void CreateFood(){
		float x= Random.Range(-9.5f, 9.5f);
		float z= Random.Range(-9.5f, 9.5f);
		GameObject obj = Instantiate (foodPrefab, new Vector3(x, 0f,  z  ), Quaternion.identity) as GameObject;
	}

	public void OnTriggerEnter (Collider other){
		if(other.tag.Equals("bound") || other.tag.Equals("body")){
			_IsOver = true;
		}
		if (other.tag.Equals ("food")) {
			Destroy (other.gameObject);
			Grow ();
			CreateFood ();
		}
	}
	private void Grow(){
		//在看不到的地方创建一个身体，下一次移动时
		GameObject obj = Instantiate (bodyPrefab, new Vector3(100f, 100f, 100f), Quaternion.identity) as GameObject;
		//获取身体的Body脚本
		Body b = obj.GetComponent<Body> ();
		//给_firstbody 赋值
		if (_FirstBody == null) {
			_FirstBody = b;
		}
		//如果有最后一节身体，就给next赋值。像不像链表？~
		if(_LastBody != null){
			//上一次的最后一个身体，是这一次的前驱 
			_LastBody.next = b;			
		}
		_LastBody = b;
	}

	private void Start(){
		CreateFood ();
	}


	private void Update(){
		if (!_IsOver) {			
			Turn ();
			Move (); 	
		}

	}
	private void Turn(){
		//监听用户按键事件W
		if(Input.GetKey(KeyCode.W)){
			_NextDir = HeadDir.Up;
			if(_CurrentDir == HeadDir.Down){
				_NextDir = _CurrentDir;
			}
		}
		if(Input.GetKey(KeyCode.S)){
			_NextDir = HeadDir.Down;
			if(_CurrentDir == HeadDir.Up){
				_NextDir = _CurrentDir;
			}
		}
		if(Input.GetKey(KeyCode.A)){
			_NextDir = HeadDir.Left;
			if(_CurrentDir == HeadDir.Right){
				_NextDir = _CurrentDir;
			}
		}
		if(Input.GetKey(KeyCode.D)){
			_NextDir = HeadDir.Right;
			if(_CurrentDir == HeadDir.Left){
				_NextDir = _CurrentDir;
			}
		}
	}

	private void Move(){
		_Timer += Time.deltaTime;
		if (_Timer >= (1f / speed)){
			//让蛇头旋转
			switch(_NextDir){
			case HeadDir.Up:
				transform.forward = Vector3.forward;
				_CurrentDir = HeadDir.Up;
				break;
			case HeadDir.Down:
				transform.forward = Vector3.back;
				_CurrentDir = HeadDir.Down;
				break;
			case HeadDir.Left:
				transform.forward = Vector3.left;
				_CurrentDir = HeadDir.Left;
				break;
			case HeadDir.Right:
				transform.forward = Vector3.right;
				_CurrentDir = HeadDir.Right;
				break;
			}
			//纪录头部移动之前的位置
			Vector3 nextPos = transform.position;
			//移动头部
			transform.Translate (Vector3.forward);
			if (_FirstBody != null) {
				_FirstBody.Move(nextPos);
			}
			//重置计时器
			_Timer = 0f;
		}
	}

}
