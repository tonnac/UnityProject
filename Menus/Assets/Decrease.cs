using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Decrease : MonoBehaviour
{
    [SerializeField]
    private Color startColor;

    [SerializeField]
    private Color endColor;

    [SerializeField]
    private Image image;
    public delegate void TimeOutDelegate();

    private event TimeOutDelegate _OnEnd;

    public event TimeOutDelegate OnEnd
    {
        add => _OnEnd += value;
        remove => _OnEnd -= value;
    }

    Slider slider;
    Text remainTime;

    bool _isPlay;
    public bool isPlay 
    {
        get => _isPlay; 
        set
        {
            _isPlay = value;
            gameObject.SetActive(value);
            if(value)
            {
                StartCoroutine(OneSec());
            }
        }
    }
    private float time;
    private float timeLimit = 5f;

    private void Awake() 
    {
        slider = GetComponent<Slider>();
        remainTime = GetComponentInChildren<Text>();
    }

    IEnumerator OneSec()
    {
        if(isPlay)
        {
            while(slider.value >= 0f)
            {
                time += Time.deltaTime;
                slider.value = Mathf.Lerp(1.0f, 0f, time / timeLimit);
                image.color = Color.Lerp(startColor, endColor, time / 3f);

                remainTime.text = Mathf.Round(timeLimit - time).ToString();

                if (slider.value <= 0f)
                {
                    isPlay = false;
                    time = 0f;
                    slider.value = 1.0f;
                    gameObject.SetActive(isPlay);
                    _OnEnd();
                    yield break;
                }
                yield return null;
            }
        }
    }
}
