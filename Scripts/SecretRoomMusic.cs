using UnityEngine;

public class SecretRoomMusic : MonoBehaviour
{
    private AudioSource _audioSource;

    [SerializeField] private AudioSource globalMusic;

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.gameObject.name);
        Debug.Log(_audioSource);
        if (other.CompareTag("Player"))
        {
            globalMusic.Stop();
            _audioSource.Play();
        }
    }
    
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _audioSource.Stop();
        }
    }
}
