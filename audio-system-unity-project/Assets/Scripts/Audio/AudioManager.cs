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
    private EventInstance musicEventInst;

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
        InitializeMusic(FMODEvents.instance.level1Music);
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

    private void InitializeMusic(EventReference musicEvent)
    {
        musicEventInst = CreateAudioInstance(musicEvent);
        musicEventInst.start();
    }

    public void SetMusicParameter(string paramName, float paramValue)
    {
        musicEventInst.setParameterByName(paramName, paramValue);
    }
    public float GetMusicParameter(string paramName)
    {
        float outVal;
        musicEventInst.getParameterByName(paramName, out outVal);
        return outVal;
    }

    public Level1MusicArea SetMusicArea(Level1MusicArea area)
    {
        float oldAreaf;
        musicEventInst.getParameterByName("Area", out oldAreaf);
        Level1MusicArea musicArea = (Level1MusicArea)oldAreaf;
        musicEventInst.setParameterByName("Area", (float)area);
        return musicArea;
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
