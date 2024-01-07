using System.Collections;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class VRSphereChanger : MonoBehaviour
{
    GameObject m_Fader;
    bool changing = false;
    bool allowInput = true;
    public float actionDelay = 1.5f; // Adjust the delay time as needed
    public XRGrabInteractable interactable; // Reference to the XRGrabInteractable
    public GameObject nextSphereObject; // Public variable for the next sphere GameObject

    void Awake()
    {
        m_Fader = GameObject.Find("Fader");
        if (m_Fader == null)
            Debug.LogWarning("No Fader object found on camera.");

        interactable = GetComponent<XRGrabInteractable>(); // Get the XRGrabInteractable component
        interactable.onSelectEntered.AddListener(ChangeSphere); // Add listener for the interaction event
    }

    public void ChangeSphere(XRBaseInteractor interactor)
    {
        if (!allowInput || changing) return;

        allowInput = false;

        if (nextSphereObject == null)
        {
            Debug.LogWarning("Next sphere object reference is missing.");
            allowInput = true;
            return;
        }

        // Access the transform of the next sphere GameObject
        Transform nextSphere = nextSphereObject.transform;

        StartCoroutine(FadeCamera(nextSphere));
    }

    IEnumerator FadeCamera(Transform nextSphere)
    {
        if (m_Fader != null)
        {
            StartCoroutine(FadeIn(0.75f, m_Fader.GetComponent<Renderer>().material));
            yield return new WaitForSeconds(0.75f);

            // Move the XR Rig to the next sphere's position
            XRController xrController = FindObjectOfType<XRController>();
            if (xrController != null)
            {
                xrController.gameObject.transform.parent.position = nextSphere.position;
            }

            StartCoroutine(FadeOut(0.75f, m_Fader.GetComponent<Renderer>().material));
            yield return new WaitForSeconds(0.75f);
        }
        else
        {
            // Move the XR Rig to the next sphere's position
            XRController xrController = FindObjectOfType<XRController>();
            if (xrController != null)
            {
                xrController.gameObject.transform.parent.position = nextSphere.position;
            }
        }

        yield return new WaitForSeconds(actionDelay); // Delay after fading completes

        allowInput = true;
    }

    IEnumerator FadeOut(float time, Material mat)
    {
        while (mat.color.a > 0.0f)
        {
            float newAlpha = Mathf.Clamp(mat.color.a - (Time.deltaTime / time), 0.0f, 1.0f);
            mat.color = new Color(mat.color.r, mat.color.g, mat.color.b, newAlpha);
            yield return null;
        }
    }

    IEnumerator FadeIn(float time, Material mat)
    {
        while (mat.color.a < 1.0f)
        {
            float newAlpha = Mathf.Clamp(mat.color.a + (Time.deltaTime / time), 0.0f, 1.0f);
            mat.color = new Color(mat.color.r, mat.color.g, mat.color.b, newAlpha);
            yield return null;
        }
    }
}
