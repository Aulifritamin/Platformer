using System.Collections;
using UnityEngine;

public class GroundDetector : MonoBehaviour
{
    [SerializeField] private Transform _groundCheck;
    [SerializeField] private LayerMask _groundMask;
    [SerializeField] private float _groundCheckRadius = 0.15f;
    private WaitForSeconds _wait = new WaitForSeconds(0.05f);

    public bool IsGrounded { get; private set; }

    private void Start()
    {
        StartCoroutine(GroundCheckRoutine());
    }
    private IEnumerator GroundCheckRoutine()
    {
        while (enabled)
        {
            IsGrounded = Physics2D.OverlapCircle(_groundCheck.position, _groundCheckRadius, _groundMask);
            yield return _wait;
        }
    }
}
