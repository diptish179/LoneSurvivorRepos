using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinManager : MonoBehaviour
{
    public int coinValue = 100;
    public int coinCount = 1;
    // Start is called before the first frame update
    StatsManager stats;
    public void Start()
    {
        stats = GameObject.FindGameObjectWithTag("GameController").GetComponent<StatsManager>();

    }
    public void OnTriggerEnter2D(Collider2D col)
    {
        if(col.CompareTag("Player"))
        {
            Debug.Log("The player picked a collectable");
           stats.UpdateScore(coinValue);
           stats.UpdateCoin(coinCount);
           
            Destroy(this.gameObject);
        }
    }

}
