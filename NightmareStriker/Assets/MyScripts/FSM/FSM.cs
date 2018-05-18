using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FSM : MonoBehaviour
{
	protected Transform mainTank;
	//移动目标点
	protected Vector3 moveTargetPoint;
	//巡逻点集合
	protected GameObject[] wayPoints;
	//开火频率
	protected float shootRate;
	//开枪计时器
	protected float elapsedTime;
	//是否死亡
	public bool isDead;

	protected virtual void Initialize ()
	{
		
	}

	protected virtual void FSMFixedUpdate ()
	{
		
	}

	protected virtual void FSMUpdate ()
	{
		
	}

	void Start ()
	{
		Initialize ();
	}

	void FixedUpdate ()
	{
		FSMFixedUpdate ();
	}

	void Update ()
	{
		FSMUpdate ();
	}
}
