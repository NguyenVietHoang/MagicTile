using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class MainGameView : MonoBehaviour
{
    [SerializeField]
    private TextComponent currentScoreTxt;
    [SerializeField]
    private TextComponent comboTxt;
    [SerializeField]
    private TextComponent scoreTypeTxt;

    [Header ("Star Container")]
    [SerializeField]
    private LevelStarViewComponent levelStarViewComponent;

    public void Init(int _maxScore)
    {
        currentScoreTxt.SetText("0", false);
        comboTxt.SetText("", false);
        scoreTypeTxt.SetText("", false);
        levelStarViewComponent.Init(_maxScore);
    }

    public void SetScore(int score, int starNb)
    {
        levelStarViewComponent.SetScore(score, starNb);
        currentScoreTxt.SetText(score.ToString(), false);
    }

    public void SetCombo(int combo) 
    {
        comboTxt.SetText((combo <=0) ? "" : ("x" + combo.ToString()));
    }

    public void SetScoreType(SCORE_TYPE type)
    {
        scoreTypeTxt.SetText(type.ToString(), (type != SCORE_TYPE.Miss)? true:false);
    }
}

public enum SCORE_TYPE
{
    Miss = 0,
    Cool = 1,
    Great =2,
    Perfect = 3,
}
