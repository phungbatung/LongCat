using UnityEngine;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    private GameUI gameUI;
    [SerializeField] private Button pause;
    [SerializeField] private Button resume;
    [SerializeField] private Button restart;
    [SerializeField] private Button menu; //aka level selector

    [SerializeField] private Transform buttonContainer;


    private void Awake()
    {
        gameUI = GetComponentInParent<GameUI>();
    }

    void Start()
    {
        pause.onClick.AddListener(ClickBtnPause);
        resume.onClick.AddListener(ClickBtnResume);
        restart.onClick.AddListener(ClickBtnRestart);
        menu.onClick.AddListener(ClickBtnMenu);
    }

    public void ClickBtnPause()
    {
        GameManager.Instance.SetState(GameState.Pause);

    }
    
    public void ClickBtnResume()
    {

    }
    
    public void ClickBtnRestart()
    {

    }

        
    public void ClickBtnMenu()
    {

    }


}
