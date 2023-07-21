using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;
public class OfflineIncome : MonoBehaviour
{
    public GameManager gm;
    public TextMeshProUGUI timeText;
    [SerializeField] TextMeshProUGUI offlineMoney;
    float offlineMoneyCount;
    [SerializeField] GameObject offlineWindow;
    public int offlineIncome;
    private void Start()
    {
        gm.money = PlayerPrefs.GetFloat("money");
        offlineIncome = PlayerPrefs.GetInt("offlineIncome");
        if (offlineIncome == 0)
        {
            offlineIncome = 1;
            PlayerPrefs.SetInt("offlineIncome", offlineIncome);
        }
        if (PlayerPrefs.HasKey("LastLogin"))
        {
            offlineWindow.SetActive(true);
            DateTime lastlogin = DateTime.Parse(PlayerPrefs.GetString("LastLogin"));
            TimeSpan ts = DateTime.Now - lastlogin;
            timeText.text = string.Format("{0} Days {1} Hours {2} Minutes {3} Seconds Ago", ts.Days, ts.Hours, ts.Minutes, ts.Seconds);
            offlineMoney.text = "" + (int)ts.TotalMinutes * offlineIncome;
            offlineMoneyCount = (int)ts.TotalMinutes * offlineIncome;
            PlayerPrefs.SetInt("money", PlayerPrefs.GetInt("money") + (int)ts.TotalMinutes * offlineIncome);
        }
        Debug.Log(offlineMoneyCount);
        if (offlineMoneyCount < 1) { offlineWindow.SetActive(false);  }
        gm.EarnMoney(offlineMoneyCount);
    }
    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && offlineWindow.activeInHierarchy)
        {
            offlineWindow.SetActive(false);
        }
    }
    private void OnApplicationQuit()
    {
        PlayerPrefs.SetString("LastLogin", DateTime.Now.ToString());
    }
}
