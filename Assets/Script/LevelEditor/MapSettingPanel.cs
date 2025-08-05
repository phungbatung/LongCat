using System.IO;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using Newtonsoft.Json;
using UnityEngine.SceneManagement;
using System.Collections;

public class MapSettingPanel : MonoBehaviour
{
    private const string _SAVE_PATH = "Resources";

    public GridEditorPanel gridEditorPanel;
    public TMP_InputField xIpt;
    public TMP_InputField yIpt;
    public Button applyBtn; 
    public Button saveBtn; 
    public Button loadBtn;
    public Button playBtn;

    private string _levelName = string.Empty;


    private void Awake()
    {
        xIpt.text = "7";
        yIpt.text = "7";
        applyBtn.onClick.AddListener(OnApply);
        saveBtn.onClick.AddListener(OnSave);
        loadBtn.onClick.AddListener(OnLoad);
        playBtn.onClick.AddListener(OnPlay);
    }

    public void OnApply()
    {
        int row = int.Parse(xIpt.text);
        int col = int.Parse(yIpt.text);
        gridEditorPanel.SetGridSize(row, col);
    }

    public void OnLoad()
    {
        string folderPath = Path.Combine(Application.dataPath, _SAVE_PATH);
        if (!Directory.Exists(folderPath))
        {
            Directory.CreateDirectory(folderPath);
        }

        var openPath = FileBrowser.OpenFile("Open", folderPath, "txt");
        if (File.Exists(openPath))
        {
            string jsonString = File.ReadAllText(openPath);
            _levelName = Path.GetFileNameWithoutExtension(openPath);

            LoadLevel(jsonString);
        }
        else
        {
            Debug.LogError("File not found: " + openPath);
        }
    }

    public void OnSave()
    {
        SaveFile(GetJson(gridEditorPanel.GetLevelData()));
    }

    public void OnPlay()
    {
        var data = gridEditorPanel.GetLevelData();

        TempDataHandler.Set("CurrentLevelData", data);
        SceneManager.LoadScene("GamePlay");

    }



    private void SaveFile(string textToSave)
    {
        string folderPath = Path.Combine(Application.dataPath, _SAVE_PATH);
        if (!Directory.Exists(folderPath))
        {
            Directory.CreateDirectory(folderPath);
        }

        var savePath = FileBrowser.SaveFile("Save", folderPath, _levelName == string.Empty ? "level" : _levelName,
            "txt");

        try
        {
            File.WriteAllText(savePath, textToSave);
            Debug.Log("Saving Ok: " + savePath);

#if UNITY_EDITOR
            AssetDatabase.Refresh();
#endif
        }
        catch (System.Exception e)
        {
            Debug.LogError("Error saving file: " + e.Message);
        }
    }

    private void LoadLevel(string jsonString)
    {
        gridEditorPanel.LoadLevelData(ConvertFromJson(jsonString));
    }

    public string GetJson(object obj)
    {
        return JsonConvert.SerializeObject(obj);
    }

    public int[,] ConvertFromJson(string jsonText)
    {
        return JsonConvert.DeserializeObject<int[,]>(jsonText);
    }
}
