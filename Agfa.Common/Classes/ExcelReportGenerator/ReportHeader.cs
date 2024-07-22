using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Xml.Serialization;

namespace Agfa.Common
{
    /// <summary>
    /// Header information to display in a report
    /// </summary>
    [Serializable]
    [SoapType]
    [XmlRoot]
    [DataContract]
    public class ReportHeader
    {
        /// <summary>
        /// Gets or sets the header text.
        /// </summary>
        /// <value>The header text.</value>
        [XmlElement]
        [SoapElement]
        [DataMember]
        public String HeaderText { get; set; }

        /// <summary>
        /// Person Who Ran the Report
        /// </summary>
        /// <value>
        /// The run by.
        /// </value>
        [XmlElement]
        [SoapElement]
        [DataMember]
        public String RunBy { get; set; }
        /// <summary>
        /// Time when the report was run
        /// </summary>
        /// <value>
        /// The run time.
        /// </value>
        [XmlElement]
        [SoapElement]
        [DataMember]
        public DateTime RunTime { get; set; }
        /// <summary>
        /// Gets or sets the alignment.
        /// </summary>
        /// <value>The alignment.</value>
        [XmlElement]
        [SoapElement]
        [DataMember]
        public Alignments Alignment { get; set; }
        /// <summary>
        /// Initializes a new instance of the <see cref="ReportHeader"/> class.
        /// </summary>
        public ReportHeader()
        {
            this.HeaderText = String.Empty;
            this.Alignment = Alignments.Left;
            this.RunBy = String.Empty;
            this.RunTime = DateTime.Now;
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="ReportHeader"/> class.
        /// </summary>
        /// <param name="HeaderText">The header text.</param>
        /// <param name="Alignment">The alignment.</param>
        public ReportHeader(String HeaderText, Alignments Alignment)
        {
            this.HeaderText = HeaderText;
            this.Alignment = Alignment;
            this.RunBy = String.Empty;
            this.RunTime = DateTime.Now;
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="ReportHeader"/> class.
        /// </summary>
        /// <param name="HeaderText">The header text.</param>
        /// <param name="Alignment">The alignment.</param>
        /// <param name="RunBy">The run by.</param>
        /// <param name="RunTime">The run time.</param>
        public ReportHeader(String HeaderText, Alignments Alignment, String RunBy, DateTime RunTime)
        {
            this.HeaderText = HeaderText;
            this.Alignment = Alignment;
            this.RunBy = RunBy;
            this.RunTime = RunTime;
        }


        /// <summary>
        /// Returns a <see cref="System.String"/> that represents this instance.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String"/> that represents this instance.
        /// </returns>
        public override String ToString()
        {
            StringBuilder strbToString = new StringBuilder();

            strbToString.Append("<ReportHeader>");
            strbToString.AppendFormat("<HeaderText>{0}</HeaderText>", this.HeaderText);
            strbToString.AppendFormat("<RunBy>{0}</RunBy>", this.RunBy);
            strbToString.AppendFormat("<RunTime>{0}</RunTime>", this.RunTime);
            strbToString.AppendFormat("<Alignment value=\"{0}\">{1}</Alignment>", (int)this.Alignment, this.Alignment.ToString());
            strbToString.Append("</ReportHeader>");
            return strbToString.ToString();
        }


        /// <summary>
        /// Determines whether the specified <see cref="System.Object"/> is equal to this instance.
        /// </summary>
        /// <param name="obj">The <see cref="System.Object"/> to compare with this instance.</param>
        /// <returns>
        /// 	<c>true</c> if the specified <see cref="System.Object"/> is equal to this instance; otherwise, <c>false</c>.
        /// </returns>
        /// <exception cref="T:System.NullReferenceException">
        /// The <paramref name="obj"/> parameter is null.
        /// </exception>
        public override bool Equals(object obj)
        {
            if (obj == null)
            {
                return false;
            }
            if (!ReferenceEquals(this, obj))
            {
                return false;
            }

            if (!(obj is ReportCriterion))
            {
                return false;
            }

            ReportHeader rc = (ReportHeader)obj;
            if (this.HeaderText != rc.HeaderText || this.Alignment != rc.Alignment)
            {
                return false;
            }

            if (this.GetHashCode() != rc.GetHashCode())
            {
                return false;
            }

            return base.Equals(obj);
        }

        /// <summary>
        /// Returns a hash code for this instance.
        /// </summary>
        /// <returns>
        /// A hash code for this instance, suitable for use in hashing algorithms and data structures like a hash table. 
        /// </returns>
        public override int GetHashCode()
        {
            return 37 ^ this.ToString().GetHashCode();
        }


    }
}
