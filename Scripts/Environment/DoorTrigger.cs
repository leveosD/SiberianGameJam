using System.Collections;
using DG.Tweening;
using UnityEngine;

public class DoorTrigger : MonoBehaviour
{
    [SerializeField] private GameObject door;
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            door.transform.DOMoveY(-1.1f, 1.18f)
                .SetEase(Ease.InSine);
            var audio = door.GetComponent<AudioSource>();
            audio.Play();
            StartCoroutine(DelayedDestroy(audio.clip.length));
        }
    }

    private IEnumerator DelayedDestroy(float delay)
    {
        yield return new WaitForSecondsRealtime(delay);
        Destroy(this);
    }
}
