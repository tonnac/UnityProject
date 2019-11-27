namespace RPG.SceneManagement
{
    using System.Collections;
    using UnityEngine;
    public class Fader : MonoBehaviour
    {
        CanvasGroup canvasGroup;
        Coroutine currentActiveFade = null;

        private void Awake() 
        {
            canvasGroup = GetComponent<CanvasGroup>();
        }

        public void FadeOutImmediate()
        {
            //canvasGroup.alpha = 1;
        }

        public IEnumerator Fade(float target, float time)
        {
            if(null != currentActiveFade)
            {
                StopCoroutine(currentActiveFade);
            }
            currentActiveFade = StartCoroutine(FadeRoutine(target, time));
            yield return currentActiveFade;
        }

        public IEnumerator FadeOut(float time)
        {
            return Fade(1f, time);
        }

        public IEnumerator FadeIn(float time)
        {
            return Fade(0f, time);
        }

        private IEnumerator FadeRoutine(float target, float time)
        {
            while(!Mathf.Approximately(canvasGroup.alpha, target))
            {
                canvasGroup.alpha += Mathf.MoveTowards(canvasGroup.alpha, target, Time.deltaTime / time);
                yield return null;
            }
        }
    }
}