using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelStarViewComponent : MonoBehaviour
{
    [SerializeField]
    private Slider scoreSlider;
    [SerializeField]
    private List<StarComponent> starComponents;

    int maxScore;
    float currValue;
    List<bool> starCheck;
    public void Init(int _maxScore)
    {
        scoreSlider.value = 0;
        maxScore = _maxScore;
        currValue = 0;

        starCheck = new List<bool>();
        for (int i = 0; i < starComponents.Count; i++)
        {
            starCheck.Add(false);
            starComponents[i].SetActive(false);
        }
    }

    public void SetScore(int score, int starNb)
    {
        currValue = Mathf.Clamp01((float)score/maxScore);
        if (starNb >0 && !starCheck[starNb - 1])
        {
            starCheck[starNb - 1] = true;
            starComponents[starNb - 1].SetActive(true);
        }
    }

    private void Update()
    {
        if(scoreSlider.value != currValue)
        {
            scoreSlider.value = Mathf.Lerp(scoreSlider.value, currValue, 0.1f);            
        }
    }
}
