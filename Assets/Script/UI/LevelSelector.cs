using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LevelSelector : MonoBehaviour
{
    [SerializeField] private Button backBtn;
    [SerializeField] private Button buttonTemplate;
    [SerializeField] private Transform container;

    private void Start()
    {
        for(int i = 1; i <= 100; i++)
        {
            int level = i;
            Button btn = Instantiate(buttonTemplate, container);
            btn?.gameObject.SetActive(true);
            btn?.GetComponentInChildren<TextMeshProUGUI>()?.SetText(i.ToString());
            btn.onClick.AddListener(() =>
            {
                GameManager.Instance.LoadLevel(level);
                gameObject.SetActive(false);
            });
        }    
        gameObject.SetActive(false);
    }

     

    public void ActiveLevelSeclector(GameObject caller)
    {
        gameObject.SetActive(true);
        backBtn.onClick.RemoveAllListeners();
        backBtn.onClick.AddListener(() =>
        {
            gameObject.SetActive(false);
            caller?.gameObject.SetActive(true);
        });
    }    

}
