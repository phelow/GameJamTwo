using UnityEngine;
using System.Collections;

public class CamReverseBlurb : TextBlurb {
	public override void RunEffect(){
		if (m_backType == false) {
			return;
		}

		TypingInput.ReverseCamera ();

	}
}
