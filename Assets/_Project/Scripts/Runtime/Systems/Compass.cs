using UnityEngine;
using UnityEngine.UI;

public class Compass : MonoBehaviour
{
    public GameManager manager;

    public RawImage compassImage;

    private void Start()
    {
        manager = FindObjectOfType<GameManager>();
    }

    private void Update()
    {
        compassImage.uvRect = new Rect(manager.PlayerFps.localEulerAngles.y / 360, 0, 1, 1);
    }

}
