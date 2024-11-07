using Fusion;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomPlayer : NetworkBehaviour
{
	public static readonly List<RoomPlayer> Players = new List<RoomPlayer>();

	public static Action<RoomPlayer> PlayerJoined;
	public static Action<RoomPlayer> PlayerLeft;
	public static Action<RoomPlayer> PlayerChanged;

	public static RoomPlayer Local;

	[Networked(OnChanged = nameof(OnStateChanged))] public NetworkBool IsReady { get; set; }
	[Networked(OnChanged = nameof(OnStateChanged))] public NetworkString<_32> Username { get; set; }
	[Networked] public NetworkBool HasFinished { get; set; }
	[Networked] public int PlayerId { get; set; }


	private static void OnStateChanged(Changed<RoomPlayer> changed) => PlayerChanged?.Invoke(changed.Behaviour);

	public override void Spawned()
	{
		base.Spawned();

		PlayerPrefs.SetString("C_Username", "camo");
		PlayerPrefs.SetString("C_PlayerId", "1");

		if (Object.HasInputAuthority)
		{
			Local = this;

			PlayerChanged?.Invoke(this);
			// RPC_SetPlayerStats(ClientInfo.Username, ClientInfo.PlayerId);
			RPC_SetPlayerStats(PlayerPrefs.GetString("C_Username"), 1);
			Debug.Log("masukroomplayer");

		}

		Players.Add(this);
		// PlayerJoined?.Invoke(this);

		DontDestroyOnLoad(gameObject);
	}

	private void RPC_SetPlayerStats(NetworkString<_32> username, int playerId)
	{
		Debug.Log("masukRPC_SetPlayerStats");
		Username = username;
		PlayerId = playerId;
	}
}
