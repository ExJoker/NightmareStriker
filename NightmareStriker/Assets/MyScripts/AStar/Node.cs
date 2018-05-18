using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node
{
	//判断当前格子是否可走
	public bool _canWalk;
	//用来保存节点的位置
	public Vector3 _workdPos;
	//当前节点对应的网格下标
	public int _gridX, _gridY;

	//节点与起始点的距离
	public int gCost;
	//节点与目标点的预估距离
	public int hCost;
	//表示g+h,为当前节点所在格子的开销
	public int fCost {
		get {
			return gCost + hCost;
		}
	}

	//声明当前节点对应格子的父节点
	public Node parent;
	//构造方法
	/// <summary>
	/// </summary>
	/// <param name="canWalk">当前格子否可走</param>
	/// <param name="position">当前格子对应节点的世界坐标</param>
	/// <param name="x">格子所在二维数组里的x值</param>
	/// <param name="y">格子所在二维数组里的y值</param>
	public Node (bool canWalk, Vector3 position, int x, int y)
	{
		_canWalk = canWalk;
		_workdPos = position;
		_gridX = x;
		_gridY = y;
	}
}
