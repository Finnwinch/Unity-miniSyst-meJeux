using System;
using Script.Moteur;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Script.Utilitaire {
    public class Expérience {
        public float Quantité { get; set; }
        public float Niveau { get; set; }
        public Expérience() {
            Quantité = 0f;
            Niveau = 0f;
        }
        public Expérience(float quantité, float niveau) {
            Quantité = quantité;
            Niveau = niveau;
        }
        public static float JetterNiveau(Entité entité) {
            float QuantitéÀJetter = 0;
            for (int i = 0; i < entité.Expérience.Niveau; i++) {
                QuantitéÀJetter+=Random.Range(0.5f, 0.96f);
            }
            QuantitéÀJetter += entité.Expérience.Quantité / 3.4f;
            entité.Expérience.Niveau = 0;
            entité.Expérience.Quantité = 0;
            return QuantitéÀJetter;
        }
        public static void PrendreNiveau(Entité entité, float pointsExpérience) {
            int niveau = Mathf.FloorToInt(pointsExpérience);
            float quantité = pointsExpérience - niveau;
            entité.Expérience.Niveau += niveau;
            entité.Expérience.Quantité += quantité;
            if (entité.Expérience.Quantité >= 1.0f) {
                int niveauxAdditionnels = Mathf.FloorToInt(entité.Expérience.Quantité);
                entité.Expérience.Niveau += niveauxAdditionnels;
                entité.Expérience.Quantité -= niveauxAdditionnels;
            }
        }
    }
}