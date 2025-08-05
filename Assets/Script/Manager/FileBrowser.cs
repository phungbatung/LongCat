using UnityEngine;

public static class FileBrowser
{
    public static string OpenFile(string name, string path, string extension)
    {
#if UNITY_EDITOR
        return UnityEditor.EditorUtility.OpenFilePanel(name, path, extension);
#else
            string filePath = M.Packages.FileBrowser.FileBrowser.OpenFile(extension);
            Debug.LogError($"filepath: {filePath}");
            return filePath;
#endif
        return string.Empty;
    }

    public static string SaveFile(string title, string path, string name, string extension)
    {
#if UNITY_EDITOR
        return UnityEditor.EditorUtility.SaveFilePanel(title, path, name, extension);
#else
            string savePath = M.Packages.FileBrowser.FileBrowser.SaveFile(extension);
            Debug.LogError($"filepath: {savePath}");
            return savePath;
#endif
        return string.Empty;
    }

    public static string OpenFolder(string name, string path)
    {
#if UNITY_EDITOR
        return UnityEditor.EditorUtility.OpenFolderPanel(name, path, "Open Folder");
#endif
        return string.Empty;
    }
}
