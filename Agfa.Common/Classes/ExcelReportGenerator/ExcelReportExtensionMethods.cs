using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Agfa.Common
{
    public static partial class Extensions
    {


        /// <summary>
        /// Creates an Excel Report from a Data Table
        /// </summary>
        /// <param name="reader">The reader.</param>
        /// <param name="ReportConfiguration">The report configuration.</param>
        /// <returns></returns>
        public static ClientFileStream CreateExcelReport(this IDataReader reader, ExcelReportConfiguration ReportConfiguration)
        {
            return ExcelReport.CreateExcelReport(reader, ReportConfiguration);
        }


        /// <summary>
        /// Creates an Excel Report from a Data Table
        /// </summary>
        /// <param name="dt">The datatable.</param>
        /// <param name="ReportConfiguration">The report configuration.</param>
        /// <returns></returns>
        public static ClientTextFile CreateExcelReport(this DataTable dt, ExcelReportConfiguration ReportConfiguration)
        {
            return ExcelReport.CreateExcelReport(dt, ReportConfiguration);
        }

        /// <summary>
        /// Creates an Excel Report from a DataSet
        /// </summary>
        /// <param name="ds">The ds.</param>
        /// <param name="ReportConfiguration">The report configuration.</param>
        /// <returns></returns>
        public static ClientTextFile CreateExcelReport(this DataSet ds, ExcelReportConfiguration ReportConfiguration)
        {
            return ExcelReport.CreateExcelReport(ds, ReportConfiguration);
        }

    }
}
