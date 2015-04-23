using UnityEngine;
using System.Collections;

public class startScreen : MonoBehaviour {

	public bool arcade;


	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	

		if(arcade)
			arcadeControls();
		else
			computerControls();

	}

	void arcadeControls() {
		
		
		if(Input.GetKey(KeyCode.Alpha1) || Input.GetKey(KeyCode.Alpha2)){
			
			Application.LoadLevel("Team Color");
			
		}
		
		if(Input.GetKeyDown(KeyCode.Alpha3))
			Application.Quit();
		
	}

	void computerControls() {
		
		
		if(Input.GetKey(KeyCode.Space)){
			
			Application.LoadLevel("Team Color");
			
		}
		
		if(Input.GetKeyDown(KeyCode.Escape))
			Application.Quit();
		
	}
}
