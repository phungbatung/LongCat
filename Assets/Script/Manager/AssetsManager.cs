using NUnit.Framework;
using System;
using System.Collections.Generic;
using UnityEngine;

public class AssetsManager : MonoBehaviour
{
    public static AssetsManager Instance { get; private set; }

    [SerializeField] private List<BlockData> blockData;
    [SerializeField] private List<ColorData> colorData;

    private void Awake()
    {
        if(Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    public GameObject GetBlockByKey(string key)
    {
        for(int i = 0; i< blockData.Count; i++)
        {
            if (blockData[i].key == key)
                return blockData[i].prefab;
        }
        Debug.LogError($"Prefab with key '{key}' not found!!!!");
        return default;
    }

    public GameObject GetBlockByType(CellType type)
    {
        return GetBlockByKey(type.ToString());
    }

    public Color GetColorByKey(string key)
    {
        for (int i = 0; i < colorData.Count; i++)
        {
            if (colorData[i].key == key)
                return colorData[i].color;
        }
        Debug.LogError($"Color with key '{key}' not found!!!!");
        return default;
    }

    [Serializable]    
    public class BlockData
    {
        public string key;
        public GameObject prefab;
    }
    
    [Serializable]    
    public class ColorData
    {
        public string key;
        public Color color;
    }

    
}
