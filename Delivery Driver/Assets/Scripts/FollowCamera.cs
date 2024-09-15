using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    [SerializeField] GameObject thingToFollow;
    // This thing's position (camera) should be the same as the car's.
    void LateUpdate()
    {
        transform.position = thingToFollow.transform.position + new Vector3 (0, 0, -10);
        //Optional: Keep viewpoint relative to car, not to world.
        //transform.rotation = thingToFollow.transform.rotation;
    }
}
