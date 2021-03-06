﻿using System.Xml;

namespace RawLauncher.Framework.Xml
{
    public static class XmlTools
    {
        public static string GetNodeValue(string filePath, string nodePath)
        {
            var xml = new XmlDocument();
            xml.Load(filePath);
            var selectSingleNode = xml.SelectSingleNode(nodePath);
            return selectSingleNode?.InnerText;
        }
    }
}
