using UnityEngine;

namespace M.Packages.FileBrowser.Wrapper
{
   /// <summary>Base class for all file browsers.</summary>
   public abstract class BaseFileBrowser
   {
      public virtual string CurrentOpenSingleFile { get; set; }
      public virtual string[] CurrentOpenFiles { get; set; }
      public virtual string CurrentOpenSingleFolder { get; set; }
      public virtual string[] CurrentOpenFolders { get; set; }
      public virtual string CurrentSaveFile { get; set; }


      public virtual byte[] CurrentSaveFileData { get; set; }

      /// <summary>Open native file browser for a single file.</summary>
      /// <param name="title">Dialog title</param>
      /// <param name="directory">Root directory</param>
      /// <param name="defaultName">Default file name (currently only supported under Windows standalone)</param>
      /// <param name="extensions">List of extension filters. Filter Example: new ExtensionFilter("Image Files", "jpg", "png")</param>
      /// <returns>Returns a string of the chosen file. Null when cancelled</returns>
      public string OpenSingleFile(string title, string directory, string defaultName, params ExtensionFilter[] extensions)
      {
         string[] files = OpenFiles(title, directory, defaultName, false, extensions);
         string file = files?.Length > 0 ? files[0] : string.Empty;

         return file;
      }

      /// <summary>Open native file browser for multiple files.</summary>
      /// <param name="title">Dialog title</param>
      /// <param name="directory">Root directory</param>
      /// <param name="defaultName">Default file name (currently only supported under Windows standalone)</param>
      /// <param name="multiselect">Allow multiple file selection</param>
      /// <param name="extensions">List of extension filters. Filter Example: new ExtensionFilter("Image Files", "jpg", "png")</param>
      /// <returns>Returns array of chosen files. Null when cancelled</returns>
      public abstract string[] OpenFiles(string title, string directory, string defaultName, bool multiselect, params ExtensionFilter[] extensions);

      /// <summary>Open native folder browser for a single folder.</summary>
      /// <param name="title">Dialog title</param>
      /// <param name="directory">Root directory</param>
      /// <returns>Returns a string of the chosen folder. Null when cancelled</returns>
      public string OpenSingleFolder(string title, string directory)
      {
         string[] folders = OpenFolders(title, directory, false);
         return folders?.Length > 0 ? folders[0] : string.Empty;
      }

      /// <summary>Open native folder browser for multiple folders.</summary>
      /// <param name="title">Dialog title</param>
      /// <param name="directory">Root directory</param>
      /// <param name="multiselect">Allow multiple folder selection</param>
      /// <returns>Returns array of chosen folders. Null when cancelled</returns>
      public abstract string[] OpenFolders(string title, string directory, bool multiselect);

      /// <summary>Open native save file browser.</summary>
      /// <param name="title">Dialog title</param>
      /// <param name="directory">Root directory</param>
      /// <param name="defaultName">Default file name</param>
      /// <param name="extensions">List of extension filters. Filter Example: new ExtensionFilter("Image Files", "jpg", "png")</param>
      /// <returns>Returns chosen file. Null when cancelled</returns>
      public abstract string SaveFile(string title, string directory, string defaultName, params ExtensionFilter[] extensions);

      #region Protected methods
/*
      protected void resetOpenFiles(params string[] paths)
      {
         CurrentOpenFiles = System.Array.Empty<string>();
         CurrentOpenSingleFile = string.Empty;
      }

      protected void resetOpenFolders(params string[] paths)
      {
         CurrentOpenFolders = System.Array.Empty<string>();
         CurrentOpenSingleFolder = string.Empty;
      }

      protected void resetSaveFile(params string[] paths)
      {
         CurrentSaveFile = string.Empty;
      }
*/
      #endregion
   }
}