using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TextBlurb : MonoBehaviour {
	[SerializeField] private TextBlurb m_nextBlurb;
	protected TextBlurb m_lastBlurb;

	[SerializeField] private Text m_text;
	private string m_startingText;
	private bool m_backType = false;



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

	public TextBlurb TryToComplete(string nextChar){
		if (m_backType == true) {
			if (m_text.text [m_text.text.Length-1] + "" == nextChar) {
				m_text.text = m_text.text.Substring (0, m_text.text.Length - 1);
			}

			return m_text.text.Length == 0 ? m_lastBlurb : this;
		}

		if (m_text.text [0] + "" == nextChar) {
			m_text.text = m_text.text.Substring (1, m_text.text.Length - 1);
		}

		return m_text.text.Length == 0 ? m_nextBlurb : this;
	}

	// Update is called once per frame
	void Update () {
	
	}

	public void Reset(){
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
	}

	public void EnableBackTypeAllPrevious(){
		this.EnableBackType ();
		if (m_lastBlurb == null) {
			return;
		}

		this.m_lastBlurb.EnableBackTypeAllPrevious ();
	}

}
