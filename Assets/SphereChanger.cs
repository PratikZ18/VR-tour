using System.Collections;
using UnityEngine;

public class SphereChanger : MonoBehaviour {

    GameObject m_Fader;
    bool changing = false;
    bool allowInput = true;
    public float actionDelay = 1.5f; // Adjust the delay time as needed
    public GameObject XRPlayer;

    void Awake()
    {
        m_Fader = GameObject.Find("Fader");
        if (m_Fader == null)
            Debug.LogWarning("No Fader object found on camera.");
    }

    public void ChangeSphere(Transform nextSphere)
    {
        if (!allowInput || changing) return;

        allowInput = false;
        StartCoroutine(FadeCamera(nextSphere));
    }

    IEnumerator FadeCamera(Transform nextSphere)
    {
        if (m_Fader != null)
        {
            StartCoroutine(FadeIn(0.75f, m_Fader.GetComponent<Renderer>().material));
            yield return new WaitForSeconds(0.75f);

            XRPlayer.transform.parent.position = nextSphere.position;

            StartCoroutine(FadeOut(0.75f, m_Fader.GetComponent<Renderer>().material));
            yield return new WaitForSeconds(0.75f);
        }
        else
        {
            XRPlayer.transform.parent.position = nextSphere.position;
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
