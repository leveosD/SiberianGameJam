using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnemyTrigger : MonoBehaviour
{
    private List<EnemyController> _enemies;

    private void Start()
    {
        _enemies = GetComponentsInChildren<EnemyController>().ToList();
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.tag);
        if (other.CompareTag("Player"))
        {
            foreach (var enemy in _enemies)
            {
                enemy.IsHunting = true;
            }
        }
    }
}
