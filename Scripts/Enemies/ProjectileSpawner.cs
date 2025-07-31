using System;
using UnityEngine;

public class ProjectileSpawner : MonoBehaviour
{
    [SerializeField] private GameObject projectilePrefab;

    public void Spawn()
    {
        var p1 = Instantiate(projectilePrefab);
        p1.GetComponent<ProjectileBehaviour>().ParentName = gameObject.name;
        p1.transform.position = transform.position - transform.forward;
        p1.transform.forward = transform.forward;
//        p1.transform.rotation = Quaternion.Euler(0, transform.rotation.y, 0);
    }

    private void Update()
    {
        Debug.DrawLine(transform.position, transform.forward * 5);
    }
}
