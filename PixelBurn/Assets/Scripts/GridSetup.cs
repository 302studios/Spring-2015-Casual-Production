using UnityEngine;
using System.Collections;

public class GridSetup : MonoBehaviour {

	int gridXLength = 10;
	int gridYLength = 10;

	public enum TheGrid {

		slide = 0,
		plain = 1,
		trees = 2,
		mountain = 3,
		blocked = 9

	}


	// Use this for initialization
	void Start () {
	
		TheGrid[,] levelGrid = new TheGrid[gridXLength,gridYLength];

	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
