using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Gigadrillgames.AUP.Helpers
{
    public static class ScrollRectExtensions
    {
        public static void ScrollToTop(this ScrollRect scrollRect)
        {
            scrollRect.verticalNormalizedPosition = 1f;
        }

        public static void ScrollToBottom(this ScrollRect scrollRect)
        {
            scrollRect.verticalNormalizedPosition = 0f;
        }
        
        
        public static IEnumerator PushToBottom(this ScrollRect scrollRect)
        {
            yield return new WaitForEndOfFrame();
            yield return new WaitForEndOfFrame();
            scrollRect.verticalNormalizedPosition = 0;
            LayoutRebuilder.ForceRebuildLayoutImmediate((RectTransform)scrollRect.transform);
        }
        
        public static IEnumerator PushToTop(this ScrollRect scrollRect)
        {
            yield return new WaitForEndOfFrame();
            yield return new WaitForEndOfFrame();
            scrollRect.verticalNormalizedPosition = 1f;
            LayoutRebuilder.ForceRebuildLayoutImmediate((RectTransform)scrollRect.transform);
        }
    }
}