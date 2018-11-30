using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerParticleScript : MonoBehaviour {

    [SerializeField] private ParticleSystem playerExplodeParticle;
    [SerializeField] private ParticleSystem playerSplashParticle;
	
	void Start () {
        EventBroadcaster.Instance.AddObserver(EventNames.FinalGameEvents.ON_PLAYER_EXPLOD_FROM_CAR, this.playPlayerExplodeParticle);
        EventBroadcaster.Instance.AddObserver(EventNames.FinalGameEvents.ON_PLAYER_SPLASH_FROM_WATER, this.playPlayerSplashParticle);

        playerExplodeParticle.Stop();
        playerSplashParticle.Stop();
	}

    private void OnDestroy()
    {
        EventBroadcaster.Instance.RemoveObserver(EventNames.FinalGameEvents.ON_PLAYER_EXPLOD_FROM_CAR);
        EventBroadcaster.Instance.RemoveObserver(EventNames.FinalGameEvents.ON_PLAYER_SPLASH_FROM_WATER);
    }


    private void playPlayerExplodeParticle()
    {
        playerExplodeParticle.Play();
    }

    private void playPlayerSplashParticle()
    {
        playerSplashParticle.Play();
    }


}
