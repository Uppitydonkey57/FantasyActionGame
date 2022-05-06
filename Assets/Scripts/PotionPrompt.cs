using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class PotionPrompt : MonoBehaviour
{
    [SerializeField] private PotionManager potionManager;
    [SerializeField] private HealthManager healthManager;

    [SerializeField] private CanvasGroup group;
    
    public int threshold = 1;

    bool isShowing;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (healthManager.Hp <= threshold && potionManager.Potions > 0 && !isShowing) 
        {
            isShowing = true;
            group.DOFade(1, 0.15f);
        } else if (isShowing) 
        {
            isShowing = false;
            group.DOFade(0, 0.15f);
        }
    }
}
