using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Transactions;
using UnityEditor.Experimental.GraphView;
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

            //obtain the player height from CharacterController
            CharacterController characterController = mPlayerTransform.GetComponent<CharacterController>();
            float pHeight = characterController.height;

            // set up raycasts
            RaycastHit hit;
            RaycastHit ceilingHit;

            // vector from camera to player, offset upwards to player's height
            Vector3 camToPlayer = (mPlayerTransform.position + Vector3.up * pHeight) - mCameraTransform.position;

            //down vector from camera
            Vector3 camDown = -mCameraTransform.up;


            Debug.DrawRay(mCameraTransform.position, camToPlayer, Color.red);
            Debug.DrawRay(mCameraTransform.position, camDown, Color.green);
            Debug.DrawRay(mCameraTransform.position, -camDown, Color.blue);

           // raycast from the camera to the player
            if (Physics.Raycast(mCameraTransform.position, camToPlayer, out hit, camToPlayer.magnitude))
            {
                // check if wall hit by the raycast i.e., obstructing the camera
                if (hit.collider.gameObject.layer == LayerMask.NameToLayer("Wall"))
                {

                    // create an offset vector to player height
                    Vector3 offsetUp = -hit.normal;
                    offsetUp.Normalize();
                    offsetUp += Vector3.up * pHeight;

                    //find the nearest point from playerTransform to wall colliders
                    Vector3 nearestPoint = hit.collider.ClosestPoint(mPlayerTransform.position);

                    // add offsetUp to move nearest point to player height
                    Vector3 targetHeight = nearestPoint + offsetUp;

                    // set final position, moving x and z values toward the player to prevent clipping into the wall
                    // afterward, set height to targetHeight's y value
                    Vector3 finalPos = new Vector3(hit.point.x - hit.normal.x * 0.45f, targetHeight.y, hit.point.z - hit.normal.z * 0.45f);
                    mCameraTransform.position = finalPos;


                }
            }

            //downward raycast to check if ceiling is below the camera
            if (Physics.Raycast(mCameraTransform.position, camDown, out ceilingHit, pHeight))
            {
                // check if ceiling is hit by raycast i.e., intersecting the camera
                if (ceilingHit.collider.gameObject.layer == LayerMask.NameToLayer("Ceiling"))
                {
                    Debug.Log("hit ceiling");
                    // Adjust camera position below the ceiling
                    Vector3 finalPos = new Vector3(mCameraTransform.position.x, ceilingHit.point.y, mCameraTransform.position.z);
                    mCameraTransform.position = finalPos;

                }

            }

            //short upward raycast for cases that the ceiling is slightly in/above the camera
            if (Physics.Raycast(mCameraTransform.position, -camDown, out ceilingHit, 0.1f))
            {
                // check if ceiling is hit by raycast i.e., intersecting the camera
                if (ceilingHit.collider.gameObject.layer == LayerMask.NameToLayer("Ceiling") && ceilingHit.point.y > pHeight)
                {

                    Debug.Log("hit ceiling");
                    // Adjust camera position below the ceiling
                    Vector3 finalPos = new Vector3(mCameraTransform.position.x, ceilingHit.point.y - 0.2f, mCameraTransform.position.z);
                    mCameraTransform.position = finalPos;


                }

            }


        }




        public abstract void Update();
    }
}
