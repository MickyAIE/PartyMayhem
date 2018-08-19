using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowSprite : MonoBehaviour
{
    //Attaches sprite to other sprite's position without affecting its rotation.

    public Transform sprite;

    private void Update()
    {
        transform.position = sprite.transform.position;
    }
}