using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Goat : MonoBehaviour
{
	public GameObject LevelManager;
	[HideInInspector]
	public int calorieCount = 0; //Amount of calories the goat has eaten
	[HideInInspector]
	public int lastCalorieCount = 0; //Amount of calories consumed at last update

	[HideInInspector]
	public bool movementEnabled = true; //Enable movement of goat

	[HideInInspector]
	public float curSpeed = normalSpeed; //Current speed of goat

	[HideInInspector]
	public float lastTimeBoosted = 0.0f; //Last time the goat had a speed boost

	[HideInInspector]
	public static float timeLimit = 120.0f;
	[HideInInspector]
	public static float normalSpeed = 0.15f; //Normal speed of goat
	[HideInInspector]
	public static float boostDuration = 2.0f; //Duration of all speed boosts. Might change to be variable



    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    	//Goat movement
    	if(this.movementEnabled){
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
	    }

        //If the calorieCount has changed, update the UI and print it out
        if(this.calorieCount != this.lastCalorieCount) {
        	print("Calories eaten: " + calorieCount);
        	this.lastCalorieCount = this.calorieCount;
        }

        //Handle speed boost logic
        if(this.curSpeed > normalSpeed){
        	DoSpeedBoost();
        }
    }

    //Function called when a collider enters this collider
    void OnTriggerEnter2D(Collider2D other) {

    	//If we hit food, eat the food
		if ((other.gameObject.GetComponent("Food") as Food) != null) {

			//Grab the Food script from the food we consumed so we can access its calories
			Food fs = other.gameObject.GetComponent<Food>();
			
			//Eat the food
			EatObject(fs.calories, other.gameObject.name);

			//If the food is a speed booster, boost speed. Speed boosts stack
			if(fs.isSpeedBoost){
				print("I AM SPEED"); //Meme
				this.curSpeed += 0.1f; //Boost speed
				this.lastTimeBoosted = Time.time; //Also record the the time we began boosting
			}

			//Destroy the collided object
			Destroy(other.gameObject);

		}
		else if ((other.gameObject.GetComponent("Farmer") as Farmer) != null) {
			print("YOU DONE GOT CAUGHT BOAH");
			LevelManager.SendMessage("EndGame");
		}
    }

    //Speed boost logic
    void DoSpeedBoost(){

    	//Check if the speed boost period has expired
    	if(Time.time - this.lastTimeBoosted >= boostDuration){
    		print("I AM NO LONGER SPEED. " + this.curSpeed);
    		this.curSpeed = normalSpeed;
    	}

    }

    //Food eating logic
    void EatObject(int calories, string name){

    	print(name + " eaten: +" + calories + " calories");
    	this.calorieCount += calories; //Add the calories to our current calories

    }

}
