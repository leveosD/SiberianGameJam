using System.Collections;
using UnityEngine;

public class FireBehaviour : MonoBehaviour
{
    private Animator animator;
    
    void Start()
    {
        animator = GetComponent<Animator>();
        float randomDelay = Random.Range(0f, 1f);
        StartCoroutine(PlayWithDelay(randomDelay));
    }

    IEnumerator PlayWithDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        animator.Play("Burn");
    }
}
