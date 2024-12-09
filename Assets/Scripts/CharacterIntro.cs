using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterIntro : MonoBehaviour
{
    public GameObject introUI;
    //public Animator curtains;
    public bool unlocked = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void OpenUI(){
       //curtains.SetTrigger("close"); 
       unlocked = true;
       introUI.SetActive(true);
    }
    public void CloseUI(){
       // curtains.SetTrigger("close");
        introUI.SetActive(false);
    }
    public void OnTriggerEnter2D(Collider2D col){
        if(col.gameObject.tag == "Player" && unlocked == false){
            OpenUI();
        }
    }
}
