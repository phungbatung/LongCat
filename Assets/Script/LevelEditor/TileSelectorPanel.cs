using UnityEngine;
using UnityEngine.UI;

public class TileSelectorPanel : MonoBehaviour
{

    private CellType _selectedType;
    private bool _selected;
    private bool _onPaintMode;

    public Button Obstacle;
    public Button Head;
    public Button Empty;
    public GameObject sampleTile;

    private void Awake()
    {
        Obstacle.onClick.AddListener(SelectObstacle);
        Head.onClick.AddListener(SelectHead);
        Empty.onClick.AddListener(SelectEmpty);
    }

    private void Update()
    {
        if(_selected)
        {
            sampleTile.transform.position = Input.mousePosition;
        }    

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            SelectObstacle();
        }  
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            SelectHead();
        }  
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            SelectEmpty();
        } 
        if(Input.GetKeyDown(KeyCode.M))
        {
            CancelSelected();
        }    
        
        if(Input.GetKeyDown(KeyCode.Mouse0))
        {
            _onPaintMode = true;
        }   
        else if (Input.GetKeyUp(KeyCode.Mouse0))
        {
            _onPaintMode= false;
        }    
    }

    public void SelectEmpty()
    {
        _selectedType = CellType.Empty;
        _selected = true;
        sampleTile.SetActive(true);
        sampleTile.GetComponent<Image>().color = AssetsManager.Instance.GetColorByKey(_selectedType.ToString());
    }
    public void SelectHead()
    {
        _selectedType = CellType.Head;
        _selected = true;
        sampleTile.SetActive(true);
        sampleTile.GetComponent<Image>().color = AssetsManager.Instance.GetColorByKey(_selectedType.ToString());
    }
    public void SelectObstacle()
    {
        _selectedType = CellType.Obstacle;
        _selected = true;
        sampleTile.SetActive(true);
        sampleTile.GetComponent<Image>().color = AssetsManager.Instance.GetColorByKey(_selectedType.ToString());
    }

    public void CancelSelected()
    {
        _selected = false;
        sampleTile.SetActive(false);
    }

    public bool GetOnSelected() => _selected;
    public bool GetOnPaintMode() => _selected && _onPaintMode;
    public CellType GetSelectedCellType()
    {
        return _selectedType;
    }
}
