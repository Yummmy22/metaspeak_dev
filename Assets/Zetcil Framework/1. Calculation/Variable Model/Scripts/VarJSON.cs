using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Zetcil
{

    public class VarJSON : MonoBehaviour
    {

        [Space(10)]
        public bool isEnabled;
        public VarString JSONValue;

        [System.Serializable]
        public class CJSONData
        {
            public string Key;
            public string Value;
        }

        [System.Serializable]
        public class CJSONRoot
        {
            public List<CJSONData> JSONData;
        }

        [Header("JSON Data")]
        public List<CJSONRoot> JSONRoot;

        [Header("Index Status")]
        public int CurrentRootIndex;
        public int CurrentItemIndex;
        public string CurrentKeyword;

        void Update()
        {

        }

        public void ClearJSON()
        {
            JSONRoot.Clear();
        }

        public void AddNewJSONItem(string aKey, string aValue)
        {
            CJSONData temp = new CJSONData();
            temp.Key = aKey;
            temp.Value = aValue;
            JSONRoot[CurrentRootIndex].JSONData.Add(temp);
        }

        public void AddNewJSONRoot()
        {
            CJSONRoot temp = new CJSONRoot();
            JSONRoot.Add(temp);
            CurrentRootIndex = JSONRoot.Count - 1;
            JSONRoot[CurrentRootIndex].JSONData = new List<CJSONData>();
        }

        public void AddJSONDataRow(string aKey, string aValue)
        {
            CJSONRoot temp = new CJSONRoot();
            JSONRoot.Add(temp);
            CurrentRootIndex = JSONRoot.Count - 1;
            JSONRoot[CurrentRootIndex].JSONData = new List<CJSONData>();
            AddNewJSONItem(aKey, aValue);
        }

        public void SetCurrentRootIndex(int aRoot)
        {
            CurrentRootIndex = aRoot;
        }

        public void SetCurrentItemIndex(int aItem)
        {
            CurrentItemIndex = aItem;
        }

        public void SetCurrentKeyword(string aWord)
        {
            CurrentKeyword = aWord;
        }

        public void GetJSONDataByCurrentIndex()
        {
            JSONValue.CurrentValue = GetJSONDataByIndex(CurrentRootIndex, CurrentItemIndex);
        }

        public void GetJSONDataByIndex(Vector2 JSONIndex)
        {
            JSONValue.CurrentValue = GetJSONDataByIndex(Mathf.RoundToInt(JSONIndex.x), Mathf.RoundToInt(JSONIndex.y));
        }

        public void GetJSONDataByCurrentKeyword()
        {
            JSONValue.CurrentValue = GetJSONDataByKeyword(CurrentRootIndex, CurrentKeyword);
        }

        public void SetDefaultValue()
        {
            JSONValue.CurrentValue = GetJSONDataByIndex(0, 0);
        }

        public string GetCurrentValue()
        {
            return JSONValue.CurrentValue;
        }

        public string GetJSONDataByIndex(int aRoot, int aItem)
        {
            if (aRoot < 0) aRoot = 0;
            if (aRoot > JSONRoot.Count-1) aRoot = JSONRoot.Count - 1;
            if (aItem < 0) aItem = 0;
            if (aItem > JSONRoot[aRoot].JSONData.Count - 1) aItem = JSONRoot[aRoot].JSONData.Count - 1;
            return JSONRoot[aRoot].JSONData[aItem].Value;
        }

        public string GetJSONDataByKeyword(int aRoot, string aKey)
        {
            if (aRoot < 0) aRoot = 0;
            if (aRoot > JSONRoot.Count - 1) aRoot = JSONRoot.Count - 1;
            string result = "";
            for (int i=0; i < JSONRoot[aRoot].JSONData.Count; i++)
            {
                if (JSONRoot[aRoot].JSONData[i].Key == aKey)
                {
                    result = JSONRoot[aRoot].JSONData[i].Value;
                }
            }
            return result;
        }

    }
}
