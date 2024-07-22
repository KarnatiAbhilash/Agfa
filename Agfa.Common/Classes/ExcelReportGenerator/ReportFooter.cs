using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace Agfa.Common
{
    /// <summary>
    /// Extra information to include in the report footer
    /// </summary>
    [Serializable]
    [SoapType]
    [XmlRoot]
    public class ReportFooter
    {
        /// <summary>
        /// Gets or sets the footer text.
        /// </summary>
        /// <value>The footer text.</value>
        [XmlElement]
        [SoapElement]
        public String FooterText { get; set; }
        /// <summary>
        /// Gets or sets the alignment.
        /// </summary>
        /// <value>The alignment.</value>
        [XmlElement]
        [SoapElement]
        public Alignments Alignment { get; set; }
        /// <summary>
        /// Initializes a new instance of the <see cref="ReportFooter"/> class.
        /// </summary>
        public ReportFooter()
        {
            this.FooterText = String.Empty;
            this.Alignment = Alignments.Left;
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="ReportFooter"/> class.
        /// </summary>
        /// <param name="FooterText">The footer text.</param>
        /// <param name="Alignment">The alignment.</param>
        public ReportFooter(String FooterText, Alignments Alignment)
        {
            this.FooterText = FooterText;
            this.Alignment = Alignment;
        }

        /// <summary>
        /// Returns a <see cref="System.String"/> XML that represents this instance.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String"/> that represents this instance.
        /// </returns>
        public override String ToString()
        {
            StringBuilder strbToString = new StringBuilder();

            strbToString.Append("<ReportFooter>");
            strbToString.AppendFormat("<FooterText>{0}</FooterText>", this.FooterText);
            strbToString.AppendFormat("<Alignment value=\"{0}\">{1}</Alignment>", (int)this.Alignment, this.Alignment.ToString());
            strbToString.Append("</ReportFooter>");
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

            ReportFooter rc = (ReportFooter)obj;
            if (this.FooterText != rc.FooterText || this.Alignment != rc.Alignment)
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
