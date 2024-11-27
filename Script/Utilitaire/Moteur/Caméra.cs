using System;
using System.Numerics;
using Script;
using UnityEngine;
using Quaternion = UnityEngine.Quaternion;
using Vector3 = UnityEngine.Vector3;

public enum ÉtatCaméra {
    PREMIÈRE,
    TROISIÈME,
    CINÉMATIQUE
}
public class Caméra {
    public Camera Composant;
    public float SensibilitéSouris = 2f;
    public static ÉtatCaméra ÉtatCaméra = ÉtatCaméra.TROISIÈME;

    public Caméra(float niveauZoomActuel) {
        Composant = Camera.main;
        if (Composant == null) {
            Debug.LogError("La caméra principale n'est pas trouvée !");
        }
        Composant.orthographicSize = niveauZoomActuel;
    }

    private void DéfinirCaméraTroisièmePersomne(Vector3 joueurPosition, float rotationX, float rotationY) {
        if (Composant == null) return;

        // Apply rotation based on mouse movements
        Composant.transform.rotation = Quaternion.Euler(rotationY, rotationX, 0);

        // Set camera position with a Z-axis offset
        Composant.transform.position = joueurPosition + Composant.transform.rotation * new Vector3(0, 1.5f, -5f);

        // Look towards the player's position
        Composant.transform.LookAt(joueurPosition);
    }

    private void DéfinirCaméraPremièrePersonne(Vector3 joueurPosition, float rotationX, float rotationY) {
        if (Composant == null) return;

        // Apply rotation based on mouse movements
        Composant.transform.rotation = Quaternion.Euler(rotationY, rotationX, 0);

        // Position the camera directly at the player's eye level
        Composant.transform.position = joueurPosition + Composant.transform.rotation * new Vector3(0, 1.5f, 0.2f);
    }
    
    public void DéplacerVersPoint(Vector3 positionCible,Quaternion rotationCible) {
        if (Composant == null) return;
        // Déplace la caméra vers la position cible avec une interpolation douce
        Composant.transform.position = Vector3.Lerp(
            Composant.transform.position, 
            positionCible, 
            Time.deltaTime * 5f // 5f est la vitesse de déplacement
        );

        // Interpole la rotation de la caméra vers la rotation cible avec une interpolation douce
        Composant.transform.rotation = Quaternion.Slerp(
            Composant.transform.rotation, 
            rotationCible, 
            Time.deltaTime * 5f // 5f est la vitesse de rotation
        );
    }

    public static void MettreAJourCamera(Caméra cam, Vector3 joueurPosition, ref float rotationX, ref float rotationY) {
        // Capture mouse movements for camera rotation
        float mouseX = 0f;
        float mouseY = 0f;

        // Détection des touches fléchées
        if (Input.GetKey(KeyCode.LeftArrow))
            mouseX = -1f;  // Flèche gauche
        if (Input.GetKey(KeyCode.RightArrow))
            mouseX = 1f;   // Flèche droite
        if (Input.GetKey(KeyCode.UpArrow))
            mouseY = 1f;   // Flèche haut
        if (Input.GetKey(KeyCode.DownArrow))
            mouseY = -1f;  // Flèche bas


        // Update camera rotation angles based on mouse sensitivity
        rotationX += mouseX * cam.SensibilitéSouris;
        rotationY -= mouseY * cam.SensibilitéSouris;

        // Clamp rotation on the Y-axis to limit looking too far up or down
        rotationY = Mathf.Clamp(rotationY, -60f, 60f); // Adjusted limit for better control

        switch (ÉtatCaméra)
        {
            case ÉtatCaméra.TROISIÈME: {
                cam.DéfinirCaméraTroisièmePersomne(joueurPosition, rotationX, rotationY);

                // Sync only the horizontal rotation of the player with the camera's rotation
                Quaternion cameraRotation = Quaternion.Euler(0, rotationX, 0);
                Partageur.Joueur.transform.rotation = cameraRotation;
                break;
            }
            case ÉtatCaméra.PREMIÈRE: {
                cam.DéfinirCaméraPremièrePersonne(joueurPosition, rotationX, rotationY);

                // Sync the full rotation of the player with the camera's rotation
                Quaternion cameraRotation = Quaternion.Euler(0, rotationX, 0);
                Partageur.Joueur.transform.rotation = cameraRotation;
                break;
            }
            case ÉtatCaméra.CINÉMATIQUE:
            {
                cam.DéplacerVersPoint(new Vector3(10f,10f,10f),Quaternion.Euler(30f, 45f, 0f));
                break;
            }
        }
    }
}