using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UITestScene : MonoBehaviour {

    public Animator anim;
    
    public void OnTopPress()
    {
        anim.SetBool("goDown", true);
    }

    public void OnDownPress()
    {
        anim.SetBool("goDown", false);
    }
}
