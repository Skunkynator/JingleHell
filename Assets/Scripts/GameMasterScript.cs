using UnityEngine;
using UnityEngine.SceneManagement;

public class GameMasterScript : MonoBehaviour
{
	[SerializeField]
	GameObject player;
	[SerializeField]
	CharacterController playerCharacterController;
	[SerializeField]
	GameObject gameOverUI;

	void Awake()
	{

		Time.timeScale = 1;
	}

	public void GameOver()
	{
		Debug.Log("Game Over");
		gameOverUI.SetActive(true);
		Time.timeScale = 0;
		Destroy(player);
	}
	
	public void ReloadCurrentLevel()
	{
		Time.timeScale = 1;
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
