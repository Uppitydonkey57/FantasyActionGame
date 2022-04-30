using UnityEngine;
using UnityEngine.UI;
using Yarn.Unity;

public class Menu : MonoBehaviour 
{
    [SerializeField] private HealthManager manager;
    [SerializeField] private PotionManager potionManager;
    [SerializeField] private SaveManager saveManager;

    public GameObject[] hearts;
    public GameObject[] potions;

    public Color potionGrey;

    [SerializeField] private Animator animator;

    private SceneChange reload;

    [SerializeField] private GameObject healthGUI;

    [SerializeField] private DialogueRunner runner;

    private void Start() 
    {
        //reload.

        manager.healthChanged.AddListener(UpdateHealth);
        potionManager.potionsChanged.AddListener(UpdatePotions);
        manager.healthChanged.AddListener(OnPlayerDeath);

        UpdateHealth(manager.Hp);
        UpdatePotions(potionManager.Potions);

        reload = new SceneChange();
        reload.switchMode = SceneChange.SwitchMode.AddBuildOrder;
        reload.buildNumber = 0;
    }

    void UpdateHealth(float health) 
    {
        Debug.Log($"Player Health Is {health}");

        for (int i = 0; i < hearts.Length; i++) 
        {
            if (i < health)
                hearts[i].SetActive(true);
            else
                hearts[i].SetActive(false);
        }
    }

    private void Update() 
    {
        if (runner.IsDialogueRunning) 
        {
            healthGUI.SetActive(false);
        } else 
        {
            healthGUI.SetActive(true);
        }
    }

    void UpdatePotions(int amount) 
    {
        Debug.Log($"You Have {amount} Potions");

        for (int i = 0; i < potions.Length; i++) 
        {
            
            if (i < amount)
                potions[i].GetComponent<Image>().color = Color.white;
            else
                potions[i].GetComponent<Image>().color = potionGrey;
        }
    }

    void OnPlayerDeath(float health) 
    {
        if (health <= 0) 
        {
            animator.SetTrigger("Dead");
        }
    }

    public void CheckPoint() 
    {
        reload.ChangeScene();
    }
}