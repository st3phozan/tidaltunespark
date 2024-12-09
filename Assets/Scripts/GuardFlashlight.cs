using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuardFlashlight : MonoBehaviour
{
    public MimiPlayerMvmt detectedMimi;
    public GuardScript guardMvmt;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public void OnTriggerStay2D(Collider2D col){
        if (col.gameObject.tag == "Mimi"){
            Debug.Log("Mimi");
            if(detectedMimi.isCamo == false){
                Debug.Log("Mimi is CAUGHT");
                guardMvmt.CaughtMimi();
            }
        }
    }
}
