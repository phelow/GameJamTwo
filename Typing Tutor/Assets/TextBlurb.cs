using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TextBlurb : MonoBehaviour {
	[SerializeField] protected TextBlurb m_nextBlurb;
	protected TextBlurb m_lastBlurb;

	[SerializeField] protected Text m_text;
	[SerializeField] private Object m_ghostLetter;
	protected string m_startingText;
	protected bool m_backType = false;

	[SerializeField]private int m_maxLetters = 10;



	public void SetLastBlurb(TextBlurb lastBlurb){
		m_lastBlurb = lastBlurb;
	}

	// Use this for initialization
	void Start () {
		m_startingText = m_text.text;

		if (m_nextBlurb == null) {
			return;
		}
		m_nextBlurb.SetLastBlurb (this);
	}


	protected void GenerateLetters(string letter){
		if (letter.Length <= 0 || letter == null) {
			return;
		}

		for (int i = 0; i < m_maxLetters; i++) {
			(Instantiate (m_ghostLetter, this.transform.position,this.transform.rotation) as GameObject).GetComponent<GhostLetter>().StartFalling(letter,m_text.alignment);
		}
	}

	public virtual TextBlurb TryToComplete(string nextChar){
		if (m_backType == true) {
			if (m_text.text [m_text.text.Length - 1] + "" == " ") {
				m_text.text = m_text.text.Substring (0, m_text.text.Length - 1);
			}

			if (m_text.text [m_text.text.Length-1] + "" == nextChar) {
				this.GenerateLetters (m_text.text);
				m_text.text = m_text.text.Substring (0, m_text.text.Length - 1);
			}

			return m_text.text.Length == 0 ? m_lastBlurb : this;
		}

		if (m_text.text [0] + "" == " ") {
			m_text.text = m_text.text.Substring (1, m_text.text.Length - 1);
		}
		if (m_text.text [0] + "" == nextChar) {
			this.GenerateLetters (m_text.text);
			m_text.text = m_text.text.Substring (1, m_text.text.Length - 1);
		}

		return m_text.text.Length == 0 ? m_nextBlurb : this;
	}

	// Update is called once per frame
	void Update () {
	
	}

	public virtual void Reset(){
		this.m_text.text = m_startingText;
	}

	public void ResetAll(){
		this.Reset();
		if (m_lastBlurb == null) {
			return;
		}
		m_lastBlurb.ResetAll ();
	}

	public virtual void RunEffect(){

	}

	public void EnableBackType(){
		this.m_backType = true;
		this.m_text.alignment = TextAnchor.MiddleLeft;
	}

	public void EnableBackTypeAllPrevious(){
		this.EnableBackType ();
		if (m_lastBlurb == null) {
			return;
		}

		this.m_lastBlurb.EnableBackTypeAllPrevious ();
	}

	public virtual void Activate(){
	}
}
