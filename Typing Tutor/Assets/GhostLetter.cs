using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GhostLetter : MonoBehaviour {
	[SerializeField]private Text m_text;
	[SerializeField]private Rigidbody2D m_rb;

	[SerializeField]private float m_maxForce = 3.0f;
	[SerializeField]private float m_maxTorque = 2.0f;

	// Use this for initialization
	public void StartFalling(string text, TextAnchor orientation){
		m_text.text = text;
		m_text.alignment = orientation;
		m_rb.AddForce (new Vector2 (Random.Range (-m_maxForce, m_maxForce), Random.Range (-m_maxForce, m_maxForce)));
		m_rb.AddTorque (Random.Range (-m_maxTorque, m_maxTorque));

		StartCoroutine (InterpolateColor ());
	}

	private IEnumerator InterpolateColor(){
		Color StartingColor = new Color (1.0f, 1.0f, 1.0f, Random.Range (0.0f, 0.5f));
		Color EndColor = new Color (0.0f, 0.0f, 0.0f, 0.0f);
		float lerpTime = Random.Range (0.1f, 0.3f);

		float t = 0.0f;
		while (t < lerpTime) {
			t += Time.deltaTime;

			m_text.color = Color.Lerp(StartingColor,EndColor,t/lerpTime/4);
			yield return new WaitForEndOfFrame ();
		}

		lerpTime = Random.Range (0.1f, 10.0f);

		t = 0.0f;
		while (t < lerpTime) {
			t += Time.deltaTime;

			m_text.color = Color.Lerp(StartingColor,EndColor,t/lerpTime);
			yield return new WaitForEndOfFrame ();
		}
		Destroy (this.gameObject);
	}
}
