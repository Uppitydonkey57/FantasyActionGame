using UnityEngine.UI;
using UnityEngine;
using System.Collections;
using DG.Tweening;

public class FadeUI : Action 
{
    public CanvasGroup canvasGroup;

    public float time;
    public float amount;

    public Action[] finishedActions;

    public override void PerformAction()
    {
        StartCoroutine(NextAction());
        canvasGroup.DOFade(amount, time);

    }

    IEnumerator NextAction() 
    {
        yield return new WaitForSeconds(amount);

        foreach (Action action in finishedActions) 
        {
            action.PerformAction();
        }
    }
}