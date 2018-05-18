using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 攻击状态
/// </summary>
public class Attack : FSMState
{
	public Attack (Transform[] points)
	{
		wayPoints = points;
		stateID = FSMStateID.ATTACK;
		FindNextPoint ();
		curSpeed = 15f;
		curRoSpeed = 6f;
		//更换巡逻点
		FindNextPoint ();
	}

	public override void Reason (Transform player, Transform npc)
	{
		if (player != null) {
			nextPoint = player.position;
		}
		float distance = Vector3.Distance (nextPoint, npc.position);
		if (distance > attackDistance && distance < chaseDistance) {
			Debug.Log ("攻击->追击");
			npc.GetComponent<AIController> ().SetTransition (Transition.SAWPLAYER);
		} else if (npc.GetComponent<AIController> ().isDead) {
			Debug.Log ("攻击-死亡");
			npc.GetComponent<AIController> ().SetTransition (Transition.NOHEALTH);
		} else if (player == null) {
			Debug.Log ("玩家死亡,攻击-巡逻");
			npc.GetComponent<AIController> ().SetTransition (Transition.LOSTPLAYER);
		}
	}

	public override void Action (Transform player, Transform npc)
	{
		if (player != null) {
			nextPoint = player.position;
		}
		Transform turrent = npc.GetComponent<AIController> ().turret;
		Quaternion targetQua = Quaternion.LookRotation (nextPoint - npc.position);
		turrent.rotation = Quaternion.Lerp (turrent.rotation, targetQua, Time.deltaTime * curRoSpeed);
		npc.GetComponent<AIController> ().Fire ();
	}


}
