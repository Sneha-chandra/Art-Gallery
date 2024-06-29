using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    private Person_Normal personInContact;
    // Start is called before the first frame update

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.X))
        {
            personInContact?.OnPlayerInteract(this);
        }
    }
    public void ChangePersonInContact(Person_Normal contact) { personInContact = contact; }
    public void RemoveContactPerson(){ personInContact = null;}
}
