using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData
{
    // == member variables ==
    public int level;
    public int score;

    // == member methods ==
    public PlayerData(Player player)
    {
        level = player.getPlayerLevel();
        score = player.getTotalScore();
    }
}
