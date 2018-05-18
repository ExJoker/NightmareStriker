using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class PlayerMovement : MonoBehaviour
{

	float currentSpeed;
	//玩家当前移动速度
	float targetSpeed;
	//玩家目标速度
	float rotSpeed = 2.0f;
	//玩家选转速度
	float maxForwardSpeed = 18f;
	//玩家最大前进速度
	float maxBackwardSpeed = -18f;
	//玩家最后后退速度

	GameObject turret;
	int floorMask;
	//地面layer
	float camRayLength = 100f;
	//摄像机发出射线最大长度
	public float turretRotSpeed = 2.0f;
	//炮台旋转速度
	Transform m_watchPoint;

	private void Awake ()
	{
		turret = GameObject.Find ("TankTurret1") as GameObject;          //获取炮台
		floorMask = LayerMask.GetMask ("floor");                         //获取地面layer编号
		m_watchPoint = transform.Find ("WatchPoint");

	}


	void LateUpdate ()
	{
		//摄像机跟随
		CameraFollowScript.Instance.TargetPoint = m_watchPoint;
	}

	private void FixedUpdate ()
	{
		float h = CrossPlatformInputManager.GetAxisRaw ("Horizontal");
		float v = CrossPlatformInputManager.GetAxisRaw ("Vertical");

		//玩家移动
		Move (h, v);
		TurnTurret ();
	}


	void Move (float h, float v)
	{   
		if (v > 0.01f) {         //前进
			targetSpeed = maxForwardSpeed;
			currentSpeed = Mathf.Lerp (currentSpeed, targetSpeed, Time.deltaTime * 2.0f);
			transform.position += v * transform.forward * Time.deltaTime * currentSpeed;
		} else if (v < -0.01f) {  //后退
			targetSpeed = maxBackwardSpeed;
			transform.position -= v * transform.forward * Time.deltaTime * targetSpeed;
		}

		//旋转
		transform.Rotate (new Vector3 (0, h, 0) * rotSpeed);
	}

	void TurnTurret ()
	{
		//发射线
		Ray camRay = Camera.main.ScreenPointToRay (Input.mousePosition);
		//碰撞点信息
		RaycastHit floorHit;
		//射线碰撞检测
		if (Physics.Raycast (camRay, out floorHit, camRayLength, floorMask)) {
			//获取鼠标方向
			Vector3 playerToMouse = floorHit.point - transform.position;
			playerToMouse.y = 0;
			//得到新旋转角度
			Quaternion newRotatation = Quaternion.LookRotation (playerToMouse);
			//差值旋转
			turret.transform.rotation = Quaternion.Slerp (turret.transform.rotation, newRotatation, Time.deltaTime * turretRotSpeed);
		}
	}

}
