using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LevelUI : MonoBehaviour
{   
    public Slider healthBar;

    public Sprite[] characterIcon;
    public Sprite[] barChange;

    public GameObject icon, bar;
    public int characterIdx = 0;
    public PlayerMovement fishPlayer;

    public MimiPlayerMvmt mimiPlayer;
    public TMP_Text fishCount, restartCt;
    public int fishInt, fishMax = 10;
    // Start is called before the first frame update
    void Start()
    {
      
    }
    public void ChangeBar(){
        bar.GetComponent<Image>().sprite = barChange[characterIdx];
        icon.GetComponent<Image>().sprite = characterIcon[characterIdx];
    }
    // Update is called once per frame
    void Update()
    {
        //icon.GetComponent<Image>().sprite = characterIcon[characterIdx];
        restartCt.text = ScoreVarPersistent.RestartCt.ToString();
        fishCount.text = fishInt.ToString();

        if (characterIdx == 0){
        healthBar.value = 1- fishPlayer.health;
        }
        else if (characterIdx == 1){
        healthBar.value = 1- mimiPlayer.health;
        }
    }
}
