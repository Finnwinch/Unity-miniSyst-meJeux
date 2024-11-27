using System.Collections.Generic;
using Script.Moteur;
using Script.Utilitaire;
using Script.Utilitaire.titre;
using UnityEngine;

namespace Script {
    public class RegistreArticle {
        public static Dictionary<string,Article> Articles;
        public static List<Article> coffres;
        public static void Initialisation() {
            Articles = new Dictionary<string, Article>();
            coffres = new List<Article>();
            Articles["poignts"] = new Article(ArticleVocation.ARME, "Épée Bois", "tu as des mains? bah défend toi!", RegistreRessource.GlobalÉpéeEnBois);
            Articles["poignts"].Utilisation = (me,attaquant,victime) => {
                me.InformationRetournée = 0x05;
            };
            Articles["ÉpéeFeux"] = new Article(ArticleVocation.ARME, "Épée Feux", "inflige plein dégâts",
                RegistreRessource.GlobalÉpéeEnFeux);
            Articles["ÉpéeFeux"].Utilisation = (me, attaquant, victime) => {
                FilsTraitement.DémarrerTraitementAvecFin(0x01, () => {
                    Vie.PrendreDommage(attaquant,victime,0x15);
                },$"{victime.name}fireAttack",0x05);
                me.InformationRetournée = 0x15;
            };
            Articles["ÉpéeDistorsion"] = new Article(ArticleVocation.ARME, "Épée Distorsion", "inflige peut de dégâts, mais vous protége",
                RegistreRessource.GlobalÉpéeEnDistorsion);
            Articles["ÉpéeDistorsion"].Utilisation = (me, attaquant, victime) => {
                victime?.RéférentDuCorps.AddForce((victime.transform.position - attaquant.transform.position).normalized * 30f,ForceMode.Impulse);
                me.InformationRetournée = 0x1;
            };
            Articles["ÉpéeRédemption"] = new Article(ArticleVocation.ARME, "Épée Rédemption", "inflige peut de dégâts, mais vous donne de la vie",
                RegistreRessource.GLobalÉpéeRédemption);
            Articles["ÉpéeRédemption"].Utilisation = (me, attaquant, victime) => {
                attaquant.Vie.Saturation += 15;
                me.InformationRetournée = 0x15;
            };
            Articles["ÉpéeExcalibur"] = new Article(ArticleVocation.ARME, "Épée Excalibur", "inflige 300 dégâts",
                RegistreRessource.GlobalÉpéeExcalibur);
            Articles["ÉpéeExcalibur"].Utilisation = (me, attaquant, victime) => {
                me.InformationRetournée = 0x300;
            };
            Articles["ÉpéeSlimeMeSlimer"] = new Article(ArticleVocation.ARME, "Épée SlimeMeSlimer", "bloquer l'ennemie pendnet 5 sec",
                RegistreRessource.GlobalÉpéeSlimeMeSlimer);
            Articles["ÉpéeSlimeMeSlimer"].Utilisation = (me, attaquant, victime) => {
                float vitesseActuel = victime.Déplacement.VitesseDéplacement;
                victime.Déplacement.VitesseDéplacement = 0f;
                me.InformationRetournée = 0x10;
            };
            Articles["PotionWadiwasi"] = new Article(ArticleVocation.POTION, "Potion Wadiwasi", "Vitesse 1",
                RegistreRessource.GlobalPotionWadiwasi);
            Articles["PotionWadiwasi"].Utilisation = (me, attaquant, victime) => {
                if (!FilsTraitement.VérifierFils("vitesse")) {
                    attaquant.Déplacement.VitesseDéplacement *= 2;
                    FilsTraitement.DémarrerTraitementAvecFin(5f, () => {
                        attaquant.Déplacement.VitesseDéplacement /= 2;
                    },"vitesse",1);
                }
            };
            coffres.Add(Articles["ÉpéeFeux"]);
            coffres.Add(Articles["ÉpéeDistorsion"]);
            coffres.Add(Articles["ÉpéeRédemption"]);
            coffres.Add(Articles["ÉpéeExcalibur"]);
            coffres.Add(Articles["ÉpéeSlimeMeSlimer"]);
            coffres.Add(Articles["PotionWadiwasi"]);
        }

        public static Article ObtenirCoffreHasard() {
            return coffres[Random.Range(0,coffres.Count)];
        }
    }
}