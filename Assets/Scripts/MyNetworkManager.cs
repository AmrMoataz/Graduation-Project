using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class MyNetworkManager : NetworkManager
{

	[Header("Other Properties")]
	[Space(10)]
	public bool Spawn = false;

	public GameObject TempCamera;
	public Canvas GameplayCanvas;

	private int _playersNum = 0;
	
	
	public override void OnServerAddPlayer(NetworkConnection conn, short playerControllerId)
	{
		if(!Spawn) return;;

		_playersNum++;
		TempCamera.SetActive(false);
		
		Transform startPosition = GetStartPosition();
		
		Debug.Log(startPosition.rotation);

		GameObject player = Instantiate(playerPrefab, startPosition.position, startPosition.rotation);
		player.GetComponent<PlayerConnectionObject>().SetPlayerCanvas(GameplayCanvas);
		player.name = "Player" + _playersNum;

		NetworkServer.AddPlayerForConnection(conn, player, playerControllerId);
	}
}
