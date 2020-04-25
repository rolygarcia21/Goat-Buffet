using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goat : MonoBehaviour
{
	[HideInInspector]
	public int calorieCount = 0;
	[HideInInspector]
	public int lastCalorieCount = 0;

	[HideInInspector]
	public static float normalSpeed = 0.15f;
	[HideInInspector]
	public float curSpeed = normalSpeed;

	[HideInInspector]
	public float lastTimeBoosted = 0.0f;
	[HideInInspector]
	public float boostDuration = 2.0f;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKey(KeyCode.W)){
        	transform.position += new Vector3(0, curSpeed, 0);
        }
        if (Input.GetKey(KeyCode.S)){
			transform.position += new Vector3(0, -curSpeed, 0);
        }
        if (Input.GetKey(KeyCode.A)){
			transform.position += new Vector3(-curSpeed, 0, 0);
        }
        if (Input.GetKey(KeyCode.D)){
			transform.position += new Vector3(curSpeed, 0, 0);
        }

        if(this.calorieCount != this.lastCalorieCount) {
        	print("Calories eaten: " + calorieCount);
        	this.lastCalorieCount = this.calorieCount;
        }

        if(this.curSpeed > normalSpeed){
        	DoSpeedBoost();
        }
    }

    //Function called when a collider enters this collider
    void OnTriggerEnter2D(Collider2D other) {

    	//If we hit food, eat the food
		if ((other.gameObject.GetComponent("Food") as Food) != null) {

			Food fs = other.gameObject.GetComponent<Food>();
			
			//Call the supplied trigger
			EatObject(fs.calories);

			if(fs.isSpeedBoost){
				print("I AM SPEED");
				this.curSpeed += 0.1f;
				this.lastTimeBoosted = Time.time;
			}

			//Destroy the collided object
			Destroy(other.gameObject);

		}
    }

    void DoSpeedBoost(){
    	if(Time.time - this.lastTimeBoosted >= this.boostDuration){
    		print("I AM NO LONGER SPEED. " + this.curSpeed);
    		this.curSpeed = normalSpeed;
    	}

    }

    void EatObject(int calories){

    	print(this.name + " eaten: +" + calories + " calories");
    	this.calorieCount += calories;

    }

}
