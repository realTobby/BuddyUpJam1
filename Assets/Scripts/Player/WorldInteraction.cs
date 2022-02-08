using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WorldInteraction : MonoBehaviour
{
    [SerializeField] GameObject interactText;

    [SerializeField] Interactable currentInteractionObject;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        if(currentInteractionObject != null)
        {
            if(Input.GetKeyDown(currentInteractionObject.GetInteractionKey().ToLower()))
            {
                currentInteractionObject.ExecuteInteraction();
            }
        }

        Vector3 rayOrigin = new Vector3(0.5f, 0.5f, 0f); // center of the screen
        float rayLength = 1.25f;

        // actual Ray
        Ray ray = Camera.main.ViewportPointToRay(rayOrigin);

        // debug Ray
        Debug.DrawRay(ray.origin, ray.direction * rayLength, Color.red);

        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, rayLength))
        {
            // our Ray intersected a collider
            if(hit.transform.CompareTag("Interactable"))
            {
                interactText.SetActive(true);

                currentInteractionObject = hit.transform.gameObject.GetComponent<Interactable>();

                interactText.GetComponent<TMPro.TextMeshProUGUI>().text = "Press '" + currentInteractionObject.GetInteractionKey() + "' to " + currentInteractionObject.GetInteractionType() + " " + currentInteractionObject.GetInteractionTitle();


            }
        }
        else
        {
            interactText.SetActive(false);
            currentInteractionObject = null;
        }


    }
}
