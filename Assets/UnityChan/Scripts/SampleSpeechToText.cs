//SampleSpeechToText.cs
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using IBM.Watson.DeveloperCloud.Services.SpeechToText.v1;

public class SampleSpeechToText : MonoBehaviour {

	[SerializeField]
	private AudioClip m_AudioClip = new AudioClip();
	private SpeechToText m_SpeechToText = new SpeechToText();

	// Use this for initialization
	IEnumerator Start()
	{
		// 音声をマイクから3秒間取得
		Debug.Log ("Start record");
		var audioSource = GetComponent<AudioSource>();
		audioSource.clip = Microphone.Start(null, true, 10, 44100);
		audioSource.loop = false;
		audioSource.spatialBlend = 0.0f;
		yield return new WaitForSeconds (3f);
		Microphone.End (null);
		Debug.Log ("Finish record");

		// ためしに録音内容を再生してみる
		audioSource.Play ();

		// 音声をテキストに変換
		m_SpeechToText.RecognizeModel = "ja-JP_BroadbandModel";
		m_SpeechToText.Recognize(audioSource.clip, HandleOnRecognize);
	}

	void HandleOnRecognize(SpeechRecognitionEvent result)
	{
		if (result != null && result.results.Length > 0)
		{
			foreach (var res in result.results)
			{
				foreach (var alt in res.alternatives)
				{
					string text = alt.transcript;
					Debug.Log(string.Format("{0} ({1}, {2:0.00})\n", text, res.final ? "Final" : "Interim", alt.confidence));
				}
			}
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
