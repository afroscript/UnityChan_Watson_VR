//SampleTextToSpeech.cs
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using IBM.Watson.DeveloperCloud.Services.TextToSpeech.v1;

public class SampleTextToSpeech : MonoBehaviour {

	TextToSpeech m_TextToSpeech = new TextToSpeech();
	string m_TestString = "おはようございます。漢字も読めます。";

	// Use this for initialization
	void Start ()
	{
		m_TextToSpeech.Voice = VoiceType.ja_JP_Emi;
		m_TextToSpeech.ToSpeech(m_TestString, HandleToSpeechCallback);
	}

	void HandleToSpeechCallback (AudioClip clip)
	{
		PlayClip(clip);
	}

	private void PlayClip(AudioClip clip)
	{
		if (Application.isPlaying && clip != null)
		{
			GameObject audioObject = new GameObject("AudioObject");
			AudioSource source = audioObject.AddComponent<AudioSource>();
			source.spatialBlend = 0.0f;
			source.loop = false;
			source.clip = clip;
			source.Play();

			GameObject.Destroy(audioObject, clip.length);
		}
	}

	// Update is called once per frame
	void Update () {

	}

}
