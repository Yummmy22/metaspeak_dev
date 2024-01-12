using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TechnomediaLabs;

namespace Zetcil
{
    public class VarCursor : MonoBehaviour
    {
        [Space(10)]
        public bool isEnabled;

        [Header("Cursor Settings")]
        public KeyCode CursorKey = KeyCode.Escape;
        public bool CursorVisible;

        // Start is called before the first frame update
        void Start()
        {
            if (isEnabled)
            {
                Cursor.visible = CursorVisible;
            }
        }

        public void SetCursorVisible(bool aStatus)
        {
            if (isEnabled)
            {
                CursorVisible = aStatus;
                Cursor.visible = CursorVisible;
            }
        }

        public void SetCursorVisible(VarBoolean aStatus)
        {
            if (isEnabled)
            {
                CursorVisible = aStatus.CurrentValue;
                Cursor.visible = CursorVisible;
            }
        }

        // Update is called once per frame
        void LateUpdate()
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = (true);

            if (isEnabled)
            {
                if (Input.GetKeyDown(CursorKey))
                {
                    SetCursorVisible(true);
                }
            }
        }
    }
}
