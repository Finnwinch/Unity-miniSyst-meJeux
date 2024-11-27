using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using Script.Moteur;
using Script.Utilitaire;
using Script.Utilitaire.Moteur;
using Script.Utilitaire.titre;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;
using Object = System.Object;

namespace Script {
    public class RegistreEntités {
        public static void Initialisation() {
            Partageur.Joueur = CréerEntitéAvecCube("Joueur", EntitéVocation.Joueur ,new Vector3(5, 5, 5), 1f, 5f,
                (ent) => {
                    ent.Expérience.Niveau = 11f;
                    ent.RéférentDeCollision.center = new Vector3(ent.RéférentDeCollision.center.x,
                        ent.RéférentDeCollision.center.y - 0.01f, ent.RéférentDeCollision.center.z);
                    FilsTraitement.DémarrerTraitementAvecFin(0x03, () => {
                        SacÀDos.PrendreArticle(ent,RegistreArticle.Articles["poignts"]);
                    }, "obtenir armes de base", 0x01);
                }
            );

            CréerEntitéAvecCube("Plateforme A", EntitéVocation.Utilité, new Vector3(20, 2, 20), 1f, 5f, (ent) => {
                int step = 0;
                bool goingRight = true;
                ent.RéférentDuCorps.isKinematic = true;
                ent.Vie.EstIntuable = true;
                FilsTraitement.DémarrerTraitement(0.01f, () => {
                    if (goingRight) {
                        Déplacement.Droite(ent);
                        step++;
                        if (step >= 7 * 10) {
                            goingRight = false;
                        }
                    }
                    else {
                        Déplacement.Gauche(ent);
                        step--;
                        if (step <= -7 * 10) {
                            goingRight = true;
                        }
                    }
                }, "MoveRightLeft");
            });
            
           CréerEntitéAvecCube("Spawneur3D", EntitéVocation.Spawneur ,new Vector3(10, 0.55f, 10), 1f, 0f, (ent) => {
               ent.Référence["Spawneur"] = new Spawneur(ent,SpawneurDifficulté.MOYEN);
               ent.RéférentDuCorps.isKinematic = true;
               ent.Vie.EstIntuable = true;
           }).MiseÀJour = (ent) => {
               Spawneur spawneur = ent.Référence["Spawneur"] as Spawneur;
               if (spawneur != null) {
                   spawneur.Activé = Vector3.Distance(ent.transform.position, Partageur.Joueur.transform.position) <= 5f;
                   if (spawneur.Activé) {
                       spawneur.IA();
                   } 
               }
           };
           
           CréerEntitéAvecCube("Coffre01", EntitéVocation.Coffre, new Vector3(20, 0.55f, 10), 1f, 0f, (ent) => {
               ent.RéférentDuCorps.isKinematic = true;
               ent.Vie.EstIntuable = true;
               GameObject spriteObject = new GameObject("SpriteAuDessus");
               SpriteRenderer spriteRenderer = spriteObject.AddComponent<SpriteRenderer>();
               spriteObject.transform.SetParent(ent.transform);
               spriteObject.transform.localScale = new Vector3(10, 10, 10);
               spriteObject.transform.localPosition = new Vector3(0, 1.5f, 0);
               
               ent.Référence["utilisé"] = false;
               ent.Référence["Action"] = (Action)(() => {
                   if (ent.Référence["utilisé"] is bool utilisé && !utilisé) {
                       Article article = RegistreArticle.ObtenirCoffreHasard();
                       SacÀDos.PrendreArticle(Partageur.Joueur, article);
                       ent.Référence["utilisé"] = true;
                       spriteRenderer.sprite = article.Model;
                   }
               });

           });
           
           CréerEntitéAvecCube("L'esprit De Pierrick", EntitéVocation.Marchand, new Vector3(20, 0.55f, 20), 1f, 0f, (ent) => {
               ent.RéférentDuCorps.isKinematic = true;
               ent.Vie.EstIntuable = true;
               GameObject spriteObject = new GameObject("SpriteAuDessus"+ent.name);
               SpriteRenderer spriteRenderer = spriteObject.AddComponent<SpriteRenderer>();
               spriteObject.transform.SetParent(ent.transform);
               spriteObject.transform.localScale = new Vector3(10, 10, 10);
               spriteObject.transform.localPosition = new Vector3(0, 1.5f, 0);
               
               ent.Référence["Action"] = (Action)(() => {
                   if (!FilsTraitement.VérifierFils("NoSpamPierrick")){
                       FilsTraitement.DémarrerTraitementAvecFin(0x03, () => {
                           if (Partageur.Joueur.Expérience.Niveau >= 5f) {
                               Article article = RegistreArticle.ObtenirCoffreHasard();
                               SacÀDos.PrendreArticle(Partageur.Joueur, article);
                               if (article.Vocation != ArticleVocation.POTION) {
                                   RegistreArticle.coffres.Remove(article);
                               }
                               Partageur.Joueur.Expérience.Niveau -= 5f;
                               spriteRenderer.sprite = article.Model;
                           }
                       },"NoSpamPierrick",0x01);
                   }
               });

           });
        }

        public static Entité CréerEntitéAvecCube(string nom, EntitéVocation vocation, Vector3 position, float échelle, float vitesseDéplacement, [CanBeNull] Action<Entité> init = null) {
            GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
            cube.name = nom;
            cube.transform.position = position;
            cube.transform.localScale = new Vector3(échelle, échelle, échelle);
            Entité nouvelleEntité = cube.AddComponent<Entité>();
            nouvelleEntité.Vocation = vocation;
            nouvelleEntité.Initialiser(vitesseDéplacement, init);
            return nouvelleEntité;
        }

        public static void Collision(Entité origin, Collision other) {
            
              if (origin.Vocation == EntitéVocation.Ennemie &&
                   other.gameObject.AddComponent<Entité>().Vocation == EntitéVocation.Joueur) {
                  if (!FilsTraitement.VérifierFils("PeutAttaquer")) {
                      FilsTraitement.DémarrerTraitementAvecFin(0x04, () => {
                          Vie.PrendreDommage(other.gameObject.AddComponent<Entité>(),Partageur.Joueur,0x10);
                      },"PeutAttaquer",0x01);
                  }
               } 
             
        }
    }
}