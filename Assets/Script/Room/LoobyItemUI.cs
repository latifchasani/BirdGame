using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoobyItemUI : MonoBehaviour
{
    public Text username;

    private RoomPlayer _player;

    public void SetPlayer(RoomPlayer player)
    {
        _player = player;
    }

    private void Update()
    {
        /*if (_player.Object != null && _player.Object.IsValid)
        {
            Debug.Log("masukLoobyItemUI");
            username.text = _player.Username.Value;
        }*/
    }
}
