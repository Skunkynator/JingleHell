using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMasterScript : MonoBehaviour
{
	[SerializeField]
	GameObject player;
	CharacterController playerCharacterController;


	void Awake()
	{
		playerCharacterController = player.GetComponent<CharacterController>();
	}

	public void gameOver()
	{
		Debug.Log("Game Over");
		Time.timeScale = 0;
		Destroy(player);
	}
}
