using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHUD : MonoBehaviour
{
    [SerializeField] PlayerController player;
    public GameObject enemy;
    [SerializeField] Image foreground;
    [SerializeField] TMP_Text hpText;
    [SerializeField] Image bloodOverlay;
    [SerializeField] Image powerbar;
    [SerializeField] TMP_Text powerText;
    [SerializeField] Image ultimatebar;
    [SerializeField] TMP_Text ultimateText;
    [SerializeField] TMP_Text goldCoinsText;
    [SerializeField] Image deathSkull;
    [SerializeField] TMP_Text deathCountText;

    private float blinkDuration = 0.5f; // duration of each blink
    private float glowDuration = 1f; // duration of each glow cycle
    private Color originalColor; // the original color of the bloodOverlay image
    private Color originalUltimateTextsColor;
    private Color originalhpTextColor;


    // Start is called before the first frame update
    void Start()
    {
        //originalColor = bloodOverlay.color; // cache the original color
        //originalUltimateTextsColor = ultimateText.color;
        //originalhpTextColor = hpText.color;
        enemy = GameObject.FindGameObjectWithTag("Enemy");
        // At the start of the game
        //TitleManager.saveData.killCount = 0;
        //TitleManager.saveData.goldCoins = 0;
    }

    // Update is called once per frame
    void Update()
    {
        double hpRatio = player.currentHP / player.maxHP;
        foreground.transform.localScale = new Vector3((float)hpRatio, 1, 1);
        hpText.text = "HEALTH " + Math.Round(hpRatio * 100);

        //Power bar controls
        //double powerRatio = player.currentPower / player.maxPower;
        //powerbar.transform.localScale = new Vector3((float)powerRatio, 1, 1);
        //powerText.text = "POWER " + Math.Round(powerRatio * 100);

    }

}

