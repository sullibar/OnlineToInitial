using Sharp7;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {


        // Tableau contenant le fichier principal
        public static List<string> arALL = new List<string>();

        // Création du client
        public static S7Client Client = new S7Client();

        public static bool ecriture1;

        public static bool ecriture2;

        public static bool ecriture3;

        public static bool ecriture4;

        public static int compteur;

        public static List<string> listToto = new List<string>();

        public static int lngDB;

        public static int progress;

        public static List<string> selectList = new List<string>();

        // Compteurs
        private DateTime cnt1;
        private TimeSpan cnt2;
        public static bool go;

        // Ouvre le fichier contenant les sources
        public static string strAppDir = Path.GetDirectoryName(Assembly.GetExecutingAssembly().GetModules()[0].FullyQualifiedName);

        // Tableaux pour le traitement des données
        public static List<string> typeData = new List<string>();

        public static List<string> valueData = new List<string>();

        public static List<ListViewItem> m_lstItem;

        //private System.IO.StreamReader file =
        //                    new System.IO.StreamReader(strAppDir + @"\resources\ALL.AWL", System.Text.Encoding.GetEncoding("iso-8859-1"), true);

        // Constantes
        const int FirstDB = 200;
        const int LastDB = 400;

       
        public Form1()
        {
            InitializeComponent();

            // Initialisation de la tâche de fond
            bw.WorkerReportsProgress = true;
            bw.WorkerSupportsCancellation = true;
            bw.DoWork += bw_DoWork;
            bw.RunWorkerCompleted += bw_RunWorkerCompleted;
            bw.ProgressChanged += bw_ProgressChanged;

            // ??
            lblInfo.Text = "";
        }

        private void bw_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            BackgroundWorker worker = sender as BackgroundWorker;
            

            // ****************************
            // TEST
            // ****************************
            int numDB;
            string sTemp;

            // DB sélectionnés

            if (ecriture1)
            {
                ecriture1 = false;
                compteur = 0;
                listToto.Clear();
                if (selectList.Count == 1)
                {
                    for (int i = 0; i < 1; i++)
                    {
                        sTemp = selectList[i];
                        sTemp = sTemp.Substring(14);
                        numDB = Int32.Parse(sTemp);

                        listToto = Fonctions.ExtractDBFromFile(sTemp, arALL);
                    }
                    
                    // Progression dans la lecture
                    //bw.ReportProgress(listToto.Count, "100");
                }
            }

            // ****************************
            // TEST 2
            // ****************************
            List<string> listTemp = new List<string>();
            List<string> valuesList = new List<string>();
            List<string> typeList = new List<string>();
            int iTemp, k = 0, nbLecture = 0;

            if (ecriture2)
            {
                // Compteur
                cnt1 = DateTime.Now;
                go = true;
                compteur = 0;

                ecriture2 = false;
                listToto.Clear();
                nbLecture = selectList.Count;
                progress = 0;
                for (int i = 0; i < selectList.Count; i++)
                {
                    
                    sTemp = selectList[i];
                    sTemp = sTemp.Substring(14);
                    numDB = Int32.Parse(sTemp);

                    listTemp = Fonctions.ExtractDBFromFile(sTemp, arALL);
                    iTemp = Fonctions.CalculSizeDB(listTemp);
                    listTemp = Fonctions.ReplaceCharacters(listTemp, out typeList);

                    // Lecture DB
                    int size = iTemp;
                    byte[] Buffer = new byte[size];
                    Buffer = Siemens.ReadDB(numDB, size);
                    valuesList = GestionAWL.ReplaceCharByValues(Buffer, typeList);

                    // Doublage des valeurs
                    int limit = valuesList.Count;
                    for (int j = 0; j < limit; j++)
                    {
                        valuesList.Add(valuesList[j]);
                    }

                    for (int j = 0; j < listTemp.Count; j++)
                    {
                        // Replace uLolo
                        string s = listTemp[j];
                        if (s.Contains("µlolo"))
                        {
                            StringBuilder b = new StringBuilder(s);
                            b.Replace("µlolo", valuesList[k]);
                            //listBox2.Items.Add(b);
                            listToto.Add(b.ToString());
                            k++;
                        }
                        else
                        {
                            //listBox2.Items.Add(listTemp[j]);
                            listToto.Add(listTemp[j]);
                        }
                    }

                    // Progression dans la lecture
                    // Progressbar
                    if (i > 0)
                        progress = Convert.ToInt32((i*100) / nbLecture);
                    if (i == (nbLecture-1))
                        progress = 100;
                    //bw.ReportProgress(listToto.Count);
                    bw.ReportProgress(progress);

                    // Nettoyage
                    valuesList.Clear();
                    typeList.Clear();
                    listTemp.Clear();
                    k = 0;

                    
                    //progress++;
                }
                              
            }

            
        }

        private void bw_RunWorkerCompleted(object sender, System.ComponentModel.RunWorkerCompletedEventArgs e)
        {
            go = false;
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            Siemens.Disconnect();
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            // DB sélectionnés
            string sTemp;
            int numDB = 0;
            List<string> listTemp = new List<string>();
            List<string> selectedList = new List<string>();

            // A GARDER
            selectList.Clear();
            foreach (var item in listBox1.SelectedItems)
            {
                selectList.Add(item.ToString());
            }

            if (selectedList.Count > 1)
                return;

            listBox2.Items.Clear();

            
            for (int i = 0; i < selectedList.Count; i++)
            {
                sTemp = selectedList[i];
                sTemp = sTemp.Substring(14);
                numDB = Int32.Parse(sTemp);

                listTemp = Fonctions.ExtractDBFromFile(sTemp, arALL);

                for (int j = 0; j < listTemp.Count; j++)
                {
                    listBox2.Items.Add(listTemp[j]);
                }
            }
            ecriture1 = true;

        }

        private void timer_Tick(object sender, EventArgs e)
        {
            bool threadActif = bw.IsBusy;

            if (!threadActif)
                bw.RunWorkerAsync();

            // Refresh progress bar
            //progressBar1.Value = (int)progress;
        }

        private void bw_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            // Fill the listbox during the background task
            for (int j = compteur; j < listToto.Count; j++)
            {
                listBox2.Items.Add(listToto[j]);
            }

            compteur = listToto.Count;

            listBox2.EndUpdate();

            progressBar1.Value = (int)e.ProgressPercentage;
           
        }

        private void btnOpenFileAwl_Click(object sender, EventArgs e)
        {
            DialogResult result = openFileDialog1.ShowDialog();
            if (result == DialogResult.OK)
            {
                System.IO.StreamReader sr = new
                         System.IO.StreamReader(openFileDialog1.FileName, System.Text.Encoding.GetEncoding("iso-8859-1"), true);

                string line;
                arALL.Clear();
                while ((line = sr.ReadLine()) != null)
                {
                    arALL.Add(line);
                }

                lblInfo.Text = "Fichier chargé";
                sr.Close();

                string s1;
                string patternInitial = @"DATA_BLOCK DB";
                List<string> sulli = new List<string>();
                string sTemp;
                int numDB = 0;
                bool nextStep = false;

                listBox1.Items.Clear();

                for (int i = 0; i < arALL.Count; i++)
                {                   
                    s1 = arALL[i];
                    Match ok = Regex.Match(s1, patternInitial);
                    if (ok.Success)
                    {
                        // Numéro du DB
                        sTemp = arALL[i];
                        sTemp = sTemp.Substring(14);
                        numDB = Int32.Parse(sTemp);

                        if ((numDB > FirstDB) && (numDB < LastDB))
                            nextStep = true;

                        if (nextStep)
                        {
                            listBox1.Items.Add(arALL[i]);
                            nextStep = false;
                        }
                            
                    }                        
                }
            }
        }

        private void btnConnection_Click(object sender, EventArgs e)
        {
            lblInfo.Text = Siemens.Connection();
        }

        private void btnExtract_Click(object sender, EventArgs e)
        {
            // Listes
            List<string> listTemp = new List<string>();
            List<string> valuesList = new List<string>();
            List<string> typeList = new List<string>();

            // DB sélectionnés
            selectList.Clear();
            listBox2.Items.Clear();
            List<string> selectedList = new List<string>();
            foreach (var item in listBox1.SelectedItems)
            {
                selectList.Add(item.ToString());
            }

            ecriture2 = true;
        }

        private void btnWriteFile_Click(object sender, EventArgs e)
        {
            // Création du fichier
            System.IO.StreamWriter resultFile =
                new System.IO.StreamWriter(strAppDir + @"\resources\" + "ALL.txt");

            for (int i = 0; i < listBox2.Items.Count; i++)
            {
                resultFile.WriteLine(listBox2.Items[i]);
            }

            // Fermeture du fichier
            resultFile.Close();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnSetting_Click(object sender, EventArgs e)
        {
            Form2 form2 = new Form2();
            form2.ShowDialog();
        }
    }
}