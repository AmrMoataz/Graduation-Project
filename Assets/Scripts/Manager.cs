using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Manager : NetworkBehaviour
{
	public List<Transform> SpawnPoints;
	[SyncVar]
	public int PlayerCount = 1;
	[HideInInspector]
	[SyncVar]
	public Transform SpawnPoint;

	public void GetSpawnPoint()
	{
		int index = Random.Range(0, SpawnPoints.Count);
		if (SpawnPoints[index].gameObject.activeSelf)
		{
			SpawnPoint = SpawnPoints[index];
			SpawnPoints[index].gameObject.SetActive(false);
			Cmd_ApplySpawnPoint(SpawnPoints[index]);
		}
	}

	#region Commads
	
	private void Cmd_ApplySpawnPoint(Transform pos)
	{
		pos.gameObject.SetActive(false);
		Rpc_ApplySpawnPoint(pos);
	}

	#endregion

	#region RPCs

	private void Rpc_ApplySpawnPoint(Transform pos)
	{
		pos.gameObject.SetActive(false);
	}

	#endregion

}
