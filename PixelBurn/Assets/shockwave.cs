using UnityEngine;
using System.Collections;

class shockwave : MonoBehaviour
{

	SpriteRenderer colorHold;
	float fade;
	Light flashLight;
	public int playerNum;

	public shockwave(Color teamColor) {

		// Constructor code
	}

		// Use this for initialization
	void Start () {
	
		colorHold = this.GetComponent<SpriteRenderer>();
		flashLight = this.GetComponentInChildren<Light>();
		//colorHold.color = Color.green;
		fade = 1f;
		//colorHold.color = new Color(colorHold.color.r, colorHold.color.g, colorHold.color.b, fade);
		flashLight.intensity = 0f;
		flashLight.color = colorHold.color;

		Destroy(this.gameObject, .5f);

	}
		
	// Update is called once per frame
	void Update () {
	
		flashLight.intensity += .4f;
		fade -= .02f;
		colorHold.color = new Color(colorHold.color.r, colorHold.color.g, colorHold.color.b, fade);

		this.transform.localScale = this.transform.localScale *1.09f;

	}

	void OnCollisionEnter2D(Collision2D col) {

		if(col.gameObject.tag == "Player 1 Character" && playerNum == 2){
			col.gameObject.GetComponent<playerInfo>().health -= 30;
		}
		if(col.gameObject.tag == "Player 2 Character" && playerNum == 1){
			col.gameObject.GetComponent<playerInfo>().health -= 30;
		}
	}
		
}
