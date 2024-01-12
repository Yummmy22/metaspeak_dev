using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

namespace Zetcil
{

    public class VarXMLScore : MonoBehaviour
    {

        [System.Serializable]
        public class CXMLScore {
            public string Name;
            public string Rank;
            public float Score;
        }

        [System.Serializable]
        public class CXMLUIScore
        {
            public Text Name;
            public Text Rank;
            public Text Score;
        }

        [System.Serializable]
        public class CXMLRank
        {
            public float StandardValue;
            public string RankName;
        }

        [Space(10)]
        public bool isEnabled;

        [Header("XML Score Settings")]
        public CXMLScore XMLScore;

        [Header("XML Rank Settings")]
        public List<CXMLRank> XMLRank;

        [Header("Array Score Settings")]
        public List<CXMLScore> XMLScores;

        [Header("UI Score Settings")]
        public List<CXMLUIScore> XMLUIScores;

        public void AutoSetPlayerRank()
        {
            foreach (CXMLRank temp in XMLRank)
            {
                if (XMLScore.Score >= temp.StandardValue)
                {
                    XMLScore.Rank = temp.RankName;
                }
            }
        }

        public void SetPlayerName(string aValue)
        {
            XMLScore.Name = aValue;
        }

        public void SetPlayerRank(string aValue)
        {
            XMLScore.Rank = aValue;
        }

        static int ESortAscending(CXMLScore p1, CXMLScore p2)
        {
            return p1.Score.CompareTo(p2.Score);
        }

        static int ESortDescending(CXMLScore p1, CXMLScore p2)
        {
            return p2.Score.CompareTo(p1.Score);
        }

        public void SortAscending()
        {
            XMLScores.Sort(ESortAscending);
        }

        public void SortDescending()
        {
            XMLScores.Sort(ESortDescending);
        }

        public void SetPlayerScore(float aValue)
        {
            XMLScore.Score = aValue;
        }

        public void AddNewData()
        {
            XMLScores.Add(XMLScore);
        }

        public void ShowUIScore()
        {
            for (int i=0; i < XMLUIScores.Count; i++)
            {
                if (i < XMLScores.Count)
                {
                    XMLUIScores[i].Name.text = XMLScores[i].Name;
                    XMLUIScores[i].Rank.text = XMLScores[i].Rank;
                    XMLUIScores[i].Score.text = XMLScores[i].Score.ToString();
                }
            }
        }

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}
