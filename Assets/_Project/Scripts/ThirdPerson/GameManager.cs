using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

namespace ThirdPerson
{
    public class GameManager : MonoBehaviour
    {
        public int keys;
        public int silverKey; // pode ser get set, pra mostrar na hud


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