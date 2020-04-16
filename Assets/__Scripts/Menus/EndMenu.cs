using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;


public class EndMenu : MonoBehaviour
{
    // == member variables == 
    [SerializeField] TextMeshProUGUI scoreText;
    [SerializeField] GameObject inputText;

    private string playerName;
    private int playerScore;

    // == member methods ==
    private void Start() 
    {
        // get player total score
        PlayerData data = SaveSystem.LoadPlayer();
        if(data != null)
        playerScore = data.score;

        scoreText.text = playerScore.ToString();
    }

    public void SubmitScore()
    {
        // get player name from input box
        playerName = inputText.GetComponent<TextMeshProUGUI>().text;

        // add new score 
        SaveSystem.AddHighScoreEntry(playerScore, playerName);

        // back to main menu
        this.ReturnToMain();
    }

    public void ReturnToMain()
    {
        SceneManager.LoadScene("Level0");
    }
}
