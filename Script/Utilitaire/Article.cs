using System;
using JetBrains.Annotations;
using Script.Utilitaire.titre;
using UnityEngine;

namespace Script.Utilitaire {
    public class Article {
        public ArticleVocation Vocation  { get; set; }
        public string Nom { get; set; }
        public string Description { get; set; }
        [CanBeNull] public int InformationRetournée { get; set; } = 5;
        public Action<Article,Entité,Entité> Utilisation { get; set; }
        public Sprite Model { get; set; }
        public Article(ArticleVocation vocation, string nom, string description, Sprite model) {
            Vocation = vocation;
            Nom = nom;
            Description = description;
            Model = model;
        }
        [CanBeNull] public int Utiliser(Entité attaquant,Entité victime) {
            Utilisation?.Invoke(this,attaquant,victime);
            return InformationRetournée;
        }
    }
}