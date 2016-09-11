using UnityEngine;
using System.Collections;
using System.Linq;

public class TypingInput : MonoBehaviour {
	private static TypingInput m_instance;
	private bool m_capslock = false;
	private bool phaseOne = true;
	private bool m_suspendUpdate = false;

	private AudioClip[] m_stageOneBlips;
	private AudioClip[] m_stageTwoBlips;

	[SerializeField]private float m_reflectTime;

	[SerializeField]private TextBlurb m_curBlurb;

	[SerializeField]private Transform m_cameraTransform;
	[SerializeField]private Rigidbody2D m_cameraRigidBody;
	[SerializeField]private AudioSource m_blipSource;
	[SerializeField]private AudioSource m_SoundTrackSource;

	[SerializeField]private AudioClip m_soundTrackTwo;
	[SerializeField]private AudioClip m_soundTrackThree;

	[SerializeField]private float m_camSpeed = 3.0f;
	[SerializeField]private float m_audioFade = 3.0f;

	public IEnumerator SetSoundTrackTwoCoroutine(){
		float t = 0.0f;

		while (t < m_audioFade) {
			t += Time.deltaTime;
			m_SoundTrackSource.volume = Mathf.Lerp (1.0f, 0.0f, t / m_audioFade);

			yield return new WaitForEndOfFrame ();
		}
		m_SoundTrackSource.Stop ();
		m_SoundTrackSource.clip = m_soundTrackTwo;
		m_SoundTrackSource.Play ();
		phaseOne = false;

		t = 0.0f;

		while (t < m_audioFade) {
			t += Time.deltaTime;
			m_SoundTrackSource.volume = Mathf.Lerp (0.0f, 1.0f, t / m_audioFade);

			yield return new WaitForEndOfFrame ();
		}

	}

	public static void SetSoundTrackTwo(){
		m_instance.StartCoroutine (m_instance.SetSoundTrackTwoCoroutine());
	}

	public IEnumerator SetSoundTrackThreeCoroutine(){
		float t = 0.0f;

		while (t < m_audioFade) {
			t += Time.deltaTime;
			m_SoundTrackSource.volume = Mathf.Lerp (1.0f, 0.0f, t / m_audioFade);

			yield return new WaitForEndOfFrame ();
		}
		m_SoundTrackSource.Stop ();
		m_SoundTrackSource.clip = m_soundTrackThree;
		m_SoundTrackSource.Play ();
		phaseOne = false;

		t = 0.0f;

		while (t < m_audioFade) {
			t += Time.deltaTime;
			m_SoundTrackSource.volume = Mathf.Lerp (0.0f, 1.0f, t / m_audioFade);

			yield return new WaitForEndOfFrame ();
		}

	}

	public static void SetSoundTrackThree(){
		m_instance.StartCoroutine (m_instance.SetSoundTrackThreeCoroutine());
	}

	// Use this for initialization
	void Start () {
		m_instance = this;
		m_stageOneBlips = Resources.LoadAll ("BlipsOne").Cast<AudioClip> ().ToArray ();
		m_stageTwoBlips = Resources.LoadAll ("BlipsTwo").Cast<AudioClip> ().ToArray ();
	}

	public IEnumerator ReflectCameraRoutine(){
		m_suspendUpdate = true;
		//Calculate distance to new position
		float distance = Vector3.Distance(m_cameraTransform.position,m_curBlurb.transform.position);
		Vector3 startPos = m_cameraTransform.position;
		Vector3 finishPos = startPos + m_cameraTransform.forward * distance * 2;

		Quaternion startRot = m_cameraTransform.rotation;
		Quaternion endRot = new Quaternion (startRot.x, startRot.y + 180, startRot.z, startRot.w);


		float t = 0.0f;

		while (t < m_reflectTime) {
			t += Time.deltaTime;
			m_cameraTransform.position = Vector3.Slerp (startPos, finishPos, t / m_reflectTime);
			yield return new WaitForEndOfFrame ();
		}

		t = 0.0f;

		while (t < m_reflectTime) {
			t += Time.deltaTime;
			m_cameraTransform.rotation = Quaternion.Slerp (startRot, endRot, t / m_reflectTime);
			yield return new WaitForEndOfFrame ();
		}

		m_suspendUpdate = false;
	}

	public static void ReverseCamera(){
		TypingInput.SetSoundTrackThree ();
		m_instance.StartCoroutine(m_instance.ReflectCameraRoutine ());
	}

	void PlayBlip(){
		if (phaseOne == true) {
			this.m_blipSource.PlayOneShot(this.m_stageOneBlips[Random.Range(0,m_stageOneBlips.Length)]);
			return;
		}
		this.m_blipSource.PlayOneShot(this.m_stageTwoBlips[Random.Range(0,m_stageTwoBlips.Length)]);
	}

	void TryToAdd(string letter, bool shift){
		if (shift || m_capslock && !(shift && m_capslock)) {
			letter = letter.ToUpper ();
		}

		TextBlurb nextBlurb = m_curBlurb.TryToComplete (letter);

		if (nextBlurb != m_curBlurb) {

			m_curBlurb.RunEffect ();
			if (nextBlurb == null) {
				return;
			}
			m_curBlurb = nextBlurb;
			m_curBlurb.Activate ();
		}
	}

	// Update is called once per frame
	void Update () {
		bool blipNeeeded = false;

		if (m_curBlurb == null || m_suspendUpdate) {
			return;
		}

		m_cameraRigidBody.AddForce ((m_curBlurb.transform.position - m_cameraTransform.position ) * Time.deltaTime * m_camSpeed);


		bool shift = false;
		if (Input.GetKeyDown (KeyCode.CapsLock)) {
			m_capslock = !m_capslock;
			blipNeeeded = true;
		}

		if (Input.GetKey (KeyCode.LeftShift) || Input.GetKey (KeyCode.RightShift)) {
			shift = true;
			blipNeeeded = true;
		}

		if(Input.GetKeyDown(KeyCode.A)){
			TryToAdd("a",shift);
			blipNeeeded = true;

		}
		else if(Input.GetKeyDown(KeyCode.B)){
			TryToAdd("b",shift);
			blipNeeeded = true;

		}
		else if(Input.GetKeyDown(KeyCode.C)){
			TryToAdd("c",shift);
			blipNeeeded = true;

		}
		else if(Input.GetKeyDown(KeyCode.D)){
			TryToAdd("d",shift);
			blipNeeeded = true;

		}
		else if(Input.GetKeyDown(KeyCode.E)){
			TryToAdd("e",shift);
			blipNeeeded = true;

		}
		else if(Input.GetKeyDown(KeyCode.F)){
			TryToAdd("f",shift);
			blipNeeeded = true;

		}
		else if(Input.GetKeyDown(KeyCode.G)){
			TryToAdd("g",shift);
			blipNeeeded = true;

		}
		else if(Input.GetKeyDown(KeyCode.H)){
			TryToAdd("h",shift);
			blipNeeeded = true;

		}
		else if(Input.GetKeyDown(KeyCode.I)){
			TryToAdd("i",shift);
			blipNeeeded = true;

		}
		else if(Input.GetKeyDown(KeyCode.J)){
			TryToAdd("j",shift);
			blipNeeeded = true;

		}
		else if(Input.GetKeyDown(KeyCode.K)){
			TryToAdd("k",shift);
			blipNeeeded = true;

		}
		else if(Input.GetKeyDown(KeyCode.L)){
			TryToAdd("l",shift);
			blipNeeeded = true;

		}
		else if(Input.GetKeyDown(KeyCode.M)){
			TryToAdd("m",shift);
			blipNeeeded = true;

		}
		else if(Input.GetKeyDown(KeyCode.N)){
			TryToAdd("n",shift);
			blipNeeeded = true;

		}
		else if(Input.GetKeyDown(KeyCode.O)){
			TryToAdd("o",shift);
			blipNeeeded = true;

		}
		else if(Input.GetKeyDown(KeyCode.P)){
			TryToAdd("p",shift);
			blipNeeeded = true;

		}
		else if(Input.GetKeyDown(KeyCode.Q)){
			TryToAdd("q",shift);
			blipNeeeded = true;

		}
		else if(Input.GetKeyDown(KeyCode.R)){
			TryToAdd("r",shift);
			blipNeeeded = true;

		}
		else if(Input.GetKeyDown(KeyCode.S)){
			TryToAdd("s",shift);
			blipNeeeded = true;

		}
		else if(Input.GetKeyDown(KeyCode.T)){
			TryToAdd("t",shift);
			blipNeeeded = true;

		}
		else if(Input.GetKeyDown(KeyCode.U)){
			TryToAdd("u",shift);
			blipNeeeded = true;

		}
		else if(Input.GetKeyDown(KeyCode.V)){
			TryToAdd("v",shift);
			blipNeeeded = true;

		}
		else if(Input.GetKeyDown(KeyCode.W)){
			TryToAdd("w",shift);
			blipNeeeded = true;

		}
		else if(Input.GetKeyDown(KeyCode.X)){
			TryToAdd("x",shift);
			blipNeeeded = true;

		}
		else if(Input.GetKeyDown(KeyCode.Y)){
			TryToAdd("y",shift);
			blipNeeeded = true;

		}
		else if(Input.GetKeyDown(KeyCode.Z)){
			TryToAdd("z",shift);
			blipNeeeded = true;

		}
		else if(Input.GetKeyDown(KeyCode.Space)){
			TryToAdd(" ",shift);
			blipNeeeded = true;

		}

		if (blipNeeeded) {
			PlayBlip ();
		}
	}
}
