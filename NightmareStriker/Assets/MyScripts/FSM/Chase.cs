using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 追逐状态
/// </summary>
public class Chase : FSMState
{
	public Chase (Transform[] points)
	{
		wayPoints = points;
		stateID = FSMStateID.CHASE;
		curRoSpeed = 6f;
		curSpeed = 18f;
		FindNextPoint ();
	}

	public override void Reason (Transform player, Transform npc)
	{
		if (player != null) {
			nextPoint = player.position;
		}
		//检测Enemy和Player之间的距离
		float distance = Vector3.Distance (npc.position, nextPoint);
		//如果距离小于攻击临界值
		if (distance <= attackDistance) {
			Debug.Log ("追逐->攻击");
			//设置当前状态机的过渡条件为:追上玩家了(REACHPLAYER)
			npc.GetComponent<AIController> ().SetTransition (Transition.REACHPLAYER);
		} else if (distance >= chaseDistance) {
			Debug.Log ("追逐->巡逻");
			//设置当前状态机的过渡条件为:丢失玩家(LOSTPLAYER)
			npc.GetComponent<AIController> ().SetTransition (Transition.LOSTPLAYER);
		} 
	}

	public override void Action (Transform player, Transform npc)
	{
		if (player != null) {
			nextPoint = player.position;
		}
		Quaternion targetQua = Quaternion.LookRotation (nextPoint - npc.position);
		npc.rotation = Quaternion.Lerp (npc.rotation, targetQua, Time.deltaTime * curRoSpeed);
		npc.Translate (Vector3.forward * Time.deltaTime * curSpeed);
	}

}
