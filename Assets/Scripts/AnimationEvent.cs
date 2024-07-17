using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationEvent : MonoBehaviour
{
    // class to determine when an animation ends
    public bool isAnimating = false;

    public void AnimationStart()
    {
        isAnimating = true;
    }
    public void AnimationEnd()
    {
        isAnimating = false;
    }
}
