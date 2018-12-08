using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

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
    [SerializeField] private AudioMixer mixer;

    private AudioSource effectsAudioSource;
    private AudioSource crashAudioSource;
    private AudioSource hoppingAudioSource;
    private AudioSource deathAudioSource;
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
        EventBroadcaster.Instance.AddObserver(EventNames.FinalGameAudioEvents.CHANGE_DAY_PHASE, this.changeBGM);
        EventBroadcaster.Instance.AddObserver(EventNames.FinalGameAudioEvents.ON_HOPPING_SOUND, this.playHoppingSound);

        hoppingAudioSource = GetComponent<AudioSource>();
        hoppingAudioSource.outputAudioMixerGroup = mixer.FindMatchingGroups("Hopping")[0];

        effectsAudioSource = gameObject.AddComponent<AudioSource>();
        effectsAudioSource.outputAudioMixerGroup = mixer.FindMatchingGroups("SFX")[0];

        crashAudioSource = gameObject.AddComponent<AudioSource>();
        crashAudioSource.outputAudioMixerGroup = mixer.FindMatchingGroups("SFX")[0];

        deathAudioSource = gameObject.AddComponent<AudioSource>();
        deathAudioSource.outputAudioMixerGroup = mixer.FindMatchingGroups("Death")[0];

        nightSoundSource = AddAudio(nightSound, true);
        morningSoundSource = AddAudio(morningSound, true);

        morningSoundSource.outputAudioMixerGroup = mixer.FindMatchingGroups("Morning Ambient")[0];
        nightSoundSource.outputAudioMixerGroup = mixer.FindMatchingGroups("Night Ambient")[0];
        playBGM();
    }

    private void OnDestroy()
    {
        EventBroadcaster.Instance.RemoveObserver(EventNames.FinalGameAudioEvents.ON_GRASS_SOUND);
        EventBroadcaster.Instance.RemoveObserver(EventNames.FinalGameAudioEvents.ON_LOG_SOUND);
        EventBroadcaster.Instance.RemoveObserver(EventNames.FinalGameAudioEvents.ON_ROAD_SOUND);
        EventBroadcaster.Instance.RemoveObserver(EventNames.FinalGameAudioEvents.ON_DEATH_SOUND);
        EventBroadcaster.Instance.RemoveObserver(EventNames.FinalGameAudioEvents.ON_SPLASH_SOUND);
        EventBroadcaster.Instance.RemoveObserver(EventNames.FinalGameAudioEvents.ON_CRASH_SOUND);
        EventBroadcaster.Instance.RemoveObserver(EventNames.FinalGameAudioEvents.ON_DAY_PHASE);
        EventBroadcaster.Instance.RemoveObserver(EventNames.FinalGameAudioEvents.ON_HOPPING_SOUND);
    }

    void playSplashSound()
    {
        effectsAudioSource.clip = splashSound;
        effectsAudioSource.Play();
    }

    void playCrashSound()
    {
        crashAudioSource.clip = crashSound;
        crashAudioSource.Play();
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
        deathAudioSource.clip = deathSound;
        deathAudioSource.Play();
    }

    void playBGM()
    {
        morningSoundSource.Play();
    }
    public void changeBGM(Parameters param)
    {
        night = param.GetBoolExtra(EventNames.FinalGameAudioEvents.CURRENT_DAY_PHASE, false);
        Debug.Log("changing BGM. Night is " + night);
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
