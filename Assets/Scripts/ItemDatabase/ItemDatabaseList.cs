using System.Collections.Generic;
using UnityEditor;

namespace GameProg2.Items
{
    public static class ItemDatabaseList
    {
        static List<ItemData> _itemList = new();
        public static List<ItemData> List
        {
            get { return _itemList; }
        }

        static ItemDatabaseList()
        {
            PopulateList();
        }

        static void PopulateList()
        {
            string[] assetNames = AssetDatabase.FindAssets("t:ItemData", new[] { "Assets/ItemDatabase/ItemData" });
            _itemList.Clear();
            foreach (string SOName in assetNames)
            {
                var SOpath = AssetDatabase.GUIDToAssetPath(SOName);
                var itemData = AssetDatabase.LoadAssetAtPath<ItemData>(SOpath);
                _itemList.Add(itemData);
            }
        }
    }
}
