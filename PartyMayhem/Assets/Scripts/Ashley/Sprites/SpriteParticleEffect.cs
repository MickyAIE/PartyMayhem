using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteParticleEffect : MonoBehaviour
{
    //Attach to any particle effect prefab to have it destroy itself when it has finished playing once.

    private Animator effect;
    private AnimatorClipInfo[] clip;
    public float lifetime;
    public float timer = 0;

    private void Awake()
    {
        effect = GetComponent<Animator>();
        clip = effect.GetCurrentAnimatorClipInfo(0);
    }

    private void Start()
    {
        lifetime = clip.Length;
    }

    private void Update()
    {
        timer += Time.deltaTime;

        if (timer >= lifetime)
            Destroy(gameObject);
    }
}