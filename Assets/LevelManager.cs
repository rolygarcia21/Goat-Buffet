using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
	public GameObject goatObject; //The goat object in a level
	public GameObject farmerObject; //The farmer object in a level
	public GameObject startScreen; //Start screen
	public GameObject endScreen; //End screen
	public Text calorieText; //UI element showing calorie count
	public Text timerText; //UI element showing countdown timer

	[HideInInspector]
	public int calorieCount; //Current number of calories the goat has eaten
	[HideInInspector]
	public bool newGame = false;
	[HideInInspector]
	public bool gameOver = false;
	[HideInInspector]
	public bool goatCaughtByFarmer = false;

	[HideInInspector]
	public static float timeLimit = 120.0f; //Time limit for a level
	[HideInInspector]
	public float newGameTime;
	[HideInInspector]
	public float timeLeft = timeLimit;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
		Goat goatScript = goatObject.GetComponent<Goat>(); //Get the script component of the goat
		Farmer farmerScript = farmerObject.GetComponent<Farmer>(); //Get the script component of the farmer

		if(!newGame){

			if(Input.GetKey(KeyCode.Space)){
				NewGame();
				return;
			}

			farmerScript.movementEnabled = false;
    		goatScript.movementEnabled = false;
			startScreen.SetActive(true);
			endScreen.SetActive(false);
		}
    	else if(gameOver){
    		if(Input.GetKey(KeyCode.Space)){
				SceneManager.LoadScene(SceneManager.GetActiveScene().name);
				return;
			}

    		farmerScript.movementEnabled = false;
    		goatScript.movementEnabled = false;
    		startScreen.SetActive(false);
    		endScreen.SetActive(true);
    	} 
    	else {

    		//Updating timer text
	    	timeLeft = timeLimit - (Time.time - newGameTime); //Find out how much time is left
			string minSec = string.Format("{0}:{1:00.0}", (int)timeLeft / 60, timeLeft % 60); //Format a string appropriately
			timerText.text = minSec; //Update the UI

			calorieCount = goatScript.calorieCount; //Record the calorie count
			calorieText.text = "Calories: " + calorieCount; //Update the UI

			if(timeLeft <= 0 || goatCaughtByFarmer){
				EndGame();
			}

    	}
        
    }

    void NewGame(){
    	Goat goatScript = goatObject.GetComponent<Goat>(); //Get the script component of the goat
		Farmer farmerScript = farmerObject.GetComponent<Farmer>(); //Get the script component of the farmer
		newGameTime = Time.time;
    	timeLeft = timeLimit;
		newGame = true;
		gameOver = false;
		farmerScript.movementEnabled = true;
		goatScript.movementEnabled = true;
		startScreen.SetActive(false);
		endScreen.SetActive(false);
    }

    void EndGame() {
    	gameOver = true;
		print("GAME OVER");
    }
}
