using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    // ==  member methods ==
    public void NewGame()
    {
        // load game level1
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void LoadGame()
    {
        // load player data
        PlayerData data = SaveSystem.LoadPlayer();
        if(data != null)
        {
            if(data.level != 4)
            {
                SceneManager.LoadScene(data.level + 1);
            }
            else
            {
                Debug.Log("Game Completed...");
            }
        }
        else
        {
            Debug.Log("No Player Data Found...");
        }
    }

    public void QuitGame()
    {
        Debug.Log("Quit!");
        //Application.Quit();
    }
}
