using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial : MonoBehaviour
{
	private string tutorialText;
	public bool tutorialDone = false;
	public PlayerMovement player;
	public List<GameObject> tutorials = new List<GameObject>();
	private int i = 0; 
	// Start is called before the first frame update
	void Start()
	{
		
	}

	// Update is called once per frame
	void Update()
	{
	}

	
	public void TutChange()
{
    // Ensure 'i' is defined and the loop syntax is correct
   	if (i < (tutorials.Count -1)){
        tutorials[i].SetActive(false);    // Disable the current tutorial
        tutorials[i + 1].SetActive(true); // Enable the next tutorial
	i++;
}
else{
tutorialDone = true;
}
    
}
	public void Tut2()
	{
		tutorialText = "Hold and release the spacebar to launch Finn from tank to tank";
	}

	public void Tut3()
	{
		tutorialText = "Hold down the spacebar longer for farther and higher jumps";
	}

	public void Tut4()
	{
		tutorialText = "Make sure to land in the tanks, landing on the table causes you to lose air";
	}

	public void Tut5()
	{
		tutorialText = "QUICK, get back in the water";
	}

	public void Tut6()
	{
		tutorialText = "Free the other fish by landing in the tank with them";
	}
}