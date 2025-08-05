using UnityEngine;

public class EditorManager : MonoBehaviour
{
    public static EditorManager Instance;

    [SerializeField] private GridEditorPanel _gridPanel;

    private void Awake()
    {
        if(Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    public void LoadLevelFromGamePlay(int[,] data)
    {

    }
}
