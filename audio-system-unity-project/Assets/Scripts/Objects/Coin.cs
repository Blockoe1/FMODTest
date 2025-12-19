using FMODUnity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(StudioEventEmitter))]
public class Coin : MonoBehaviour
{
    private StudioEventEmitter emitter;
    //[SerializeField] private EventReference coinSound;
    private SpriteRenderer visual;
    private ParticleSystem collectParticle;
    private bool collected = false;

    private void Awake() 
    {
        visual = this.GetComponentInChildren<SpriteRenderer>();
        collectParticle = this.GetComponentInChildren<ParticleSystem>();
        collectParticle.Stop();
    }

    private void Start()
    {
        emitter = AudioManager.instance.InitializeEventEmitter(FMODEvents.instance.coinIdle, gameObject);
        emitter.Play();
    }

    private void OnTriggerEnter2D() 
    {
        if (!collected) 
        {
            collectParticle.Play();
            CollectCoin();
        }
    }

    private void CollectCoin() 
    {
        collected = true;
        visual.gameObject.SetActive(false);
        GameEventsManager.instance.CoinCollected();

        // Play the collected sound;
        //AudioManager.instance.PlayOneShot(coinSound, transform.position);

        // Play the collected sound using the FMODEvents singleton
        AudioManager.instance.PlayOneShot(FMODEvents.instance.coinCollected, transform.position);
        // Stop the idle sound.
        emitter.Stop();
    }

}
