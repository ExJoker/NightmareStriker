using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 该类是所有状态的基类,它的所有派生类都代表FSM中的某一个状态
/// </summary>
abstract public class FSMState
{
	//声明记录所有转换条件-状态的映射关系集合
	protected Dictionary<Transition,FSMStateID> map = new Dictionary<Transition, FSMStateID> ();
	//获取状态ID
	protected FSMStateID stateID;

	public FSMStateID ID{ get { return stateID; } }

	//与状态相关的一些字段
	//目标点的位置
	protected Vector3 nextPoint;
	//巡逻点的集合
	protected Transform[] wayPoints;
	//移动速度
	protected float curSpeed;
	//转向速度
	protected float curRoSpeed;
	//追击临界距离(当AI角色与玩家之间的距离小于这个值,开始追逐)
	protected const float chaseDistance = 40f;
	//攻击临界距离(当AI角色与玩家之间的距离小于这个值,开始攻击)
	protected const float attackDistance = 30f;
	//到达目标点的临界值(距离巡逻点小于这个值,认为到达)
	protected const float arriveDistance = 3f;

	//该抽象方法用于转换到其他状态
	public abstract void Reason (Transform player, Transform npc);
	//该抽象方法用于实现当前状态下所对应的行为
	public abstract void Action (Transform player, Transform npc);

	//添加映射关系的方法(转换条件-状态ID)
	public void AddTransition (Transition transition, FSMStateID id)
	{
		//检查该映射关系是否已经存在了
		if (map.ContainsKey (transition)) {
			Debug.Log ("映射关系已经存在");
			return;
		}

		map.Add (transition, id);
		Debug.Log ("添加转换条件: " + transition + "状态id: " + id);
	}
	//删除映射关系
	public void DeleteTransition (Transition transition)
	{
		if (map.ContainsKey (transition)) {
			map.Remove (transition);
			return;
		}
		Debug.LogError ("FSMState Error:转换条件不存在");
	}

	//通过某个转换条件查询对应的状态id
	public FSMStateID GetStateID (Transition transition)
	{
		if (map.ContainsKey (transition)) {
			return map [transition];
		}
		Debug.Log ("转换条件不存在");
		return FSMStateID.NONE;
	}

	//随机从巡逻点集合中取出一个点,作为移动目标
	public void FindNextPoint ()
	{
		int randomIndex = Random.Range (0, wayPoints.Length);
	
		nextPoint = wayPoints [randomIndex].position;
	}


}
