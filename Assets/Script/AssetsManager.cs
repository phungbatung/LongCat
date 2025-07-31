using NUnit.Framework;
using System;
using System.Collections.Generic;
using UnityEngine;

public class AssetsManager : MonoBehaviour
{
    public static AssetsManager Instance { get; private set; }

    [SerializeField] private List<BlockData> blockData;

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

    public GameObject GetBlockByType(BlockType type)
    {
        return GetBlockByKey(type.ToString());
    }

    [Serializable]    
    public class BlockData
    {
        public string key;
        public GameObject prefab;
    }

    
}
