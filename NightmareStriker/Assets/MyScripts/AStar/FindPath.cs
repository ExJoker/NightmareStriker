using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FindPath : MonoBehaviour
{
	//玩家的位置和终点的位置
	public Transform player, endPoint;
	//地图格子脚本
	Grid grid;


	void Start ()
	{
		//获得格子脚本
		grid = GetComponent<Grid> ();

	}

	void Update ()
	{
		//寻路
		FindingPath (player.position, endPoint.position);
	}

	void FindingPath (Vector3 StartPos, Vector3 endPos)
	{
		//通过起始点和终止点的位置获得对应的节点
		Node startNode = grid.GetNodeFromPosition (StartPos);
		Node endNode = grid.GetNodeFromPosition (endPos);

		//开启列表
		List<Node> openSet = new List<Node> ();
		List<Node> CloseSet = new List<Node> ();

		//将起始点先加入到OpenSet中
		openSet.Add (startNode);

		//通过循环遍历,找出开启列表中小号值f最小节点
		while (openSet.Count > 0) {
			//当前节点
			Node currentNode = openSet [0];
			//遍历如果开启列表集合中节点的开销f小于当前节点的开销f 
			//或者等于当前节点的开销f并且开启列表中节点距离目标点h小于
			//当前节点的h,这时,我们更换这个列表中的节点为当前节点
			for (int i = 0; i < openSet.Count; i++) {
				if (openSet [i].fCost < currentNode.fCost ||
				    openSet [i].fCost == currentNode.fCost &&
				    openSet [i].hCost < currentNode.hCost) {
					currentNode = openSet [i];
				}
			}
			//从开启列表中移除当前节点
			openSet.Remove (currentNode);
			//将其加入关闭列表
			CloseSet.Add (currentNode);

			//如果当前节点是结束节点
			if (currentNode == endNode) {
				//生成路径
				GeneratePath (startNode, endNode);
				//已经查找到最优路径,结束查询
				return;
			}
			//遍历当前节点中周围的八个节点(当前节点已经是开销f最小的节点)
			foreach (Node node in grid.GetNeibourNode(currentNode)) {
				//如果该节点不能走,或者该节点已经在关闭列表里了
				if (!node._canWalk || CloseSet.Contains (node)) {
					continue;
				}
				//计算当前节点到开始节点的距离+当前节点和相邻节点之间的距离
				int newCost = currentNode.gCost + GetDistanceBetweenTwoNode (currentNode, node);
				//判断这个新的开销和原来开销的大小关系
				if (newCost < node.gCost || !openSet.Contains (node)) {
					node.gCost = newCost;
					//获得这个节点的预估值h
					node.hCost = GetDistanceBetweenTwoNode (node, endNode);
					//将这个节点的父物体设置为当前节点
					node.parent = currentNode;
					//如过node没有在开启列表中,加入进入
					if (!openSet.Contains (node)) {
						openSet.Add (node);
					}
				}
			}
		}
	}

	//得到两个节点之间的距离
	int GetDistanceBetweenTwoNode (Node a, Node b)
	{
		//表示横轴上间隔的格子
		int x = Mathf.Abs (a._gridX - b._gridX);
		//表示纵轴上间隔的格子
		int y = Mathf.Abs (a._gridY - b._gridY);

		//表示横轴上间隔的格子数比纵轴上间隔的格子多
		if (x > y) {
			return 14 * y + 10 * (x - y);
		} else {
			return 14 * x + 10 * (y - x);
		}
	}
	//生成路径
	void GeneratePath (Node startNode, Node endNode)
	{
		List<Node> path = new List<Node> ();

		//从最终节点找其父节点,回溯到起始节点
		Node temp = endNode;
		while (temp != startNode) {
			//将该节点放到Path中
			path.Add (temp);
			temp = temp.parent;
		}
		//将路径列表反转,因为路径要从开始节点开始
		path.Reverse ();
		//将计算好的路径传递给Grid脚本中的path，进行绘制
		grid.path = path;
	}
}
