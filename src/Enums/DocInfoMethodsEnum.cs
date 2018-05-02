namespace Bizy.OuinneBiseSharp.Enums
{
    using System.ComponentModel;

    public enum DocInfoMethodsEnum
    {
        /// <summary>
        ///     Sales amount
        /// </summary>
        [Description("vente chiffre affaire")]
        VenteChiffreAffaire,

        /// <summary>
        ///     Sales amount by document category
        /// </summary>
        [Description("vente chiffre affaire categorie")]
        VenteChiffreAffaireCategorie,

        /// <summary>
        ///     Sales amount by item group
        /// </summary>
        [Description("vente chiffre affaire groupe article")]
        VenteChiffreAffaireGroupeArticle,

        /// <summary>
        ///     Sales amount by payment method
        /// </summary>
        [Description("vente chiffre affaire paiement methode")]
        VenteChiffreAffairePaiementMethode,

        /// <summary>
        ///     Sales amount by payment method type
        /// </summary>
        [Description("vente chiffre affaire paiement type")]
        VenteChiffreAffairePaiementType,

        /// <summary>
        ///     Margin on the sales amount
        /// </summary>
        [Description("vente chiffre affaire marge")]
        VenteChiffreAffaireMarge,

        /// <summary>
        ///     Margin by group on the sales amount
        /// </summary>
        [Description("vente marge groupe")]
        VenteMargeGroupe,

        /// <summary>
        ///     Purchases amount by item group
        /// </summary>
        [Description("achat montant total groupe article")]
        AchatMontantTotalGroupeArticle,

        /// <summary>
        ///     Purchase amount
        /// </summary>
        [Description("achat montant total")]
        AchatMontantTotal,

        /// <summary>
        ///     Purchase amount by category
        /// </summary>
        [Description("achat montant total categorie")]
        AchatMontantTotalCategorie
    }
}