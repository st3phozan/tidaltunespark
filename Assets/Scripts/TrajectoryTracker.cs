using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrajectoryTracker : MonoBehaviour
{
    public GameObject player;
    public LineRenderer trajRenderer;
	public int trajPoints = 1000;
	public float intervalPoints = .01f;
    public Transform startPos;
    public bool trajOn = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    public void launchTraj() {
    }
    // Update is called once per frame
    void Update()
    {
        if (trajOn){
            trajRenderer.enabled = true;
        }
        else{
            trajRenderer.enabled = false;
        }
    }
    
    /*public void TrajectoryStart(){
        Vector3 origin = startPos.position;
        Vector3 startVelocity = player.GetComponent<Rigidbody2D>().velocity;

        trajRenderer.positionCount = trajPoints;
        float time =0;
        int i = 0;
        //for (int i = 0; i < trajPoints; i++){

            var x = (startVelocity.x + time) + (Physics.gravity.x/2 *time *time);
            var y = (startVelocity.y + time) + (Physics.gravity.y/2 *time *time);
            Vector3 point = new Vector3(x, y, 0);
            trajRenderer.SetPosition(i, origin + point);
            i++;
            time += intervalPoints;
        //}
    }*/
}
