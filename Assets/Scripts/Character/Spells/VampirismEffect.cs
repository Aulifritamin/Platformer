using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VampirismEffect : MonoBehaviour
{
   [SerializeField] private SpriteRenderer _areaRenderer;

   private void Awake()
   {
       _areaRenderer.enabled = false;
   }

    public void ActivateEffect()
    {
         _areaRenderer.enabled = true;
    }
    
    public void DeactivateEffect()
    {
        _areaRenderer.enabled = false;
    }
}
