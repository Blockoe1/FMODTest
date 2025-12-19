using UnityEngine;
using FMODUnity;

public class AudioManager : MonoBehaviour
{
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
}
