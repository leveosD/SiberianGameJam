using UnityEngine;

public interface IMovable
{
    void Move(Vector2 position);
    void Rotate(Vector3 position);
    void MileAttack();
}
