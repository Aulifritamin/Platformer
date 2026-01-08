using System.Collections;
using UnityEngine;

public class GroundDetector : MonoBehaviour
{
    [SerializeField] private Transform _groundCheck;
    [SerializeField] private LayerMask _groundMask;
    [SerializeField] private Vector2 _boxSize = new Vector2(0.2f, 0.1f);
    private WaitForSeconds _wait = new WaitForSeconds(0.03f);

    public bool IsGrounded { get; private set; }

    private void Start()
    {
        StartCoroutine(GroundCheckRoutine());
    }
    
    private IEnumerator GroundCheckRoutine()
    {
        while (enabled)
        {
            IsGrounded = Physics2D.OverlapBox(_groundCheck.position, _boxSize, 0, _groundMask);
            yield return _wait;
        }
    }

    private void OnDrawGizmos()
    {
        if (_groundCheck == null)
            return;

        Gizmos.color = IsGrounded ? Color.green : Color.red;
        Gizmos.DrawWireCube(_groundCheck.position, _boxSize);
    }
}
