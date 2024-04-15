using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class Info : MonoBehaviour
{
    public int Health;
    public int Kills;
    public int KillsHS;
    public int Difficulty;
    public int Combo;
    public int MaxCombo;

    [SerializeField] private bool IgnoreDifficulty;

    [SerializeField] private TMP_Text KillsTextLabel;
    [SerializeField] private TMP_Text ComboTextLabel;
    [SerializeField] private GameObject HealthIconPrefab;
    [SerializeField] private Transform HealthBarTF;

    private List<GameObject> HealthIconList = new List<GameObject>();
    private DataController dataController;


    private void Awake()
    {
        dataController = GameObject.Find("DataController").GetComponent<DataController>();
        Difficulty = dataController.DifficultyVal;
    }

    private void Start()
    {
        Health = IgnoreDifficulty ? Health : (5 - Difficulty * 2);
        HealthIconList.Add(HealthIconPrefab); //first prefab

        dataController.Combo = 0;
        dataController.Kills = 0;
            
        UpdateCombo();
        UpdateHealth();
    }

    private void UpdateHealth()
    {
        for (int i = 0; i < Health; i++)
        {
            if (HealthIconList.Count < i + 1)
            {
                GameObject HealthIconClone = Instantiate(HealthIconPrefab, HealthBarTF);
                RectTransform rectTransform = HealthIconClone.GetComponent<RectTransform>();
                rectTransform.anchoredPosition += new Vector2(i * 100, 0);
                HealthIconList.Add(HealthIconClone);
            }
        }   

        for (int i = 0; i < HealthIconList.Count; i++)
        {
            if (i >= Health)
            {
                HealthIconList[i].SetActive(false);
            }
            else
            {
                HealthIconList[i].SetActive(true);
            }
        }
    }

    public void UpdateCombo()
    {
        if(Combo > MaxCombo)
        {
            MaxCombo = Combo;
            dataController.Combo = MaxCombo;
        }
        if(Combo > 0)
        {
            ComboTextLabel.text = "Combo X"+Combo;
        }
        else
        {
            ComboTextLabel.text = "";
        }
    }

    public void Kill()
    {
        Kills++;
        dataController.Kills = Kills;
        KillsTextLabel.text = Kills.ToString();
        if(Kills > KillsHS)
        {
            KillsHS = Kills;
            dataController.KillsHighScore = KillsHS;
        }
    }

    public void TakeDamage()
    {
        Health -= 1;
        UpdateHealth();
        if(Health <= 0)
        {
            dataController.Defeat = true;
            SceneManager.LoadScene(0);
        }
    }
}
