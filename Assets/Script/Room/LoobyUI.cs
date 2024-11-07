using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoobyUI : MonoBehaviour
{
	public GameObject textPrefab;
	public Transform parent;

	private static readonly Dictionary<RoomPlayer, LoobyItemUI> ListItems = new Dictionary<RoomPlayer, LoobyItemUI>();
	// private List<String> ListItems = new List<String>();
	private static bool IsSubscribed;

	public void Setup()
	{
		Debug.Log("masukSetup");

		// AddPlayer();

		/*var obj = Instantiate(textPrefab, parent).GetComponent<String>();
		ListItems.Add(obj);*/

		// if (IsSubscribed) return;

		RoomPlayer.PlayerJoined += AddPlayer;
		// RoomPlayer.PlayerLeft += RemovePlayer;

		// RoomPlayer.PlayerChanged += EnsureAllPlayersReady;

		// readyUp.onClick.AddListener(ReadyUpListener);

		// IsSubscribed = true;
	}

	private void AddPlayer(RoomPlayer player)
	{
		// RoomPlayer player = new RoomPlayer();


		if (ListItems.ContainsKey(player))
        {
            var toRemove = ListItems[player];
            Destroy(toRemove.gameObject);

            ListItems.Remove(player);
        }

        var obj = Instantiate(textPrefab, parent).GetComponent<LoobyItemUI>();
         // obj.SetPlayer(player);	

		ListItems.Add(player, obj);
		// ListItems.Add(obj);

		// UpdateDetails(GameManager.Instance);

		Debug.Log("called");
	}
}
