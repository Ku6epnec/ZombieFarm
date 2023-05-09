
using ZombieFarm.Config.LinkTargets;
using UnityEditor;
using UnityEngine;
using UnityTools.Editor.Links;
using ZombieFarm.Config.Links;

namespace ZombieFarm.Editor.Links
{
    [CustomPropertyDrawer(typeof(LinkToItem), true)]
    public class LinkToItemPropertyDrawer : LinkToConfigPropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            DrawLink<Item>(position, property, label, nameof(LinkToItem.itemId));
        }
    }
}
