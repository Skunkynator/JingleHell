using UnityEngine;
using UnityEngine.SceneManagement;

public class GameMasterScript : MonoBehaviour
{
	public static GameMasterScript instance;

	[SerializeField]
	GameObject player;
	[SerializeField]
	CharacterController playerCharacterController;
	[SerializeField]
	GameObject gameOverUI;

	void Awake()
	{
		instance = this;
		//gameOverUI = GameObject.FindGameObjectWithTag("GameOverUI");
		Time.timeScale = 1;
	}

	public void GameOver()
	{
		Debug.Log("Game Over");
		if(gameOverUI != null)
		{
			gameOverUI.SetActive(true);
		}
		else
		{
			print("NO");
		}
		Time.timeScale = 0;
	}
	
	public void ReloadCurrentLevel()
	{
		SceneManager.LoadScene(SceneManager.GetActiveScene().name);
	}

	public void LoadLevel(int buildIndex)
	{
		SceneManager.LoadScene(buildIndex);
	}

	public void LoadMainMenu()
	{
		SceneManager.LoadScene("MainMenu");
	}

	public void TogglePauseGame()
	{

	}

	public void QuitGame()
	{
		Application.Quit();
	}
}
