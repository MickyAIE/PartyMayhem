using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowSprite : MonoBehaviour
{
    //This script lets an object follow around another object around without affecting it's rotation.
    //Used for shadows on sprites that need to rotate eg. balls.

    public Transform sprite; //The sprite you want your sprite to follow.

    private void Update()
    {
        transform.position = sprite.transform.position;
    }
}