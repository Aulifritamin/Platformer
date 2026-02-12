using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SkillView : MonoBehaviour
{
    [SerializeField] protected Vampirism Skill;

    protected virtual void OnEnable()
    {
        Skill.TimeChanged += OnUpdateView;
    }
    
    protected virtual void OnDisable()
    {
        Skill.TimeChanged -= OnUpdateView;
    }

    protected abstract void OnUpdateView(float currentTime, float maxTime);
}
