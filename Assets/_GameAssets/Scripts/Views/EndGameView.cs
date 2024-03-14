using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class EndGameView : MonoBehaviour
{    
    public OnEventTrigger onMainMenuClick;
    public OnEventTrigger onQuickGameClick;

    [SerializeField]
    private GameObject root;
    [SerializeField]
    private TextMeshProUGUI scoreTxt;
    [SerializeField]
    private List<Image> starImage;
    [SerializeField]
    private Button mainMenuButton;
    [SerializeField]
    private Button quitButton;

    public void Init()
    {
        SetActive(false);
    }

    public void SetActive(bool active)
    {
        root.SetActive(active);

        mainMenuButton.onClick.RemoveAllListeners();
        mainMenuButton.onClick.AddListener(ToMainMenu_Pressed);
        quitButton.onClick.RemoveAllListeners();
        quitButton.onClick.AddListener(QuitGame_Pressed);
    }

    public void SetViewData(int currentScore, int starNb)
    {
        scoreTxt.text = currentScore.ToString();
       
        for(int i = 0; i < starImage.Count; i++)
        {
            if(i <= (starNb -1))
            {
                starImage[i].color = Color.white;
            }
            else
            {
                starImage[i].color = Color.black;
            }
        }
    }

    private void ToMainMenu_Pressed()
    {
        onMainMenuClick?.Invoke();
    }

    private void QuitGame_Pressed()
    {
        onQuickGameClick?.Invoke();
    }
}
