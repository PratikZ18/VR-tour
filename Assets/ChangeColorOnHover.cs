using UnityEngine;

public class ChangeColorOnHover : MonoBehaviour
{
    public Color normalColor = Color.white; // Default color of the object
    public Color hoverColor = Color.blue; // Color when hovered
    private Renderer objectRenderer;

    void Start()
    {
        // Get the Renderer component of the object
        objectRenderer = GetComponent<Renderer>();
        // Set the default color
        objectRenderer.material.color = normalColor;
    }

    public void OnHover()
    {
        // When the mouse enters the object
        objectRenderer.material.color = hoverColor; // Change the color on hover
    }

   public void ExitHover()
    {
        // When the mouse exits the object
        objectRenderer.material.color = normalColor; // Revert back to the default color
    }


     void OnMouseEnter()
    {
        // When the mouse enters the object
        objectRenderer.material.color = hoverColor; // Change the color on hover
    }

   void OnMouseExit()
    {
        // When the mouse exits the object
        objectRenderer.material.color = normalColor; // Revert back to the default color
    }
}

