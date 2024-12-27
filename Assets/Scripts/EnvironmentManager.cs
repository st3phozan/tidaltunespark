using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvironmentManager : MonoBehaviour
{
    public List<GameObject> masks = new List<GameObject>();
    public int pastIdx = 0;
    public float minWaitTime = 1f;  
    public float maxWaitTime = 5f;  // Maximum wait time in seconds
    // Start is called before the first frame update
    void Start()
    {
        GameObject[] flickers = GameObject.FindGameObjectsWithTag("flickLights");
        masks.AddRange(flickers);
        StartCoroutine(LightFlicker());
 
    }
    private IEnumerator LightFlicker()
    {
        while (true)
        {
            float waitTime = Random.Range(minWaitTime, maxWaitTime);
            yield return new WaitForSeconds(waitTime);
            
            // Your action here
            int tempIdx = Random.Range(0, masks.Count);
            masks[tempIdx].GetComponent<SpriteMask>().enabled = false;

            masks[pastIdx].GetComponent<SpriteMask>().enabled = true;
            
            pastIdx = tempIdx;

        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
