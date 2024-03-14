using UnityEngine;
using UnityEngine.UI;

namespace Gigadrillgames.AUP.Helpers
{
    public class ImageAnimator : MonoBehaviour
    {
        public RawImage targetImage;
        public Texture2D[] frames;
        public int framesPerSecond = 10;

        private void Update()
        {
            int index = (int) (Time.time * framesPerSecond) % frames.Length;
            targetImage.texture = frames[index];
        }
    }
}