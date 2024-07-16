using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Compass : MonoBehaviour
{
    private GameManager manager;

    private Transform player;

    public RawImage compassImage;

    public GameObject iconPrefab;
    public List<QuestPoint> quests = new();

    public float compassUnit;

    public QuestPoint missionA;

    public float maxDist = 20f;



    private void Start()
    {
        manager = FindObjectOfType<GameManager>();
        player = manager.PlayerFps;

        NewQuest(missionA);

        compassUnit = compassImage.rectTransform.rect.width / 360;
    }

    private void Update()
    {
        compassImage.uvRect = new Rect(manager.PlayerFps.localEulerAngles.y / 360, 0, 1, 1);

        foreach (QuestPoint q in quests)
        {
            q.image.rectTransform.anchoredPosition = GetPosOnCompass(q);

            float dist = Vector2.Distance(new Vector2(player.position.x, player.position.z), q.transform.position);
            float scale = 0;

            if (dist <= maxDist)
            {
                scale = 1 - (dist / maxDist);
            }

            q.image.rectTransform.localScale = Vector3.one * scale;
        }
    }

    public void NewQuest(QuestPoint quest)
    {
        GameObject q = Instantiate(iconPrefab, compassImage.transform);
        quest.image = q.GetComponent<Image>();
        quest.image.sprite = quest.icon;

        quests.Add(quest);
    }

    private Vector2 GetPosOnCompass(QuestPoint quest)
    {
        Vector2 playerPos = new(player.position.x, player.position.z);
        Vector2 playerFwd = new(player.forward.x, player.forward.z);

        float angle = Vector2.SignedAngle(quest.Position - playerPos, playerFwd);

        return new Vector2(compassUnit * angle, 0);
    }
}
