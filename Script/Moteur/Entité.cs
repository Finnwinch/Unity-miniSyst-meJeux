using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using Script;
using Script.Utilitaire;
using Script.Utilitaire.Moteur;
using Script.Utilitaire.titre;
using UnityEngine;
using Object = System.Object;

public class Entité : MonoBehaviour {
    public EntitéVocation Vocation { get; set; }
    public Vie Vie { get; set; }
    public Expérience Expérience { get; set; }
    public Endurance Endurance { get; set; }
    public SacÀDos SacÀDos { get; set; }
    public Déplacement Déplacement { get; set; }
    public Action<Entité> MiseÀJour;
    public Action<Entité> Démarrage;
    public MeshRenderer RéférentDeRendu;
    public BoxCollider RéférentDeCollision;
    public Rigidbody RéférentDuCorps;
    public CapsuleCollider RéférentDeDétection;
    public Animator RéférantDeAnimation;
    public Dictionary<string, Object> Référence;
    public float VieMaximum;

    // Références pour la barre de vie
    private GameObject barreDeVie;
    private MeshRenderer barreDeVieRenderer;
    private Material barreDeVieMaterial;

    // Valeurs de la barre de vie
    public float barreDeVieLargeurMax = 2f; // Longueur maximale de la barre de vie
    private float vieActuelle;

    private void InitialiserEntité(float VitesseDéplacement, Action<Entité> Démarrage) {
        RéférentDeRendu = gameObject.AddComponent<MeshRenderer>();
        RéférentDeCollision = gameObject.AddComponent<BoxCollider>();
        RéférentDuCorps = gameObject.AddComponent<Rigidbody>();
        RéférentDuCorps.constraints = RigidbodyConstraints.FreezeRotation;
        RéférentDeDétection = gameObject.AddComponent<CapsuleCollider>();
        RéférantDeAnimation = gameObject.AddComponent<Animator>();
        Référence = new Dictionary<String, Object>();
        Vie = new Vie();
        VieMaximum = Vie.Saturation;
        Expérience = new Expérience();
        Endurance = new Endurance();
        SacÀDos = new SacÀDos();
        Déplacement = new Déplacement(VitesseDéplacement);
        Démarrage?.Invoke(this);
        if (Vocation != EntitéVocation.Joueur && Vocation != EntitéVocation.Utilité && Vocation != EntitéVocation.Spawneur && Vocation != EntitéVocation.Coffre && Vocation != EntitéVocation.Marchand) {
            InitialiserBarreDeVie();
        }
    }

    private void InitialiserBarreDeVie() {
        barreDeVie = GameObject.CreatePrimitive(PrimitiveType.Cube);
        barreDeVie.transform.SetParent(this.transform);
        barreDeVie.transform.localPosition = new Vector3(0, 2, 0);
        barreDeVie.transform.localScale = new Vector3(barreDeVieLargeurMax, 0.1f, 0.1f);
        barreDeVieRenderer = barreDeVie.GetComponent<MeshRenderer>();
        barreDeVieMaterial = barreDeVieRenderer.material;
        
        barreDeVieMaterial.color = Color.green;
    }

    private void Update() {
        MiseÀJour?.Invoke(this);
        if (Vocation != EntitéVocation.Joueur && Vocation != EntitéVocation.Utilité && Vocation != EntitéVocation.Spawneur && Vocation != EntitéVocation.Coffre && Vocation != EntitéVocation.Marchand) {
           MettreAJourBarreDeVie(); 
        }
    }
    
    public void Initialiser(float VitesseDéplacement, [CanBeNull] Action<Entité> Démarrage) {
        InitialiserEntité(VitesseDéplacement, Démarrage);
    }

    private void MettreAJourBarreDeVie() {
        float pourcentageVie = Vie.Saturation / VieMaximum;
        float nouvelleLargeur = barreDeVieLargeurMax * pourcentageVie;
        barreDeVie.transform.localScale = new Vector3(nouvelleLargeur, 0.1f, 0.1f);
        if (pourcentageVie < 0.25f) {
            barreDeVieMaterial.color = Color.red;
        } else if (pourcentageVie < 0.5f) {
            barreDeVieMaterial.color = Color.yellow;
        } else {
            barreDeVieMaterial.color = Color.green;
        }
    }

    private void OnCollisionEnter(Collision other) {
        RegistreEntités.Collision(this, other);
    }
}
