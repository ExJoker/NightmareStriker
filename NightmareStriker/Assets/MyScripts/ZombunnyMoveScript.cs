using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ZombunnyMoveScript : MonoBehaviour
{
	
	//导航组件
	NavMeshAgent nav;
	//获取玩家
	Transform player;
	//获取Animator组件(动画状态机)
	Animator ZAnim;
	int z_move, z_death;
	//血量
	public float Z_HP = 50f;
	//怪物受伤和死亡的声音
	AudioSource source;
	public AudioClip Z_Hurt;
	public AudioClip Z_Dead;
	//粒子系统
	ParticleSystem Z_Particle;
	//碰撞器组件
	CapsuleCollider capCollider;
	//触发器组件
	SphereCollider sphCollider;


	void Start ()
	{
		capCollider = GetComponent<CapsuleCollider> ();
		sphCollider = GetComponent<SphereCollider> ();
		nav = GetComponent<NavMeshAgent> ();
		player = GameObject.FindGameObjectWithTag ("Player").transform;
		source = GetComponent<AudioSource> ();
		ZAnim = GetComponent<Animator> ();
		z_move = Animator.StringToHash ("Move");
		z_death = Animator.StringToHash ("Death");
		Z_Particle = gameObject.GetComponentInChildren<ParticleSystem> ();
	}


	void Update ()
	{
		if (Vector3.Distance (transform.position, player.position) < 5f) {
			nav.isStopped = true;
			ZAnim.SetBool (z_move, false);
		} else {
			nav.isStopped = false;
			nav.destination = player.position;
			transform.LookAt (player.position);
			ZAnim.SetBool (z_move, true);
		}
	}
	//怪物收到的伤害
	public void ZDamage (float damage)
	{
		Z_HP -= damage;
		if (Z_HP <= 0) {
			ZDeadth ();
		} else {
			//受伤粒子
			Z_Particle.Play ();
			source.clip = Z_Hurt;
			source.Play ();
		}
	}
	//怪物死亡
	public void ZDeadth ()
	{
		//关闭碰撞器
		capCollider.enabled = false;
		//关闭触发器
		sphCollider.enabled = false;
		//获取怪物死亡动画
		ZAnim.SetBool (z_death, true);
		//播放死亡声音
		source.clip = Z_Dead;
		source.Play ();
		Z_Particle.Play ();
		//关闭导航系统
		nav.enabled = false;
		//关闭脚本
		enabled = false;
		Destroy (gameObject, 1.5f);
	}
}
