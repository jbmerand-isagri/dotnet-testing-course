using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;

namespace OC.Exercice.UnitTests
{
    [TestClass]
    public class JeuTests
    {
        [TestMethod]
        [Description("Etant donné un tour de jeu, lorsque j'affronte un monstre faible et que j'ai un lancer supérieur au second et du Soleil, alors le résultat est gagné avec un point sans perdre de points de vie")]
        public void Tour_AvecUnDeSuperieurAuSecond_EtUnMonstreFaible_EtDuSoleil_RetourneGagneAvecUnPointEtSansPerdreDePointsDeVie()
        {
            // Arrange
            var fournisseurMeteo = Mock.Of<IFournisseurMeteo>();
            var fabriqueDeMonstres = Mock.Of<IFabriqueDeMonstres>();

            Mock.Get(fournisseurMeteo).Setup(f => f.QuelTempsFaitIl()).Returns(Meteo.Soleil);
            Mock.Get(fabriqueDeMonstres).Setup(fa => fa.GetMonstres()).Returns(new Monstre[] { new Monstre() });

            Jeu jeu = new Jeu(fournisseurMeteo, fabriqueDeMonstres);

            // Act
            var resultat = jeu.Tour(6, 1);

            // Assert
            resultat.Should().Be(Resultat.Gagne);
            jeu.Heros.Points.Should().Be(1);
            jeu.Heros.PointDeVies.Should().Be(15);
        }

        [TestMethod]
        [Description("Etant donné un tour de jeu, lorsque j'affronte un monstre faible et que j'ai un lancer égal au second et du Soleil, alors le résultat est gagné avec un point sans perdre de points de vie")]
        public void Tour_AvecUnDeEgalAuSecond_EtUnMonstreFaible_EtDuSoleil_RetourneGagneAvecUnPointEtSansPerdreDePointsDeVie()
        {
            // Arrange
            var fournisseurMeteo = Mock.Of<IFournisseurMeteo>();
            var fabriqueDeMonstres = Mock.Of<IFabriqueDeMonstres>();

            Mock.Get(fournisseurMeteo).Setup(f => f.QuelTempsFaitIl()).Returns(Meteo.Soleil);
            Mock.Get(fabriqueDeMonstres).Setup(fa => fa.GetMonstres()).Returns(new Monstre[] { new Monstre() });

            Jeu jeu = new Jeu(fournisseurMeteo, fabriqueDeMonstres);

            // Act
            var resultat = jeu.Tour(5, 5);

            // Assert
            resultat.Should().Be(Resultat.Gagne);
            jeu.Heros.Points.Should().Be(1);
            jeu.Heros.PointDeVies.Should().Be(15);
        }

        [TestMethod]
        [Description("Etant donné un tour de jeu, lorsque j'affronte un monstre faible et que j'ai un lancer inférieur au second et du soleil, alors le résultat est perdu, sans points et en perdant des points de vie")]
        public void Tour_AvecUnDeInferieurAuSecond_EtUnMonstreFaible_EtDuSoleil_RetournePerduSansPointEnPerdantDesPointsDeVie()
        {
            // Arrange
            var fournisseurMeteo = Mock.Of<IFournisseurMeteo>();
            var fabriqueDeMonstres = Mock.Of<IFabriqueDeMonstres>();

            Mock.Get(fournisseurMeteo).Setup(f => f.QuelTempsFaitIl()).Returns(Meteo.Soleil);
            Mock.Get(fabriqueDeMonstres).Setup(fa => fa.GetMonstres()).Returns(new Monstre[] { new Monstre() });
            Jeu jeu = new Jeu(fournisseurMeteo, fabriqueDeMonstres);

            // Act
            var resultat = jeu.Tour(2, 4);

            // Assert
            resultat.Should().Be(Resultat.Perdu);
            jeu.Heros.Points.Should().Be(0);
            jeu.Heros.PointDeVies.Should().Be(13);
        }

        [TestMethod]
        [Description("Etant donné un tour de jeu, lorsque j'affronte un monstre faible et que j'ai un lancer inférieur au second et de la pluie, alors le résultat est perdu, sans points et en classiquement des points de vie")]
        public void Tour_AvecUnDeInferieurAuSecond_EtDeLaPluie_RetournePerduSansPointEnPerdantClassiquementDesPointsDeVie()
        {
            // Arrange
            var fournisseurMeteo = Mock.Of<IFournisseurMeteo>();
            Mock.Get(fournisseurMeteo).Setup(m => m.QuelTempsFaitIl()).Returns(Meteo.Pluie);
            var fabriqueDeMonstres = Mock.Of<IFabriqueDeMonstres>();
            Mock.Get(fabriqueDeMonstres).Setup(fa => fa.GetMonstres()).Returns(new Monstre[] { new Monstre() });
            Jeu jeu = new Jeu(fournisseurMeteo, fabriqueDeMonstres);

            // Act
            var resultat = jeu.Tour(2, 4);

            // Assert
            resultat.Should().Be(Resultat.Perdu);
            jeu.Heros.Points.Should().Be(0);
            jeu.Heros.PointDeVies.Should().Be(13);
        }

        [TestMethod]
        [Description("Etant donné un tour de jeu, lorsque j'affronte un monstre faible et que j'ai un lancer inférieur au second et du vent, alors le résultat est perdu, sans points et en perdant deux fois plus de points de vie")]
        public void Tour_AvecUnDeInferieurAuSecond_EtUnMonstreFaible_EtDeLaPluie_RetournePerduSansPointEnPerdantDeuxFoisPlusDePointsDeVie()
        {
            // Arrange
            var fournisseurMeteo = Mock.Of<IFournisseurMeteo>();
            Mock.Get(fournisseurMeteo).Setup(m => m.QuelTempsFaitIl()).Returns(Meteo.Tempete);
            var fabriqueDeMonstres = Mock.Of<IFabriqueDeMonstres>();
            Mock.Get(fabriqueDeMonstres).Setup(fa => fa.GetMonstres()).Returns(new Monstre[] { new Monstre() });
            Jeu jeu = new Jeu(fournisseurMeteo, fabriqueDeMonstres);

            // Act
            var resultat = jeu.Tour(2, 4);

            // Assert
            resultat.Should().Be(Resultat.Perdu);
            jeu.Heros.Points.Should().Be(0);
            jeu.Heros.PointDeVies.Should().Be(11);
        }
    }
}