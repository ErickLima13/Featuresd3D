using UnityEngine;

public class TheOtherObjectIsBehind : MonoBehaviour
{
    public bool IsBehind
    {
        get; private set;
    }

    public bool GetIsBehind(Transform obj, Transform other)
    {
        IsBehind = Vector3.Dot(obj.position - other.position, other.forward) < 0;
        return IsBehind;
    }

    //private void Update()
    //{
    //    IsBehind = GetIsBehind(transform, FindObjectOfType<GameManager>().PlayerFps);

    //    float distance = Vector3.Distance(transform.position, FindObjectOfType<GameManager>().PlayerFps.position);

    //    if (IsBehind && distance > 1.5f)
    //    {

    //        transform.position = Vector3.MoveTowards(new(transform.position.x,0,transform.position.z), FindObjectOfType<GameManager>().PlayerFps.position, 0.5f);
         

    //    }
    //}
}
