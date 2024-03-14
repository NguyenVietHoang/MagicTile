using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarComponent : MonoBehaviour
{
    [SerializeField]
    private Animator anim;
    [SerializeField]
    private ParticleSystem starVfx;

    public void SetActive(bool active)
    {
        anim.SetBool("On", active);

        if(active)
        {
            starVfx.Stop();
        }
        else
        {
            starVfx.Play();
        }
    }
}
