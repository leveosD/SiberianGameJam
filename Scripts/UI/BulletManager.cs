using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class BulletManager: MonoBehaviour
{
    private List<Image> _slots;
    private List<Image> _bullets;
    
    private int _index = 0;
    private int _max;

    public int Index
    {
        set { _index = value; }
    }

    private void Awake()
    {
        _slots = GetComponentsInChildren<Image>().Where(go => go.CompareTag("EmptySlotUI")).ToList();
        _bullets = GetComponentsInChildren<Image>().Where(go => go.CompareTag("BulletUI")).ToList();

        _max = _slots.Count;
        _index = _max - 1;
        
        int i = 0;
        var defPos = _slots[0].rectTransform.position;
        var addX = new Vector3(30, 0, 0);
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

    public void Shot(int bps)
    {
        for (int i = 0; i < bps; i++)
        {
            _bullets[_index].enabled = false;
            _index--;
        }
    }

    public void Reload()
    {
        for(int i = _index + 1; i < _max; i++)
        {
            _bullets[i].enabled = true;
        }
        _index = _max - 1;
    }
}
