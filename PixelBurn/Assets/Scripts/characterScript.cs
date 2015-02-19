using UnityEngine;
using System.Collections;

public class characterScript : MonoBehaviour {



	public furyWeaponHolder theHammer;
	public furyWeaponHolder theStaff;
	public furyWeaponHolder theSword;

	public SpriteRenderer hammerRender; 
	public SpriteRenderer staffRender;
	public SpriteRenderer swordRender;

	int hammerChoice;
	int staffChoice;
	int swordChoice;

	public SpriteRenderer spriteRender;

	// Use this for initialization
	void Start () {

		// Hammer Start
		hammerRender = theHammer.gameObject.GetComponent<SpriteRenderer> ();
		hammerRender.sprite = theHammer.weapons[hammerChoice];
		hammerRender.enabled = false;

		// Staff Start
		staffRender = theStaff.gameObject.GetComponent<SpriteRenderer> ();
		staffRender.sprite = theStaff.weapons[staffChoice];
		staffRender.enabled = false;

		// Sword Start
		swordRender = theSword.gameObject.GetComponent<SpriteRenderer> ();
		swordRender.sprite = theSword.weapons[swordChoice];
		swordRender.enabled = false;



	}

	
	// Update is called once per frame
	void Update () {
	
	}

	public void showWeapon(int weaponChoice) {
		
		switch (weaponChoice) {

			case 1:
				hammerRender.enabled = true;
				break;

			case 2:
				staffRender.enabled = true;
				break;

			case 3:
				swordRender.enabled = true;
				break;
			default:
				break;
		}

	}
	
	public void hideWeapons(){
		
		hammerRender.enabled = false;
		staffRender.enabled = false;
		swordRender.enabled = false;
		
	}
}
