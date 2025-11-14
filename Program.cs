using System;
using System.Collections.Generic;
using System.IO;

namespace ProjetFichier
{
    public struct Client
    {
        public int NumeroClient;
        public string Nom;      // MAJUSCULE
        public string Prenom;   // Première lettre majuscule
        public string Telephone;

        public Client()
        {
            NumeroClient = 0;
            Nom = string.Empty;
            Prenom = string.Empty;
            Telephone = string.Empty;
        }
        public void WriteTo(BinaryWriter bw)
        {
            bw.Write(NumeroClient);
            bw.Write(Nom ?? "");
            bw.Write(Prenom ?? "");
            bw.Write(Telephone ?? "");
        }

        public static Client ReadFrom(BinaryReader br)
        {
            return new Client
            {
                NumeroClient = br.ReadInt32(),
                Nom = br.ReadString(),
                Prenom = br.ReadString(),
                Telephone = br.ReadString()
            };
        }
    }


    internal class Program
    {
        private static object br;

        static void Main(string[] args)
        {
            Console.WriteLine("Hello World");
        }







        public static List<Client> LireTousLesClients(string cheminFichier)
        {
            var clients = new List<Client>(); 

            if (!File.Exists(cheminFichier))
            {
                Console.WriteLine("Le fichier n'existe pas.");
                return clients;
            }

            using (var reader = new BinaryReader(File.Open(cheminFichier, FileMode.Open, FileAccess.Read)))
            {
                while (reader.BaseStream.Position < reader.BaseStream.Length)
                {
                    
                    clients.Add(Client.ReadFrom(reader));
                }
            }

            return clients; 
        }


        public static void AfficherClientParNom()
        {
            string LeFichier = @"C:\Users\leona\source\repos\ClientFichier.bin";
            string NomRecherche = Majuscule(Console.ReadLine());



            if (!File.Exists(LeFichier))
            {
                Console.WriteLine("Le fichier n'existe pas.");
                return;
            }
            Console.Write("Entrez le nom du client rechercher");
            List<Client> clients = LireTousLesClients(LeFichier);
          
            
            
            List<Client> ClientsTrouves = clients.FindAll(c => c.Nom == NomRecherche);

            if (ClientsTrouves.Count == 0)
            {

                Console.WriteLine("Aucun client trouvé avec ce nom.");

            }
            else
            {
                foreach (Client c in ClientsTrouves)
                {
                    int index = clients.IndexOf(c); // Récupère l'index dans la liste complète
                    Console.WriteLine($"Fiche n°{index + 1} :");
                    Console.WriteLine($"  Numéro : {c.NumeroClient}");
                    Console.WriteLine($"  Nom : {c.Nom}");
                    Console.WriteLine($"  Prénom : {c.Prenom}");
                    Console.WriteLine($"  Téléphone : {c.Telephone}\n");
                }
            }

        }

        public static string FirstMajuscule(string prenom)
        {
            if (string.IsNullOrWhiteSpace(prenom)) return string.Empty;
            prenom = prenom.Trim();
            char first = char.ToUpperInvariant(prenom[0]);
            string rest = prenom.Length > 1 ? prenom.Substring(1).ToLowerInvariant() : string.Empty;
            return first + rest;
        }

        public static string Majuscule(string Nom )

        {  
            return Nom.ToUpper(); 


        }

       



        static void EcrireFichier()
        {

            //Ouvrir un fichier non binaire
            StreamWriter writer = null;
            int valeur;
            string LeFichier = @"C:\Users\leona\source\repos\GestionFichierMonFichier.txt";

            try
            {
                writer = new StreamWriter(LeFichier);
                Console.WriteLine("Fichier ouvert ");
                Console.WriteLine("Fichier ouvert. Saisissez des entiers (0 pour arrêter) :");
                valeur = int.Parse(Console.ReadLine());

                while (valeur != 0)
                {
                    writer.WriteLine($"{valeur} ");
                    valeur = int.Parse(Console.ReadLine());

                }
                Console.WriteLine("Ecriture effectuée");





            }
            catch (Exception e)
            {
                Console.WriteLine("Erreur" + e.Message);
            }
            finally
            {
                if (writer != null)
                    writer.Close();
                Console.WriteLine("Fichier fermer");


            }





        }

        static void LireFichier()
        {
            List<int> listInt = new List<int>();
            string UnLigne;
            int somme;
            StreamReader Reader = null;
            string LeFichier = @"C:\Users\leona\source\repos\GestionFichierMonFichier.txt";



            try
            {
                Reader = new StreamReader(LeFichier);
                Console.WriteLine("ouvertur du ficher a lire ");
                while ((UnLigne = Reader.ReadLine()) != null)
                {
                    if (int.TryParse(UnLigne, out somme))
                    {
                        listInt.Add(somme);
                        Console.WriteLine($"{somme}");
                    }
                }
                Console.WriteLine($"Nombre d'entier : {listInt.Count}");
                Console.WriteLine("Voici les valeurs");

                foreach (int Unesomme in listInt)
                {
                    Console.WriteLine($"{Unesomme}");

                }

            }
            catch (Exception e)
            {
                Console.WriteLine("Erreur" + e.Message);


            }
            finally
            {
                if (Reader != null)
                    Reader.Close();
                Console.WriteLine("Fichier fermer");
            }


        }
        static void EcrireBinaireAvecBitConverter()
        {
            string cheminFichier = @"C:\Users\leona\source\repos\GestionFichier\MesDataBin.txt";
            FileStream fs = null;

            try
            {
                fs = new FileStream(cheminFichier, FileMode.Create);
                Console.WriteLine("Fichier binaire ouvert. Saisissez des entiers (0 pour arrêter) :");

                int valeur;
                while (true)
                {
                    Console.Write("Entrez un entier : ");
                    valeur = int.Parse(Console.ReadLine());

                    if (valeur == 0)
                        break;

                    // Convertir l'entier en tableau d'octets
                    byte[] bytes = BitConverter.GetBytes(valeur);
                    // Écrire les octets dans le fichier
                    fs.Write(bytes, 0, bytes.Length);
                }

                Console.WriteLine("Écriture terminée.");
            }
            catch (Exception e)
            {
                Console.WriteLine("Erreur : " + e.Message);
            }
            finally
            {
                if (fs != null)
                    fs.Close();
            }
        }
    }
}