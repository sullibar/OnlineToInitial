using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using System.Text;

// Siemens
using Sharp7;

namespace WindowsFormsApp1
{
    public class Debug
    {

    }

    internal class Siemens
    {
        // Création du client
        public static S7Client Client = new S7Client();

        /// <summary>
        /// Connexion à la CPU avec les paramètres de configuration associés
        /// Version 1.0
        /// </summary>
        public static string Connection()
        {
            // Result of the function
            var res = -1;

            // Connexion à l'automate
            string sAdresseIp = OnlineToInitial.Properties.Settings.Default.AdresseIp;
            int iRack = Int32.Parse(OnlineToInitial.Properties.Settings.Default.Rack);
            int iSlot = Int32.Parse(OnlineToInitial.Properties.Settings.Default.Slot);

            res = Client.ConnectTo(sAdresseIp, iRack, iSlot);

            // Debug
            if (res != 0)
                return Client.ErrorText(res);
            else
                return "Connection OK"; 

        }

        /// <summary>
        /// Déconnexion à la CPU
        /// </summary>
        public static string Disconnect()
        {
            // Result of the function
            var res = -1;

            // Déconnexion seulement si une connexion est active
            res = Client.Disconnect();

            // Debug
            if (res != 0)
                return Client.ErrorText(res);
            else
                return "Disconnect OK";
        }

        /// <summary>
        /// Lecture d'un DB
        /// Version 1.0
        /// </summary>
        public static byte[] ReadDB(int nbDB, int Size)
        {
            // ??
            var res = -1; // Result of the function
            byte[] Buffer = new byte[10000]; // Buffer 1000 bytes
            int Start; // Parameters of the function            

            // Initialisation
            Start = 0;

            res = Client.DBRead(nbDB, Start, Size, Buffer); // Read DB

            // Debug
            //if (res != 0)
            //    MessageBox.Show("Lecture: " + Client.ErrorText(res));
            //else
            //    MessageBox.Show("Lecture: Lecture OK");

            return Buffer;
        }
    }

    public class GestionAWL
    {
        /// <summary>
        /// ???
        /// </summary>
        public static List<string> ReplaceCharByValues(byte[] buf, List<string> tab)
        {
            // ??
            int offset = 0;
            int cntBool = 0;
            List<string> listTemp = new List<string>();

            for (int i = 0; i < tab.Count; i++)
            {
                // For Bool
                if (tab[i] == "BOOL")
                {
                    bool tempB;
                    //int offsetB = 5;
                    tempB = S7.GetBitAt(buf, offset, cntBool);
                    listTemp.Add(tempB.ToString());

                    cntBool++; // Incrément compteur booléen
                    if (cntBool == 8)
                    {
                        offset++;
                        cntBool = 0;
                    }
                }
                // For Byte
                if (tab[i] == "BYTE")
                {
                    int tempB;
                    tempB = S7.GetByteAt(buf, offset);
                    listTemp.Add("B#16#" + "0");
                    cntBool = 0;

                    offset = offset + 2;
                }
                // For Word
                if (tab[i] == "WORD")
                {
                    int tempW;
                    tempW = S7.GetWordAt(buf, offset);
                    listTemp.Add("W#16#" + "0");
                    cntBool = 0;

                    offset = offset + 2;
                }
                // For Int
                if (tab[i] == "INT")
                {
                    int tempI;
                    tempI = S7.GetIntAt(buf, offset);
                    listTemp.Add(tempI.ToString());
                    cntBool = 0;

                    offset = offset + 2;
                }
                // For DInt
                if (tab[i] == "DINT")
                {
                    double tempDI;
                    tempDI = S7.GetDIntAt(buf, offset);
                    listTemp.Add(tempDI.ToString());
                    cntBool = 0;

                    offset = offset + 4;
                }
                // For Real
                if (tab[i] == "REAL")
                {
                    double tempR;
                    float tempFR, test;
                    tempR = S7.GetRealAt(buf, offset);
                    tempFR = Convert.ToSingle(tempR);
                    test = (float)(Math.Truncate((double)tempFR * 100.0) / 100.0);
                    listTemp.Add(tempR.ToString("0.00"));
                    cntBool = 0;

                    offset = offset + 4;
                }
            }

            return listTemp;
        }
    }

    class Fonctions
    {
        /// <summary>
        /// ???
        /// </summary>
        public static void FonctionBase(List<string> source)
        {
            // ??
        }

        /// <summary>
        /// ???
        /// </summary>
        public static List<string> ExtractDBFromFile(string numDB, List<string> source)
        {
            // Localisation du début et fin du DB
            int iStart = 0;
            int iStop = 0;
            bool iStartOK = false;
            string nameDB;
            nameDB = "DATA_BLOCK DB " + numDB;

            for (int i = 0; i < source.Count; i++)
            {
                if (source[i] == nameDB)
                {
                    iStart = i;
                    iStartOK = true;
                }
                if (source[i] == "END_DATA_BLOCK" && iStartOK)
                {
                    iStop = i + 1;
                    break;
                }
            }

            // Extraction 
            List<string> listTemp = new List<string>();

            while (iStart < iStop)
            {
                listTemp.Add(source[iStart]);
                iStart++;
            }

            return listTemp;
        }

        /// <summary>
        /// ???
        /// </summary>
        public static List<string> ReplaceCharacters(List<string> source, out List<string> typeData2)
        {
            // ??
          
            List<string> listTemp = new List<string>();
            List<string> typeData = new List<string>();
            List<string> valueData = new List<string>();
            int j = 0, k = 0;
            string sTemp;

            // Patern Regex
            string patternInitial = @"    *(\w+) : (\w+) *(?::= (.+?))?;	(.*)";
            string patternInitialReplace = @"(?<lolo>    *(\w+) : (\w+) *)(?::= (.+?))?(?<toto>;	(.*))";
            string patternInitialReplace2 = @"${lolo}:= µlolo${toto}";

            string patternInitialEmpty = @"    *(\w+) : (\w+) ;(.*)";

            string patternEnCours = @"   ([\w\.]+) := (.+);";
            string patternEnCoursReplace = @"(?<lolo>   ([\w\.]+)) := (.+)(?<toto>;)";
            string patternEnCoursReplace2 = @"${lolo}:= µlolo${toto}";

            while (j < source.Count)
            {
                // Initialisation
                sTemp = source[j];

                // Regex valeurs initiales
                Match result = Regex.Match(sTemp, patternInitial);
                Match resultVIE = Regex.Match(sTemp, patternInitialEmpty);
                Match resultVE = Regex.Match(sTemp, patternEnCours);
                GroupCollection data = result.Groups;

                if (result.Success || resultVE.Success)
                {
                    if (result.Success)
                    {

                        typeData.Add(data[2].ToString());
                        string replacedString = Regex.Replace(sTemp, patternInitialReplace, patternInitialReplace2);
                        listTemp.Add(replacedString);
                        k++;
                    }

                    if (resultVE.Success)
                    {
                        string replacedString2 = Regex.Replace(sTemp, patternEnCoursReplace, patternEnCoursReplace2);
                        listTemp.Add(replacedString2);
                    }
                }
                else
                {
                    listTemp.Add(sTemp);
                }

                j++;
            }
            typeData2 = typeData;
            return listTemp;
        }

        /// <summary>
        /// ???
        /// </summary>
        public static int CalculSizeDB(List<string> source)
        {
            // ??
            int j = 0;
            string sTemp;
            string patternInitial = @"    *(\w+) : (\w+) *(?::= (.+?))?;	(.*)";
            string patternEnCours = @"   ([\w\.]+) := (.+);";
            List<string> typeData = new List<string>();

            while (j < source.Count)
            {
                // Initialisation
                sTemp = source[j];

                // Regex valeurs initiales
                Match result = Regex.Match(sTemp, patternInitial);
                Match resultVE = Regex.Match(sTemp, patternEnCours);
                GroupCollection data = result.Groups;

                if (result.Success || resultVE.Success)
                {
                    typeData.Add(data[2].ToString());
                }

                j++;
            }

            // Longueur du DB
            int lngDB = 0;
            int lngBool = 0;
            for (int i = 0; i < typeData.Count; i++)
            {
                if (typeData[i] == "BOOL" && lngBool == 0)
                {
                    lngDB = lngDB + 1;
                    lngBool = lngBool + 1;
                }
                if (typeData[i] == "BOOL" && lngBool > 0)
                {
                    lngBool = lngBool + 1;
                    if (lngBool == 8)
                        lngBool = 0;
                }
                if (typeData[i] == "BYTE")
                {
                    lngDB = lngDB + 1;
                    lngBool = 0;
                }
                if (typeData[i] == "WORD")
                {
                    lngDB = lngDB + 1;
                    lngBool = 0;
                }
                if (typeData[i] == "INT")
                {
                    lngDB = lngDB + 2;
                    lngBool = 0;
                }
                if (typeData[i] == "DINT")
                {
                    lngDB = lngDB + 4;
                    lngBool = 0;
                }
                if (typeData[i] == "REAL")
                {
                    lngDB = lngDB + 4;
                    lngBool = 0;
                }
            }

            return lngDB;
        }

        /// <summary>
        /// ???
        /// </summary>
        public static void ReadDB(List<string> source)
        {
            // ??
        }

    }
}
