using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrainTransport : MonoBehaviour
{
    public bool isStart = true, drainEnter = false, endingDrain = false;
    public GameObject endDrain, player, camera, endUI;
     public EndUI endUIScript;
    private Vector3 camStart, camEnd;
    private float timeStart, timeStep;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (drainEnter){
        timeStep = (Time.time - timeStart)/3;
        camera.transform.position = new Vector3(Mathf.Lerp(camStart.x, endDrain.transform.position.x, timeStep), camera.transform.position.y, camera.transform.position.z);
        }
    }
    private IEnumerator TravelTimer(float duration)
    {
        yield return new WaitForSeconds(duration); // Wait for the given duration
        camera.transform.position = camStart;
        drainEnter = false;
        player.SetActive(true);
        player.transform.position = endDrain.transform.position;
        
       
        
    }
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player"){
            if (isStart){
                player = col.gameObject;
                player.SetActive(false);
                camStart = camera.transform.position;
                timeStart = Time.time;
                drainEnter = true;
                StartCoroutine(TravelTimer(3));
            }
            else if (endingDrain){
                player = col.gameObject;
                player.SetActive(false);
                endUI.SetActive(true);

                endUIScript.StartEndOfLevel();
            }
        }
    }
}
