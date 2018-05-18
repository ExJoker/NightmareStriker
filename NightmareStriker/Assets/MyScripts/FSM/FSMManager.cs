using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//分配状态的ID
public enum FSMStateID
{
	NONE = 0,
	PARTROL,
	CHASE,
	ATTACK,
	DEAD
}

//分配的转换条件的编号
public enum Transition
{
	//看见玩家
	SAWPLAYER,
	//追上玩家,玩家在攻击范围
	REACHPLAYER,
	//丢失玩家
	LOSTPLAYER,
	//血量为0
	NOHEALTH
}

public class FSMManager : FSM
{
	//状态列表
	List<FSMState> fsmStates;
	//当前的状态ID
	FSMStateID currentStateID;

	public FSMStateID CurrentStateID{ get { return currentStateID; } }
	//当前状态
	FSMState currentState;

	public FSMState CurrentState{ get { return currentState; } }

	//构造函数
	public FSMManager ()
	{
		fsmStates = new List<FSMState> ();
	}
	//添加状态
	public void AddFSMState (FSMState fsmState)
	{
		if (fsmState == null) {
			Debug.Log ("状态不能为空");
			return;
		}

		//如果当前列表为空,直接加入
		if (fsmStates.Count == 0) {
			fsmStates.Add (fsmState);
			currentState = fsmState;
			currentStateID = fsmState.ID;
			return;
		}
		//如果要加入的状态已经存在列表中,直接返回
		foreach (FSMState state in fsmStates) {
			if (state.ID == fsmState.ID) {
				Debug.Log ("状态已经存在列表中" + fsmState.ID);
			}
		}
		//如果要加入的状态不在列表中
		fsmStates.Add (fsmState);
	}

	//从状态列表中删除一个状态
	public void DeleteState (FSMStateID id)
	{
		foreach (FSMState state in fsmStates) {
			if (state.ID == id) {
				fsmStates.Remove (state);
				return;
			}
		}
		Debug.Log ("删除的状态不存在");
	}

	//根据当前状态和转换条件,转换到新状态
	public void PerformTransition (Transition transition)
	{
		//获取到当前状态对应的id
		FSMStateID id = currentState.GetStateID (transition);
		currentStateID = id;
		//遍历状态列表,如果查找到与条件满足的状态,存放在currentState中
		foreach (FSMState state in fsmStates) {
			if (state.ID == currentStateID) {
				currentState = state;
				break;
			}
		}
	}
}
