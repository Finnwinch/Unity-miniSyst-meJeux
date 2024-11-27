using System;
using System.Collections.Generic;
using Script.Moteur;
using Script.Utilitaire.titre;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Script.Utilitaire.Moteur {
    public class Spawneur {
        public static int Méchants;
        public Boolean Activé;
        public Entité SpawneurObj;
        public SpawneurDifficulté Difficulté;
        public Spawneur(Entité Spawneur, SpawneurDifficulté Difficulté) {
            Méchants = 0;
            Activé = false;
            SpawneurObj = Spawneur;
            this.Difficulté = Difficulté;
        }

        public void IA() {
            if (!FilsTraitement.VérifierFils("wait")) {
                FilsTraitement.DémarrerTraitementAvecFin(1f, () => {
                    EssaiecréeMéchant();
                },"wait",1);
            }
        }

        private bool PeuxCréeUnMéchant() {
            return Méchants < 5;
        }

        private void CréeUnMéchant() {
            RegistreEntités.CréerEntitéAvecCube($"{SpawneurObj.name}@Méchant${Méchants}", EntitéVocation.Ennemie,SpawneurObj.transform.position + new Vector3(Random.insideUnitCircle.x, 0, Random.insideUnitCircle.y) * 5f,0x01,0x01,
                (ent) => {
                    switch (Difficulté) {
                        case SpawneurDifficulté.FACILE :
                            ent.Déplacement.VitesseDéplacement = 0x01;
                            ent.Expérience.Niveau = 0f;
                            ent.Expérience.Quantité = 0.57f;
                            ent.Vie.Chance = 1;
                            break;
                        case SpawneurDifficulté.MOYEN :
                            ent.Déplacement.VitesseDéplacement = 0x03;
                            ent.Expérience.Niveau = 5f;
                            ent.Expérience.Quantité = 0.57f;
                            ent.Vie.Chance = 2;
                            ent.Vie.Saturation = 200;
                            //ent.Déplacement.VitesseDéplacement = 8f;
                            break;
                        case SpawneurDifficulté.DIFFICILE :
                            ent.Déplacement.VitesseDéplacement = 0x05;
                            ent.Expérience.Niveau = 15f;
                            ent.Expérience.Quantité = 0.57f;
                            ent.Vie.Chance = 1;
                            ent.Vie.Saturation = 300;
                            //ent.Déplacement.VitesseDéplacement = 12f;
                            break;
                    }
                }
            ).MiseÀJour = (ent) => {
                ent.transform.position = Vector3.MoveTowards(ent.transform.position, Partageur.Joueur.transform.position - new Vector3(0x01,0x01,0x00), ent.Déplacement.VitesseDéplacement * Time.deltaTime);
            };
            Méchants++;
        }

        private void EssaiecréeMéchant() {
            if (PeuxCréeUnMéchant()) {
                CréeUnMéchant();
            }
        }
    }
}