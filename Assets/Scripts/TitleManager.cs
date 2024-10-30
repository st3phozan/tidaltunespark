using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleManager : MonoBehaviour
{
    public Animator curtainAnim;
    public bool isLoaded = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
         if (Input.GetKeyDown(KeyCode.Return) && isLoaded )
        {
            curtainAnim.SetBool("StartGame", true);
	    isLoaded = false;
        }
    }
    public void StartScreenLoaded(){
    isLoaded = true;
    }
    public void StartGame(){

	  SceneManager.LoadScene("Level1");

    }

}
