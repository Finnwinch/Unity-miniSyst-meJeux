using Script.Moteur;
using UnityEngine;

namespace Script.Utilitaire.Moteur {
    public class Déplacement {
        public float VitesseDéplacement { get; set; }
        public float ForceSaut { get; set; }
        
        public Déplacement(float vitesseDéplacement, float forceSaut = 5f) {
            VitesseDéplacement = vitesseDéplacement;
            ForceSaut = forceSaut;
        }

        public static void Avancer(Entité ent) {
            Vector3 direction = ent.transform.forward;
            ent.transform.position += direction.normalized * (ent.Déplacement.VitesseDéplacement * Time.deltaTime);
        }
        
        public static void Courir(Entité ent) {
            Vector3 direction = ent.transform.forward;
            ent.transform.position += direction.normalized * (ent.Déplacement.VitesseDéplacement *3f * Time.deltaTime);
        }

        public static void Reculer(Entité ent) {
            Vector3 direction = -ent.transform.forward;
            ent.transform.position += direction.normalized * (ent.Déplacement.VitesseDéplacement * Time.deltaTime);
        }

        public static void Droite(Entité ent) {
            Vector3 direction = ent.transform.right;
            ent.transform.position += direction.normalized * (ent.Déplacement.VitesseDéplacement * Time.deltaTime);
        }

        public static void Gauche(Entité ent) {
            Vector3 direction = -ent.transform.right;
            ent.transform.position += direction.normalized * (ent.Déplacement.VitesseDéplacement * Time.deltaTime);
        }
        
        public static void Sauter(Entité ent) {
            Vector3 jumpForce = Vector3.up * ent.Déplacement.ForceSaut;
            ent.RéférentDuCorps.AddForce(jumpForce, ForceMode.Impulse);
        }
    }
}