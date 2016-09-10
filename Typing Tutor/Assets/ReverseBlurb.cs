using UnityEngine;
using System.Collections;

public class ReverseBlurb : TextBlurb {

	// Update is called once per frame
	void Update () {
	
	}
		

	public override void RunEffect(){
		//Make the player have to type in reverse

		//1: reset all of the words
		this.ResetAll();


		//2: set the boolean of backtyping to true
		this.EnableBackTypeAllPrevious();

	}
}
