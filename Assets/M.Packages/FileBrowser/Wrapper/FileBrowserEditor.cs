#if UNITY_EDITOR
using System;
using UnityEditor;
using UnityEngine;

namespace M.Packages.FileBrowser.Wrapper
{
   public class FileBrowserEditor : BaseFileBrowser
   {
      #region Implemented methods
      public override string[] OpenFiles(string title, string directory, string defaultName, bool multiselect, params ExtensionFilter[] extensions)
      {
         if (PlatformHelper.IsMacOSEditor && extensions?.Length > 1)
            Debug.LogWarning("Multiple 'extensions' are not supported in the Editor.");

         if (multiselect)
            Debug.LogWarning("'multiselect' for files is not supported in the Editor.");

         if (!string.IsNullOrEmpty(defaultName))
            Debug.LogWarning("'defaultName' is not supported in the Editor.");

         //resetOpenFiles();

         var path = extensions == null ? EditorUtility.OpenFilePanel(title, directory, string.Empty) : EditorUtility.OpenFilePanelWithFilters(title, directory, GetFilterFromFileExtensionList(extensions));

         if (string.IsNullOrEmpty(path))
         {
            CurrentOpenFiles = System.Array.Empty<string>();
            CurrentOpenSingleFile = string.Empty;

            return null;
         }

         // CurrentOpenSingleFile = Crosstales.Common.Util.FileHelper.ValidateFile(path);
         CurrentOpenSingleFile = path;
         CurrentOpenFiles = new[] { CurrentOpenSingleFile };

         return CurrentOpenFiles;
      }

      public override string[] OpenFolders(string title, string directory, bool multiselect)
      {
         if (multiselect)
            Debug.LogWarning("'multiselect' for folders is not supported in the Editor.");

         //resetOpenFolders();

         string path = EditorUtility.OpenFolderPanel(title, directory, string.Empty);

         if (string.IsNullOrEmpty(path))
         {
            CurrentOpenFolders = System.Array.Empty<string>();
            CurrentOpenSingleFolder = string.Empty;

            return null;
         }

         // CurrentOpenSingleFolder = Crosstales.Common.Util.FileHelper.ValidatePath(path);
         CurrentOpenSingleFolder = path;
         CurrentOpenFolders = new[] { CurrentOpenSingleFolder };

         return CurrentOpenFolders;
      }

      public override string SaveFile(string title, string directory, string defaultName, params ExtensionFilter[] extensions)
      {
         //resetSaveFile();

         string ext = extensions?.Length > 0 ? extensions[0].Extensions[0].Equals("*") ? string.Empty : extensions[0].Extensions[0] : string.Empty;
         string name = string.IsNullOrEmpty(ext) ? defaultName : $"{defaultName}.{ext}";

         if (extensions?.Length > 1)
            Debug.LogWarning($"Multiple 'extensions' are not supported in the Editor! Using only the first entry '{ext}'.");

         string path = EditorUtility.SaveFilePanel(title, directory, name, ext);

         if (string.IsNullOrEmpty(path))
         {
            CurrentSaveFile = string.Empty;

            return null;
         }

         // CurrentSaveFile = Crosstales.Common.Util.FileHelper.ValidateFile(path);
         CurrentSaveFile = path;

         return CurrentSaveFile;
      }
      #endregion


      #region Private methods

      private static string[] GetFilterFromFileExtensionList(ExtensionFilter[] extensions)
      {
         if (extensions?.Length > 0)
         {
            string[] filters = new string[extensions.Length * 2];

            for (int ii = 0; ii < extensions.Length; ii++)
            {
               filters[ii * 2] = extensions[ii].Name;
               filters[ii * 2 + 1] = string.Join(",", extensions[ii].Extensions);
            }

            Debug.Log($"getFilterFromFileExtensionList: {filters.CtDump()}");

            return filters;
         }

         return Array.Empty<string>();
      }

      #endregion
   }
}
#endif