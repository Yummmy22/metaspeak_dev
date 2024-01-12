using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TechnomediaLabs;

namespace Zetcil
{

    public class VarVector2 : MonoBehaviour
    {

        [Space(10)]
        public bool isEnabled;

        [Header("Variables Settings")]
        public bool usingVector2;
        [ConditionalField("usingVector2")] public Vector2 CurrentValue;
        [ConditionalField("usingVector2")] public VarFloat Vector2X;
        [ConditionalField("usingVector2")] public VarFloat Vector2Y;
        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            Vector2X.CurrentValue = CurrentValue.x;
            Vector2Y.CurrentValue = CurrentValue.y;
        }

        public float GetCurrentValueX()
        {
            return Vector2X.CurrentValue;
        }

        public float GetCurrentValueY()
        {
            return Vector2Y.CurrentValue;
        }

        public Vector2 GetCurrentValue()
        {
            return CurrentValue;
        }

        public void SetCurrentValue(Vector2 aValue)
        {
            CurrentValue = aValue;
        }

        public void SetCurrentValueX(float aValue)
        {
            CurrentValue.x = aValue;
        }

        public void SetCurrentValueY(float aValue)
        {
            CurrentValue.y = aValue;
        }

        public void AddToCurrentValue(Vector2 aValue)
        {
            CurrentValue += aValue;
        }

        public void AddToCurrentValueX(float aValue)
        {
            CurrentValue.x += aValue;
        }

        public void AddToCurrentValueY(float aValue)
        {
            CurrentValue.y += aValue;
        }

        public void SubFromCurrentValue(Vector2 aValue)
        {
            CurrentValue -= aValue;
        }

        public void SubFromCurrentValueX(float aValue)
        {
            CurrentValue.x -= aValue;
        }

        public void SubFromCurrentValueY(float aValue)
        {
            CurrentValue.y -= aValue;
        }
    }
}
