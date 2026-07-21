using NUnit.Framework;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private LevelStep[] levelSteps;

    private int currentStepIndex = 0;

    public void OnPlayerColliderHit(Collider col)
    {
        CheckCollidingTag(col.tag);
    }

    private void CheckCollidingTag(string tag)
    {
        if (levelSteps[currentStepIndex].collidingTag == tag)
        {
            HandleLevelStep(levelSteps[currentStepIndex]);
        }
    }

    private void HandleLevelStep(LevelStep step)
    {
        switch (step.stepType)
        {
            case LevelStepType.Start:
                step.activateAfterCompletionObj.SetActive(true);
                currentStepIndex++;
                break;
            case LevelStepType.End:
                Debug.Log("Level Completed!");
                break;
        }
    }
}

[Serializable]
public class LevelStep
{
    public LevelStepType stepType;
    public string collidingTag;
    public GameObject activateAfterCompletionObj;
}
public enum LevelStepType
{
    None = 0,
    Start = 1,
    End = 2
}