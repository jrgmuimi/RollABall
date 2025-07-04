using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    public CanvasGroup canvasGroup;
    public float fadeDuration = 1.0f;
    public TextMeshProUGUI howTo;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void PlayGame()
    {
        StartCoroutine(FadeAndThenShowInstructions());
    }

    public void Call()
    {
        StartCoroutine(ShowLevel());
    }

    private System.Collections.IEnumerator ShowLevel()
    {
        // Start fading out
        howTo.color = new Color(255f/255f, (250f - (40 * PlayerController.winCount))/255f, 0f);
        howTo.text = $"Level {(PlayerController.winCount + 1)}";
        yield return StartCoroutine(FadeInRoutine(canvasGroup));
        yield return new WaitForSeconds(0.5f);
        yield return StartCoroutine(FadeOutRoutine(canvasGroup));
    }

    private System.Collections.IEnumerator FadeAndThenShowInstructions()
    {
        // Start fading out
        yield return StartCoroutine(FadeOutRoutine(canvasGroup));

        // Only after fade completes:
        howTo.gameObject.SetActive(true);

        StartCoroutine(FadeInRoutine(howTo.GetComponent<CanvasGroup>()));
        yield return new WaitUntil(() => Input.anyKeyDown);

        SceneManager.LoadScene("Minigame");
    }

    private System.Collections.IEnumerator FadeOutRoutine(CanvasGroup cGroup)
    {
        float t = 0f;
        float startAlpha = cGroup.alpha;

        while (t < fadeDuration)
        {
            t += Time.deltaTime;
            canvasGroup.alpha = Mathf.Lerp(startAlpha, 0f, t / fadeDuration);
            yield return null;
        }

        cGroup.alpha = 0f;
        cGroup.interactable = false;
    }

    private System.Collections.IEnumerator FadeInRoutine(CanvasGroup cGroup)
    {
        cGroup.alpha = 0;

        float t = 0f;
        while (t < fadeDuration)
        {
            t += Time.deltaTime;
            cGroup.alpha = Mathf.Lerp(0f, 1f, t / fadeDuration);
            yield return null;
        }

        cGroup.alpha = 1f;
    }
}
