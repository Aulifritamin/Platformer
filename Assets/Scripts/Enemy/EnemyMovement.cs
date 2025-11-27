using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] private float _moveSpeed = 2f;

    private Rigidbody2D _rigidbody;

    public void Initialize(Rigidbody2D rigidbody)
    {
        _rigidbody = rigidbody;
    }

    public void Move(float direction)
    {
        _rigidbody.velocity = new Vector2(direction * _moveSpeed, _rigidbody.velocity.y);
    }
}
