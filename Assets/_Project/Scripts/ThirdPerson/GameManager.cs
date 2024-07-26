using System.Collections;
using TMPro;
using UnityEngine;

namespace ThirdPerson
{
    public enum GameState
    {
        Gameplay, CutScene
    }

    public class GameManager : MonoBehaviour
    {
        // regra de 3 - 3 vidas, 3 gargulas, 3 fantasmas, 3 chaves (fazer fade de vitoria)

        public PlayerMovement playerMovement;

        [SerializeField] private GameState currentState;

        [Header("keys")]
        public int totalKeys;
        public int silverKey; // pode ser get set, pra mostrar na hud
        public TextMeshProUGUI countKeysTxt;

        [SerializeField] private CanvasGroup panelImage;
        [SerializeField] private AudioSource sfx;
        [SerializeField] private AudioClip gameOver, win;

        private bool isDone;

        public Gargoyle[] gargoyles;
        public GhostController[] ghostControllers;

        private void Start()
        {
            playerMovement = FindObjectOfType<PlayerMovement>();
            playerMovement.OnGetPlayerEvent += Fade;

            gargoyles = FindObjectsOfType<Gargoyle>();
            ghostControllers = FindObjectsOfType<GhostController>();

            foreach (Gargoyle g in gargoyles)
            {
                g.OnGargoyleSeeThePlayer += SetCommand;
            }

        }

        private void OnDestroy()
        {
            playerMovement.OnGetPlayerEvent -= Fade;

            foreach (Gargoyle g in gargoyles)
            {
                g.OnGargoyleSeeThePlayer -= SetCommand;
            }
        }

        private void SetCommand(Transform p)
        {
            foreach (GhostController gc in ghostControllers)
            {
                gc.SetToFollowPlayer(true, p);
                StartCoroutine(WaitGhosts());
            }
        }

        private IEnumerator WaitGhosts()
        {
            yield return new WaitForSeconds(1f);

            foreach (GhostController gc in ghostControllers)
            {
                gc.SetToFollowPlayer(false, null);
            }
        }

        public void Fade()
        {
            isDone = false;
            StartCoroutine(FadeDelay());
        }

        private IEnumerator FadeDelay()
        {
            sfx.PlayOneShot(gameOver);

            currentState = GameState.CutScene;

            while (panelImage.alpha < 1.0f)
            {
                panelImage.alpha += 0.8f * Time.deltaTime;
                yield return new WaitForSeconds(0.01f);

                if (isDone)
                {
                    break;
                }
            }

            yield return new WaitWhile(() => panelImage.alpha != 1.0f);

            isDone = true;

            playerMovement.transform.position = playerMovement.startPos;

            if (playerMovement.transform.position == playerMovement.startPos)
            {
                while (panelImage.alpha != 0f)
                {
                    panelImage.alpha -= 0.8f * Time.deltaTime;
                    yield return new WaitForSeconds(0.01f);
                }
            }

            yield return new WaitWhile(() => panelImage.alpha != 0f);

            currentState = GameState.Gameplay;

            yield return new WaitForEndOfFrame();

            playerMovement.alreadyGot = false;
        }

        public bool NonGameplay()
        {
            return currentState != GameState.Gameplay;
        }

        public void CheckKeys()
        {
            if (silverKey >= totalKeys)
            {
                print("Passou");
            }
            else
            {
                print("não tem chave");
            }
        }

        public void GetKey()
        {
            silverKey++;
            countKeysTxt.text = silverKey + " / " + totalKeys;
        }


    }
}