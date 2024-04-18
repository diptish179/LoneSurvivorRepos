using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatsManager : MonoBehaviour
{
    public int score;
    public int coinCount;

    // Start is called before the first frame update

    public void UpdateCoin(int coin)
    {
        coinCount += coin;
        Debug.Log(coinCount);

    }

    public void UpdateScore(int newScore)
    {
        score += newScore;
        Debug.Log(score);
    }


}
//SINGLE RESPONSIBILITY 