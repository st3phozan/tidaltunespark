using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GuardScript : MonoBehaviour
{
    public Transform minPt, maxPt;
    public float speed = 2;
    public Animator guardAnim;
    public GameObject flashL, flashR;
    Rigidbody2D rb;
    private Vector3 direction;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        direction = Vector3.left;
        flashL.SetActive(true);
        flashR.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        //movement
        rb.velocity = direction*speed;
        if (transform.position.x <= minPt.position.x ){
            direction = Vector3.right;
            flashL.SetActive(false);
            flashR.SetActive(true);
            guardAnim.SetBool("right", true);
            guardAnim.SetBool("left", false);

        }
        else if (transform.position.x >= maxPt.position.x ){
            direction = Vector3.left;
            flashL.SetActive(true);
            flashR.SetActive(false);
            guardAnim.SetBool("right", false);
            guardAnim.SetBool("left", true);

        }

        
    }
    public void CaughtMimi(){
        speed = 0;
        StartCoroutine(GameEnd());
    }
    IEnumerator GameEnd(){

        yield return new WaitForSeconds(2);
        SceneManager.LoadScene("Level2");
        ScoreVarPersistent.RestartCt += 1;
    }
}
