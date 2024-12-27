using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial : MonoBehaviour
{
	private string tutorialText;
	public bool tutorialDone = false;
	public PlayerMovement player;
	public List<GameObject> tutorials = new List<GameObject>();

	public GameObject tutGroup;
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
  
   	if (i < (tutorials.Count -1)){
        tutorials[i].SetActive(false);   
        tutorials[i + 1].SetActive(true);
	i++;
}
else{
tutorialDone = true;
tutGroup.SetActive(false);
}
    
}
	
}