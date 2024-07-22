using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace Agfa.Common
{
    /// <summary>
    /// Configuration information for an excel data dump report
    /// </summary>
    [Serializable]
    [SoapType]
    [XmlRoot]
    public class ExcelReportConfiguration : IDisposable
    {
        /// <summary>
        /// Gets or sets the width of the border.
        /// </summary>
        /// <value>The width of the border.</value>
        [XmlElement]
        [SoapElement]
        public int BorderWidth { get; set; }

        /// <summary>
        /// Gets or sets the report title.
        /// </summary>
        /// <value>The report title.</value>
        [XmlElement]
        [SoapElement]
        public String ReportTitle { get; set; }

        /// <summary>
        /// Gets or sets the header.
        /// </summary>
        /// <value>The header.</value>
        [XmlElement]
        [SoapElement]
        public ReportHeader Header { get; set; }
        /// <summary>
        /// Gets or sets the footer.
        /// </summary>
        /// <value>The footer.</value>
        [XmlElement]
        [SoapElement]
        public ReportFooter Footer { get; set; }

        /// <summary>
        /// 
        /// </summary>
        private List<ReportCriterion> lstReportCriteria = new List<ReportCriterion>();
        /// <summary>
        /// Gets or sets the report criteria.
        /// </summary>
        /// <value>The report criteria.</value>
        [XmlElement]
        [SoapElement]
        public ReportCriterion[] ReportCriteria
        {
            get
            {
                return this.lstReportCriteria.ToArray();
            }
            set
            {
                this.lstReportCriteria.Clear();
                this.lstReportCriteria.AddRange(value);

            }
        }


        private List<String> lstHiddenColumns = new List<String>();
        /// <summary>
        /// Gets or sets the hidden columns.
        /// </summary>
        /// <value>The hidden columns.</value>
        [XmlElement]
        [SoapElement]
        public String[] HiddenColumns
        {
            get
            {
                return this.lstHiddenColumns.ToArray();
            }
            set
            {
                this.lstHiddenColumns.Clear();
                this.lstHiddenColumns.AddRange(value);
            }
        }


        private List<DataColumnHeader> lstDataColumnHeaders = new List<DataColumnHeader>();
        /// <summary>
        /// Gets or sets the data column headers.
        /// </summary>
        /// <value>The data column headers.</value>
        [XmlElement]
        [SoapElement]
        public DataColumnHeader[] DataColumnHeaders
        {
            get
            {
                return this.lstDataColumnHeaders.ToArray();
            }
            set
            {
                this.lstDataColumnHeaders.Clear();
                this.lstDataColumnHeaders.AddRange(value);
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ExcelReportConfiguration"/> class.
        /// </summary>
        public ExcelReportConfiguration()
        {
            this.BorderWidth = 0;
            this.Header = new ReportHeader();
            this.Footer = new ReportFooter();
            this.ReportTitle = String.Empty;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ExcelReportConfiguration"/> class.
        /// </summary>
        /// <param name="BorderWidth">Width of the border.</param>
        /// <param name="ReportTitle">The report title.</param>
        /// <param name="Header">The header.</param>
        /// <param name="Footer">The footer.</param>
        /// <param name="ReportCriteria">The report criteria.</param>
        /// <param name="HiddenColumns">The hidden columns.</param>
        /// <param name="DataColumnHeaders">The data column headers.</param>
        public ExcelReportConfiguration(int BorderWidth, String ReportTitle, ReportHeader Header, ReportFooter Footer, ReportCriterion[] ReportCriteria, String[] HiddenColumns, DataColumnHeader[] DataColumnHeaders)
        {
            this.BorderWidth = BorderWidth;
            this.Header = Header;
            this.Footer = Footer;
            this.ReportTitle = ReportTitle.Trim();
            this.ReportCriteria = ReportCriteria;
            this.HiddenColumns = HiddenColumns;
            this.DataColumnHeaders = DataColumnHeaders;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ExcelReportConfiguration"/> class.
        /// </summary>
        /// <param name="BorderWidth">Width of the border.</param>
        /// <param name="ReportTitle">The report title.</param>
        /// <param name="Header">The header.</param>
        /// <param name="Footer">The footer.</param>
        /// <param name="ReportCriteria">The report criteria.</param>
        /// <param name="HiddenColumns">The hidden columns.</param>
        /// <param name="DataColumnHeaders">The data column headers.</param>
        public ExcelReportConfiguration(int BorderWidth, String ReportTitle, ReportHeader Header, ReportFooter Footer, ReportCriterion ReportCriteria, String HiddenColumns, DataColumnHeader DataColumnHeaders)
        {
            this.BorderWidth = BorderWidth;
            this.Header = Header;
            this.Footer = Footer;
            this.ReportTitle = ReportTitle.Trim();
            this.ReportCriteria = new ReportCriterion[] { ReportCriteria };
            this.HiddenColumns = new String[] { HiddenColumns };
            this.DataColumnHeaders = new DataColumnHeader[] { DataColumnHeaders };
        }


        /// <summary>
        /// Gets a value indicating whether this <see cref="ExcelReportConfiguration"/> is valid.
        /// </summary>
        /// <value><c>true</c> if valid; otherwise, <c>false</c>.</value>
        public Boolean Valid
        {
            get
            {
                if (String.IsNullOrEmpty(ReportTitle))
                {
                    return false;
                }

                return true;
            }
        }

        #region IDisposable Members

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            if (this.lstHiddenColumns != null)
            {
                this.lstHiddenColumns.Clear();
            }
            if (this.lstDataColumnHeaders != null)
            {
                this.lstDataColumnHeaders.Clear();
            }
            if (this.lstReportCriteria != null)
            {
                this.lstReportCriteria.Clear();
            }

        }

        #endregion
    }
}
