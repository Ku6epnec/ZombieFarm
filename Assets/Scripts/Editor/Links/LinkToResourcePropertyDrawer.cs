using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityTools.Editor.Links;
using ZombieFarm.Config.Links;
using ZombieFarm.Config.LinkTargets;

namespace ZombieFarm.Editor.Links
{
    [CustomPropertyDrawer(typeof(LinkToResource), true)]
    public class LinkToResourcePropertyDrawer : LinkToConfigPropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            DrawLink<Resource>(position, property, label, nameof(LinkToResource.itemId));
        }
    }
}
