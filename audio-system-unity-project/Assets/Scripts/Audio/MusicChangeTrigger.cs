using UnityEngine;

public class MusicChangeTrigger : MonoBehaviour
{
    [SerializeField] private Level1MusicArea interiorMusicArea;

    private Level1MusicArea oldMusicArea;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            oldMusicArea = AudioManager.instance.SetMusicArea(interiorMusicArea);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            AudioManager.instance.SetMusicArea(oldMusicArea);
        }
    }
}
