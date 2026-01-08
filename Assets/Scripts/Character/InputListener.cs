using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputListener : MonoBehaviour
{
    private const string _horizontal = "Horizontal";
    private const string _vertical = "Vertical";
    private const string _jump = "Jump";
    private const string _attack = "Fire1";

    public Vector2 MoveInput { get; private set; }
    public bool JumpInput { get; private set; }
    public bool AttackInput { get; private set; }

    public event Action JumpPressed;
    public event Action AttackPressed;


    private void Update()
    {
        MoveInput = new Vector2(Input.GetAxisRaw(_horizontal), Input.GetAxisRaw(_vertical)); 
        
        if (Input.GetButtonDown(_jump))
        {
            JumpPressed?.Invoke();
        }

        if (Input.GetButtonDown(_attack))
        {
            AttackPressed?.Invoke();
        }
    }
}
