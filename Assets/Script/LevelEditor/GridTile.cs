using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[Serializable]
public class GridTile : MonoBehaviour, IPointerEnterHandler, IPointerDownHandler
{
    private TileSelectorPanel _tileSelectorPanel;
    public int Row;
    public int Col;
    public CellType Type;
    [SerializeField] private Image img;


    public void SetValue(int row, int col, CellType type, TileSelectorPanel tileSelectorPanel)
    {
        img.gameObject.SetActive(true);
        Row = row;
        Col = col;
        _tileSelectorPanel = tileSelectorPanel;
        SetCellType(type);
    }

    public void SetCellType(CellType type)
    {
        Type = type;
        img.color = AssetsManager.Instance.GetColorByKey(type.ToString());
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (_tileSelectorPanel.GetOnSelected())
            SetCellType(_tileSelectorPanel.GetSelectedCellType());
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (_tileSelectorPanel.GetOnPaintMode())
            SetCellType(_tileSelectorPanel.GetSelectedCellType());
    }
}
