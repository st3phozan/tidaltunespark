using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishTank : MonoBehaviour
{
    public bool freed = false;
    public LevelUI mainUI; 
    public int fishInTank;
    public Animator fishFly;


    // Start is called before the first frame update
    void Start()
    {
        
    }
    public void FreeFish(){
        mainUI.fishInt += fishInTank;
        fishFly.SetBool("freedFish", true); 
	Destroy(this.gameObject);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
