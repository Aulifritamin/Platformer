using UnityEngine;

public class CharacterRotator : MonoBehaviour
{
    private float _facingRotationY = 180f;
    private bool _isFacingRight = true;

    public void Rotate(float direction)
    {
        if (direction > 0 && _isFacingRight == false)
        {
            Turn();
        }
        else if (direction < 0 && _isFacingRight == true)
        {
            Turn();
        }
    }

    private void Turn()
    {
        _isFacingRight = !_isFacingRight;

        transform.Rotate(0f, _facingRotationY, 0f);
    }
}
