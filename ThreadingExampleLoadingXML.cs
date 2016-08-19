using System;
using System.IO;
using System.Xml;
using System.Xml.Linq;
using System.Diagnostics;
using System.Threading;

public class Program
{
    public static void Main()
    {
        // Create an XML file with 10000 records
        XmlWriterSettings settings = new XmlWriterSettings();
        settings.Indent = true;
        using (XmlWriter writer = XmlWriter.Create("books.xml", settings))
        {
            writer.WriteStartElement("bookstore");
            for (int i = 0; i < 10000; i++) {
                writer.WriteStartElement("book");
                writer.WriteElementString("ISBN", (10000000 + i).ToString());
                writer.WriteElementString("year", "2010");
                writer.WriteElementString("title", "My Favorite Book");
                writer.WriteElementString("publisher", "abc publications");
                writer.WriteElementString("price", "29.99");
                writer.WriteEndElement();
            }
            writer.WriteEndElement();
        }

        Thread rvf = new Thread(new ThreadStart(ReadViaFilename));
        rvf.Start();
        Thread rvs = new Thread(new ThreadStart(ReadViaStream));
        rvs.Start();
        Thread rvtr = new Thread(new ThreadStart(ReadViaTextReader));
        rvtr.Start();
        Thread rvxr = new Thread(new ThreadStart(ReadViaXMLReader));
        rvxr.Start();
    }

    public static void ReadViaFilename() {
        string filename = @"books.xml";
        string start = DateTime.Now.ToString("HH:mm:ss:fff");
        var sw = Stopwatch.StartNew();
        XElement root = XElement.Load(filename);
        sw.Stop();
        string end = DateTime.Now.ToString("HH:mm:ss:fff");
        Console.WriteLine("Loading using file name");
        Console.WriteLine("Elapsed: {0} ms", sw.ElapsedMilliseconds);
        Console.WriteLine("Start: {0}", start);
        Console.WriteLine("End: {0}", end);
        Console.WriteLine();
    }

    public static void ReadViaStream() {
        string filename = @"books.xml";
        string start = DateTime.Now.ToString("HH:mm:ss:fff");
        var sw = Stopwatch.StartNew();
        FileStream filestream = File.OpenRead(filename);
        XElement root = XElement.Load(filestream);
        sw.Stop();
        string end = DateTime.Now.ToString("HH:mm:ss:fff");
        Console.WriteLine("Loading using a stream");
        Console.WriteLine("Elapsed: {0} ms", sw.ElapsedMilliseconds);
        Console.WriteLine("Start: {0}", start);
        Console.WriteLine("End: {0}", end);
        Console.WriteLine();
    }

    public static void ReadViaTextReader() {
        string filename = @"books.xml";
        string start = DateTime.Now.ToString("HH:mm:ss:fff");
        var sw = Stopwatch.StartNew();
        TextReader reader = new StreamReader(filename);
        XElement root = XElement.Load(reader);
        sw.Stop();
        string end = DateTime.Now.ToString("HH:mm:ss:fff");
        Console.WriteLine("Loading using a TextReader");
        Console.WriteLine("Elapsed: {0} ms", sw.ElapsedMilliseconds);
        Console.WriteLine("Start: {0}", start);
        Console.WriteLine("End: {0}", end);
        Console.WriteLine();
    }

    public static void ReadViaXMLReader() {
        string filename = @"books.xml";
        string start = DateTime.Now.ToString("HH:mm:ss:fff");
        var sw = Stopwatch.StartNew();
        XmlReader xmlreader = new XmlTextReader(new StreamReader(filename));
        XElement root = XElement.Load(xmlreader);
        sw.Stop();
        string end = DateTime.Now.ToString("HH:mm:ss:fff");
        Console.WriteLine("Loading using an XmlReader");
        Console.WriteLine("Elapsed: {0} ms", sw.ElapsedMilliseconds);
        Console.WriteLine("Start: {0}", start);
        Console.WriteLine("End: {0}", end);
        Console.WriteLine();
    }
}
