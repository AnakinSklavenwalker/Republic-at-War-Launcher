﻿using System;
using System.IO;
using System.Xml;
using System.Xml.Serialization;
using RawLauncher.Framework.Utilities;

namespace RawLauncher.Framework.Xml
{
    public class XmlObjectParser<T> where T : class
    {
        public XmlObjectParser(string path)
        {
            if (!File.Exists(path))
                throw new FileNotFoundException(nameof(path));
            FileStream = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.Read);
        }

        public XmlObjectParser(Stream fileStream)
        {
            FileStream = fileStream;
        }

        private Stream FileStream { get; }

        public T Parse()
        {
            FileStream.Position = 0;
            var reader = XmlReader.Create(FileStream,
                new XmlReaderSettings { ConformanceLevel = ConformanceLevel.Document });
            T instance;
            try
            {
                instance = new XmlSerializer(typeof(T)).Deserialize(reader) as T;
            }
            catch (Exception e)
            {
                throw new Exception(MessageProvider.GetMessage("ExceptionXmlParserError"), e.InnerException);
            }
            finally
            {
                //FileStream.Close();
            }
            return instance;
        }
    }
}