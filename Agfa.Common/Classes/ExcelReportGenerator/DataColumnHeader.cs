using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace Agfa.Common
{
    /// <summary>
    /// A column header for an excel dump using dataset.tohtmltable or datatable.tohtmltable
    /// </summary>
    [Serializable]
    [SoapType]
    [XmlRoot]
    public class DataColumnHeader
    {
        /// <summary>
        /// Gets or sets the name of data column in the data table
        /// </summary>
        /// <value>The data column name</value>
        [XmlElement]
        [SoapElement]
        public String DataColumn { get; set; }

        /// <summary>
        /// Gets or sets the desired header text.
        /// </summary>
        /// <value>The header text.</value>
        [XmlElement]
        [SoapElement]
        public String HeaderText { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="DataColumnHeader"/> class.
        /// </summary>
        public DataColumnHeader()
        {
            this.DataColumn = String.Empty;
            this.HeaderText = String.Empty;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DataColumnHeader"/> class.
        /// </summary>
        /// <param name="DataColumn">The data column.</param>
        /// <param name="HeaderText">The header text.</param>
        public DataColumnHeader(String DataColumn, String HeaderText)
        {
            this.DataColumn = DataColumn.Trim();
            this.HeaderText = HeaderText.Trim();
        }

        /// <summary>
        /// Creates this instance.
        /// </summary>
        /// <returns></returns>
        public static DataColumnHeader Create()
        {
            return new DataColumnHeader();
        }

        /// <summary>
        /// Creates the specified data column.
        /// </summary>
        /// <param name="DataColumn">The data column.</param>
        /// <param name="HeaderText">The header text.</param>
        /// <returns></returns>
        public static DataColumnHeader Create(String DataColumn, String HeaderText)
        {
            return new DataColumnHeader(DataColumn, HeaderText);
        }
    }
}
