using UnityEngine;
using System.Collections;

public class TypingInput : MonoBehaviour {

	private bool m_capslock = false;

	[SerializeField]private TextBlurb m_curBlurb;

	// Use this for initialization
	void Start () {
		
	}


	void TryToAdd(string letter, bool shift){
		if (shift || m_capslock && !(shift && m_capslock)) {
			letter = letter.ToUpper ();
		}

		TextBlurb nextBlurb = m_curBlurb.TryToComplete (letter);

		if (nextBlurb != m_curBlurb) {
			m_curBlurb = nextBlurb;

			if (m_curBlurb == null) {
				return;
			}

			m_curBlurb.RunEffect ();
		}
	}

	// Update is called once per frame
	void Update () {
		bool shift = false;
		if (Input.GetKeyDown (KeyCode.CapsLock)) {
			m_capslock = true;
		}

		if (Input.GetKey (KeyCode.LeftShift) || Input.GetKey (KeyCode.RightShift)) {
			shift = true;
		}

		if(Input.GetKeyDown(KeyCode.A)){
			TryToAdd("a",shift);

		}
		else if(Input.GetKeyDown(KeyCode.B)){
			TryToAdd("b",shift);

		}
		else if(Input.GetKeyDown(KeyCode.C)){
			TryToAdd("c",shift);

		}
		else if(Input.GetKeyDown(KeyCode.D)){
			TryToAdd("d",shift);

		}
		else if(Input.GetKeyDown(KeyCode.E)){
			TryToAdd("e",shift);

		}
		else if(Input.GetKeyDown(KeyCode.F)){
			TryToAdd("f",shift);

		}
		else if(Input.GetKeyDown(KeyCode.G)){
			TryToAdd("g",shift);

		}
		else if(Input.GetKeyDown(KeyCode.H)){
			TryToAdd("h",shift);

		}
		else if(Input.GetKeyDown(KeyCode.I)){
			TryToAdd("i",shift);

		}
		else if(Input.GetKeyDown(KeyCode.J)){
			TryToAdd("j",shift);

		}
		else if(Input.GetKeyDown(KeyCode.K)){
			TryToAdd("k",shift);

		}
		else if(Input.GetKeyDown(KeyCode.L)){
			TryToAdd("l",shift);

		}
		else if(Input.GetKeyDown(KeyCode.M)){
			TryToAdd("m",shift);

		}
		else if(Input.GetKeyDown(KeyCode.N)){
			TryToAdd("n",shift);

		}
		else if(Input.GetKeyDown(KeyCode.O)){
			TryToAdd("o",shift);

		}
		else if(Input.GetKeyDown(KeyCode.P)){
			TryToAdd("p",shift);

		}
		else if(Input.GetKeyDown(KeyCode.Q)){
			TryToAdd("q",shift);

		}
		else if(Input.GetKeyDown(KeyCode.R)){
			TryToAdd("r",shift);

		}
		else if(Input.GetKeyDown(KeyCode.S)){
			TryToAdd("s",shift);

		}
		else if(Input.GetKeyDown(KeyCode.T)){
			TryToAdd("t",shift);

		}
		else if(Input.GetKeyDown(KeyCode.U)){
			TryToAdd("u",shift);

		}
		else if(Input.GetKeyDown(KeyCode.V)){
			TryToAdd("v",shift);

		}
		else if(Input.GetKeyDown(KeyCode.W)){
			TryToAdd("w",shift);

		}
		else if(Input.GetKeyDown(KeyCode.X)){
			TryToAdd("x",shift);

		}
		else if(Input.GetKeyDown(KeyCode.Y)){
			TryToAdd("y",shift);

		}
		else if(Input.GetKeyDown(KeyCode.Z)){
			TryToAdd("z",shift);

		}
		else if(Input.GetKeyDown(KeyCode.Space)){
			TryToAdd(" ",shift);

		}
	}
}
