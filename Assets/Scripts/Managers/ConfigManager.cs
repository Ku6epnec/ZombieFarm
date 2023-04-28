using CozyServer.DTS.Links;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;
using UnityTools.Runtime.Links;
using ZombieFarm.Config.LinkTargets;

public class ConfigManager : MonoBehaviour
{
    Dictionary<ILink, ScriptableObject> items;

    public void Initialize()
    {
        List<string> buffer = new List<string>();
        GetListOfAssets<Item>(buffer);

        foreach (string asset in buffer)
        {
            Item item = AssetDatabase.LoadAssetAtPath<Item>(asset);
            LinkToItem linkToItem = new LinkToItem();
            linkToItem.itemId = asset;
            items.Add(linkToItem, item);
        }
    }


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
}
