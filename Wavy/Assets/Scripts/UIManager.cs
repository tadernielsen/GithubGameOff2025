using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public GameObject pauseMenuUI;
    public GameObject goalUI;
    public GameObject playerUI;

    [Header("Main Game UI")]
    public GameObject dropsBar;
    public GameObject healthBar;
    public GameObject itemBar;

    private TextMeshProUGUI dropsValue;
    private TextMeshProUGUI parValue;
    private TextMeshProUGUI healthValue;
    private TextMeshProUGUI objectName;

    void Awake()
    {
        dropsValue = dropsBar.transform.Find("Drops Number").GetComponent<TextMeshProUGUI>();
        parValue = dropsBar.transform.Find("Par Number").GetComponent<TextMeshProUGUI>();
        healthValue = healthBar.transform.Find("Health Number").GetComponent<TextMeshProUGUI>();
        objectName = itemBar.transform.Find("Item").GetComponent<TextMeshProUGUI>();
    }

    public void UpdateDrops(int drops = 0)
    {
        dropsValue.text = drops.ToString();
    }

    public void UpdatePar(int par)
    {
        parValue.text = par.ToString();
    }

    public void UpdateHealth(int health)
    {
        healthValue.text = health.ToString();
    }

    public void UpdateObject(string itemName)
    {
        objectName.text = itemName;
    }

    public void TogglePauseMenu()
    {
        pauseMenuUI.SetActive(!pauseMenuUI.activeSelf);

        if (pauseMenuUI.activeSelf)
        {
            Time.timeScale = 0f;
        }
        else
        {
            Time.timeScale = 1f;
        }
    }

    public void OpenGoalUI(int drops = 0, int par = 0)
    {
        goalUI.SetActive(true);
        goalUI.transform.Find("Drop amount").GetComponent<TextMeshProUGUI>().text = "Drops: " + drops.ToString();
        goalUI.transform.Find("Par amount").GetComponent<TextMeshProUGUI>().text = "Par: " + par.ToString();
        // Time
    }

    public void CloseGoalUI()
    {
        goalUI.SetActive(false);
    }

    public void TogglePlayerUI()
    {
        playerUI.SetActive(!playerUI.activeSelf);
    }
}
