using UnityEngine;

public class AmbienceChangeTrigger : MonoBehaviour
{
    [SerializeField] private string paramName;
    [SerializeField] private float paramEnterValue;

    private float paramExitValue;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            paramExitValue = AudioManager.instance.GetAmbienceParameter(paramName);
            AudioManager.instance.SetAmbienceParameter(paramName, paramEnterValue);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            AudioManager.instance.SetAmbienceParameter(paramName, paramExitValue);
        }
    }
}
