using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TileControler : MonoBehaviour
{
    public delegate void OnEventTrigger<T>(T data);
    public OnEventTrigger<TileControler> OnClick;

    [SerializeField]
    private Animator anim;
    [SerializeField]
    EventTrigger trigger;
    [SerializeField]
    float fallSpeed = 2.0f;

    float limitPosition;
    public void Init(float limitPos, float newFallSpeed = -1f)
    {
        trigger.triggers.Clear();
        EventTrigger.Entry entry = new EventTrigger.Entry
        {
            eventID = EventTriggerType.PointerClick
        };
        entry.callback.AddListener(TileClickedEvent);
        trigger.triggers.Add(entry);

        if(newFallSpeed > 0)
        {
            fallSpeed = newFallSpeed;
        }
        
        limitPosition = limitPos;
        anim.SetTrigger("Idle");
        startFalling = true;
    }

    public float GetPos()
    { 
        return transform.position.y; 
    }

    private void TileClickedEvent(BaseEventData data)
    {
        float curPos = GetPos();
        OnClick?.Invoke(this);
        startFalling = false;

        trigger.triggers.Clear();
        anim.SetTrigger("Clicked");
        Invoke("AutoDestroy", 0.5f);
    }

    private void AutoDestroy()
    {
        Destroy(gameObject);
    }

    bool startFalling = false;
    private void Update()
    {
        if(startFalling)
        {
            if(transform.position.y > limitPosition)
            {
                transform.Translate(Vector2.down * fallSpeed * Time.deltaTime);
            }
            else
            {
                TileClickedEvent(null);
            }
        }
    }
}
