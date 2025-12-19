using UnityEngine;
using FMODUnity;
using FMOD.Studio;
using System.Collections.Generic;

public class AudioManager : MonoBehaviour
{
    private List<EventInstance> eventInsts;
    public static AudioManager instance { get; private set; }

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
    }

    private void OnDestroy()
    {
        CleanUp();
    }
}
