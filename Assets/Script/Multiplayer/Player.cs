using Fusion;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : NetworkBehaviour
{
	// private NetworkCharacterControllerPrototype _cc;
	private NetworkRigidbody2D _cc;

	private void Awake()
	{
		_cc = GetComponent<NetworkRigidbody2D>();
	}

	public override void FixedUpdateNetwork()
	{
		Debug.Log("asdqwe");
		if (GetInput(out NetworkInputData data))
		{
			data.movement.Normalize();
			//_cc.Move(5 * data.movement * Runner.DeltaTime);
			_cc.WriteVelocity(5 * data.movement);
			
		}
	}
}
