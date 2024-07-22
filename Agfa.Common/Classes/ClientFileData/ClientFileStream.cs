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
    /// <summary>
    /// A binary file to send to the web browser
    /// </summary>
    [Serializable()]
    [SoapType]
    [XmlRoot]
    [DataContract]
    public partial class ClientFileStream : ClientFileData, IClientFileData, IDisposable
    {
        /// <summary>
        /// Gets or sets the file data which will be sent to the web browser
        /// </summary>
        /// <value>The file data.</value>
        //[XmlElement]
        //[SoapElement]
        //[DataMember]
        //public FileStream Stream { get; set; }

        [XmlElement]
        [SoapElement]
        [DataMember]
        public String StreamFileName { get; set; }
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
                if (String.IsNullOrEmpty(this.StreamFileName) || !File.Exists(this.StreamFileName))
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="ClientFileStream"/> class.
        /// </summary>
        public ClientFileStream()
        {
            this.FileName = String.Empty;
            this.Title = String.Empty;
            this.FileExtension = String.Empty;
            this.MIMEType = String.Empty;
            // this.Stream = null;
            this.StreamFileName = null;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ClientFileStream"/> class.
        /// </summary>
        /// <param name="Title">The title.</param>
        /// <param name="FileExtension">The file extension.</param>
        /// <param name="MIMEType">Type of the MIME.</param>
        /// <param name="Stream">The file data.</param>
        public ClientFileStream(String Title, String FileExtension, String MIMEType, String StreamFileName)
        {

            this.Title = Title;
            this.FileExtension = FileExtension;
            this.MIMEType = MIMEType;
            //this.Stream = Stream;
            this.StreamFileName = StreamFileName;

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ClientFileStream"/> class.
        /// </summary>
        /// <param name="FileName">Name of the file.</param>
        /// <param name="MIMEType">Type of the MIME.</param>
        /// <param name="Stream">The file data.</param>
        public ClientFileStream(String FileName, String MIMEType, FileStream Stream, String StreamFileName)
        {
            this.FileName = FileName;
            this.MIMEType = MIMEType;
            // this.Stream = Stream;
            this.StreamFileName = StreamFileName;
        }

        /// <summary>
        /// Gets the stream.
        /// </summary>
        /// <returns></returns>
        public FileStream GetStream()
        {
            FileStream fs = new FileStream(this.StreamFileName, FileMode.Open, FileAccess.Read, FileShare.Read);
            return fs;
        }

        /// <summary>
        /// Saves the file.
        /// </summary>
        /// <param name="IncludeDateInFileName">if set to <c>true</c> [include date in file name].</param>
        public override String SaveFile(Boolean IncludeDateInFileName)
        {
            SaveDirectory.ThrowIfArgumentIsNull("SaveDirectory");
            String strPath = (IncludeDateInFileName ? SaveFileNameWithDate : SaveFileName);
            File.Copy(this.StreamFileName, strPath, true);
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
            File.Copy(this.StreamFileName, strPath, true);
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
            return this.StreamFileName;
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
        public override int GetHashCodeCaseInsensitive()
        {
            return this.GetHashCode();
        }


        private bool disposed = false; // to detect redundant calls

        private void Cleanup()
        {

            if (!String.IsNullOrEmpty(StreamFileName) && File.Exists(StreamFileName))
            {
                File.Delete(StreamFileName);
            }

        }
        protected virtual void Dispose(bool disposing)
        {
            if (disposed)
            {
                return;
            }
            if (disposing)
            {
                Cleanup();
            }
            disposed = true;
        }




        ~ClientFileStream()
        {
            Dispose(false);
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }


    }
}
