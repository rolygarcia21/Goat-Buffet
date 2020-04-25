using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Farmer : MonoBehaviour
{
	public GameObject goatObject;
	[HideInInspector]
	private Vector3 goatPos;

	// Total distance between the markers.
	[HideInInspector]
    private float journeyLength;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
    	goatPos = new Vector3(goatObject.transform.position.x, goatObject.transform.position.y, goatObject.transform.position.z);


        transform.position = Vector3.MoveTowards(transform.position, goatPos, 0.15f);
    }
}