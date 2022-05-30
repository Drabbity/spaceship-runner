using UnityEngine;

public class ButtonOnClick : MonoBehaviour
{
    public void PlayButtonSound()
    {
        AudioManager.Instance.PlaySfx(SoundType.Click);
    }
    public void LoadScene(string sceneName)
    {
        GameManager.Instance.LoadScene(sceneName);
    }
}
