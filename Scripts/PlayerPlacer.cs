using UnityEngine;

public class PlayerPlacer : MonoBehaviour
{
    [SerializeField] private Vector3 startPosition;
    
    private void Start()
    {
        FindObjectOfType<InputController>().transform.position = startPosition;
    }
}
