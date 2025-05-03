using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class BulletManager: MonoBehaviour
{
    private List<Image> _slots;
    private List<Image> _bullets;

    private int _index;

    private void Awake()
    {
        _index = 5;
        
        _slots = GetComponentsInChildren<Image>().Where(go => go.CompareTag("EmptySlotUI")).ToList();
        _bullets = GetComponentsInChildren<Image>().Where(go => go.CompareTag("BulletUI")).ToList();

        Debug.Log(_slots + "\n" + _bullets);
        
        int i = 0;
        var defPos = _slots[0].rectTransform.position;
        var addX = new Vector3(100, 0, 0);
        foreach (var slot in _slots)
        {
            slot.rectTransform.position = defPos + addX *  i;
            i += 1;
        }

        i = 0;
        foreach (var bullet in _bullets)
        {
            bullet.rectTransform.position = defPos + addX * i;
            i += 1;
        }
    }

    public void Shot()
    {
        _bullets[_index].enabled = false;
        _index--;
    }

    public void Reload()
    {
        for(int i = _index + 1; i < 6; i++)
        {
            _bullets[i].enabled = true;
        }
        _index = 5;
    }
}
