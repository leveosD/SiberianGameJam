using UnityEngine;

[CreateAssetMenu(fileName = "Heal", menuName = "MyAssets/Items/Heal")]
public class Heal : ScriptableObject
{
    [SerializeField] private int id;
    [SerializeField] private int health;
    [SerializeField] private int armor;
    
    public int Id
    {
        get => id;
    }

    public int Health
    {
        get => health;
    }
    
    public int Armor
    {
        get => armor;
    }
}