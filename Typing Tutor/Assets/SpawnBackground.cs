using UnityEngine;
using System.Collections;

public class SpawnBackground : MonoBehaviour {
	private GameObject m_camera;
	[SerializeField]private GameObject m_ghostLetter;
	private int m_maxLetters = 3;

	[SerializeField]private int m_max_sites = 10;
	[SerializeField]private float m_maxForward = 100.0f;
	[SerializeField]private float m_maxSide = 100.0f;
	string st = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";

	public void GenerateLetters(string letter){
		int sites = Random.Range (m_max_sites/2, m_max_sites);

		if (letter.Length <= 0 || letter == null) {
			return;
		}
		for (int j = 0; j < sites; j++) {

			Vector3 pos = this.transform.position + this.transform.forward * Random.Range (m_maxSide, m_maxForward) + new Vector3 (Random.Range (-m_maxSide, m_maxSide), Random.Range (-m_maxSide, m_maxSide), Random.Range (-m_maxSide, m_maxSide));
			for (int i = 0; i < m_maxLetters; i++) {
				(Instantiate (m_ghostLetter, pos, this.transform.rotation) as GameObject).GetComponent<GhostLetter> ().StartFalling (letter, TextAnchor.LowerCenter);
			}
		}
	}

	public void MyCallbackEventHandler(BeatDetection.EventInfo eventInfo)
	{
		
		GenerateLetters ("" +st[Random.Range(0,st.Length)]);

		//Spawn ghost text in front of the camera

		/*
		switch(eventInfo.messageInfo)
		{
		case BeatDetection.EventType.Energy:
			StartCoroutine(showText(energy, genergy));
			break;
		case BeatDetection.EventType.HitHat:
			StartCoroutine(showText(hithat, ghithat));
			break;
		case BeatDetection.EventType.Kick:
			StartCoroutine(showText(kick, gkick));
			break;
		case BeatDetection.EventType.Snare:
			StartCoroutine(showText(snare, gsnare));
			break;
		}*/
	}

	// Use this for initialization
	void Start () {
		//Register the beat callback function
		this.GetComponent<BeatDetection>().CallBackFunction = MyCallbackEventHandler;
	}
}
