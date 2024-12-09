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

        // Switch character based on key press
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
        // Example code for managing character-specific behaviors

        // Disable all character objects initially
        //fishObject.SetActive(false);
        fishMvmt.chosen = false;
        fishObject.SetActive(false);
        octoObject.SetActive(false);
        crabObject.SetActive(false);
        levelUI.characterIdx = 0;

        switch (charInPlay)
        {
            case CharacterPlay.fish:
                // Enable fish object and apply specific behavior for fish
                //ishObject.SetActive(true);\
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
                // Enable octo object and apply specific behavior for octo
                octoObject.SetActive(true);
                Debug.Log("Switched to Octo");
                levelUI.characterIdx = 1;
                levelUI.ChangeBar();
                // Example octo-specific behavior
                // (e.g., enable ink spray ability or play tentacle animation)
                //octoObject.GetComponent<Animator>().SetTrigger("SprayInk");
                break;

            case CharacterPlay.crab:
                // Enable crab object and apply specific behavior for crab
                crabObject.SetActive(true);
                Debug.Log("Switched to Crab");

                // Example crab-specific behavior
                // (e.g., enable side-walking or pinch animation)
                crabObject.GetComponent<Animator>().SetTrigger("PinchClaws");
                break;
        }
    }
}
