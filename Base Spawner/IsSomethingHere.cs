using UnityEngine;

public class IsSomethingHere : MonoBehaviour {

    public bool Somethinghere;
    private void OnTriggerStay(Collider other)
    {
        if (other != null)
        {
            Somethinghere = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        Somethinghere = false;
    }
}
