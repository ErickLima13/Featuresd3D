using System.Collections;
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

        private PlayerMovement playerMovement;

        [SerializeField] private GameState currentState;

        public int keys;
        public int silverKey; // pode ser get set, pra mostrar na hud

        [SerializeField] private CanvasGroup panelImage;
        [SerializeField] private AudioSource sfx;
        [SerializeField] private AudioClip gameOver, win;

        private bool isDone;

        private void Start()
        {
            playerMovement = FindObjectOfType<PlayerMovement>();
            playerMovement.OnGetPlayerEvent += Fade;
        }

        private void OnDestroy()
        {
            playerMovement.OnGetPlayerEvent -= Fade;
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
                panelImage.alpha += 0.3f * Time.deltaTime;
                yield return new WaitForSeconds(0.05f);

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
                    panelImage.alpha -= 0.3f * Time.deltaTime;
                    yield return new WaitForSeconds(0.01f);
                }
            }

            yield return new WaitWhile(() => panelImage.alpha != 0f);

            currentState = GameState.Gameplay;
        }

        public bool NonGameplay()
        {
            return currentState != GameState.Gameplay;
        }

        public void CheckKeys()
        {
            if (silverKey >= keys)
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
        }


    }
}