using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LevelUI : MonoBehaviour
{
    public Slider healthBar;
    public PlayerMovement player;
    public TMP_Text fishCount;
    public int fishInt, fishMax = 10;
    // Start is called before the first frame update
    void Start()
    {
      
    }

    // Update is called once per frame
    void Update()
    {
        fishCount.text = fishInt.ToString() + "/" + fishMax.ToString();
        healthBar.value = 1- player.health;
    }
}
