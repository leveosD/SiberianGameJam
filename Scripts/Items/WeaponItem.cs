using System.Collections;
using UnityEngine;

public class WeaponItem : PickedItem
{
    private void OnEnable()
    {
        var weapon = item as Weapon;
        if (PlayerPrefs.GetInt("WeaponCounter") >= weapon?.Id)
        {
            Destroy(gameObject);
        }
    }

    protected override void LaterEffect()
    {
        StartCoroutine(DelayedDestroy(audioSource.clip.length));
    }

    private IEnumerator DelayedDestroy(float delay)
    {
        yield return new WaitForSecondsRealtime(delay);
        Destroy(gameObject);
    }

    public override void PickItem(GameObject go)
    {
        go.GetComponent<WeaponController>().AddWeapon((Weapon)item);
    }
}