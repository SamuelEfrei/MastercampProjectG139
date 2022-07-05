using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace MastercampProjectG139
{
    internal class NumeroINSEE
    {
        /// <summary>
        /// Constante pour le calcul de la clé
        /// </summary>
        private const Int16 CLE_VERIF = 97;
        /// <summary>
        /// Nombre de caractère du numéro INSEE
        /// </summary>
        private const Int16 NB_CARACTERES = 13;


        #region "Constructeur"
        /// <summary>
        /// Constructeur par défaut
        /// </summary>
        public NumeroINSEE()
        {
        }
        #endregion

        #region "Méthodes publiques"
        /// <summary>
        /// Verifie le numéro INSEE passé en paramètre (numero + clé)
        /// </summary>
        /// <param name="strNumero">Numéro INSEE</param>
        /// <param name="strCle">Clé de verification du numéro INSEE</param>
        /// <returns>True si le numéro et la clé sont cohérents, sinon false</returns>
        public bool VerifierINSEE(string strNumero, string strCle)
        {
            if (CalculerCleINSEE(strNumero).ToString() == strCle)
                return true;
            else
                return false;
        }

        /// <summary>
        /// Verifie le numéro INSEE passé en paramètre
        /// </summary>
        /// <param name="strNumero">Numéro INSEE avec la clé</param>
        /// <returns>True si le numéro et la clé sont cohérent, sinon false</returns>
        public bool VerifierINSEE(string strNumero)
        {
            string strCle = "";
            strNumero = NettoyerString(strNumero);

            try
            {
                strCle = strNumero.Remove(0, NB_CARACTERES);
                strNumero = strNumero.Remove(NB_CARACTERES);
            }
            catch
            {
                // il manque des caractères
                return false;
            }

            return VerifierINSEE(strNumero, strCle);
        }

        /// <summary>
        /// Calcul la clé correspondante au numéro INSEE passé en paramètre
        /// </summary>
        /// <param name="strNumero">Numero INSEE</param>
        /// <returns>Clé du numéro INSEE passé en paramètre, 0 si numéro invalide</returns>
        public Int16 CalculerCleINSEE(string strNumero)
        {
            // clé retournée
            Int16 cle = 0;
            // numéro apres convertion
            Int64 numero = NumeroEnInt(strNumero);

            if (numero != 0)
            {
                cle = (short)(CLE_VERIF - (numero % CLE_VERIF));
            }

            return cle;
        }
        #endregion

        #region "Méthodes privées"
        /// <summary>
        /// Enlève les caractères ne pouvant faire partie du numéro
        /// A-Z0-9 uniquement
        /// </summary>
        /// <param name="strNumero">Numéro INSEE</param>
        /// <returns>Retourne la chaîne épurée</returns>
        private string NettoyerString(string strNumero)
        {
            strNumero = strNumero.ToUpper();
            Regex regINSEE = new Regex("[^A-Z0-9_]");
            strNumero = regINSEE.Replace(strNumero, "");

            return strNumero;
        }

        /// <summary>
        /// Convertion du numéro (string) en entier
        /// </summary>
        /// <param name="strNumero">Numéro INSEE</param>
        /// <returns>Retourne le numéro INSEE sous forme d'un entier, 0 si numéro invalide</returns>
        private Int64 NumeroEnInt(string strNumero)
        {
            // le numero apres convertion
            long numero = 0;

            // Pour les Corses !
            // Emplacement de la lettre pour les corses
            const Int16 INDICE_LETTRE_CORSE = 6;
            // Constante pour calcul Corse 2A
            const Int32 CORSEA = 1000000;
            // Constante pour calcul Corse 2B
            const Int32 CORSEB = 2000000;

            strNumero = NettoyerString(strNumero);

            // le numero doit faire NB_CARACTERES sinon c'est pas
            // la peine d'aller plus loin
            if (strNumero.Length != NB_CARACTERES)
                return numero;

            // convertion en entier, si la chaîne ne peut etre convertie
            // soit une erreur, soit un Corse...
            if (!long.TryParse(strNumero, out numero))
            {
                // verification du 7eme caractère
                if (strNumero[INDICE_LETTRE_CORSE] == 'A')
                {
                    // un Corse du Sud
                    strNumero = strNumero.Replace('A', '0');
                    if (long.TryParse(strNumero, out numero))
                    {
                        numero -= CORSEA;
                    }
                }
                else if (strNumero[INDICE_LETTRE_CORSE] == 'B')
                {
                    // Haute Corse
                    strNumero = strNumero.Replace('B', '0');
                    if (long.TryParse(strNumero, out numero))
                    {
                        numero -= CORSEB;
                    }
                }
            }

            return numero;
        }
        #endregion
    }
}
