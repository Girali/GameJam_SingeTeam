using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class FreezeFrameManager : MonoBehaviour
{
    public Canvas freezeFrameCanvas;
    public Canvas gameUICanvas;

    private void Start()
    {
        _isPaused = false;
        if (freezeFrameCanvas && gameUICanvas)
        {
            gameUICanvas.enabled = false;
            freezeFrameCanvas.enabled = true;
            _isPaused = true;
            StartCoroutine(LoopTextColor(freezeFrameCanvas.GetComponentInChildren<Text>()));
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && _isPaused)
        {
            freezeFrameCanvas.enabled = false;
            gameUICanvas.enabled = true;
            _isPaused = false;
        }
    }

    private IEnumerator LoopTextColor(Text text)
    {
        while (true)
        {
            float t = 0f;
            Color startColor = text.color;
            Color endColor = new Color(Random.Range(0.75f, 1f), Random.Range(0.75f, 1f), Random.Range(0.75f, 1f));

            while (t < 1f)
            {
                text.color = Color.Lerp(startColor, endColor, t);
                t += Time.deltaTime / 0.1f;

                yield return null;
            }
            text.color = endColor;
            yield return new WaitForSeconds(0.05f);
        }
    }

    private bool _isPaused;
}
