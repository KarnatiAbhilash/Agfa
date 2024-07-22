using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using System.Runtime.Serialization;
using System.IO;
using Agfa.Common.ExtensionMethods;

namespace Agfa.Common
{
    /// <summary>
    /// A text file to be sent to the web browser
    /// </summary>
    [Serializable()]
    [SoapType]
    [XmlRoot]
    [DataContract]
    public partial class ClientTextFile : ClientFileData, IClientFileData
    {
        /// <summary>
        /// Gets or sets the file contents.
        /// </summary>
        /// <value>The file contents.</value>
        [XmlElement]
        [SoapElement]
        [DataMember]
        public String FileContents { get; set; }
        /// <summary>
        /// Gets a value indicating whether the data is valid.
        /// </summary>
        /// <value>
        /// 	<c>true</c> if this instance is data valid; otherwise, <c>false</c>.
        /// </value>
        [XmlElement]
        [SoapElement]
        [DataMember]
        public override Boolean IsDataValid
        {
            get
            {
                return !String.IsNullOrEmpty(FileContents);
            }
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="ClientTextFile"/> class.
        /// </summary>
        public ClientTextFile()
        {
            this.FileName = String.Empty;
            this.Title = String.Empty;
            this.FileExtension = String.Empty;
            this.MIMEType = String.Empty;
            this.FileContents = String.Empty;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ClientTextFile"/> class.
        /// </summary>
        /// <param name="Title">The title.</param>
        /// <param name="FileExtension">The file extension.</param>
        /// <param name="MIMEType">Type of the MIME.</param>
        /// <param name="FileContents">The file contents.</param>
        public ClientTextFile(String Title, String FileExtension, String MIMEType, String FileContents)
        {
            this.Title = Title;
            this.FileExtension = FileExtension;
            this.MIMEType = MIMEType;
            this.FileContents = FileContents;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ClientTextFile"/> class.
        /// </summary>
        /// <param name="FileName">Name of the file.</param>
        /// <param name="MIMEType">Type of the MIME.</param>
        /// <param name="FileContents">The file contents.</param>
        public ClientTextFile(String FileName, String MIMEType, String FileContents)
        {
            this.FileName = FileName;
            this.MIMEType = MIMEType;
            this.FileContents = FileContents;
        }


        /// <summary>
        /// Saves the file.
        /// </summary>
        /// <param name="IncludeDateInFileName">if set to <c>true</c> [include date in file name].</param>
        public override String SaveFile(Boolean IncludeDateInFileName)
        {
            SaveDirectory.ThrowIfArgumentIsNull("SaveDirectory");
            String strPath = (IncludeDateInFileName ? SaveFileNameWithDate : SaveFileName);

            using (StreamWriter sw = new StreamWriter(strPath, false))
            {
                sw.Write(FileContents);
            }
            return strPath;
        }

        /// <summary>
        /// Archives the file.
        /// </summary>
        /// <param name="IncludeDateInFileName">if set to <c>true</c> [include date in file name].</param>
        public override String ArchiveFile(Boolean IncludeDateInFileName)
        {
            ArchiveDirectory.ThrowIfArgumentIsNull("ArchiveDirectory");
            String strPath = (IncludeDateInFileName ? ArchiveFileNameWithDate : ArchiveFileName);

            using (StreamWriter sw = new StreamWriter(strPath, false))
            {
                sw.Write(FileContents);
            }
            return strPath;
        }

        /// <summary>
        /// Returns a <see cref="System.String"/> that represents this instance.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String"/> that represents this instance.
        /// </returns>
        public override string ToString()
        {
            return this.FileContents.Default(String.Empty);
        }

        /// <summary>
        /// Returns a hash code for this instance.
        /// </summary>
        /// <returns>
        /// A hash code for this instance, suitable for use in hashing algorithms and data structures like a hash table. 
        /// </returns>
        public override int GetHashCode()
        {
            return this.ToString().GetHashCode();
        }
        /// <summary>
        /// Gets the hash code case insensitive.
        /// </summary>
        /// <returns></returns>
        public int GetHashCodeCaseInsensitive()
        {
            return this.ToString().ToLower().GetHashCode();
        }


    }
}
