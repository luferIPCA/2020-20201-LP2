﻿/**
 * Generic classes
 * https://www.tutorialsteacher.com/csharp/csharp-generics
 * 
 * Generic Collections
 * https://msdn.microsoft.com/en-us/library/system.collections.generic.aspx
 * List<T>
 * Dictionary<k,T>
 * HashSet<T>
 * LinkedList<T>
 * Queue<T>
 * Stack<T>
 * SortedList<TKey, TValue>
 * 
 * IComparable versus IComparer
 * https://support.microsoft.com/pt-pt/help/320727/how-to-use-the-icomparable-and-icomparer-interfaces-in-visual-c
 * lufer
 * */

using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Serialization;

namespace Generics_II
{
    class Program
    {
        /// <summary>
        /// Auxiliar
        /// </summary>
        /// <param name="p"></param>
        private static void Mostra(Product p)
        {
            Console.WriteLine(p.name);
        }

        static void Main(string[] args)
        {

            #region List<T>

            List<int> inteiros = new List<int>();
            inteiros.Add(1);

            //inteiros.Add("1");

            List<Product> products = new List<Product>();
            
            // Criar Lista
            products.Add(new Product() { name = "Mesa" });      //0 //3
            products.Add(new Product() { name = "Cadeira" });   //1 //1

            products.Add(new Product() { name = "" });          //2 //0
            Product p1 = new Product() { name = "Candeeiro" };  //3 //2
            
            if (!products.Contains(p1))
                products.Add(p1);

            int i = products.IndexOf(p1);
            //products[i].name


            //Percorre Lista
            Console.WriteLine("\nAntes:");
            foreach (Product p in products)
            {
                Console.WriteLine(p.name);
            }

            //Sort(IComparer<T> p)
            
            //products.Sort(new Product());
            //ou
            //IComparable
            products.Sort();

            Console.WriteLine("\nDepois:");
            foreach (Product p in products)
            {
                Console.WriteLine(p.name);
            }

            Console.ReadKey();

            #region Serializing_to_XML
            //Enviar dados da Lista para ficheiro XML
            XmlSerializer serializer = new XmlSerializer(typeof(List<Product>));
            TextWriter textWriter = new StreamWriter(@"c:\temp\ListProductsSerialized.xml");
            serializer.Serialize(textWriter, products);
            textWriter.Close();
            textWriter.Dispose();

            //Deserialize


            #endregion


            #endregion

            #region Dictionary

            #region UM

            //Hashtable ht = new Hashtable();
            //ht.Add(1, "ola");
            //ht.Add("ola", 1);

            //Dicionário de string->int
            Dictionary<string, int> dictionary = new Dictionary<string, int>();
   
            dictionary.Add("O", 2);
            dictionary.Add("Benfica", 1);
            dictionary.Add("É", 0);
            dictionary.Add("o maior", -1);
            //dictionary.Add("ok", "ola");  //Erro?

            //Existe
            if (dictionary.ContainsKey("Benfica"))
            {
                int value = dictionary["Benfica"];
                Console.WriteLine(value);
            }

            //TryGetValues
            int val;
            if (dictionary.TryGetValue("Benfica", out val)) // Returns true.
            {
                Console.WriteLine(val); 
            }

            //Percorrer
            foreach (KeyValuePair<string, int> pair in dictionary)
            {
                Console.WriteLine("{0}, {1}", pair.Key, pair.Value);
            }
            //ou
            foreach (var pair in dictionary)
            {
                Console.WriteLine("{0}, {1}", pair.Key, pair.Value);
            }

            //Armazenar Keys numa List

            List<string> list = new List<string>(dictionary.Keys);

            //list = {"o", "Benfica", "e", "o maior"}
            foreach (string x in list)
            {
                Console.WriteLine("{0}, {1}", x, dictionary[x]);
            }
            #endregion

            #region DOIS

            //Dicionário de string-> Listas(int)
            Dictionary<string, List<int>> dictionary2 = new Dictionary<string, List<int>>();
            dictionary2["Benfica"] = new List<int>();
            dictionary2["Benfica"].Add(120);  //Erro?

            //dictionary2.Add("Porto", new List<int>() { 123 });

            //Na primeira vez cria a Lista
            if (!dictionary2.ContainsKey("Benfica"))
            {
                //cria a lista
                dictionary2.Add("Benfica", new List<int>());
                //adiciona
                dictionary2["Benfica"].Add(600000);
            }
            //Numa próxima vez, apenas acrescenta à Lista
            else
            {
                //Verificar se o valor já existe na Lista
                if (!dictionary2["Benfica"].Contains(600000))
                    dictionary2["Benfica"].Add(600000);
            }

            


            //Criar Lista com todos os valores do Dicionary
            List<int> aux = new List<int>();
            foreach(string s1 in dictionary2.Keys)
            {
                //aux.Add(dictionary2[s1]); //erro?
                //Percorre a List de int de cada chave do Diccionary
                foreach (int ii in dictionary2[s1])
                {
                    aux.Add(ii);
                }
            }


            //Apagar

            dictionary.Remove("Porto");

            //Sort das chaves
            List<String> myKeys = new List<String>(dictionary.Keys);
            myKeys.Sort();      //porque são strings!

            //Sort com LINQ
            //foreach (KeyValuePair<string, Int16> author in dictionary.OrderBy(key => key.Key))
            //{
            //    Console.WriteLine("Key: {0}, Value: {1}", author.Key, author.Value);
            //}

            #endregion

            #region Serializar-Dictionary

            SerializableDictionary<string, List<string>> b = new SerializableDictionary<string, List<string>>();
            List<string> stringList = new List<string>();
            stringList.Add("Benfica");
            b.Add("2019", stringList);

            //Serializing XML
            XmlSerializer s = new XmlSerializer(typeof(SerializableDictionary<string, List<string>>));
            FileStream xmlFile = new FileStream(@"c:\temp\dictionarySerialized.xml", FileMode.Create);
            s.Serialize(xmlFile, b);
            //ou TextWriter textWriter1 = new StreamWriter(@"c:\temp\dictionarySerialized.xml");
            //textWriter.Close();
            //textWriter.Dispose();
            xmlFile.Close();
            xmlFile.Dispose();

            b.Clear();

            //Deserializing
            XmlSerializer xs = new XmlSerializer(typeof(SerializableDictionary<string, List<string>>));
           FileStream xmlFile1 = new FileStream(@"c:\temp\dictionarySerialized.xml", FileMode.Open);
            b = (SerializableDictionary<string, List<string>>)xs.Deserialize(xmlFile1);

            //Show Dictionary
           foreach(string s1 in b.Keys)
            {
                Console.WriteLine(b[s1][0]);
            }
            #endregion

            #endregion

            #region HashSet
            //HashSet is an unordered collection containing unique elements
            //https://blogs.msdn.microsoft.com/bclteam/2006/11/09/introducing-hashsett-kim-hamilton/

            string[] nomes =
            {
            "ola",
            "ole",
            "ola",
            "Benfica",
            "Porto",
            "Porto"
            };

            // Mostra array
            Console.WriteLine(string.Join(",", nomes));

            // HashSet elimina repetidos
            var hash = new HashSet<string>(nomes);

            // convert em array de strings
            string[] array2 = hash.ToArray();   //Linq            

            // mostra conjunto
            Console.WriteLine(string.Join(",", array2));
            #endregion            

            #region LinkedList
            //
            // Lista Duplamente ligada
            //
            LinkedList<string> linked = new LinkedList<string>();
            //
            // AddLast adiciona no fim.
            // AddFirst adiciona no inicio.
            //
            linked.AddLast("ola");
            linked.AddLast("ole");
            linked.AddLast("Porto");
            linked.AddFirst("Benfica");
            //
            // Percorre a Lista.
            //
            foreach (var s2 in linked)
            {
                Console.WriteLine(s2);
            }
            #endregion

            #region SortedList<TKey, TValue>
            //SortedList ordenado pela Key

            SortedList<DateTime, List<Product>> sl = new SortedList<DateTime, List<Product>>();
            sl.Add(new DateTime(2021, 04, 28), new List<Product>());
            DateTime date = new DateTime(2021, 04, 28);
            sl[date].Add(new Product("Maquina", 128));
            date = date.AddDays(3);
            if (!sl.ContainsKey(date))
                sl.Add(date, new List<Product>());
            sl[date].Add(new Product("Maquina", 129));
            
            #endregion

            #region LINQ

            Action<string, string> concat;

            //Exemplo de inicialização
            List<int> numbers = new List<int>() { 5, 4, 1, 3, 9, 8, 6, 7, 2, 0 };
            int numCount1 =
                (from num in numbers
                 where num < 3 || num > 7
                 select num).Count();

            int numCount2 = numbers.Where(n => n < 3 || n > 7).Count();

            List<Product> parts;
            parts = products.FindAll(x => x.name.Contains("ola"));
            Product p2 = products.Find(x => x.name.Contains("ola"));
            bool aux2 = products.Exists(x => x.name.CompareTo("ola") == 0);

            //h1
            //Percorre Lista com método auxiliar:Mostra        
            products.ForEach(Mostra);
            //h2
            products.ForEach(delegate (Product p)
            {
                Console.WriteLine(p.name);
            });
            #endregion

            #region GENERIC_CLASS

            XInt x1 = new XInt();
            x1.x = 12;
            x1.valores[2] = 13;


            XString x2 = new XString();
            x2.x = "ola";
            x2.valores[0] = "ok";


            X<int> x3 = new X<int>();
            x3.x = x1.x;
            x3.valores[0] = x1.valores[0];

            X<string> x4 = new X<string>();

            X<double> x5 = new X<double>();

            Z<int, string> x6 = new Z<int, string>();
            x6.X = 12;
            x6.valores = "ola";

            Z<int, Hashtable> x7 = new Z<int, Hashtable>();
            x7.X = 12;
            x7.valores[12] = "ola";

            //x7.X = "12";

            Z<Product, Dictionary<string, List<Product>>> x8 = new Z<Product, Dictionary<string, List<Product>>>();
            x8.X = new Product();
            //x8.X = 12;

            #endregion

            Console.ReadKey();
        }  
    }
}
