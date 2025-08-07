
using UnityEngine;

namespace M.Packages.FileBrowser
{
   public static class PlatformHelper
   {
#if UNITY_EDITOR
       private static bool _isIl2CPPSet;
       
       [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.SubsystemRegistration)]
       private static void Init()
       {
           Debug.Log("Counter reset: " + _isIl2CPPSet);
           _isIl2CPPSet = false;   
       }
#endif
       
       /// <summary>Checks if the current build target uses IL2CPP.</summary>
       /// <returns>True if the current build target uses IL2CPP.</returns>
       public static bool IsIL2CPP
       {
           get
           {
               bool isIl2CPP = false;
#if UNITY_EDITOR
               if (!_isIl2CPPSet) //prevent problems if called from threads
               {
                   _isIl2CPPSet = true;

                   UnityEditor.BuildTarget target = UnityEditor.EditorUserBuildSettings.activeBuildTarget;
                   UnityEditor.BuildTargetGroup group = UnityEditor.BuildPipeline.GetBuildTargetGroup(target);
#if UNITY_2023_1_OR_NEWER
                   UnityEditor.Build.NamedBuildTarget nbt = UnityEditor.Build.NamedBuildTarget.FromBuildTargetGroup(group);
                   isIl2CPP = UnityEditor.PlayerSettings.GetScriptingBackend(nbt) == UnityEditor.ScriptingImplementation.IL2CPP;
#else
                   isIl2CPP = UnityEditor.PlayerSettings.GetScriptingBackend(group) == UnityEditor.ScriptingImplementation.IL2CPP;
#endif
               }

               return isIl2CPP;
#else
#if ENABLE_IL2CPP
            return true;
#else
            return false;
#endif
#endif
           }
       }
       
       /// <summary>Checks if we are inside the macOS Editor.</summary>
       /// <returns>True if we are inside the macOS Editor.</returns>
       public static bool IsMacOSEditor
       {
           get
           {
#if UNITY_EDITOR_OSX
            return true;
#else
               return false;
#endif
           }
       }
       
       /// <summary>Checks if the current platform is OSX.</summary>
       /// <returns>True if the current platform is OSX.</returns>
      public static bool IsMacOSPlatform
      {
         get
         {
#if UNITY_STANDALONE_OSX
            return true;
#else
            return false;
#endif
         }
      }
      
      /// <summary>Checks if the current platform is Windows.</summary>
      /// <returns>True if the current platform is Windows.</returns>
      public static bool IsWindowsPlatform
      {
          get
          {
#if UNITY_STANDALONE_WIN
              return true;
#else
            return false;
#endif
          }
      }
      
      private static readonly System.Random _rnd = new System.Random();
      /// <summary>Creates a string of characters with a given length.</summary>
      /// <param name="generateChars">Characters to generate the string (if more than one character is used, the generated string will be a randomized result of all characters)</param>
      /// <param name="stringLength">Length of the generated string</param>
      /// <returns>Generated string</returns>
      public static string CreateString(string generateChars, int stringLength)
      {
          if (generateChars != null)
          {
              if (generateChars.Length > 1)
              {
                  char[] chars = new char[stringLength];

                  for (int ii = 0; ii < stringLength; ii++)
                  {
                      chars[ii] = generateChars[_rnd.Next(0, generateChars.Length)];
                  }

                  return new string(chars);
              }

              return generateChars.Length == 1 ? new string(generateChars[0], stringLength) : string.Empty;
          }

          return string.Empty;
      }
   }
}