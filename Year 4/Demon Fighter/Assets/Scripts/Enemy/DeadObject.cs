using UnityEngine;

public class DeadObject : MonoBehaviour
{
    void Start()
    {
        Invoke("FreezeObject", 5f);
    }

    void FreezeObject()
    {
        GetComponent<Rigidbody>().isKinematic = true;
        GetComponent<CapsuleCollider>().enabled = false;
    }
}
