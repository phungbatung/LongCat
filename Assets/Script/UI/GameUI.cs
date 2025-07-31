using UnityEngine;
using UnityEngine.UI;

public class GameUI : MonoBehaviour
{
    [SerializeField] private Transform WinPanel;
    [SerializeField] private Transform LosePanel;

    private void Start()
    {
        WinPanel.GetComponentInChildren<Button>().onClick.AddListener(()=> 
        { 
            GameManager.Instance.LoadNextLevel();
            WinPanel.gameObject.SetActive(false);
        });
        LosePanel.GetComponentInChildren<Button>().onClick.AddListener(() =>
        {
            GameManager.Instance.ReloadLevel();
            LosePanel.gameObject.SetActive(false);
        });
        GameManager.Instance.OnWin += () => WinPanel.gameObject.SetActive(true);
        GameManager.Instance.OnLose += () => LosePanel.gameObject.SetActive(true);
    }
}
