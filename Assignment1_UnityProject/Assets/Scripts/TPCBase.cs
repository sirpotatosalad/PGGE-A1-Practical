using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Transactions;
using UnityEngine;
using UnityEngine.AI;

namespace PGGE
{
    // The base class for all third-person camera controllers
    public abstract class TPCBase
    {
        protected Transform mCameraTransform;
        protected Transform mPlayerTransform;

        public Transform CameraTransform
        {
            get
            {
                return mCameraTransform;
            }
        }
        public Transform PlayerTransform
        {
            get
            {
                return mPlayerTransform;
            }
        }

        public TPCBase(Transform cameraTransform, Transform playerTransform)
        {
            mCameraTransform = cameraTransform;
            mPlayerTransform = playerTransform;
        }


        public void RepositionCamera()
        {
            //-------------------------------------------------------------------
            // Implement here.
            //-------------------------------------------------------------------
            //-------------------------------------------------------------------
            // Hints:
            //-------------------------------------------------------------------
            // check collision between camera and the player.
            // find the nearest collision point to the player
            // shift the camera position to the nearest intersected point
            //-------------------------------------------------------------------
            CharacterController characterController = mPlayerTransform.GetComponent<CharacterController>();
            float offset = characterController.height;
            RaycastHit hit;
            Vector3 camToPlayer = mPlayerTransform.position - mCameraTransform.position;
            //Vector3 camToPlayer = mCameraTransform.position - mPlayerTransform.position;
            //camToPlayer += Vector3.forward * -offset;
            //camToPlayer = camToPlayer.normalized;


            if (Physics.Raycast(mCameraTransform.position, camToPlayer, out hit, camToPlayer.magnitude))
            {
                if (hit.collider.gameObject.layer == LayerMask.NameToLayer("Wall"))
                {

                    Debug.Log("Hit the wall");
                    // Calculate the offset vector to reposition the camera
                    //Vector3 offsetVector = camToPlayer.normalized + Vector3.forward * -offset + Vector3.up * offset;

                    Vector3 offsetVector = -hit.normal;
                    offsetVector.Normalize();
                    offsetVector += Vector3.up * offset /*+ Vector3.left * offset*/;

                    Vector3 nearestPoint = hit.collider.ClosestPoint(mPlayerTransform.position);

                    // Adjust the target position to snap a little more toward the player
                    Vector3 targetPos = (nearestPoint - mPlayerTransform.position) + offsetVector;

                    mCameraTransform.position = Vector3.Lerp(mCameraTransform.position, targetPos, Time.deltaTime * CameraConstants.Damping);
                    //mCameraTransform.position = position;


                    Debug.DrawRay(mCameraTransform.position, camToPlayer, Color.red, 0.1f);
                    Debug.DrawRay(mCameraTransform.position, offsetVector, Color.blue, 0.1f);
                    Debug.DrawLine(mCameraTransform.position, targetPos, Color.cyan, 0.1f);

                }
            }

            


        }

        public abstract void Update();
    }
}
