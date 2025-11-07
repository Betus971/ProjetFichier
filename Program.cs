using System;

namespace ProjetFichier
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World");
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