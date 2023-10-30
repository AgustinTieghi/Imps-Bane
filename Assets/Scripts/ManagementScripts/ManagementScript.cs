using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using static UnityEngine.ParticleSystem;

public class ManagementScript : MonoBehaviour
{
    //Try to fix this
    [Header("UI Text References")]
    [SerializeField] private TextMeshProUGUI moneyText;
    [SerializeField] private TextMeshProUGUI enemiesLeftText;
    [SerializeField] private TextMeshProUGUI playerHPText;
    [SerializeField] private TextMeshProUGUI turretStatsText;
    [SerializeField] private TextMeshProUGUI shopText;
    [SerializeField] private TextMeshProUGUI setupText;
    [Header("UI Object References")]
    [SerializeField] private GameObject upgradeUI;
    [SerializeField] private GameObject skipUI;
    [SerializeField] private GameObject shopUI;
    [SerializeField] private GameObject warningUI;
    [SerializeField] private GameObject turretStatsUI;
    [SerializeField] private GameObject nextWaveTimer;
    [SerializeField] private GameObject sellUI;
    [SerializeField] private GameObject constructUI;
    [Header("Scene Objects References")]
    [SerializeField] private TurretManager turretManager;
    [SerializeField] private TurretScript turretScript;
    [SerializeField] private Turret turret;
    private string gameSceneName = "GameOver";
    [Header("Player Stats")]
    public int money;
    public int hp;

    public bool paused = false;
    public static ManagementScript instance;
    private void Awake()
    {
        if (instance != null)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
        }
    }

    void Update()
    {
        if (hp <= 0)
        {
            SceneManager.LoadScene(gameSceneName);
        }

        Time.timeScale = paused ? 0 : 1;

        ManageUIItems();

        ManageTurretStatsText();

        CheckActivateUpgradeOffer();

        UpdateUIText();

        ManageShop();
    }
    public void Pause()
    {
        paused = !paused;
    }

    void UpdateUIText()
    {
        turretScript = turretManager.selectedTurretScript;
        playerHPText.text = "Current HP: " + hp;
        moneyText.text = "Money: " + money;
        if(EnemySpawner.instance != null)
        {
            enemiesLeftText.text = "Enemies Left: " + EnemySpawner.instance.enemiesLeft;
            if (EnemySpawner.instance.timer < EnemySpawner.instance.setupTime)
            {
                nextWaveTimer.SetActive(true);
                float seconds = Mathf.FloorToInt(EnemySpawner.instance.timer % 60);

                setupText.text = string.Format("Next Wave In" + ": {0:00}", seconds);
            }
            else
            {
                nextWaveTimer.SetActive(false);
            }
        }    
    }

    void CheckActivateUpgradeOffer()
    {
        if (turretManager.selectedTurret != null && money >= turretManager.upgradeCost && turretManager.selectedTurretScript.upgradable)
        {
            upgradeUI.SetActive(true);
        }
        else
        {
            upgradeUI.SetActive(false);
        }
    }
    void ManageShop()
    {
        shopUI.SetActive(turretManager.constructMode);

        if (turretManager.index == 0)
        {
            shopText.text = "Selected: \n" + "Cannon Turret\n" + "$250";
        }
        else if (turretManager.index == 1)
        {
            shopText.text = "Selected: \n" + "Bomb Turret\n" + "$350";
        }
        else
        {
            shopText.text = "Selected: \n" + "Ice Turret\n" + "$300";
        }
    }
    void ManageTurretStatsText()
    {
        if (turretScript != null)
        {
            turretStatsUI.SetActive(true);
            turretStatsText.text = "Turret Stats: \n" +
                "Damage: " + turretScript.dmg + "\n" +
                "Fire Rate: " + turretScript.fireRate + "\n" +
                "Range: " + turretScript.range + "\n" +
                "Sell Value: " + turretManager.turretCost / 2;
        }
        else if (turretScript == null)
        {
            turretStatsText.text = "";
            turretStatsUI.SetActive(false);
        }
    }

    void ManageUIItems()
    {
        if(EnemySpawner.instance != null)
        {
            skipUI.SetActive(EnemySpawner.instance.inSetupTime);

            warningUI.SetActive(EnemySpawner.instance.inSetupTime);       
        }
        sellUI.SetActive(turretManager.selected);

        constructUI.SetActive(!turretManager.constructMode);
    }
}
