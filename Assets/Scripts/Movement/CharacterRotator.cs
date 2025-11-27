using UnityEngine;

public class CharacterRotator : MonoBehaviour
{
    private Quaternion _rightRot;
    private Quaternion _leftRot;

    private void Awake()
    {
        _rightRot = Quaternion.Euler(0f, 0f, 0f);
        _leftRot = Quaternion.Euler(0f, 180f, 0f);
    }

    public void Rotate(float direction)
    {
        if (direction > 0)
        {
            transform.rotation = _rightRot;
        }
        else if (direction < 0 )
        {
            transform.rotation = _leftRot;
        }
    }
}
