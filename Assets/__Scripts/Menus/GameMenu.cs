using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameMenu : MonoBehaviour
{
    // == member variables ==
    public static bool isPaused = false;
    public GameObject pausedMenuUI;
    public GameObject optionsMenuUI;
    public GameObject defeatMenuUI;
    public GameObject victoryMenuUI;
    public GameObject dialogueMenuUI;

    // == member methods ==
    void Update()
    {
        // if ESC is pressed pause game
        if (Input.GetKeyDown(KeyCode.Escape)) 
        {
            if (isPaused)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }
    }

    public void ResumeGame()
    {
        // turn off menus and restart game time
        pausedMenuUI.SetActive(false);
        optionsMenuUI.SetActive(false);
        dialogueMenuUI.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;
    }

    public void PauseGame()
    {
        // turn on menu and stop game time
        pausedMenuUI.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;
    }

    public void PlayerDefeat()
    {
        // turn on defeat menu and pause game
        defeatMenuUI.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;
    }

    public void PlayDialogue()
    {
        // show NPC dialogue
        dialogueMenuUI.transform.Find("NPC").GetComponent<DialogueTrigger>().TriggerDialogue();
        Invoke("StopGamePlay", 0.5f);
    }

    public void PlayerVictory()
    {
        // turn on victory menu and pause game
        victoryMenuUI.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;
    }

    public void ReturnMainMenu()
    {
        // load level0 and restart game time
        Time.timeScale = 1f;
        isPaused = false;
        SceneManager.LoadScene("Level0");
    }

    public void RestartLevel()
    {
        // reload scene and restart game time
        Time.timeScale = 1f;
        isPaused = false;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void NextLevel()
    {
        // load next scene and restart game time
        Time.timeScale = 1f;
        isPaused = false;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    private void StopGamePlay()
    {
        // stop game time
        Time.timeScale = 0f;
        isPaused = true;
    }

    public void ResumeGamePlay()
    {
        // restart game time
        Time.timeScale = 1f;
        isPaused = false;
    }
}
