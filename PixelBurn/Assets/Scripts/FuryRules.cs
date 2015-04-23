using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class FuryRules : MonoBehaviour {


	public enum GridValues{

		plain = 1,
		trees = 2,
		mountain = 3

	}

	public bool canPlay;
	public bool playerOneTurn;

	public Color[] teamColors;

	public int p1Num;
	public int p2Num;

	GameObject laMusica;

	public Text playAgain;
	public Image againBG;
 

	// Use this for initialization
	void Start () {
	
		playerOneTurn = true;
		canPlay = true;

		laMusica = GameObject.Find("Menu Music");
	}

	// Update is called once per frame
	void Update () {
	
		if(laMusica != null)
			Destroy(laMusica);

		if(Input.GetKey(KeyCode.Escape)){
			Application.Quit();
		}


	}

}
