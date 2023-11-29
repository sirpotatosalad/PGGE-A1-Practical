using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkSfx : MonoBehaviour
{
    public AudioSource AudioSource;

    public AudioClip[] concreteClips;
    public AudioClip[] dirtClips;
    public AudioClip[] metalClips;
    public AudioClip[] woodClips;
    public AudioClip[] sandClips;

    RaycastHit hit;
    public Transform RayStart;
    public float range;
    public LayerMask layerMask;

    public void DetectGroundOnStep()
    {
        // cast a ray downward from RayStart, ignoring all layers that are not Ground (set in inspector)
        if (Physics.Raycast(RayStart.position, RayStart.transform.up * -1, out hit, range, layerMask))
        {
            //check for specific ground type hit by ray and pass the array of audioclips for the ground type
            if (hit.collider.CompareTag("Concrete"))
            {
                PlayFootstepSound(concreteClips);
            }
            if (hit.collider.CompareTag("Dirt"))
            {
                PlayFootstepSound(dirtClips);
            }
            if (hit.collider.CompareTag("Metal"))
            {
                PlayFootstepSound(metalClips);
            }
            if (hit.collider.CompareTag("Wood"))
            {
                PlayFootstepSound(woodClips);
            }
            if (hit.collider.CompareTag("Sand"))
            {
                PlayFootstepSound(sandClips);
            }
        }
    }

    // function will play a random clip given an array of audio clips for a certain ground type
    void PlayFootstepSound(AudioClip[] footstepSounds)
    {
        if (footstepSounds != null &&  footstepSounds.Length > 0)
        {
            //play a random clip from given array
            AudioClip randomClip = footstepSounds[Random.Range(0, footstepSounds.Length)];

            // play audio clip with a randomised volume and pitch
            AudioSource.volume = Random.Range(0.5f, 1f);
            AudioSource.pitch = Random.Range(0.8f, 1f);
            AudioSource.PlayOneShot(randomClip);
        }
    }

    void Update()
    {
        Debug.DrawRay(RayStart.position, RayStart.transform.up * -1, Color.green);
    }



}
