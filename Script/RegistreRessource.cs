using System;
using UnityEngine;

namespace Script {
    public class RegistreRessource : MonoBehaviour {
        [SerializeField] Sprite ÉpéeEnBois;
        [SerializeField] Sprite ÉpéeEnFeux;
        [SerializeField] Sprite ÉpéeEnDistorsion;
        [SerializeField] Sprite ÉpéeRédemption;
        [SerializeField] Sprite ÉpéeExcalibur;
        [SerializeField] Sprite ÉpéeSlimeMeSlimer;
        [SerializeField] private Sprite PotionWadiwasi;
        public static Sprite GlobalÉpéeEnBois;
        public static Sprite GlobalÉpéeEnFeux;
        public static Sprite GlobalÉpéeEnDistorsion;
        public static Sprite GLobalÉpéeRédemption;
        public static Sprite GlobalÉpéeExcalibur;
        public static Sprite GlobalÉpéeSlimeMeSlimer;
        public static Sprite GlobalPotionWadiwasi;
        protected void Start() {
            GlobalÉpéeEnBois = ÉpéeEnBois;
            GlobalÉpéeEnFeux = ÉpéeEnFeux;
            GlobalÉpéeEnDistorsion = ÉpéeEnDistorsion;
            GLobalÉpéeRédemption = ÉpéeRédemption;
            GlobalÉpéeExcalibur = ÉpéeExcalibur;
            GlobalÉpéeSlimeMeSlimer = ÉpéeSlimeMeSlimer;
            GlobalPotionWadiwasi = PotionWadiwasi;
        }
    }
}