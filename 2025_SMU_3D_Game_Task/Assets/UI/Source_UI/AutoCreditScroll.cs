using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class AutoScrollCredits : MonoBehaviour
{
    [SerializeField] private ScrollRect scrollRect;
    [SerializeField] private float duration = 12f;   // 전체 스크롤 시간(초)
    [SerializeField] private bool playOnStart = true;
    [SerializeField] private bool loop = false;

    private Coroutine co;

    private void OnEnable()
    {
        if (playOnStart) Play();
    }

    public void Play()
    {
        if (co != null) StopCoroutine(co);
        co = StartCoroutine(ScrollRoutine());
    }

    private IEnumerator ScrollRoutine()
    {
        // 레이아웃 계산 1프레임 기다려야 Content 높이가 확정됨
        yield return null;
        Canvas.ForceUpdateCanvases();

        while (true)
        {
            float t = 0f;

            // 맨 위에서 시작 (1 = Top, 0 = Bottom)
            scrollRect.verticalNormalizedPosition = 1f;

            while (t < duration)
            {
                t += Time.unscaledDeltaTime; // ★ timeScale 0이어도 움직이게
                float n = Mathf.Clamp01(t / duration);
                scrollRect.verticalNormalizedPosition = Mathf.Lerp(1f, 0f, n);
                yield return null;
            }

            if (!loop) break;
        }
    }
}