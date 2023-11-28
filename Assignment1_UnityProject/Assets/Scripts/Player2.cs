using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PGGE.Patterns;
using PGGE;


public class Player2 : MonoBehaviour
{
    [HideInInspector]
    public FSM mFsm = new FSM();
    public Animator mAnimator;
    public PlayerMovement mPlayerMovement;


    [HideInInspector]
    public bool[] mAttackButtons = new bool[3];

    public LayerMask mPlayerMask;

    public AudioSource mAudioSource;
    public AudioClip mAudioClipReload;


    void Update()
    {
        mFsm.Update();

        // For Student ----------------------------------------------------//
        // Implement the logic of button clicks for shooting. 
        //-----------------------------------------------------------------//

        if (Input.GetButton("Fire1"))
        {
            mAttackButtons[0] = true;
            mAttackButtons[1] = false;
            mAttackButtons[2] = false;
        }
        else
        {
            mAttackButtons[0] = false;
        }

        if (Input.GetButton("Fire2"))
        {
            mAttackButtons[0] = false;
            mAttackButtons[1] = true;
            mAttackButtons[2] = false;
        }
        else
        {
            mAttackButtons[1] = false;
        }

        if (Input.GetButton("Fire3"))
        {
            mAttackButtons[0] = false;
            mAttackButtons[1] = false;
            mAttackButtons[2] = true;
        }
        else
        {
            mAttackButtons[2] = false;
        }
    }


    public void Move()
    {
        mPlayerMovement.HandleInputs();
        mPlayerMovement.Move();
    }

    public void Reload()
    {
        StartCoroutine(Coroutine_DelayReloadSound());
    }

    IEnumerator Coroutine_DelayReloadSound(float duration = 1.0f)
    {
        yield return new WaitForSeconds(duration);

        mAudioSource.PlayOneShot(mAudioClipReload);
    }

   

}
