using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Farmer : MonoBehaviour
{
	public GameObject goatObject; //Goat object to chase
	[HideInInspector]
	public Vector3 goatPos; //Goat's position when scanned last
	[HideInInspector]
	public static float scanTime = 1.0f; //Interval of time between scanning for goat location
	[HideInInspector]
	public float lastScan = 0.0f; //Time of last scan

	[HideInInspector]
	public bool movementEnabled = true; //Enable movement of farmer

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
    	//Find out where the goat currently is if we need to
    	if(Time.time - this.lastScan > scanTime){
    		goatPos = new Vector3(goatObject.transform.position.x, goatObject.transform.position.y, goatObject.transform.position.z);
    		this.lastScan = Time.time; //Record the time we checked its location at
    	}
        
        if(this.movementEnabled){
	        //Move towards the goat at all times
	        transform.position = Vector3.MoveTowards(transform.position, goatPos, 0.15f);
	    }
    }
}