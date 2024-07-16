using UnityEngine;

[CreateAssetMenu(fileName = "Item", menuName = "Item/Novo", order = 1)]
public class Item : ScriptableObject
{
    public int idItem;
    public Sprite imagem;
    public string nameItem;
    public string description;

    [Header("Action Buttons")]
    public bool use;
    public bool check;
    public bool drop;


}
