using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyVision : MonoBehaviour
{
    [SerializeField] private Transform _viewPoint;
    [SerializeField] private float _viewRadius = 3f;
    [SerializeField] private LayerMask _playerMask;
    [SerializeField] private LayerMask _obstacleMask;

    private void Awake()
    {
        if(_viewPoint == null)
        {
            _viewPoint = transform;
        }
    }

    public Transform CheckVision()
    {
        Collider2D playerCollider = Physics2D.OverlapCircle(_viewPoint.position, _viewRadius, _playerMask);

        if(playerCollider == null)
        {
            return null;
        } 

        Vector2 directionToPlayer = (playerCollider.transform.position - _viewPoint.position).normalized;
        RaycastHit2D hit = Physics2D.Raycast(_viewPoint.position, directionToPlayer, _viewRadius, _obstacleMask);

        if(hit.collider != null)
        {
            return null;
        }

        return playerCollider.transform;
    }
}
