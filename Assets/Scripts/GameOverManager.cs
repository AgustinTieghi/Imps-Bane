using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverManager : MonoBehaviour
{
    public List<Animator> animators;
    void Start()
    {
        foreach (var animator in animators)
        {
            animator.SetBool("Run Forward", false);
        }
    }  
}
