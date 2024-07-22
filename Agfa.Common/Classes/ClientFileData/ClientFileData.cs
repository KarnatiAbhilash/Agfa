using Agfa.Common.ExtensionMethods;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Xml.Serialization;

namespace Agfa.Common
{
    /// <summary>
    /// 
    /// </summary>
    [Serializable()]
    [SoapType]
    [XmlRoot]
    [DataContract]
    public abstract partial class ClientFileData : IClientFileData
    {
        /// <summary>
        /// Gets or sets the title of the file
        /// </summary>
        /// <value>The title.</value>
        [XmlElement]
        [SoapElement]
        [DataMember]
        public String Title { get; set; }
        /// <summary>
        /// Gets or sets the file extension.
        /// </summary>
        /// <value>The file extension.</value>
        /// <example>.xls</example>
        [XmlElement]
        [SoapElement]
        [DataMember]
        public String FileExtension { get; set; }
        /// <summary>
        /// Gets or sets the type of the MIME.
        /// </summary>
        /// <value>The type of the MIME.</value>
        /// <example>application/vnd.ms-excel</example>
        [XmlElement]
        [SoapElement]
        [DataMember]
        public String MIMEType { get; set; }
        /// <summary>
        /// Gets the name of the file
        /// Contatonation of the Title and File Extension
        /// </summary>
        /// <value>The name of the file.</value>

        private String strFileName;

        /// <summary>
        /// Gets the name of the file
        /// Contatonation of the Title and File Extension
        /// </summary>
        /// <value>The name of the file.</value>
        [XmlElement]
        [SoapElement]
        [DataMember]
        public String FileName
        {
            get
            {
                if (String.IsNullOrEmpty(strFileName))
                {
                    if (!(FileExtension.StartsWith(".")))
                    {
                        strFileName = Title + "." + FileExtension;
                    }
                    else
                    {
                        strFileName = Title + FileExtension;
                    }

                }
                return strFileName;
            }
            set
            {
                this.strFileName = value;
                String[] stra = strFileName.Split(new Char[] { '.' });

                if (stra.Length > 0)
                {
                    this.Title = stra[0];
                }
                else
                {
                    this.Title = String.Empty;
                }
                if (stra.Length > 1)
                {
                    this.FileExtension = stra[1];
                }

                else
                {
                    this.FileExtension = String.Empty;
                }




            }
        }
        /// <summary>
        /// Gets a value indicating whether this instance is valid.
        /// </summary>
        /// <value><c>true</c> if this instance is valid; otherwise, <c>false</c>.</value>
        [XmlElement]
        [SoapElement]
        [DataMember]
        public Boolean IsValid
        {
            get
            {
                if (!String.IsNullOrEmpty(this.Title) && !String.IsNullOrEmpty(this.FileExtension) && !String.IsNullOrEmpty(this.MIMEType) && this.IsDataValid)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
        /// <summary>
        /// Gets a value indicating whether the data is valid.
        /// </summary>
        /// <value>
        /// 	<c>true</c> if this instance is data valid; otherwise, <c>false</c>.
        /// </value>
        [XmlElement]
        [SoapElement]
        [DataMember]
        public virtual Boolean IsDataValid { get { return false; } }



        private String strSaveDirectory;
        /// <summary>
        /// Directory where the File will be Saved
        /// </summary>
        /// <value>The Save directory.</value>
        [XmlElement]
        [SoapElement]
        [DataMember]
        public string SaveDirectory
        {
            get { return this.strSaveDirectory; }
            set { this.strSaveDirectory = value; }
        }



        private String strArchiveDirectory;
        /// <summary>
        /// Directory where the File will be archived
        /// </summary>
        /// <value>The archive directory.</value>
        [XmlElement]
        [SoapElement]
        [DataMember]
        public string ArchiveDirectory
        {
            get { return this.strArchiveDirectory; }
            set { this.strArchiveDirectory = value; }
        }

        /// <summary>
        /// complete relative path to the location where the file will
        /// be archived
        /// </summary>
        /// <value></value>
        [XmlElement]
        [DataMember]
        [SoapElement]
        public String ArchiveFileName
        {
            get { return String.Format("{0}\\{1}", this.ArchiveDirectory, this.FileName); }
        }

        /// <summary>
        /// complete relative path to the location where the file will
        /// be archived
        /// </summary>
        /// <value></value>
        [XmlElement]
        [DataMember]
        [SoapElement]
        public String ArchiveFileNameWithDate
        {
            get { return String.Format("{0}\\{1}_{2}.{3}", this.ArchiveDirectory, this.Title, DateTime.Now.ToString("yyyyMMddhhmm"), this.FileExtension); }
        }

        /// <summary>
        /// complete relative path to the location where the file will
        /// be Saved
        /// </summary>
        /// <value></value>
        [XmlElement]
        [DataMember]
        [SoapElement]
        public String SaveFileName
        {
            get { return String.Format("{0}\\{1}", this.SaveDirectory, this.FileName); }
        }

        /// <summary>
        /// complete relative path to the location where the file will
        /// be Saved
        /// </summary>
        /// <value></value>
        [XmlElement]
        [DataMember]
        [SoapElement]
        public String SaveFileNameWithDate
        {
            get { return String.Format("{0}\\{1}_{2}.{3}", this.SaveDirectory, this.Title, DateTime.Now.ToString("yyyyMMddhhmm"), this.FileExtension); }
        }
        /// <summary>
        /// Gets or sets a value indicating whether this instance is attachment.
        /// </summary>
        /// <value>
        /// 	<c>true</c> if this instance is attachment; otherwise, <c>false</c>.
        /// </value>
        [XmlElement]
        [SoapElement]
        [DataMember]
        public Boolean IsAttachment { get; set; }

        /// <summary>
        /// Saves the file.
        /// </summary>
        public String SaveFile()
        {
            return SaveFile(false);
        }

        /// <summary>
        /// Saves the file.
        /// </summary>
        /// <param name="IncludeDateInFileName">if set to <c>true</c> [include date in file name].</param>
        public virtual String SaveFile(Boolean IncludeDateInFileName)
        {

            SaveDirectory.ThrowIfArgumentIsNull("SaveDirectory");
            String strPath = (IncludeDateInFileName ? SaveFileNameWithDate : SaveFileName + ".bin");

            using (FileStream s = File.Open(strPath, FileMode.Create))
            {
                BinaryFormatter bformatter = new BinaryFormatter();
                bformatter.Serialize(s, this);
            }
            return strPath;
        }


        /// <summary>
        /// Archives the file.
        /// </summary>
        /// <param name="IncludeDateInFileName">if set to <c>true</c> [include date in file name].</param>
        /// <returns></returns>
        public virtual String ArchiveFile(Boolean IncludeDateInFileName)
        {
            ArchiveDirectory.ThrowIfArgumentIsNull("ArchiveDirectory");
            String strPath = (IncludeDateInFileName ? ArchiveFileNameWithDate : ArchiveFileName + ".bin");

            using (FileStream s = File.Open(strPath, FileMode.Create))
            {
                BinaryFormatter bformatter = new BinaryFormatter();
                bformatter.Serialize(s, this);
            }

            return strPath;
        }


        /// <summary>
        /// Archives the file.
        /// </summary>
        /// <returns></returns>
        public String ArchiveFile()
        {
            return ArchiveFile(false);
        }

        /// <summary>
        /// Gets the hash code for the data in a case insensative way
        /// </summary>
        /// <returns></returns>
        public virtual int GetHashCodeCaseInsensitive()
        {
            return this.GetHashCode();
        }

    }

    /// <summary>
    /// Extension Methods for IClientFileData
    /// </summary>
    public static partial class IClientFileDataExtensions
    {

        /// <summary>
        /// Determines whether the specified data is excel.
        /// </summary>
        /// <param name="data">The data.</param>
        /// <returns>
        /// 	<c>true</c> if the specified data is excel; otherwise, <c>false</c>.
        /// </returns>
        public static Boolean IsExcel(this IClientFileData data)
        {
            if (data.MIMEType.Equals("application/excel", StringComparison.CurrentCultureIgnoreCase) ||
                data.MIMEType.Equals("application/vnd.ms-excel", StringComparison.CurrentCultureIgnoreCase) ||
                data.MIMEType.Equals("application/x-excel", StringComparison.CurrentCultureIgnoreCase) ||
                data.MIMEType.Equals("application/x-msexcel", StringComparison.CurrentCultureIgnoreCase) ||
                data.MIMEType.Equals("application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", StringComparison.CurrentCultureIgnoreCase) ||
                data.MIMEType.Equals("application/vnd.ms-excel.sheet.macroEnabled.12", StringComparison.CurrentCultureIgnoreCase) ||
                data.MIMEType.Equals("application/vnd.openxmlformats-officedocument.spreadsheetml.template", StringComparison.CurrentCultureIgnoreCase) ||
                data.MIMEType.Equals("application/vnd.ms-excel.template.macroEnabled.12", StringComparison.CurrentCultureIgnoreCase) ||
                data.MIMEType.Equals("application/vnd.ms-excel.sheet.binary.macroEnabled.12", StringComparison.CurrentCultureIgnoreCase) ||
                data.MIMEType.Equals("application/vnd.ms-excel.addin.macroEnabled.12", StringComparison.CurrentCultureIgnoreCase)
                )
            {
                return true;
            }
            return false;
        }
        /// <summary>
        /// Determines whether the specified data is excel.
        /// </summary>
        /// <param name="data">The data.</param>
        /// <returns>
        /// 	<c>true</c> if the specified data is excel; otherwise, <c>false</c>.
        /// </returns>
        public static Boolean IsExcel2003(this IClientFileData data)
        {
            if (data.MIMEType.Equals("application/excel", StringComparison.CurrentCultureIgnoreCase) ||
                data.MIMEType.Equals("application/vnd.ms-excel", StringComparison.CurrentCultureIgnoreCase) ||
                data.MIMEType.Equals("application/x-excel", StringComparison.CurrentCultureIgnoreCase) ||
                data.MIMEType.Equals("application/x-msexcel", StringComparison.CurrentCultureIgnoreCase)
                )
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// Determines whether the specified data is excel.
        /// </summary>
        /// <param name="data">The data.</param>
        /// <returns>
        /// 	<c>true</c> if the specified data is excel; otherwise, <c>false</c>.
        /// </returns>
        public static Boolean IsExcel2007(this IClientFileData data)
        {
            if (data.MIMEType.Equals("application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", StringComparison.CurrentCultureIgnoreCase) ||
                data.MIMEType.Equals("application/vnd.ms-excel.sheet.macroEnabled.12", StringComparison.CurrentCultureIgnoreCase) ||
                data.MIMEType.Equals("application/vnd.openxmlformats-officedocument.spreadsheetml.template", StringComparison.CurrentCultureIgnoreCase) ||
                data.MIMEType.Equals("application/vnd.ms-excel.template.macroEnabled.12", StringComparison.CurrentCultureIgnoreCase) ||
                data.MIMEType.Equals("application/vnd.ms-excel.sheet.binary.macroEnabled.12", StringComparison.CurrentCultureIgnoreCase) ||
                data.MIMEType.Equals("application/vnd.ms-excel.addin.macroEnabled.12", StringComparison.CurrentCultureIgnoreCase)
                )
            {
                return true;
            }
            return false;
        }


        /// <summary>
        /// Determines whether the specified data is Word.
        /// </summary>
        /// <param name="data">The data.</param>
        /// <returns>
        /// 	<c>true</c> if the specified data is excel; otherwise, <c>false</c>.
        /// </returns>
        public static Boolean IsWord(this IClientFileData data)
        {
            if (data.MIMEType.Equals("application/msword", StringComparison.CurrentCultureIgnoreCase) ||
                data.MIMEType.Equals("application/vnd.openxmlformats-officedocument.wordprocessingml.document", StringComparison.CurrentCultureIgnoreCase) ||
                data.MIMEType.Equals("application/vnd.ms-word.document.macroEnabled.12", StringComparison.CurrentCultureIgnoreCase) ||
                data.MIMEType.Equals("application/vnd.ms-word.template.macroEnabled.12", StringComparison.CurrentCultureIgnoreCase) ||
                data.MIMEType.Equals("application/vnd.openxmlformats-officedocument.wordprocessingml.template", StringComparison.CurrentCultureIgnoreCase)
                )
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// Determines whether the specified data is Word.
        /// </summary>
        /// <param name="data">The data.</param>
        /// <returns>
        /// 	<c>true</c> if the specified data is excel; otherwise, <c>false</c>.
        /// </returns>
        public static Boolean IsWord2003(this IClientFileData data)
        {
            if (data.MIMEType.Equals("application/msword", StringComparison.CurrentCultureIgnoreCase)
                )
            {
                return true;
            }
            return false;
        }


        /// <summary>
        /// Determines whether the specified data is Word.
        /// </summary>
        /// <param name="data">The data.</param>
        /// <returns>
        /// 	<c>true</c> if the specified data is excel; otherwise, <c>false</c>.
        /// </returns>
        public static Boolean IsWord2007(this IClientFileData data)
        {
            if (data.MIMEType.Equals("application/vnd.openxmlformats-officedocument.wordprocessingml.document", StringComparison.CurrentCultureIgnoreCase) ||
                data.MIMEType.Equals("application/vnd.ms-word.document.macroEnabled.12", StringComparison.CurrentCultureIgnoreCase) ||
                data.MIMEType.Equals("application/vnd.ms-word.template.macroEnabled.12", StringComparison.CurrentCultureIgnoreCase) ||
                data.MIMEType.Equals("application/vnd.openxmlformats-officedocument.wordprocessingml.template", StringComparison.CurrentCultureIgnoreCase)
                )
            {
                return true;
            }
            return false;
        }



        /// <summary>
        /// Determines whether the specified data is PowerPoint.
        /// </summary>
        /// <param name="data">The data.</param>
        /// <returns>
        /// 	<c>true</c> if the specified data is excel; otherwise, <c>false</c>.
        /// </returns>
        public static Boolean IsPowerPoint(this IClientFileData data)
        {
            if (data.MIMEType.Equals("application/vnd.ms-powerpoint", StringComparison.CurrentCultureIgnoreCase) ||
                data.MIMEType.Equals("application/vnd.openxmlformats-officedocument.presentationml.presentation", StringComparison.CurrentCultureIgnoreCase) ||
                data.MIMEType.Equals("application/vnd.ms-powerpoint.presentation.macroEnabled.12", StringComparison.CurrentCultureIgnoreCase) ||
                data.MIMEType.Equals("application/vnd.openxmlformats-officedocument.presentationml.slideshow", StringComparison.CurrentCultureIgnoreCase) ||
                data.MIMEType.Equals("application/vnd.ms-powerpoint.slideshow.macroEnabled.12", StringComparison.CurrentCultureIgnoreCase) ||
                data.MIMEType.Equals("application/vnd.openxmlformats-officedocument.presentationml.template", StringComparison.CurrentCultureIgnoreCase) ||
                data.MIMEType.Equals("application/vnd.ms-powerpoint.template.macroEnabled.12", StringComparison.CurrentCultureIgnoreCase) ||
                data.MIMEType.Equals("application/vnd.ms-powerpoint.addin.macroEnabled.12", StringComparison.CurrentCultureIgnoreCase) ||
                data.MIMEType.Equals("application/vnd.openxmlformats-officedocument.presentationml.slide", StringComparison.CurrentCultureIgnoreCase) ||
                data.MIMEType.Equals("application/vnd.ms-powerpoint.slide.macroEnabled.12", StringComparison.CurrentCultureIgnoreCase)
                )
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// Determines whether the specified data is PowerPoint.
        /// </summary>
        /// <param name="data">The data.</param>
        /// <returns>
        /// 	<c>true</c> if the specified data is excel; otherwise, <c>false</c>.
        /// </returns>
        public static Boolean IsPowerPoint2003(this IClientFileData data)
        {
            if (data.MIMEType.Equals("application/vnd.ms-powerpoint", StringComparison.CurrentCultureIgnoreCase))
            {
                return true;
            }
            return false;
        }


        /// <summary>
        /// Determines whether the specified data is PowerPoint.
        /// </summary>
        /// <param name="data">The data.</param>
        /// <returns>
        /// 	<c>true</c> if the specified data is excel; otherwise, <c>false</c>.
        /// </returns>
        public static Boolean IsPowerPoint2007(this IClientFileData data)
        {
            if (data.MIMEType.Equals("application/vnd.openxmlformats-officedocument.presentationml.presentation", StringComparison.CurrentCultureIgnoreCase) ||
                data.MIMEType.Equals("application/vnd.ms-powerpoint.presentation.macroEnabled.12", StringComparison.CurrentCultureIgnoreCase) ||
                data.MIMEType.Equals("application/vnd.openxmlformats-officedocument.presentationml.slideshow", StringComparison.CurrentCultureIgnoreCase) ||
                data.MIMEType.Equals("application/vnd.ms-powerpoint.slideshow.macroEnabled.12", StringComparison.CurrentCultureIgnoreCase) ||
                data.MIMEType.Equals("application/vnd.openxmlformats-officedocument.presentationml.template", StringComparison.CurrentCultureIgnoreCase) ||
                data.MIMEType.Equals("application/vnd.ms-powerpoint.template.macroEnabled.12", StringComparison.CurrentCultureIgnoreCase) ||
                data.MIMEType.Equals("application/vnd.ms-powerpoint.addin.macroEnabled.12", StringComparison.CurrentCultureIgnoreCase) ||
                data.MIMEType.Equals("application/vnd.openxmlformats-officedocument.presentationml.slide", StringComparison.CurrentCultureIgnoreCase) ||
                data.MIMEType.Equals("application/vnd.ms-powerpoint.slide.macroEnabled.12", StringComparison.CurrentCultureIgnoreCase)
                )
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// Determines whether the specified data is OneNote.
        /// </summary>
        /// <param name="data">The data.</param>
        /// <returns>
        /// 	<c>true</c> if the specified data is excel; otherwise, <c>false</c>.
        /// </returns>
        public static Boolean IsOneNote(this IClientFileData data)
        {
            if (data.MIMEType.Equals("application/msonenote", StringComparison.CurrentCultureIgnoreCase)
                )
            {
                return true;
            }
            return false;
        }


        /// <summary>
        /// Determines whether the specified data is PDF.
        /// </summary>
        /// <param name="data">The data.</param>
        /// <returns>
        /// 	<c>true</c> if the specified data is excel; otherwise, <c>false</c>.
        /// </returns>
        public static Boolean IsPDF(this IClientFileData data)
        {
            if (data.MIMEType.Equals("application/pdf", StringComparison.CurrentCultureIgnoreCase) ||
                data.MIMEType.Equals("application/acrobat", StringComparison.CurrentCultureIgnoreCase) ||
                data.MIMEType.Equals("application/x-pdf", StringComparison.CurrentCultureIgnoreCase) ||
                data.MIMEType.Equals("applications/vnd.pdf", StringComparison.CurrentCultureIgnoreCase) ||
                data.MIMEType.Equals("text/pdf", StringComparison.CurrentCultureIgnoreCase) ||
                data.MIMEType.Equals("text/x-pdf", StringComparison.CurrentCultureIgnoreCase)
                )
            {
                return true;
            }
            return false;
        }


        /// <summary>
        /// Determines whether [is octet stream] [the specified data].
        /// </summary>
        /// <param name="data">The data.</param>
        /// <returns>
        /// 	<c>true</c> if [is octet stream] [the specified data]; otherwise, <c>false</c>.
        /// </returns>
        public static Boolean IsOctetStream(this IClientFileData data)
        {
            return data.MIMEType.Equals("application/octet-stream", StringComparison.CurrentCultureIgnoreCase);
        }




    }
}
