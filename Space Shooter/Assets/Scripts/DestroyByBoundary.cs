using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyByBoundary : MonoBehaviour {

    //OnTriggerEnter -> Cuando otro collider entra en contacto
    //OnTriggerstay -> Cuando un collider permanece en contacto
    //OnTriggerExit -> Cuando un collider deja de estar en contacto

    private void OnTriggerExit(Collider otherCollider)
    {
        Destroy(otherCollider.gameObject);
    }
}