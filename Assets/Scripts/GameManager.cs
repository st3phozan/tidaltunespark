using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public AudioSource backgroundMusic;
    public bool Level1Override = true;
    public LevelUI levelUI;
    public PlayerMovement playerMovememt;

    // Start is called before the first frame update
    void Start()
    {
        backgroundMusic.Play();
    }

    // Update is called once per frame
    void Update()
    {
        if (Level1Override){
            levelUI.characterIdx = 0;
            playerMovememt.chosen = true;
        }
        
    }
}
