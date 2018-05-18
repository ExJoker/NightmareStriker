using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 巡逻的状态脚本
/// </summary>
public class Patrol : FSMState
{
	public Patrol (Transform[] points)
	{
		wayPoints = points;
		stateID = FSMStateID.PARTROL;
		curRoSpeed = 6.0f;
		curSpeed = 15f;
	}

	//巡逻过程中,发生状态改变是的方法,重写父类中的Reason抽象方法
	public override void Reason (Transform player, Transform npc)
	{
		if (player != null && Vector3.Distance (nextPoint, npc.position) <= chaseDistance) {
			Debug.Log ("转换当前状态: 巡逻->追击");
			npc.GetComponent<AIController> ().SetTransition (Transition.SAWPLAYER);
		}
		
	}
	//巡逻状态下的行为
	public override void Action (Transform player, Transform npc)
	{
		if (Vector3.Distance (npc.position, nextPoint) <= arriveDistance) {
			//更换巡逻点
			FindNextPoint ();
		}
		//旋转
		Quaternion targetQua = Quaternion.LookRotation (nextPoint - npc.position);
		npc.rotation = Quaternion.Lerp (npc.rotation, targetQua, Time.deltaTime * curSpeed);
		//移动
		npc.Translate (Vector3.forward * Time.deltaTime * curSpeed);
	}

}
