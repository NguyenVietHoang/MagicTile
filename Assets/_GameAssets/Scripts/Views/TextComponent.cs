using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TextComponent : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI text;
    [SerializeField]
    private Animator anim;
    [SerializeField]
    private ParticleSystem fx;

    public void SetText(string data, bool triggerFx = false)
    {
        text.text = data;
        anim.SetTrigger("Blink");

        if(fx != null && triggerFx)
        {
            fx.Play();
        }
    }
}
