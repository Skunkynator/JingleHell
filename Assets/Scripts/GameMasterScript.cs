using UnityEngine;
using UnityEngine.SceneManagement;

public class GameMasterScript : MonoBehaviour
{
	public static GameMasterScript instance { get; private set; }

	[SerializeField]
	internal GameObject player;
	[SerializeField]
	internal PlayerController playerCharacterController;
	[SerializeField]
	internal GameObject currentRoom;
	[SerializeField]
	internal GameObject gameOverUI;
	[SerializeField]
	internal GameObject pauseMenuUI;

	internal bool paused = false;

	private void Awake()
	{
		instance = this;
		Time.timeScale = 1;
	}

	internal void GameOver()
	{
		gameOverUI.SetActive(true);
		Time.timeScale = 0;
	}

	internal void ReloadCurrentLevel()
	{
		SceneManager.LoadScene(SceneManager.GetActiveScene().name);
	}

	internal void LoadLevel(int buildIndex)
	{
		SceneManager.LoadScene(buildIndex);
	}

	internal void LoadMainMenu()
	{
		SceneManager.LoadScene("MainMenu");
	}

	internal void TogglePauseMenu()
	{
		if (!paused)
		{
			Time.timeScale = 0;
			pauseMenuUI.SetActive(true);
			paused = true;
		}
		else
		{
			pauseMenuUI.SetActive(false);
			Time.timeScale = 1;
			paused = false;
		}
	}

	internal void ClosePauseMenu()
	{
		pauseMenuUI.SetActive(false);
		Time.timeScale = 1;
		paused = false;
	}

	internal void QuitGame()
	{
		Application.Quit();
	}
}
