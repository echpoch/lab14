using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization.Formatters.Soap;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;
using System.Xml.Linq;
namespace lab14
{
    class Program
    {
        static void Main(string[] args)
        {
            var student1 = new Student("Sergey", 18);

            var student2 = new Student("Sergey2", 19);

            var student3 = new Student("Sergey4", 20);

            var student4 = new Student("Sergey5", 21);

            var student5 = new Student("Sergey6", 22);

            var student6 = new Student("Sergey7", 23);

            List<Student> students = new List<Student>();
            students.Add(student1);

            students.Add(student2);

            students.Add(student3);

            students.Add(student4);

            students.Add(student5);

            students.Add(student6);


            foreach (Student st in students)
            {
                Console.WriteLine(st);
            }


            Console.WriteLine("Сериализация объекта в бинарный формат ! ");
            var binFormatted = new BinaryFormatter();

            using (var file = new FileStream("file.bin", FileMode.Create))
            {
                binFormatted.Serialize(file, students);
            }


            using (var file = new FileStream("file.bin", FileMode.OpenOrCreate))
            {
                binFormatted.Deserialize(file);
            }


            foreach (Student st in students)
            {
                Console.WriteLine(st);
            }

            Console.WriteLine("______________");


            using (var file1 = new FileStream("file1.bin", FileMode.Create))
            {
                binFormatted.Serialize(file1, student1);
            }


            using (var file1 = new FileStream("file1.bin", FileMode.OpenOrCreate))
            {
                Console.WriteLine(binFormatted.Deserialize(file1));
            }


            Console.WriteLine("______________");
            var XMLFormatted = new XmlSerializer(typeof(List<Student>));

            using (var file = new FileStream("file.xml", FileMode.Create))
            {

                XMLFormatted.Serialize(file, students);
            }


            using (var file = new FileStream("file.xml", FileMode.OpenOrCreate))
            {
                var a = XMLFormatted.Deserialize(file) as List<Student>;
                foreach (Student i in a)
                {
                    Console.WriteLine(i);
                }
            }

            Console.WriteLine("________________");

            var SOAPFormatted = new SoapFormatter();


            using (var file = new FileStream("file.soap", FileMode.Create))
            {

                SOAPFormatted.Serialize(file, student1);
            }


            using (var file = new FileStream("file.soap", FileMode.OpenOrCreate))
            {
                Console.WriteLine(SOAPFormatted.Deserialize(file));

            }

            Console.WriteLine("________________");

            var jsonFormatted = new DataContractJsonSerializer(typeof(List<Student>));

            using (var file = new FileStream("file.json", FileMode.Create))
            {

                jsonFormatted.WriteObject(file, students);
            }


            using (var file = new FileStream("file.json", FileMode.OpenOrCreate))
            {
                var list = jsonFormatted.ReadObject(file) as List<Student>;

                foreach (Student i in list)
                {
                    Console.WriteLine(i);
                }

            }

            Console.WriteLine("____________");

            XmlDocument XmlDoc = new XmlDocument();
            XmlDoc.Load(@"D:\3 семестр\ООП\lab14(serialization)\lab14\lab14\bin\Debug\file.xml");

            XmlElement Root = XmlDoc.DocumentElement;
            XmlNodeList ChildNodes = Root.SelectNodes("*");
            foreach (XmlNode i in ChildNodes)
            {
                Console.WriteLine(i.OuterXml);
            }
            Console.WriteLine("____________");
            XmlNodeList ChildNodesStudent = Root.SelectNodes("Student");

            foreach (XmlNode i in ChildNodesStudent)
            {
                Console.WriteLine(i.OuterXml);
            }

            XDocument xmldoc = XDocument.Load(@"D:\3 семестр\ООП\lab14(serialization)\lab14\lab14\bin\Debug\lab1.xml");
            Console.WriteLine("______________");
            var items = from i in xmldoc.Elements("FACULTY").Elements("SPECIALIZATION").Elements("NAME")
                        where (string)i.Value == "Исит"
                        select i;

            var items2 = from i in xmldoc.Elements("FACULTY").Elements("SPECIALIZATION").Elements("EXAM")
                         where (string)i.Value == "бел.яз"
                         select i;





            foreach (var i in items)
            {
                Console.WriteLine(i);
            }

            foreach (var i in items2)
            {
                Console.WriteLine(i);
            }

            Console.ReadLine();
        }
    }
}
