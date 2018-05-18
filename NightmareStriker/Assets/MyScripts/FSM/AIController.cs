using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIController : FSMManager
{
	public Transform turret;
	GameObject bullet;
	Transform firePos;
	GameObject tankExplosion;
	int hp = 20;

	void InitQuote ()
	{
		turret = transform.Find ("TankRenderers/TankTurret");
		firePos = turret.GetChild (0);
		bullet = Resources.Load ("Prefabs/Shell") as GameObject;
		tankExplosion = Resources.Load ("Prefabs/TankExplosion") as GameObject;

	}

	void InitArgs ()
	{
		shootRate = 2f;
		elapsedTime = shootRate;
	}

	void constructFSM ()
	{
		mainTank = GameObject.FindGameObjectWithTag ("Player").transform;
		wayPoints = GameObject.FindGameObjectsWithTag ("WayPoint");

		Transform[] points = new Transform[wayPoints.Length];
		for (int i = 0; i < wayPoints.Length; i++) {
			points [i] = wayPoints [i].transform;
		}
		//构建巡逻状态
		Patrol patrol = new Patrol (points);
		patrol.AddTransition (Transition.SAWPLAYER, FSMStateID.CHASE);
		patrol.AddTransition (Transition.NOHEALTH, FSMStateID.DEAD);

		//构建追击状态
		Chase chase = new Chase (points);
		chase.AddTransition (Transition.REACHPLAYER, FSMStateID.ATTACK);
		chase.AddTransition (Transition.LOSTPLAYER, FSMStateID.PARTROL);
		//构建攻击状态
		Attack attack = new Attack (points);
		attack.AddTransition (Transition.SAWPLAYER, FSMStateID.CHASE);
		attack.AddTransition (Transition.NOHEALTH, FSMStateID.DEAD);
		attack.AddTransition (Transition.LOSTPLAYER, FSMStateID.PARTROL);

		//加入状态列表
		AddFSMState (patrol);
		AddFSMState (chase);
		AddFSMState (attack);

	}


	//重写父类中的初始方法
	protected override void Initialize ()
	{
		base.Initialize ();
		//构建FSM状态机
		constructFSM ();
		//初始化应用程序
		InitQuote ();
		//初始化变量
		InitArgs ();

	}

	protected override void FSMUpdate ()
	{
		base.FSMUpdate ();
		elapsedTime += Time.deltaTime;
	}

	protected override void FSMFixedUpdate ()
	{
		base.FSMFixedUpdate ();
		CurrentState.Reason (mainTank, this.transform);
		CurrentState.Action (mainTank, this.transform);
		Debug.Log ("当前状态为: " + CurrentState);
	}

	//设置新的状态
	public void SetTransition (Transition trans)
	{
		PerformTransition (trans);
	}

	//被攻击
	public void UnderFire ()
	{
		Debug.Log ("我被打了....");
		float x = Random.Range (-2f, -5f);
		GetComponent<Rigidbody> ().AddExplosionForce (10f,
			transform.position - new Vector3 (x, 2.0f, 0),
			20f);
		GetComponent<Rigidbody> ().velocity =
			transform.TransformDirection (new Vector3 (x, 2.0f, 0));
		hp -= Random.Range (10, 20);
		if (hp <= 0) {
			isDead = true;
			Destroy (gameObject, 0.5f);
			GameObject tankExp = Instantiate (tankExplosion, transform.position, transform.rotation);
			Destroy (tankExp, 1f);
			return;
		}

	}

	//Enemy开火的方法
	public void Fire ()
	{
		Debug.Log ("发射子弹");
		if (elapsedTime >= shootRate) {
			elapsedTime = 0;
			GameObject obj = Instantiate (bullet, firePos.position, firePos.rotation);
			obj.GetComponent<Rigidbody> ().velocity = obj.transform.forward * 80f;
		}

		 
	}

}
