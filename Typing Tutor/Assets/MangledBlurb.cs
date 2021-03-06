﻿using UnityEngine;
using System.Collections;

public class MangledBlurb : TextBlurb {


	public override void Activate(){
		StartCoroutine (spawnLetters());
	}

	// Update is called once per frame
	public IEnumerator spawnLetters () {
		int dispChar = 0;
		while (this.m_text.text.Length > 0 && this.m_text.text != null) {
			this.GenerateLetters ("" + this.m_text.text [dispChar]);	
			dispChar++;
			if (dispChar >= this.m_text.text.Length - 1) {
				dispChar = 0;
			}
			this.GenerateLetters (this.m_startingText.Substring(0,m_startingText.Length - m_text.text.Length));
			yield return new WaitForSeconds (Random.Range (0.1f, 1.0f));
		}
	}

	public override TextBlurb TryToComplete(string nextChar){
		if (m_text.text.Length == 0) {
			return m_nextBlurb;
		}

		if (m_backType == true) {
			if (m_text.text [m_text.text.Length - 1] + "" == " ") {
				m_text.text = m_text.text.Substring (0, m_text.text.Length - 1);
			}

			if (m_text.text [m_text.text.Length - 1] + "" == nextChar) {
				m_text.text = m_text.text.Substring (0, m_text.text.Length - 1);
			}

			return m_text.text.Length == 0 ? m_lastBlurb : this;
		}

		if (m_text.text [0] + "" == " ") {
			m_text.text = m_text.text.Substring (1, m_text.text.Length - 1);
		}
		if (m_text.text [0] + "" == nextChar) {
			m_text.text = m_text.text.Substring (1, m_text.text.Length - 1);
		}

		return m_text.text.Length == 0 ? m_nextBlurb : this;
	}

}
