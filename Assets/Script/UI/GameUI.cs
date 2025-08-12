using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameUI : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private TextMeshProUGUI levelText;
    [SerializeField] private LevelSelector levelSelector;
    [SerializeField] private Transform WinPanel;
    [SerializeField] private Transform LosePanel;

    [Header("Effect")]
    [SerializeField] private CaptureEffect captureWinScreenController;
    [SerializeField] private FadeController fadeController;
    [SerializeField] private ParticleSystem leftPartical;
    [SerializeField] private ParticleSystem rightPartical;

    [Header("Ads")]
    [SerializeField] private RewardAds rewardAds;
    private int loseCount=0;
    private void Start()
    {
        WinPanel.GetComponentInChildren<Button>().onClick.AddListener(()=> 
        {
            rewardAds.ShowAd();
            fadeController.FadeInOut(ClickBtnNextLevel);
            
        });
        LosePanel.GetComponentInChildren<Button>().onClick.AddListener(() =>
        {
            loseCount++;
            if(loseCount>=3)
            {
                rewardAds.ShowAd();
                loseCount = 0;
            }
            fadeController.FadeInOut(ClickBtnRetry);
        });
        GameManager.Instance.OnWin += OnWinGame;
        GameManager.Instance.OnLose += OnLoseGame;
        GameManager.Instance.OnLoadLevel += OnLoadLevel;
    }

    public void OnWinGame()
    {
        Action callback = ShowListWinUI;
        captureWinScreenController.PlayCaptureAnimation(callback);
        
    }  
    
    public void OnLoseGame()
    {
        Action callback = ShowListLoseUI;
        captureWinScreenController.PlayCaptureAnimation(callback);
    }

    public void OnLoadLevel(int level)
    {
        levelText.text = $"Level {level}";
    }

    public void ShowListWinUI()
    {
        WinPanel.gameObject.SetActive(true);
        rightPartical.gameObject.SetActive(true);
        leftPartical.gameObject.SetActive(true);
        rightPartical.Play();
        leftPartical.Play();
    }

    public void ShowListLoseUI()
    {
        LosePanel.gameObject.SetActive(true);
    }

    public void ClickBtnNextLevel()
    {
        GameManager.Instance.LoadNextLevel();
        WinPanel.gameObject.SetActive(false);
        captureWinScreenController.gameObject.SetActive(false);
    }public void ClickBtnRetry()
    {
        GameManager.Instance.ReloadLevel();
        LosePanel.gameObject.SetActive(false);
        captureWinScreenController.gameObject.SetActive(false);
    }
}
