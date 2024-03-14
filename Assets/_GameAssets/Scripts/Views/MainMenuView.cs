using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuView : MonoBehaviour
{
    public OnEventTrigger<int> onLevelClick;
    public OnEventTrigger onQuickGameClick;

    [SerializeField]
    private Button easyButton;
    [SerializeField]
    private Button mediumButton;
    [SerializeField]
    private Button hardButton;
    [SerializeField]
    private Button quitButton;

    public void Init()
    {
        easyButton.onClick.RemoveAllListeners();
        easyButton.onClick.AddListener(() => onLevelClick?.Invoke(0));

        mediumButton.onClick.RemoveAllListeners();
        mediumButton.onClick.AddListener(() => onLevelClick?.Invoke(1));

        hardButton.onClick.RemoveAllListeners();
        hardButton.onClick.AddListener(() => onLevelClick?.Invoke(2));

        quitButton.onClick.RemoveAllListeners();
        quitButton.onClick.AddListener(() => onQuickGameClick?.Invoke());
    }
}
