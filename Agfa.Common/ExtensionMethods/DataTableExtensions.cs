using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Agfa.Common.ExtensionMethods
{
    public static partial class DataTableExtensions
    {
        /// <summary>
        /// Select Distinct rows from a data table
        /// </summary>
        /// <param name="dt">The dt.</param>
        /// <param name="tableName">Name of the table for the returned table</param>
        /// <param name="columnNames">Columns to include</param>
        /// <returns></returns>
        public static DataTable SelectDistinct(this DataTable dt, String tableName, params String[] columnNames)
        {
            if (dt != null)
            {
                return dt.DefaultView.ToTable(tableName, true, columnNames);
            }
            else
            {
                return dt;
            }
        }
        /// <summary>
        /// Select Distinct rows from a data table
        /// </summary>
        /// <param name="dt">The dt.</param>
        /// <param name="columnNames">Columns to include</param>
        /// <returns></returns>
        public static DataTable SelectDistinct(this DataTable dt, params String[] columnNames)
        {

            if (dt != null)
            {
                return dt.DefaultView.ToTable(true, columnNames);
            }
            else
            {
                return dt;
            }
        }


        #region DataTable To HTML

        /// <summary>
        /// Create an HTML Table from a Data Table
        /// </summary>
        /// <param name="dt">The dt.</param>
        /// <returns></returns>
        public static String ToHTMLTable(this DataTable dt)
        {
            return ToHTMLTable(dt, 0, null, null, null);
        }

        /// <summary>
        /// Create an HTML Table from a Data Table
        /// </summary>
        /// <param name="dt">The dt.</param>
        /// <param name="BorderWidth">Width of the border.</param>
        /// <returns></returns>
        public static String ToHTMLTable(this DataTable dt, int BorderWidth)
        {
            return ToHTMLTable(dt, BorderWidth, null, null, null);
        }

        /// <summary>
        /// Create an HTML Table from a Data Table
        /// </summary>
        /// <param name="dt">The dt.</param>
        /// <param name="BorderWidth">Width of the border.</param>
        /// <param name="HeaderText">The header text.</param>
        /// <returns></returns>
        public static String ToHTMLTable(this DataTable dt, int BorderWidth, String HeaderText)
        {
            return ToHTMLTable(dt, BorderWidth, HeaderText, null, null);
        }

        /// <summary>
        /// Create an HTML Table from a Data Table
        /// </summary>
        /// <param name="dt">The dt.</param>
        /// <param name="BorderWidth">Width of the border.</param>
        /// <param name="HeaderText">The header text.</param>
        /// <param name="FooterText">The footer text.</param>
        /// <returns></returns>
        public static String ToHTMLTable(this DataTable dt, int BorderWidth, String HeaderText, String FooterText)
        {
            return ToHTMLTable(dt, BorderWidth, HeaderText, FooterText, null);
        }

        /// <summary>
        /// Create an HTML Table from a Data Table
        /// </summary>
        /// <param name="dt">The dt.</param>
        /// <param name="BorderWidth">Width of the border.</param>
        /// <param name="HeaderText">The header text.</param>
        /// <param name="FooterText">The footer text.</param>
        /// <param name="ColumnSuppressionList">The column suppression list.</param>
        /// <returns></returns>
        public static String ToHTMLTable(this DataTable dt, int BorderWidth, String HeaderText, String FooterText, String[] ColumnSuppressionList)
        {
            return ToHTMLTable(dt, BorderWidth, HeaderText, Alignments.Center, FooterText, Alignments.Left, ColumnSuppressionList, null);
        }
        /// <summary>
        /// Create an HTML Table from a Data Table
        /// </summary>
        /// <param name="dt">The dt.</param>
        /// <param name="BorderWidth">Width of the border.</param>
        /// <param name="HeaderText">The header text.</param>
        /// <param name="FooterText">The footer text.</param>
        /// <param name="ColumnSuppressionList">The column suppression list.</param>
        /// <param name="DataColumnHeaders">The data column headers.</param>
        /// <returns></returns>
        public static String ToHTMLTable(this DataTable dt, int BorderWidth, String HeaderText, Alignments HeaderAlignment, String FooterText, Alignments FooterAlignment, String[] ColumnSuppressionList, DataColumnHeader[] DataColumnHeaders)
        {
            if (dt == null || dt.Columns.Count == 0 || dt.Rows.Count == 0)
            {
                return String.Empty;
            }
            int intColumnsCount = dt.Columns.Count;
            int intVisibleColumns = intColumnsCount;


            if (ColumnSuppressionList != null && ColumnSuppressionList.Length > 0 && ColumnSuppressionList.Length <= intColumnsCount)
            {
                if (ColumnSuppressionList.Length == 1 && String.IsNullOrEmpty(ColumnSuppressionList[0]))
                {
                    ColumnSuppressionList = null;
                }
                else
                {
                    intVisibleColumns -= ColumnSuppressionList.Length;
                }
            }

            String strColumnName = String.Empty;
            String strHeader = String.Empty;
            StringBuilder strbHTML = new StringBuilder();
            strbHTML.AppendFormat("<table id=\"{0}\" class=\"{1}\" {2}>", dt.GetType().GUID == Guid.Empty ? Guid.NewGuid() : dt.GetType().GUID, dt.GetType().Name, BorderWidth <= 0 ? String.Empty : "border=\"" + BorderWidth.ToString() + "\" ");
            strbHTML.Append("<thead>");

            if (!String.IsNullOrEmpty(HeaderText))
            {
                strbHTML.Append("<tr>");

                strbHTML.AppendFormat("<th colspan=\"{0}\" align=\"{1}\">", intVisibleColumns, HeaderAlignment.ToString().ToLower()).Append(HeaderText).Append("</th>");

                strbHTML.Append("</tr>");
            }

            strbHTML.Append("<tr>");
            if (ColumnSuppressionList != null && ColumnSuppressionList.Length > 0 && ColumnSuppressionList.Length <= intColumnsCount)
            {
                foreach (var strColumn in ColumnSuppressionList)
                {
                    if (dt.Columns.Contains(strColumn)) dt.Columns.Remove(strColumn);
                }
            }



            if (DataColumnHeaders != null && DataColumnHeaders.Length > 0)
            {
                foreach (DataColumn dc in dt.Columns)
                {
                    strColumnName = dc.ColumnName.Trim();
                    strHeader = (from ColumnHeader in DataColumnHeaders where ColumnHeader.DataColumn.Equals(strColumnName, StringComparison.CurrentCultureIgnoreCase) select ColumnHeader.HeaderText).FirstOrDefault();
                    if (!String.IsNullOrEmpty(strHeader))
                    {
                        strbHTML.Append("<th>").Append(strHeader.Trim()).Append("</th>");
                    }
                    else
                    {
                        strbHTML.Append("<th>").Append(strColumnName).Append("</th>");
                    }

                }
            }
            else
            {
                foreach (DataColumn dc in dt.Columns)
                {
                    strColumnName = dc.ColumnName.Trim();
                    strbHTML.Append("<th>").Append(strColumnName).Append("</th>");
                }
            }




            strbHTML.Append("</tr>");
            strbHTML.Append("</thead>");
            strbHTML.Append("<tbody>");
            foreach (DataRow dr in dt.Rows)
            {
                strbHTML.Append(ToHTMLTableRow(dr, dt.Columns.Count));
            }
            strbHTML.Append("</tbody>");
            //Not blank and not a line break, add footer the the table
            if (!String.IsNullOrEmpty(FooterText) && !(FooterText.Equals("<br/>", StringComparison.CurrentCultureIgnoreCase) || FooterText.Equals("<br />", StringComparison.CurrentCultureIgnoreCase)))
            {
                strbHTML.Append("<tfoot>");
                strbHTML.Append("<tr>");

                strbHTML.AppendFormat("<td colspan=\"{0}\" align=\"{1}\">", intVisibleColumns, FooterAlignment.ToString().ToLower()).Append(FooterText).Append("</td>");

                strbHTML.Append("</tr>");
                strbHTML.Append("</tfoot>");
                strbHTML.Append("</table>");
            }
            else if (!String.IsNullOrEmpty(FooterText))//intended a line break after table, add line break after closing table tag
            {
                strbHTML.Append("</table>");
                strbHTML.Append("<br/>");
            }
            else//was null, just close table
            {
                strbHTML.Append("</table>");
            }
            return strbHTML.ToString();
        }

        /// <summary>
        /// Creates a HTML Table Row from a data table row
        /// </summary>
        /// <param name="dr">The dr.</param>
        /// <param name="Columns">The columns.</param>
        /// <returns></returns>
        private static String ToHTMLTableRow(DataRow dr, int Columns)
        {
            if (dr == null)
            {
                return String.Empty;
            }
            StringBuilder strbHTML = new StringBuilder();

            strbHTML.Append("<tr>");
            for (int x = 0; x < Columns; x++)
            {
                var val = dr[x].ToString();
                strbHTML.Append("<td>").Append(val == null ? "&nbsp;" : val).Append("</td>");
            }
            strbHTML.Append("</tr>");
            return strbHTML.ToString();
        }

        #endregion

        #region DataSet Extensions

        /// <summary>
        /// Converts a dataset to HTML
        /// </summary>
        /// <param name="ds">The ds.</param>
        /// <param name="BorderWidth">Width of the border.</param>
        /// <param name="HeaderText">The header text.</param>
        /// <param name="FooterText">The footer text.</param>
        /// <param name="ColumnSuppressionList">The column suppression list.</param>
        /// <returns></returns>
        public static String ToHTMLTable(this DataSet ds, int BorderWidth, String HeaderText, String FooterText, String[] ColumnSuppressionList)
        {
            return ToHTMLTable(ds, BorderWidth, HeaderText, FooterText, ColumnSuppressionList, null);
        }

        public static String ToHTMLTable(this DataSet ds, int BorderWidth, String HeaderText, String FooterText, String[] ColumnSuppressionList, DataColumnHeader[] DataColumnHeaders)
        {
            return ToHTMLTable(ds, BorderWidth, HeaderText, Alignments.Center, FooterText, Alignments.Left, ColumnSuppressionList, DataColumnHeaders);
        }
        /// <summary>
        /// Converts a dataset to HTML
        /// </summary>
        /// <param name="ds">The ds.</param>
        /// <param name="BorderWidth">Width of the border.</param>
        /// <param name="HeaderText">The header text.</param>
        /// <param name="FooterText">The footer text.</param>
        /// <param name="ColumnSuppressionList">The column suppression list.</param>
        /// <param name="DataColumnHeaders">The data column headers.</param>
        /// <returns></returns>
        public static String ToHTMLTable(this DataSet ds, int BorderWidth, String HeaderText, Alignments HeaderAlignment, String FooterText, Alignments FooterAlignment, String[] ColumnSuppressionList, DataColumnHeader[] DataColumnHeaders)
        {
            if (ds == null || ds.Tables.Count == 0)
            {
                return String.Empty;
            }
            int intTableCount = ds.Tables.Count;
            int intCurrentTable = 0;

            StringBuilder strbHTML = new StringBuilder();

            foreach (DataTable dt in ds.Tables)
            {
                intCurrentTable += 1;
                if (intTableCount == 1)//Only one table, show header and footer
                {
                    strbHTML.AppendLine(dt.ToHTMLTable(BorderWidth, HeaderText, HeaderAlignment, FooterText, FooterAlignment, ColumnSuppressionList, DataColumnHeaders));
                }
                else if (intCurrentTable == 1 && intCurrentTable < intTableCount)//First Table, show header only,Line break on footer
                {
                    strbHTML.AppendLine(dt.ToHTMLTable(BorderWidth, HeaderText, HeaderAlignment, "<br/>", FooterAlignment, ColumnSuppressionList, DataColumnHeaders));
                }
                else if (intCurrentTable > 1 && intCurrentTable < intTableCount)//Sub Table, show No Headers, No Footer,line break on footer
                {
                    strbHTML.AppendLine(dt.ToHTMLTable(BorderWidth, String.Empty, HeaderAlignment, "<br/>", FooterAlignment, ColumnSuppressionList, DataColumnHeaders));
                }
                else//last Table, show footer only
                {
                    strbHTML.AppendLine(dt.ToHTMLTable(BorderWidth, String.Empty, HeaderAlignment, FooterText, FooterAlignment, ColumnSuppressionList, DataColumnHeaders));
                }


            }
            return strbHTML.ToString();
        }
        #endregion



    }
}
