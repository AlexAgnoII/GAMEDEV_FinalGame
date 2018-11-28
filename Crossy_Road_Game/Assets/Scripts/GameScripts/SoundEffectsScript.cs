using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundEffectsScript : MonoBehaviour {

    [SerializeField] private AudioClip grassSound;
    [SerializeField] private AudioClip roadSound;
    [SerializeField] private AudioClip logSound;
    [SerializeField] private AudioClip deathSound;
    [SerializeField] private AudioClip hopSound;
    [SerializeField] private AudioClip splashSound;
    [SerializeField] private AudioClip crashSound;
    [SerializeField] private AudioClip nightSound;
    [SerializeField] private AudioClip morningSound;

    private AudioSource effectsAudioSource;
    private AudioSource hoppingAudioSource;
    private AudioSource nightSoundSource;
    private AudioSource morningSoundSource;

    bool night;

    // Use this for initialization
    void Start () {
        EventBroadcaster.Instance.AddObserver(EventNames.FinalGameAudioEvents.ON_GRASS_SOUND, this.playGrassSound);
        EventBroadcaster.Instance.AddObserver(EventNames.FinalGameAudioEvents.ON_LOG_SOUND, this.playLogSound);
        EventBroadcaster.Instance.AddObserver(EventNames.FinalGameAudioEvents.ON_ROAD_SOUND, this.playRoadSound);
        EventBroadcaster.Instance.AddObserver(EventNames.FinalGameAudioEvents.ON_DEATH_SOUND, this.playDeathSound);
        EventBroadcaster.Instance.AddObserver(EventNames.FinalGameAudioEvents.ON_SPLASH_SOUND, this.playSplashSound);
        EventBroadcaster.Instance.AddObserver(EventNames.FinalGameAudioEvents.ON_CRASH_SOUND, this.playCrashSound);
        EventBroadcaster.Instance.AddObserver(EventNames.FinalGameAudioEvents.ON_DAY_PHASE, this.playBGM);
        EventBroadcaster.Instance.AddObserver(EventNames.FinalGameAudioEvents.ON_HOPPING_SOUND, this.playHoppingSound);

        hoppingAudioSource = GetComponent<AudioSource>();
        effectsAudioSource = gameObject.AddComponent<AudioSource>();
        effectsAudioSource.volume = 1;
        nightSoundSource = AddAudio(nightSound, true);
        morningSoundSource = AddAudio(morningSound, true);
        
    }
	
    void playSplashSound()
    {
        effectsAudioSource.clip = splashSound;
        effectsAudioSource.Play();
    }

    void playCrashSound()
    {
        effectsAudioSource.clip = crashSound;
        effectsAudioSource.Play();
    }

    void playHoppingSound()
    {
        hoppingAudioSource.clip = hopSound;
        hoppingAudioSource.Play();

    }
    void playGrassSound()
    {
        effectsAudioSource.clip = grassSound;
        effectsAudioSource.Play();
    }

    void playRoadSound()
    {
        effectsAudioSource.clip = roadSound;
        effectsAudioSource.Play();
    }
    void playLogSound()
    {
        effectsAudioSource.clip = logSound;
        effectsAudioSource.Play();
    }
    void playDeathSound()
    {
        hoppingAudioSource.clip = deathSound;
        hoppingAudioSource.Play();
    }

    public void playBGM(Parameters param)
    {
        night = param.GetBoolExtra(DirectionalLightScript.DAY_PHASE, false);

        if (night == true)
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

    public AudioSource AddAudio(AudioClip audioClip, bool loop)
    {
        AudioSource newAudio = gameObject.AddComponent<AudioSource>();
        newAudio.clip = audioClip;
        newAudio.loop = loop;
        return newAudio;
    }

    // Update is called once per frame
    void Update () {
		
	}
}
