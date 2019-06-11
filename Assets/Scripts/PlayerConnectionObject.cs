using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class PlayerConnectionObject : NetworkBehaviour
{
	
	public GameObject PlayerUnit;
	

	private PlayerScript _playerScript;
	//private Manager _managerScript;
//	[SyncVar]
//	private Transform _spawnPosition;
	[SyncVar]
	private GameObject _tempCamera;
	private Button _shootingBtn;
	//private GameObject manager;
	private Canvas _playerCanvas;

	public void SetPlayerCanvas(Canvas canvas)
	{
		_playerCanvas = canvas;
	}

	

	private void Start()
	{
		if (!isLocalPlayer)
		{
			
			return;
		}
		
		
		
		
		//since instantiate doesn't spawn an object in all the client machiene but only in your own
		//so we use network commands that only run on the servers
		
		Cmd_SpawnPlayerUnit();
		
	}


	#region Misc

	private bool guiIsOn = true;

	private void TurnOffGUIInSeconds(int seconds)
	{
		StartCoroutine(_TurnOffGUIInSeconds(seconds));
	}

	private IEnumerator _TurnOffGUIInSeconds(int seconds)
	{
		yield return new WaitForSeconds(seconds);
		guiIsOn = false;
	}

	void OnGUI()
	{
		if(guiIsOn)
		{
			
			GUI.Label(new Rect(5,5,500,500), "");
		}
	}

	#endregion
	

	private void Update()
	{
		if (!isLocalPlayer)
		{
			return;
		}

		if (Input.GetKeyDown(KeyCode.S))
		{
			Cmd_SpawnPlayerUnit();
		}

		if (Input.GetKeyDown(KeyCode.Q))
		{
			string newName = "Amr" + Random.Range(1, 100); 
			Debug.Log("PlayerConnectionObject::Update -- Sending a request to the server to change name");
			Cmd_ChangePlayerName(newName);
		}
		
		
	}

	
	// WARNING: If using a hook on a syncVar, then our local value does NOT get automatically updated.
	private void OnNameChanged(string newName)
	{
//		Debug.Log("PlayerConnectionObject::OnNameChanged -- Changing the name from " + PlayerName + " to " + newName);
//		
//		// WARNING: If using a hook on a syncVar, then our local value does NOT get automatically updated.
//		PlayerName = newName;
//		
//		gameObject.name = newName;
	}

	

	#region Commands

	[Command]
	private void Cmd_SpawnPlayerUnit()
	{
		
		TurnOffGUIInSeconds(100);
		GameObject playerUnit = Instantiate(PlayerUnit, transform.position, transform.rotation);
		//playerUnit.transform.parent = this.transform;

//		GameObject playerCanvas = Instantiate(PlayerCanvas);
//		playerCanvas.name = playerUnit.name + " Canvas";

		PlayerScript playerScript = playerUnit.GetComponent<PlayerScript>();
//		playerScript.Canvas = _playerCanvas.gameObject;
//		playerScript.ScoreText = _playerCanvas.GetComponent<FillPlayerCanvasVars>().ScoreText;
//		playerScript.FinalScoreText = _playerCanvas.GetComponent<FillPlayerCanvasVars>().GameOverText;
//		_shootingBtn = _playerCanvas.GetComponent<FillPlayerCanvasVars>().ShootingButton;	
//		_shootingBtn.onClick.AddListener(playerUnit.GetComponent<RayCastShooting>().Shoot);

		
		
		NetworkServer.SpawnWithClientAuthority(playerUnit, connectionToClient);
		//NetworkServer.SpawnWithClientAuthority(playerCanvas, connectionToClient);
	}

	[Command]
	private void Cmd_ChangePlayerName(string newName)
	{
//		Debug.Log("PlayerConnectionObject::Cmd_ChangePlayerName -- Applying name change in the server by " + newName);
//		PlayerName = newName;
		
		// To apply the change that the server listened to
		// we will use something called RPC that let the server talk to all the clients in the game
		// actually we don't need to use it since PlayeName is a SyncVar valuable
		//r	pc_ChanePlayerName(PlayerName);
	}
	
	#endregion

	#region RPCs

	[ClientRpc]
	private void Rpc_ChanePlayerName(string newName)
	{
		Debug.Log("PlayerConnectionObject::Rpc_ChangePlayerName -- Applying the change to all the other clients by " + newName);
	}
	

	#endregion
}
