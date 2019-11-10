namespace RPG.SceneManagement
{
    using System.Collections;
    using UnityEngine;
    public class Fader : MonoBehaviour 
    {
        CanvasGroup canvasGroup;

        private void Start() 
        {
            canvasGroup = GetComponent<CanvasGroup>();

            StartCoroutine(FadeOutIn());
        }

        IEnumerator FadeOutIn()
        {
            yield return FadeOut(3f);
            print("Faded out");
            yield return FadeIn(2f);
            print("Faded in");
        }

        public IEnumerator FadeOut(float time)
        {
            while(canvasGroup.alpha < 1f)
            {
                canvasGroup.alpha += Mathf.Clamp01(Time.deltaTime / time);
                yield return null;
            }
        }

        public IEnumerator FadeIn(float time)
        {
            while(canvasGroup.alpha > 0f)
            {
                canvasGroup.alpha -= Mathf.Clamp01(Time.deltaTime / time);
                yield return null;
            }
        }
    }
}