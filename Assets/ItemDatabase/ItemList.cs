using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Inventory
{
    public class ItemList
    {
        List<ItemData> itemList = new();

        public List<ItemData> List
        {
            get
            {
                if (!itemList.Any()) //If nothing in the list
                {
                    Debug.Log("List was Empty");
                    PopulateList();
                    return itemList;
                }
                else
                {
                    Debug.Log("List was not Empty");
                    return itemList;
                }
            }
        }


        void PopulateList()
        {
            string[] assetNames = AssetDatabase.FindAssets("t:ItemData", new[] { "Assets/ItemDatabase/ItemData" });
            itemList.Clear();
            foreach (string SOName in assetNames)
            {
                var SOpath = AssetDatabase.GUIDToAssetPath(SOName);
                var itemData = AssetDatabase.LoadAssetAtPath<ItemData>(SOpath);
                itemList.Add(itemData);
            }
        }


    }
}
