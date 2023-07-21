using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using AssetKits.ParticleImage;
using DG.Tweening;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour
{
    public float money;
    public TextMeshProUGUI moneyText;
    public ParticleImage particleImage;
    public Transform characters, moveObject;
    public GameObject chicken;
    public int chickenCount;
    float moveObjectSpeed;
    float characterSpeed;
    public int chickyCost;
    public TextMeshProUGUI chickyCostText;
    public int speedCost;
    public TextMeshProUGUI speedCostText;
    public bool holding;
    public bool open;
    public GameObject upgradePanel;
    public int offlineIncomeCost;
    public TextMeshProUGUI offlineIncomeCostText;
    public OfflineIncome offlineIncome;

    public TextMeshProUGUI soldObjectCountText;
    public int totalObjectCount,soldObjectCount;
    public float earningMoneyInThisLevel;
    public GameObject winPanel;
    public TextMeshProUGUI earningMoneyInThisLevelText;
    public int index;
    public AudioSource audioS;
    public AudioClip[] voices;
    private void Start()
    {
        index = PlayerPrefs.GetInt("index");
        if(index!= SceneManager.GetActiveScene().buildIndex)
        {
            SceneManager.LoadScene(index);
        }
        chickenCount = PlayerPrefs.GetInt("chickenCount");
        if (chickenCount == 0)
        {
            chickenCount = 3;
            PlayerPrefs.SetInt("chickenCount", chickenCount);
        }
        moveObjectSpeed =PlayerPrefs.GetFloat("moveObjectSpeed");
        if (moveObjectSpeed == 0)
        {
            moveObjectSpeed = 2;
            PlayerPrefs.SetFloat("moveObjectSpeed", moveObjectSpeed);
        }
        characterSpeed = PlayerPrefs.GetFloat("characterSpeed");
        if (characterSpeed == 0)
        {
            characterSpeed = 125;
            PlayerPrefs.SetFloat("characterSpeed", characterSpeed);
        }
        moveObject.GetComponent<MoveObject>().moveSpeed = moveObjectSpeed;

        chickyCost = PlayerPrefs.GetInt("chickyCost");
        if (chickyCost == 0)
        {
            chickyCost = 100;
            PlayerPrefs.SetInt("chickyCost", chickyCost);
        }
        chickyCostText.text = chickyCost.ToString();

        speedCost = PlayerPrefs.GetInt("speedCost");
        if (speedCost == 0)
        {
            speedCost = 150;
            PlayerPrefs.SetInt("speedCost", speedCost);
        }
        speedCostText.text = speedCost.ToString();

        offlineIncomeCost = PlayerPrefs.GetInt("offlineIncomeCost");
        if (offlineIncomeCost == 0)
        {
            offlineIncomeCost = 200;
            PlayerPrefs.SetInt("offlineIncomeCost", offlineIncomeCost);
        }
        offlineIncomeCostText.text = offlineIncomeCost.ToString();

       


        for (int i = 0; i < characters.childCount; i++)
        {
            characters.GetChild(i).GetComponent<ChickenParent>().controller.speed = characterSpeed;
        }
       

        


        for (int i = 0; i < chickenCount; i++)
        {
            characters.GetChild(i).gameObject.SetActive(true);
        }

        for (int i = 0; i < characters.childCount; i++)
        {
            if (characters.GetChild(i).gameObject.activeInHierarchy)
            {
                if (moveObject.GetChild(i).GetComponent<Node>().isEmpty)
                {
                    characters.GetChild(i).GetComponent<ChickenParent>().controller.moveObject = moveObject.GetChild(i);
                    moveObject.GetChild(i).GetComponent<Node>().isEmpty = false;
                }
            }
          
            
        }

        totalObjectCount = FindObjectsOfType<Collectable>().Length;


        SetSoldObjectCount(0);
    }
  
 
    public void SetSoldObjectCount(int value)
    {
        soldObjectCount += value;
        soldObjectCountText.text = soldObjectCount + "/" + totalObjectCount;
        if (soldObjectCount == totalObjectCount)
        {
            LevelDone();
        }
    }
    public void NextLevelButton()
    {
        if (SceneManager.GetActiveScene().buildIndex == SceneManager.sceneCountInBuildSettings - 1)
        {
            index = 1;
            PlayerPrefs.SetInt("index", index);
            SceneManager.LoadScene(1);
        }
        else
        {
            index += 1;
            PlayerPrefs.SetInt("index", index);
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+1);
        }
      
    }
    public void LevelDone()
    {
        winPanel.SetActive(true);
        earningMoneyInThisLevelText.text = earningMoneyInThisLevel.ToString();
        if (FindObjectOfType<Tutorial>() !=null)
        {
            var tutorial = FindObjectOfType<Tutorial>();
            tutorial.gameObject.SetActive(false);
        }
    }
    public void EarnMoney(float value)
    {
        money += value;
        PlayerPrefs.SetFloat("money", money);
        moneyText.text = money.ToString();
        if (value > 0)
        {
            earningMoneyInThisLevel += value;
        }
        
    }
   public void ChickyUpgradeButton()
    {
        if (money >= chickyCost)
        {
            EarnMoney(-chickyCost);
            chickyCost += chickyCost*2;
            PlayerPrefs.SetInt("chickyCost", chickyCost);
            chickyCostText.text = chickyCost.ToString();
            SpawnCharacter();
        }
    }

    public void SpeedUpgradeButton()
    {
        if (money >= speedCost)
        {
            EarnMoney(-speedCost);
            speedCost += speedCost*2;
            PlayerPrefs.SetInt("speedCost", speedCost);
            speedCostText.text = speedCost.ToString();
            UpgradeSpeed();
        }
    }
    void UpgradeSpeed()
    {
        moveObject.GetComponent<MoveObject>().moveSpeed += moveObject.GetComponent<MoveObject>().moveSpeed / 10;
        PlayerPrefs.SetFloat("moveObjectSpeed", moveObject.GetComponent<MoveObject>().moveSpeed);
        for (int i = 0; i < characters.childCount; i++)
        {
            characters.GetChild(i).GetComponent<ChickenParent>().controller.speed += characters.GetChild(i).GetComponent<ChickenParent>().controller.speed / 10;
        }
        PlayerPrefs.SetFloat("characterSpeed", characters.GetChild(0).GetComponent<ChickenParent>().controller.speed);
    }

    public void OfflineIncomeButton()
    {
        if (money >= offlineIncomeCost)
        {
            EarnMoney(-offlineIncomeCost);
            offlineIncomeCost += offlineIncomeCost*2;
            PlayerPrefs.SetInt("offlineIncomeCost", offlineIncomeCost);
            offlineIncomeCostText.text = offlineIncomeCost.ToString();
            offlineIncome.offlineIncome += 1;
            PlayerPrefs.SetInt("offlineIncome", offlineIncome.offlineIncome);
        }
    }
    void SpawnCharacter()
    {
        for (int j = 0; j < characters.childCount; j++)
        {
            if (!characters.GetChild(j).gameObject.activeInHierarchy)
            {
                var newChicky = characters.GetChild(j).gameObject;
                newChicky.SetActive(true);
                chickenCount += 1;
                PlayerPrefs.SetInt("chickenCount", chickenCount);
                for (int i = 0; i < moveObject.childCount; i++)
                {
                    if (moveObject.GetChild(i).GetComponent<Node>().isEmpty)
                    {

                        newChicky.transform.parent = characters;
                        newChicky.transform.position = moveObject.GetChild(i).position;
                        newChicky.GetComponent<ChickenParent>().controller.moveObject = moveObject.GetChild(i);
                        moveObject.GetChild(i).GetComponent<Node>().isEmpty = false;
                        return;
                    }
                }
            }
        }
    }
    public void CloseUpgradePanel()
    {
        upgradePanel.SetActive(false);
    }
}
