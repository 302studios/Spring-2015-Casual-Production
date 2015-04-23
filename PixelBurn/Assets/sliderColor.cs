using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class sliderColor : MonoBehaviour {

	public Image player1Col;
	public Image player2Col;
	public Image winnersCircle;

	// Use this for initialization
	void Start () {
	
		player1Col.color = GameObject.Find("World").GetComponent<WorldRules>().teamColors [PlayerPrefs.GetInt ("Player 1 Color")];
		player2Col.color = GameObject.Find("World").GetComponent<WorldRules>().teamColors [PlayerPrefs.GetInt ("Player 2 Color")];

	}
	
	// Update is called once per frame
	void Update () {
	
		if(this.GetComponent<Slider>().value < .5f)
			winnersCircle.color = GameObject.Find("World").GetComponent<WorldRules>().teamColors [PlayerPrefs.GetInt ("Player 2 Color")];
		else
			winnersCircle.color = GameObject.Find("World").GetComponent<WorldRules>().teamColors [PlayerPrefs.GetInt ("Player 1 Color")];

	}
}
