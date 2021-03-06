﻿using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Xml.Schema;
using System.Xml.Serialization;
using RawLauncher.Framework.Hash;
using RawLauncher.Framework.Versioning;

namespace RawLauncher.Framework.Models
{
    /// <remarks/>
    [GeneratedCode("xsd", "4.6.81.0")]
    [Serializable]
    [DebuggerStepThrough]
    [DesignerCategory(@"code")]
    [XmlType(AnonymousType = true)]
    [XmlRoot(Namespace = "", IsNullable = false)]
    public class FileContainer
    {
        [XmlElement("Version", Order = 1)]
        public string StringVersion { get; set; }

        [XmlIgnore]
        public ModVersion Version => ModVersion.Parse(StringVersion);

        [XmlElement("File", Order = 2)]
        public List<FileContainerFile> Files;

        [XmlElement("Folder", Order = 3)]
        public List<FileContainerFolder> Folders;

        public List<FileContainerFolder> GetFoldersOfType(TargetType type)
        {
            return Folders.Where(folder => folder.TargetType == type).ToList();
        } 

    }

    /// <remarks/>
    [GeneratedCode("xsd", "4.6.81.0")]
    [Serializable]
    [DebuggerStepThrough]
    [DesignerCategory(@"code")]
    [XmlType(AnonymousType = true)]
    public class FileContainerFile
    {
        private string _hashField;
        private string _nameField;
        private string _sourcePathField;
        private string _targetPathField;
        private TargetType _targetTypeField;

        /// <remarks/>
        [XmlElement(Form = XmlSchemaForm.Unqualified)]
        public string Name
        {
            get => _nameField;
            set => _nameField = value;
        }

        /// <remarks/>
        [XmlElement(Form = XmlSchemaForm.Unqualified)]
        public string TargetPath
        {
            get => _targetPathField;
            set => _targetPathField = value;
        }

        /// <remarks/>
        [XmlElement(Form = XmlSchemaForm.Unqualified)]
        public TargetType TargetType
        {
            get => _targetTypeField;
            set => _targetTypeField = value;
        }

        /// <remarks/>
        [XmlElement(Form = XmlSchemaForm.Unqualified)]
        public string Hash
        {
            get => _hashField;
            set => _hashField = value;
        }

        /// <remarks/>
        [XmlElement(Form = XmlSchemaForm.Unqualified)]
        public string SourcePath
        {
            get => _sourcePathField;
            set => _sourcePathField = value;
        }

        /// <summary>
        /// Create a new List where without files that should be excluded. 
        /// Works like: 
        /// 
        /// "\Folder\" -> Exclude all files in folder
        /// "\Folder\*" -> Exclude all files in folder and Subfolders
        /// "\Folder\*File.txt" -> Exlude all files having "File.txt" in its name
        /// </summary>
        /// <param name="fileList"></param>
        /// <param name="excludeList"></param>
        /// <returns></returns>
        public static List<FileContainerFile> ListFromExcludeList(List<FileContainerFile> fileList,
            List<string> excludeList)
        {
            return excludeList == null ? fileList : fileList.Where(file => !ShallExclude(file, excludeList)).ToList();
        }

        public static bool ShallExclude(FileContainerFile file, List<string> excludeList)
        {
            var exclude = false;
            if (excludeList == null)
                return false;
            foreach (var s in excludeList)
            {
                if (file.TargetPath.Replace(s, "") == file.Name)
                    exclude = true;
                if (s.Contains("*") && s[s.Length - 1] == '*')
                {
                    var t = s.Remove(s.Length - 1);
                    if (file.TargetPath.Contains(t))
                        exclude = true;
                }
                else if (s.Contains("*"))
                {
                    var t = Path.GetFileName(s);
                    t = t.Replace("*", "");
                    if (file.Name.Contains(t))
                        exclude = true;
                }
            }
            return exclude;
        }

        public ModVersion TryGetModVersionFromSourcePath()
        {
            var verStr = SourcePath.Split('\\').First();
            var ver = ModVersion.Parse(verStr);
            return ver;
        }
    }

    /// <remarks/>
    [GeneratedCode("xsd", "4.6.81.0")]
    [Serializable]
    public enum TargetType
    {
        /// <remarks/>
        Mod,

        /// <remarks/>
        Ai,
    }

    public enum CheckResult
    {
        None,
        Exist,
        Count,
        Hash
    }

    /// <remarks/>
    [GeneratedCode("xsd", "4.6.81.0")]
    [Serializable]
    [DebuggerStepThrough]
    [DesignerCategory(@"code")]
    [XmlType(AnonymousType = true)]
    public class FileContainerFolder
    {
        private string _countField;
        private string _hashField;
        private string _targetPathField;
        private TargetType _targetTypeField;

        /// <remarks/>
        [XmlElement(Form = XmlSchemaForm.Unqualified)]
        public string TargetPath
        {
            get => _targetPathField;
            set => _targetPathField = value;
        }

        /// <remarks/>
        [XmlElement(Form = XmlSchemaForm.Unqualified)]
        public TargetType TargetType
        {
            get => _targetTypeField;
            set => _targetTypeField = value;
        }

        /// <remarks/>
        [XmlElement(Form = XmlSchemaForm.Unqualified)]
        public string Hash
        {
            get => _hashField;
            set => _hashField = value;
        }

        /// <remarks/>
        [XmlElement(Form = XmlSchemaForm.Unqualified, DataType = "integer")]
        public string Count
        {
            get => _countField;
            set => _countField = value;
        }

        public CheckResult Check(string referencePath)
        {
            if (!Directory.Exists(referencePath))
                return CheckResult.Exist;
            if (Directory.GetFiles(referencePath).Length.ToString() != Count)
                return CheckResult.Count;
            var hashProvider = new HashProvider();
            if (hashProvider.GetDirectoryHash(referencePath) != Hash)
                return CheckResult.Hash;
            return CheckResult.None;
        }

        public static List<FileContainerFolder> ListFromExcludeList(List<FileContainerFolder> folderList,
            IReadOnlyCollection<string> excludeList)
        {
            List<FileContainerFolder> list = new List<FileContainerFolder>();
            if (excludeList == null)
                return folderList;
            var subExclude = new List<string>();

            foreach (var folder in folderList)
            {
                var exclude = false;
                foreach (var s in excludeList)
                {
                    if (s.Replace(folder.TargetPath, "") == "")
                        exclude = true;
                    if (s.Replace(folder.TargetPath, "") == "*")
                        subExclude.Add(folder.TargetPath);
                }
                if (!exclude)
                    list.Add(folder);
            }
            foreach (FileContainerFolder folder in from folder in folderList from s in subExclude where folder.TargetPath.Contains(s) select folder)
                list.Remove(folder);
            return list;
        }
    }

    /// <remarks/>
    [GeneratedCode("xsd", "4.6.81.0")]
    [Serializable]
    [DebuggerStepThrough]
    [DesignerCategory(@"code")]
    [XmlType(AnonymousType = true)]
    [XmlRoot(Namespace = "", IsNullable = false)]
    public class NewDataSet
    {
        private FileContainer[] _itemsField;

        /// <remarks/>
        [XmlElement("FileContainer")]
        public FileContainer[] Items
        {
            get => _itemsField;
            set => _itemsField = value;
        }
    }
}