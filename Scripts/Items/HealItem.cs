using UnityEngine;

public class HealItem : PickedItem
{
    private void OnEnable()
    {
        InputController.PlayerDead += Reset;
    }
    private void OnDisable()
    {
        InputController.PlayerDead -= Reset;
    }

    private void Reset()
    {
        picked = false;
        GetComponent<SpriteRenderer>().enabled = true;
    }

    public override void PickItem(GameObject go)
    {
        Heal h = item as Heal;
        go.GetComponent<InputController>().Heal(h.Health, h.Armor);
    }
}
