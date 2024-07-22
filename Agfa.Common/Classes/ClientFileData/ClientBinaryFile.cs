using Agfa.Common.ExtensionMethods;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Xml.Serialization;

namespace Agfa.Common
{
    [SoapType]
    [XmlRoot]
    [DataContract]
    [Serializable]
    public class ClientBinaryFile : ClientFileData, IClientFileData
    {
        [XmlElement]
        [SoapElement]
        [DataMember]
        public byte[] FileData { get; set; }

        [XmlElement]
        [SoapElement]
        [DataMember]
        public override bool IsDataValid => this.FileData != null && this.FileData.Length != 0;

        public ClientBinaryFile()
        {
            this.FileName = string.Empty;
            this.Title = string.Empty;
            this.FileExtension = string.Empty;
            this.MIMEType = string.Empty;
            this.FileData = new byte[0];
        }

        public ClientBinaryFile(string Title, string FileExtension, string MIMEType, byte[] FileData)
        {
            this.Title = Title;
            this.FileExtension = FileExtension;
            this.MIMEType = MIMEType;
            this.FileData = FileData;
        }

        public ClientBinaryFile(string FileName, string MIMEType, byte[] FileData)
        {
            this.FileName = FileName;
            this.MIMEType = MIMEType;
            this.FileData = FileData;
        }

        public Stream GetStream() => this.FileData.ToStream();

        public override string SaveFile(bool IncludeDateInFileName)
        {
            this.SaveDirectory.ThrowIfArgumentIsNull<string>("SaveDirectory");
            string path = IncludeDateInFileName ? this.SaveFileNameWithDate : this.SaveFileName;
            using (FileStream fileStream = File.Open(path, FileMode.Create, FileAccess.ReadWrite))
                fileStream.Write(this.FileData, 0, this.FileData.Length);
            return path;
        }

        public override string ArchiveFile(bool IncludeDateInFileName)
        {
            this.ArchiveDirectory.ThrowIfArgumentIsNull<string>("ArchiveDirectory");
            string path = IncludeDateInFileName ? this.ArchiveFileNameWithDate : this.ArchiveFileName;
            using (FileStream fileStream = File.Open(path, FileMode.Create, FileAccess.ReadWrite))
                fileStream.Write(this.FileData, 0, this.FileData.Length);
            return path;
        }

        public override string ToString() => this.FileData != null ? this.FileData.ToBase64String() : string.Empty;

        public override int GetHashCode() => this.ToString().GetHashCode();

        public new int GetHashCodeCaseInsensitive() => this.GetHashCode();
    }
}
