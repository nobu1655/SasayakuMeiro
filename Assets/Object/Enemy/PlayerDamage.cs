using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerDamage : MonoBehaviour
{
    public Transform playerStartPoint;
    public CharacterController cc;

    public Image fadeImage;   // •‚¢UI‰æ‘œ
    public float stunTime = 1f;
    public float fadeTime = 1f;

    bool isDamaged = false;

    public void OnHit()
    {
        if (isDamaged) return;
        StartCoroutine(DamageSequence());
    }

    IEnumerator DamageSequence()
    {
        isDamaged = true;

        // ‡@ “®‚¯‚È‚­‚·‚é
        cc.enabled = false;

        // ‡A 1•b~‚Ü‚é
        yield return new WaitForSeconds(stunTime);

        // ‡B ƒtƒF[ƒhƒAƒEƒg
        yield return StartCoroutine(Fade(0f, 1f));

        // ‡C ‰ŠúˆÊ’u‚É–ß‚·
        transform.position = playerStartPoint.position;

        // ‡D ƒtƒF[ƒhƒCƒ“
        yield return StartCoroutine(Fade(1f, 0f));

        // ‡E “®‚¯‚é‚æ‚¤‚É‚·‚é
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
