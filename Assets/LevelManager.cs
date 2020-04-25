using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
	public GameObject goatObject; //The goat object in a level
	public GameObject farmerObject; //The farmer object in a level
	public Text calorieText; //UI element showing calorie count
	public Text timerText; //UI element showing countdown timer

	[HideInInspector]
	public int calorieCount; //Current number of calories the goat has eaten
	[HideInInspector]
	public bool gameOver = false;
	[HideInInspector]
	public bool goatCaughtByFarmer = false;

	[HideInInspector]
	public static float timeLimit = 10.0f; //Time limit for a level

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
		Goat goatScript = goatObject.GetComponent<Goat>(); //Get the script component of the goat
		Farmer farmerScript = farmerObject.GetComponent<Farmer>(); //Get the script component of the farmer

    	if(gameOver){
    		farmerScript.movementEnabled = false;
    		goatScript.movementEnabled = false;
    	} 
    	else {

    		//Updating timer text
	    	float timeLeft = timeLimit - Time.time; //Find out how much time is left
			string minSec = string.Format("{0}:{1:00.0}", (int)timeLeft / 60, timeLeft % 60); //Format a string appropriately
			timerText.text = minSec; //Update the UI

			calorieCount = goatScript.calorieCount; //Record the calorie count
			calorieText.text = "Calories: " + calorieCount; //Update the UI

			if(timeLeft <= 0 || goatCaughtByFarmer){
				EndGame();
			}

    	}
        
    }

    void EndGame(){
    	gameOver = true;
    	print("GAME OVER");
    }
}
