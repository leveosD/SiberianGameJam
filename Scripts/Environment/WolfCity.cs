using UnityEngine;
using UnityEngine.SceneManagement;

public class WolfCity : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            SceneManager.LoadScene(2);
        }
    }
}
