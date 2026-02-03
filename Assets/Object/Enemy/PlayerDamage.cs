using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerDamage : MonoBehaviour
{
    public Transform playerStartPoint;
    public CharacterController cc;

    public Image fadeImage;   
    public float stunTime = 1f;
    public float fadeTime = 1f;

    public Timer timer;
    public float penaltyTime = 10f;


    bool isDamaged = false;

    public void OnHit()
    {
        if (isDamaged) return;
        StartCoroutine(DamageSequence());

        if (Timer.Instance != null)
        {
            Timer.Instance.timeRemaining -= penaltyTime;

            //É}ÉCÉiÉXÇ…Ç»ÇËÇ∑Ç¨ñhé~
            if (Timer.Instance.timeRemaining < 0)
            {
                Timer.Instance.timeRemaining = 0;
            }
        }
    }

    IEnumerator DamageSequence()
    {
        isDamaged = true;

        // á@ ìÆÇØÇ»Ç≠Ç∑ÇÈ
        cc.enabled = false;

        // áA 1ïbé~Ç‹ÇÈ
        yield return new WaitForSeconds(stunTime);

        // áB ÉtÉFÅ[ÉhÉAÉEÉg
        yield return StartCoroutine(Fade(0f, 1f));

        // áC èâä˙à íuÇ…ñﬂÇ∑
        transform.position = playerStartPoint.position;

        // áD ÉtÉFÅ[ÉhÉCÉì
        yield return StartCoroutine(Fade(1f, 0f));

        // áE ìÆÇØÇÈÇÊÇ§Ç…Ç∑ÇÈ
        cc.enabled = true;
        isDamaged = false;
    }

    IEnumerator Fade(float from, float to)
    {
        float t = 0f;
        Color c = fadeImage.color;

        while (t < fadeTime)
        {
            t += Time.deltaTime;
            float a = Mathf.Lerp(from, to, t / fadeTime);
            fadeImage.color = new Color(c.r, c.g, c.b, a);
            yield return null;
        }
    }
}
