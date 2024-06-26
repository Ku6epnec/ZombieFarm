using CozyServer.DTS.Links;
using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityTools.Runtime.Links;
using ZombieFarm.Managers.Interfaces;

namespace ZombieFarm.Managers
{
    public class ConfigManager : MonoBehaviour, IConfigManager
    {
        public GameSettings GameSettings => gameSettings;

        [SerializeField] private GameSettings gameSettings;

        private readonly Dictionary<Type, Dictionary<string, object>> cache = new Dictionary<Type, Dictionary<string, object>>();

        private void GetListOfAssets<T>(List<string> buffer)
        {
            string pathToSearch = Path.Combine(Application.dataPath, LinkBase.GetResourcesPathForAsset<T>());

            buffer.Clear();

            buffer.AddRange(Directory.GetFiles(pathToSearch, "*.asset", SearchOption.AllDirectories));

            for (int i = buffer.Count - 1; i >= 0; i--)
            {
                buffer[i] =
                    Path.Combine(Path.GetDirectoryName(buffer[i]), Path.GetFileNameWithoutExtension(buffer[i])) // get same path without extension
                        .Substring(pathToSearch.Length + 1) // remove path to config root && 1 separator
                        .Replace(Path.DirectorySeparatorChar, '/')
                        .Replace(Path.AltDirectorySeparatorChar, '/'); // replace all kind of path separators to unity-style path separator
            }
        }

        public T GetByLink<T>(ILink link) where T : UnityEngine.Object
        {
            if (cache.TryGetValue(typeof(T), out Dictionary<string, object> typedCache) == false)
            {
                typedCache = new Dictionary<string, object>();
                cache.Add(typeof(T), typedCache);
            }

            if (typedCache.TryGetValue(link.LinkedObjectId, out object cachedObject) == false)
            {
                cachedObject = Resources.Load<T>(Path.Combine(GetPathForAssetInsideResources<T>(), link.LinkedObjectId));
                typedCache.Add(link.LinkedObjectId, cachedObject);
            }

            return cachedObject as T;

        }

        public static string GetPathForAssetInsideResources<T>()
        {
            return Path.Combine("LinkTargets", typeof(T).Name).Replace(@"\", @"/");
        }
    }
}