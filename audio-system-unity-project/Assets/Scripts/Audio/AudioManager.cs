using UnityEngine;
using FMODUnity;
using FMOD.Studio;
using System.Collections.Generic;

public class AudioManager : MonoBehaviour
{
    private List<EventInstance> eventInsts;
    private List<StudioEventEmitter> eventEmitters;
    public static AudioManager instance { get; private set; }

    private EventInstance ambienceEventInst;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Debug.LogError("Duplicate AudioManager found.");
            return;
        }
        else
        {
            instance = this;
        }
        eventInsts = new List<EventInstance> ();
        eventEmitters = new List<StudioEventEmitter> ();
    }

    private void Start()
    {
        InitializeAmbience(FMODEvents.instance.windAmbience);
    }

    private void InitializeAmbience(EventReference ambienceEvent)
    {
        ambienceEventInst = CreateAudioInstance(ambienceEvent);
        ambienceEventInst.start();
    }

    public void SetAmbienceParameter(string paramName, float paramValue)
    {
        ambienceEventInst.setParameterByName(paramName, paramValue);
    }
    public float GetAmbienceParameter(string paramName)
    {
        float outVal;
        ambienceEventInst.getParameterByName(paramName, out outVal);
        return outVal;
    }

    /// <summary>
    /// Play a one shot sound with FMOD.
    /// </summary>
    /// <param name="sound"></param>
    /// <param name="pos"></param>
    public void PlayOneShot(EventReference sound, Vector3 pos)
    {
        RuntimeManager.PlayOneShot(sound, pos);
    }

    public EventInstance CreateAudioInstance(EventReference sound)
    {
        EventInstance evInst = RuntimeManager.CreateInstance(sound);
        eventInsts.Add(evInst);
        return evInst;
    }

    public StudioEventEmitter InitializeEventEmitter(EventReference sound, GameObject gameObj)
    {
        StudioEventEmitter eventEmit = gameObj.GetComponent<StudioEventEmitter>();
        eventEmit.EventReference = sound;
        eventEmitters.Add(eventEmit);
        return eventEmit;
    }

    /// <summary>
    /// Cleans up all event instances when the scene is unloaded.
    /// </summary>
    private void CleanUp()
    {
        foreach(EventInstance evInst in eventInsts)
        {
            evInst.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
            evInst.release();
        }
        foreach(StudioEventEmitter eventEmitter in eventEmitters)
        {
            eventEmitter.Stop();
        }
    }

    private void OnDestroy()
    {
        CleanUp();
    }
}
