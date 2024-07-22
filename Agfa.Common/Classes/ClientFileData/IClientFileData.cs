using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Agfa.Common
{
    /// <summary>
    /// A file which will be download to a web browser
    /// </summary>
    public interface IClientFileData
    {
        /// <summary>
        /// Gets or sets the file extension.
        /// </summary>
        /// <value>The file extension.</value>
        /// <example>.xls</example>
        String FileExtension { get; set; }
        /// <summary>
        /// Gets the name of the file
        /// Contatonation of the Title and File Extension
        /// </summary>
        /// <value>The name of the file.</value>
        String FileName { get; }
        /// <summary>
        /// Gets or sets the type of the MIME.
        /// </summary>
        /// <value>The type of the MIME.</value>
        /// <example>application/vnd.ms-excel</example>
        String MIMEType { get; set; }
        /// <summary>
        /// Gets or sets the title of the file, File Name without extension
        /// </summary>
        /// <value>The title.</value>
        String Title { get; set; }
        /// <summary>
        /// Gets a value indicating whether this instance is valid.
        /// </summary>
        /// <value><c>true</c> if this instance is valid; otherwise, <c>false</c>.</value>
        Boolean IsValid { get; }
        /// <summary>
        /// Gets a value indicating whether the data is valid.
        /// </summary>
        /// <value>
        /// 	<c>true</c> if this instance is data valid; otherwise, <c>false</c>.
        /// </value>
        Boolean IsDataValid { get; }


        /// <summary>
        /// Directory where the File will be archived
        /// </summary>
        /// <value>The archive directory.</value>
        String ArchiveDirectory { get; set; }

        /// <summary>
        /// complete relative path to the location where the file will
        /// be archived
        /// </summary>
        String ArchiveFileName { get; }

        /// <summary>
        /// Gets the archive file name with date.
        /// </summary>
        /// <value>The archive file name with date.</value>
        String ArchiveFileNameWithDate { get; }

        /// <summary>
        /// Archives the file.
        /// </summary>
        /// <param name="IncludeDateInFileName">if set to <c>true</c> [include date in file name].</param>
        /// <returns></returns>
        String ArchiveFile(Boolean IncludeDateInFileName);
        /// <summary>
        /// Archives the file.
        /// </summary>
        /// <returns></returns>
        String ArchiveFile();



        /// <summary>
        /// Directory where the File will be Saved
        /// </summary>
        /// <value>The Save directory.</value>
        String SaveDirectory { get; set; }

        /// <summary>
        /// complete relative path to the location where the file will
        /// be Saved
        /// </summary>
        String SaveFileName { get; }

        /// <summary>
        /// Gets the Save file name with date.
        /// </summary>
        /// <value>The Save file name with date.</value>
        String SaveFileNameWithDate { get; }

        /// <summary>
        /// Saves the file.
        /// </summary>
        /// <param name="IncludeDateInFileName">if set to <c>true</c> [include date in file name].</param>
        /// <returns></returns>
        String SaveFile(Boolean IncludeDateInFileName);

        /// <summary>
        /// Gets or sets a value indicating whether this instance is attachment.
        /// </summary>
        /// <value>
        /// 	<c>true</c> if this instance is attachment; otherwise, <c>false</c>.
        /// </value>
        Boolean IsAttachment { get; set; }

        /// <summary>
        /// Gets the hash code for the data in a case insensative way
        /// </summary>
        /// <returns></returns>
        int GetHashCodeCaseInsensitive();

    }
}
