using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSwitch : MonoBehaviour
{
    enum CharacterPlay { fish, octo, crab };
    CharacterPlay charInPlay;
    public CameraTracker cameraTracker;

    // References to each character's GameObject
    public GameObject fishObject, movingFishObject;
    public GameObject octoObject;
    public GameObject crabObject;

    public LevelUI levelUI;

    public PlayerMovement fishMvmt;

    public MimiPlayerMvmt octoMvmt;
    public GameObject playerPos;


    // Start is called before the first frame update
    void Start()
    {
        // Set initial character
        charInPlay = CharacterPlay.fish;

        UpdateCharacter();
    }

    // Update is called once per frame
    void Update()
    {

        //Finn is Key 1, Mimi is Key 2
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            fishMvmt.transform.position = playerPos.transform.position;
            charInPlay = CharacterPlay.fish;

            UpdateCharacter();
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            octoMvmt.transform.position = playerPos.transform.position;
            charInPlay = CharacterPlay.octo;
            UpdateCharacter();
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            charInPlay = CharacterPlay.crab;
            UpdateCharacter();
        }
    }

    void UpdateCharacter()
    {


        fishMvmt.chosen = false;
        fishObject.SetActive(false);
        octoObject.SetActive(false);
        crabObject.SetActive(false);
        levelUI.characterIdx = 0;

        switch (charInPlay)
        {
            case CharacterPlay.fish:
   

                cameraTracker.CharacterChange(movingFishObject);
                fishMvmt.chosen = true;
                fishMvmt.chooseCharacter();
                Debug.Log("Switched to Fish");
                levelUI.characterIdx = 0;
                levelUI.ChangeBar();
                fishObject.SetActive(true);


                break;

            case CharacterPlay.octo:
                cameraTracker.CharacterChange(octoObject);

                octoObject.SetActive(true);
                Debug.Log("Switched to Octo");
                levelUI.characterIdx = 1;
                levelUI.ChangeBar();
              
                break;

            case CharacterPlay.crab:

                crabObject.SetActive(true);
                Debug.Log("Switched to Crab");


                break;
        }
    }
}
