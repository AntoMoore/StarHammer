﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HighScoreTable : MonoBehaviour
{
    // == member variables ==
    private Transform entryContainer;
    private Transform entryTemplate;
    private List<HighScoreEntry> highScoreEntryList;
    private HighScoreList highScoreList;
    private List<Transform> highScoreEntryTransformList;

    // == gets/sets ==
    private void Awake() 
    {
        entryContainer = transform.Find("ScoreContainer");
        entryTemplate = entryContainer.Find("ScoreEntry");
        entryTemplate.gameObject.SetActive(false);

        // load high scores from data
        highScoreList = SaveSystem.LoadHighScores();
        if(highScoreList != null)
        {
            //highScoreEntryList = new List<HighScoreEntry>();
            highScoreEntryList = highScoreList.getHighScoreList();
        }
        else
        {
            // initalise new list with sample data (only happens when program is first run)
            highScoreEntryList = new List<HighScoreEntry>()
            {
                new HighScoreEntry(2500,"KAT"),
                new HighScoreEntry(3500, "KEN"),
                new HighScoreEntry(4000, "SAM"),
                new HighScoreEntry(4500, "JOE"),
                new HighScoreEntry(5000, "BOB")
            };

            // save score data
            SaveSystem.SaveHighScores(highScoreEntryList);
        }

        // sort list in descending order
        for(int i = 0; i < highScoreEntryList.Count; i++)
        {
            for(int j = i + 1; j < highScoreEntryList.Count; j++)
            {
                if(highScoreEntryList[j].score > highScoreEntryList[i].score)
                {
                    // swap scores
                    HighScoreEntry temp = highScoreEntryList[i];
                    highScoreEntryList[i] = highScoreEntryList[j];
                    highScoreEntryList[j] = temp;
                }
            }
        }

        // create a list of scores and display them in a table
        highScoreEntryTransformList = new List<Transform>();
        foreach (HighScoreEntry highScoreEntry in highScoreEntryList)
        {
            CreateHighScoreTransform(highScoreEntry, entryContainer, highScoreEntryTransformList);
        }
    }

    private void CreateHighScoreTransform(HighScoreEntry highScoreEntry, Transform container, List<Transform> transformList)
    {
        // == local variables ==
        float templateHeight = 30f;
        Transform entryTransform = Instantiate(entryTemplate, container);
        RectTransform entryRectTransform = entryTransform.GetComponent<RectTransform>();
        entryRectTransform.anchoredPosition = new Vector2(0, -templateHeight * transformList.Count);
        entryTransform.gameObject.SetActive(true);
        int rank = transformList.Count + 1;
        string rankText;
        
        // apply prefix and suffix to rank
        switch(rank)
        {
            case 1:
                rankText = "1st";
                break;
            case 2:
                rankText = "2nd";
                break;
            case 3:
                rankText = "3rd";
                break;
            default:
                rankText = rank + "th";
                break;
        }

        // apply rank text
        entryTransform.Find("PlayerRank").GetComponent<TextMeshProUGUI>().text = rankText;

        // apply player score
        int score = highScoreEntry.score;
        entryTransform.Find("PlayerScore").GetComponent<TextMeshProUGUI>().text = score.ToString();

        // apply player name
        string name = highScoreEntry.name;
        entryTransform.Find("PlayerName").GetComponent<TextMeshProUGUI>().text = name;

        // add entry to list
        transformList.Add(entryTransform);
    }
}
