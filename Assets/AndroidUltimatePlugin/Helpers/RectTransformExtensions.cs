using UnityEngine;

namespace Gigadrillgames.AUP.Helpers
{
    public static class RectTransformExtensions
    {
        public static void SetRect(RectTransform trs, float left, float top, float right, float bottom)
        {
            trs.offsetMin = new Vector2(left, bottom);
            trs.offsetMax = new Vector2(-right, -top);
        }
    }
}