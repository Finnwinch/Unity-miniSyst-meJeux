using System;
using Script.Moteur;
using Script.Utilitaire.titre;
using UnityEngine;

namespace Script.Utilitaire.Moteur {
    public class Navigation {
        public static void Initialisation() {
            Partageur.Joueur.MiseÀJour = (ent) => {
            Vector3 moveDirection = Vector3.zero;

            // Avancer
            ÉvénementButtonAppuyer(KeyCode.W, true, () => {
                Déplacement.Avancer(ent);
            });
        
            // Aller à gauche
            ÉvénementButtonAppuyer(KeyCode.A, true, () => {
                Déplacement.Gauche(ent);
            });
        
            // Reculer
            ÉvénementButtonAppuyer(KeyCode.S, true, () => {
                Déplacement.Reculer(ent);
            });
        
            // Aller à droite
            ÉvénementButtonAppuyer(KeyCode.D, true, () => {
                Déplacement.Droite(ent);
            });
        
            // Sauter
            ÉvénementButtonAppuyer(KeyCode.Space, ent.Endurance.Saturation>=0.5f, () => {
                FilsTraitement.DémarrerTraitementAvecFin(0.3f, () => {
                    Déplacement.Sauter(ent);
                    Endurance.Fatigué(ent,EnduranceCoût.SAUT);
                },"NoSpamInUpdateLoop"+Guid.NewGuid().ToString(),1);
            });
            
            // Courir
            ÉvénementBoutonsAppuyer(KeyCode.W, KeyCode.LeftShift, ent.Endurance.Saturation >= 0.2f, () => {
                FilsTraitement.DémarrerTraitementAvecFin(0.1f, () => {
                    Déplacement.Courir(ent);
                    Endurance.Fatigué(ent,EnduranceCoût.DÉPLACEMENT_RAPIDE);
                },"NoSpamInUpdateLoop"+Guid.NewGuid().ToString(),1);
            });
            
            if (!FilsTraitement.VérifierFils("ToggleCameraCooldown")) {
                ÉvénementButtonAppuyer(KeyCode.F1, ent.Endurance.Saturation >= 0.3f, () => {
                    // Start the toggle cooldown coroutine with a specified duration
                    FilsTraitement.DémarrerTraitementAvecFin(0.1f, () => {
                        if (Caméra.ÉtatCaméra == ÉtatCaméra.PREMIÈRE)
                        {
                            Caméra.ÉtatCaméra = ÉtatCaméra.TROISIÈME;
                        }
                        else
                        {
                            Caméra.ÉtatCaméra = ÉtatCaméra.PREMIÈRE;
                        }
                    }, "ToggleCameraCooldown", 1);
                });
            }
            
            if (!FilsTraitement.VérifierFils("ToggleCameraCooldown")) {
                ÉvénementButtonAppuyer(KeyCode.F2, ent.Endurance.Saturation >= 0.3f, () => {
                    // Start the toggle cooldown coroutine with a specified duration
                    FilsTraitement.DémarrerTraitementAvecFin(0.1f, () =>
                    {
                        Caméra.ÉtatCaméra = ÉtatCaméra.CINÉMATIQUE;
                    }, "ToggleCameraCooldown", 1);
                });
            }
            
            ÉvénementButtonAppuyer(KeyCode.Escape,true, () => {
                Contrôleur.JeuxEnPause = !Contrôleur.JeuxEnPause;
                //FilsTraitement.DémarrerTraitementAvecFin(0.001f, () => {
                  Time.timeScale = Contrôleur.JeuxEnPause ? 0f : 1f;  
                //},"nospamforwaitmenu",1);
                
            });

            if (!FilsTraitement.VérifierFils("Attaque")) {
                Debug.DrawRay(Partageur.Joueur.transform.position, Partageur.Joueur.transform.forward * 1f, Color.red, 0.1f);
                Ray ray = new Ray(Partageur.Joueur.transform.position, Partageur.Joueur.transform.forward);
                RaycastHit hit;
                float distance = 1f;
                if (Physics.Raycast(ray, out hit, distance)) {
                    Entité entitéDevant = hit.collider.GetComponent<Entité>();
                    if (entitéDevant != null) {
                        if (entitéDevant.Endurance.Saturation >= 0.1f) {
                            ÉvénementButtonAppuyer(KeyCode.E, true, () => {
                                FilsTraitement.DémarrerTraitementAvecFin(0.1f, () => {
                                    if (entitéDevant != null) {
                                        Vie.PrendreDommage(Partageur.Joueur, entitéDevant, SacÀDos.ObtenirArticle(Partageur.Joueur).Utiliser(Partageur.Joueur,entitéDevant));
                                    }
                                    Endurance.Fatigué(Partageur.Joueur, EnduranceCoût.ATTAQUER);
                                }, "Attaque", 1);
                            });
                            ÉvénementButtonAppuyer(KeyCode.F,entitéDevant != null, () => {
                                if (entitéDevant.Vocation == EntitéVocation.Coffre ||
                                    entitéDevant.Vocation == EntitéVocation.Marchand) {
                                    if (entitéDevant.Référence["Action"] is Action action) {
                                        action.Invoke();
                                    }
                                }
                            });
                            /*
                             * ÉvénementButtonAppuyer(KeyCode.F,SacÀDos.ObtenirArticle(Partageur.Joueur).Vocation != ArticleVocation.ARME,
                               () => {
                                   SacÀDos.ObtenirArticle(Partageur.Joueur).Utiliser(Partageur.Joueur,Partageur.Joueur);
                               });
                             */
                        }
                    }
                }
            }
            ÉvénementButtonAppuyer(KeyCode.Tab,true, () => {
                FilsTraitement.DémarrerTraitementAvecFin(0x01, () => {
                    Partageur.Joueur.SacÀDos.Navigation(Partageur.Joueur,true); //wierd lol de passer la référence dans le paramétre pour obtenir la référence du référant
                    Debug.Log(Partageur.Joueur.SacÀDos.articles.ToString());
                },"nospamtoogleweapons",0x01);
            });
            if (moveDirection != Vector3.zero) {
                ent.transform.position += moveDirection * (ent.Déplacement.VitesseDéplacement * Time.deltaTime);
            }
        }; 
    }
        public static void ÉvénementButtonAppuyer(KeyCode touche, bool condition, Action callback) {
            if (Input.GetKey(touche) && condition) {
                callback();
            }
        }
        public static void ÉvénementBoutonsAppuyer(KeyCode touche1, KeyCode touche2, bool condition, Action callback) {
            if (Input.GetKey(touche1) && Input.GetKey(touche2) && condition) {
                callback();
            }
        }
    }
}