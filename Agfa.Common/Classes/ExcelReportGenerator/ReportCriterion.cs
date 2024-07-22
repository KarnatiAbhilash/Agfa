using Agfa.Common.CollectionsExtensions;
using Agfa.Common.ExtensionMethods;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace Agfa.Common
{
    /// <summary>
    /// 
    /// </summary>
    public static class ReportCriterionExtensions
    {
        /// <summary>
        /// Gets the criteria.
        /// </summary>
        /// <param name="Criteria">The criteria.</param>
        /// <param name="Name">The name.</param>
        /// <returns></returns>
        public static IEnumerable<ReportCriterion> GetCriteria(this IEnumerable<ReportCriterion> Criteria, String Name)
        {
            if (String.IsNullOrEmpty(Name) || Criteria == null)
            {
                yield break;
            }
            foreach (ReportCriterion Crit in Criteria)
            {
                if (Crit != null && Crit.Name.Equals(Name, StringComparison.CurrentCultureIgnoreCase))
                {
                    yield return Crit;
                }
            }
        }

        /// <summary>
        /// Gets the first value.
        /// </summary>
        /// <param name="Criteria">The criteria.</param>
        /// <returns></returns>
        public static String GetFirstValue(this IEnumerable<ReportCriterion> Criteria)
        {
            if (Criteria == null || Criteria.First() == null || Criteria.First().Values == null || Criteria.First().Values.Length == 0)
            {
                return null;
            }

            return Criteria.First().Values.First();
        }
        /// <summary>
        /// Create an HTML table for the report criteria
        /// </summary>
        /// <param name="ReportCriteria">The report criteria.</param>
        /// <param name="BorderWidth">Width of the border.</param>
        /// <param name="ReportHeader">The report header.</param>
        /// <returns></returns>
        public static String ToHtmlTable(this IEnumerable<ReportCriterion> ReportCriteria, int BorderWidth, String ReportHeader)
        {
            return ReportCriterion.ToHtmlTable(ReportCriteria, BorderWidth, ReportHeader);
        }
    }

    /// <summary>
    /// Report Criteria to display on the report
    /// </summary>
    [Serializable]
    [SoapType]
    [XmlRoot]
    public class ReportCriterion
    {
        /// <summary>
        /// Gets or sets the name of the report criteria filter
        /// </summary>
        /// <value>The name.</value>
        [XmlElement]
        [SoapElement]
        public String Name { get; set; }

        /// <summary>
        /// Gets or sets the values used to filter
        /// </summary>
        /// <value>The values.</value>
        [XmlElement]
        [SoapElement]
        public String[] Values { get; set; }

        /// <summary>
        /// Gets a value indicating whether this instance has value.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance has value; otherwise, <c>false</c>.
        /// </value>
        public Boolean HasValue
        {
            get
            {
                if (Values == null || Values.Length == 0)
                {
                    return false;
                }

                if (String.IsNullOrEmpty(Values.ToDelimitedString("")))
                {
                    return false;
                }

                return true;
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ReportCriterion"/> class.
        /// </summary>
        public ReportCriterion()
        {
            this.Name = String.Empty;
            this.Values = new String[] { };
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ReportCriterion"/> class.
        /// </summary>
        /// <param name="Name">The name.</param>
        /// <param name="Values">The values.</param>
        public ReportCriterion(String Name, String[] Values)
        {
            this.Name = Name.Trim();
            this.Values = Values;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ReportCriterion"/> class.
        /// </summary>
        /// <param name="Name">The name.</param>
        /// <param name="Value">The value.</param>
        public ReportCriterion(String Name, String Value)
        {
            this.Name = Name.Trim();
            this.Values = new String[] { Value };
        }

        /// <summary>
        /// Returns a <see cref="System.String"/> SML that represents this instance.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String"/> that represents this instance.
        /// </returns>
        public override String ToString()
        {
            StringBuilder strbToString = new StringBuilder();

            strbToString.Append("<ReportCriterion>");
            strbToString.AppendFormat("<Name>{0}</Name>", this.Name);

            strbToString.Append("<Values>");
            foreach (String value in this.Values)
            {
                strbToString.AppendFormat("<Value>{0}</Value>", value);
            }
            strbToString.Append("</Values>");
            strbToString.Append("</ReportCriterion>");
            return strbToString.ToString();
        }

        /// <summary>
        /// Returns an HTML table row.
        /// </summary>
        /// <returns></returns>
        public String ToHTMLTableRow()
        {
            StringBuilder strbToString = new StringBuilder();

            strbToString.Append("<tr>");
            strbToString.AppendFormat("<td>{0}</td>", this.Name);

            strbToString.Append("<td> ");
            foreach (String value in this.Values)
            {
                strbToString.AppendFormat("{0},", value);
            }
            strbToString.Remove(strbToString.Length - 1, 1);
            strbToString.Append("</td>");
            strbToString.Append("</tr>");


            return strbToString.ToString();
        }

        /// <summary>
        /// Returns an HTML table
        /// </summary>
        /// <param name="ReportCriteria">The report criteria.</param>
        /// <param name="BorderWidth">Width of the border.</param>
        /// <param name="ReportHeader">The report header.</param>
        /// <returns></returns>
        public static String ToHtmlTable(IEnumerable<ReportCriterion> ReportCriteria, int BorderWidth, String ReportHeader)
        {
            StringBuilder strbHTML = new StringBuilder();
            strbHTML.AppendFormat("<table border=\"{0}\">", BorderWidth);

            if (!String.IsNullOrEmpty(ReportHeader))
            {
                strbHTML.Append("<thead>");
                strbHTML.Append("<tr>");
                strbHTML.Append("<th colspan=\"2\" align=\"center\">");
                strbHTML.Append(ReportHeader);
                strbHTML.Append("</th>");
                strbHTML.Append("</tr>");
                strbHTML.Append("</thead>");
            }

            strbHTML.Append("<tbody>");

            foreach (ReportCriterion crit in ReportCriteria)
            {
                strbHTML.Append(crit.ToHTMLTableRow());
            }

            strbHTML.Append("</tbody>");
            strbHTML.Append("</table>");
            return strbHTML.ToString();
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

            ReportCriterion rc = (ReportCriterion)obj;
            if (this.Name != rc.Name)
            {
                return false;
            }
            if (this.Values.Length != rc.Values.Length)
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

        /// <summary>
        /// Creates this instance.
        /// </summary>
        /// <returns></returns>
        public static ReportCriterion Create()
        {
            return new ReportCriterion();
        }

        /// <summary>
        /// Creates the specified name.
        /// </summary>
        /// <param name="Name">The name.</param>
        /// <param name="Values">The values.</param>
        /// <returns></returns>
        public static ReportCriterion Create(String Name, String[] Values)
        {
            return new ReportCriterion(Name, Values);
        }

        /// <summary>
        /// Creates the specified name.
        /// </summary>
        /// <param name="Name">The name.</param>
        /// <param name="Value">The value.</param>
        /// <returns></returns>
        public static ReportCriterion Create(String Name, String Value)
        {
            return new ReportCriterion(Name, Value);
        }


    }
}
