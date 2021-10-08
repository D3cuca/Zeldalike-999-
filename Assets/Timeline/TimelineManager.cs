using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
public class TimelineManager : MonoBehaviour
{
    private bool Fix = false;
    public Animator PlayerAnimator;
    public RuntimeAnimatorController PlayerRuntimeAnim;
    public PlayableDirector Director;

    // Start is called before the first frame update
    void OnEnable()
    {
        PlayerRuntimeAnim = PlayerAnimator.runtimeAnimatorController;
        PlayerAnimator.runtimeAnimatorController = null;
    }

    // Update is called once per frame
    void Update()
    {
        if(Director.state != PlayState.Playing && !Fix)
        {
            Fix = true;
            PlayerAnimator.runtimeAnimatorController = PlayerRuntimeAnim;
            PlayerAnimator.SetFloat("moveX", 0);
            PlayerAnimator.SetFloat("moveY", -1);
        }
    }
}
