using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundMusicScript : MonoBehaviour {

    [SerializeField] private AudioClip nightSound;
    [SerializeField] private AudioClip morningSound;
    private AudioSource nightSoundSource;
    private AudioSource morningSoundSource;
    bool night;

// Use this for initialization
    void Start () {
        EventBroadcaster.Instance.AddObserver(EventNames.FinalGameAudioEvents.ON_DAY_PHASE, this.playBGM);
        nightSoundSource = AddAudio(nightSound, true);
        morningSoundSource = AddAudio(morningSound, true);
    }

	// Update is called once per frame
	void Update () {

	}

    public AudioSource AddAudio(AudioClip audioClip, bool loop)
    {
        AudioSource newAudio = gameObject.AddComponent<AudioSource>();
        newAudio.clip = audioClip;
        newAudio.loop = loop;
        return newAudio;
    }

    public void playBGM(Parameters param)
    {
        Debug.Log("at playBGM");
        night = param.GetBoolExtra(DirectionalLightScript.DAY_PHASE, false);

        if(night == true)
        {
            morningSoundSource.Stop();
            nightSoundSource.Play();
        }
        else
        {
            nightSoundSource.Stop();
            morningSoundSource.Play();
        }
    }
}