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
        if (Physics.Raycast(RayStart.position, RayStart.transform.up * -1, out hit, range, layerMask))
        {
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

    void PlayFootstepSound(AudioClip[] footstepSounds)
    {
        if (footstepSounds != null &&  footstepSounds.Length > 0)
        {
            AudioClip randomClip = footstepSounds[Random.Range(0, footstepSounds.Length)];
            AudioSource.volume = Random.Range(0.5f, 1f);
            AudioSource.pitch = Random.Range(0.8f, 1f);
            AudioSource.PlayOneShot(randomClip);
        }
    }

    // Update is called once per frame
    void Update()
    {
        Debug.DrawRay(RayStart.position, RayStart.transform.up * -1, Color.green);
    }



}
