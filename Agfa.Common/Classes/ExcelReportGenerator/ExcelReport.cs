using Agfa.Common.CollectionsExtensions;
using Agfa.Common.ExtensionMethods;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;

namespace Agfa.Common
{

    public class ExcelWriter2007 : IDisposable
    {
        public const String DefaultFont = "Calibri";
        public const int DefaultFontSize = 11;
        public String Title { get; set; }
        public String Author { get; set; }
        public String Subject { get; set; }
        public String Keywords { get; set; }
        public String Category { get; set; }
        public String Comments { get; set; }
        public String Company { get; set; }
        public String HyperlinkBase { get; set; }

        private String strTempDirectory = null;
        private String TempDirectory
        {
            get
            {
                if (String.IsNullOrEmpty(strTempDirectory))
                {
                    strTempDirectory = Path.Combine(Path.GetTempPath(), Path.GetRandomFileName());
                    // strTempDirectory = Path.Combine("C:\\", Path.GetRandomFileName());
                    Directory.CreateDirectory(TempDirectory);
                }
                return strTempDirectory;
            }
        }

        private String generatedFileName = null;
        private FileStream readonlyFileStream = null;

        private String GetExcelColumnName(int columnNumber)
        {
            int dividend = columnNumber;
            string columnName = String.Empty;
            int modulo;

            while (dividend > 0)
            {
                modulo = (dividend - 1) % 26;
                columnName = Convert.ToChar(65 + modulo).ToString() + columnName;
                dividend = (int)((dividend - modulo) / 26);
            }

            return columnName;
        }

        private String[] GetColumnHeaders(DataTable ReaderSchema)
        {
            List<String> lst = new List<String>();
            foreach (DataRow row in ReaderSchema.Rows)
            {
                lst.Add(row[0].ToString());
            }
            return lst.ToArray();
        }


        public String CreateExcel2007File(IDataReader reader, ReportCriterion[] Header, String FileName)
        {

            int iTotalCells = 0;
            int iTableHeaderRowNumber = Header.Count() + 3;
            int iRowCount = iTableHeaderRowNumber;
            //Ramu Added
            int iStartCellNumber = 0;
            int iEndCellNumber = 0;
            //End
            String[] straColumnHeader = new String[] { String.Empty };
            generatedFileName = Path.Combine(TempDirectory, FileName.AppendIfMissing(".xlsx"));
            FileInfo newFile = new FileInfo(generatedFileName);

            if (File.Exists(generatedFileName))
            {
                File.Delete(generatedFileName);
            }






            // ok, we can run the real code of the sample now
            using (ExcelPackage xlPackage = new ExcelPackage(newFile))
            {
                // set some core property values
                xlPackage.Workbook.Properties.Title = this.Title.Default(String.Empty);
                xlPackage.Workbook.Properties.Author = this.Author.Default(String.Empty);
                xlPackage.Workbook.Properties.Subject = this.Subject.Default(String.Empty);
                xlPackage.Workbook.Properties.Keywords = this.Keywords.Default(String.Empty);
                xlPackage.Workbook.Properties.Category = this.Category.Default(String.Empty);
                xlPackage.Workbook.Properties.Comments = this.Comments.Default(String.Empty);
                xlPackage.Workbook.Properties.Company = this.Company.Default(String.Empty);

                if (!String.IsNullOrEmpty(this.HyperlinkBase))
                {
                    xlPackage.Workbook.Properties.HyperlinkBase = new Uri(this.HyperlinkBase);
                }

                ExcelWorksheet worksheet = xlPackage.Workbook.Worksheets.Add(this.Title);

                if (worksheet != null)
                {


                    worksheet.View.ShowGridLines = false;
                    //Report Title
                    worksheet.Cells[1, 1].Value = this.Title;
                    


                    //Report Criteria
                    for (int i = 0; i < Header.Length; i++)
                    {
                        worksheet.Cells[i + 2, 1].Value = Header[i].Name;
                        worksheet.Cells[i + 2, 2].Value = Header[i].Values.ToDelimitedString(", ");
                    }

                    //Report Criteria Format
                    using (ExcelRange r = worksheet.Cells[1, 1, Header.Length + 1, 4])
                    {
                        r.Style.Font.SetFromFont(new Font(DefaultFont, DefaultFontSize));//Common font to all systems
                        r.Style.Font.Color.SetColor(Color.Black);
                        r.Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                        r.Style.Border.BorderAround(ExcelBorderStyle.Thin, Color.Black);
                        r.Style.Border.Top.Style = ExcelBorderStyle.Thin;
                        r.Style.Border.Bottom.Style = r.Style.Border.Top.Style;
                        r.Style.Border.Left.Style = r.Style.Border.Top.Style;
                        r.Style.Border.Right.Style = r.Style.Border.Top.Style;
                    }

                    for (int i = 0; i < Header.Length; i++)
                    {
                        using (ExcelRange r = worksheet.Cells[i + 2, 2, i + 2, 4])
                        {
                            r.Merge = true;
                        }

                    }



                    //Report title format
                    using (ExcelRange r = worksheet.Cells[1, 1, 1, 4])
                    {
                        r.Style.Font.SetFromFont(new Font(DefaultFont, DefaultFontSize, FontStyle.Bold));//Common font to all systems              
                        r.Style.Fill.PatternType = ExcelFillStyle.Solid;
                        r.Style.Fill.BackgroundColor.SetColor(Color.FromArgb(240, 240, 240));
                        r.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                        r.Merge = true;
                    }
                    int endRow = 3;
                    if (Header.Length == 2)
                    {
                        endRow = 2;
                    }
                    //using (ExcelRange r = worksheet.Cells[1, 5, endRow, 14])
                    //{
                    //    r.Style.Font.SetFromFont(new Font(DefaultFont, 26, FontStyle.Bold));//Common font to all systems  
                    //    r.Style.Font.UnderLine = true;
                    //    r.Style.Fill.PatternType = ExcelFillStyle.Solid;
                    //    r.Style.Fill.BackgroundColor.SetColor(Color.FromArgb(240, 240, 240));
                    //    r.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    //    r.Style.VerticalAlignment = ExcelVerticalAlignment.Bottom;
                    //    r.Merge = true;
                    //}
                    //using (ExcelRange r = worksheet.Cells[endRow + 1, 5, Header.Length + 1, 14])
                    //{
                    //    r.Style.Font.SetFromFont(new Font(DefaultFont, 14, FontStyle.Bold));//Common font to all systems 
                    //    r.Style.Fill.PatternType = ExcelFillStyle.Solid;
                    //    r.Style.Fill.BackgroundColor.SetColor(Color.FromArgb(240, 240, 240));
                    //    r.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    //    r.Style.VerticalAlignment = ExcelVerticalAlignment.Top;
                    //    r.Merge = true;
                    //}



                    // lets set the header text 
                    worksheet.HeaderFooter.OddHeader.CenteredText = this.Title;
                    // add the page number to the footer plus the total number of pages
                    worksheet.HeaderFooter.OddFooter.RightAlignedText = String.Format("Page {0} of {1}", ExcelHeaderFooter.PageNumber, ExcelHeaderFooter.NumberOfPages);
                    // add the sheet name to the footer
                    worksheet.HeaderFooter.OddFooter.CenteredText = ExcelHeaderFooter.SheetName;
                    // add the file path to the footer
                    //worksheet.HeaderFooter.OddFooter.LeftAlignedText = ExcelHeaderFooter.FilePath + ExcelHeaderFooter.FileName;



                    if (reader != null && !reader.IsClosed)
                    {
                        using (reader)
                        {
                            iTotalCells = reader.FieldCount;
                            using (DataTable schema = reader.GetSchemaTable())
                            {
                                straColumnHeader = GetColumnHeaders(schema);
                            }

                            for (int i = 0; i < iTotalCells; i++)
                            {
                                worksheet.Cells[iTableHeaderRowNumber, i + 1].Value = straColumnHeader[i];

                            }

                            worksheet.View.FreezePanes(iTableHeaderRowNumber + 1, 1); //Rama Added

                            iStartCellNumber = iRowCount + 1;
                            bool isGetData = false;
                            while (reader.Read())
                            {
                                isGetData = true;
                                iRowCount++;
                                for (int iCurrentCell = 0; iCurrentCell < iTotalCells; iCurrentCell++)
                                {
                                    if (reader.IsDBNull(iCurrentCell) || reader[iCurrentCell] == DBNull.Value || reader[iCurrentCell] is DBNull || reader[iCurrentCell] == null)
                                    {
                                        worksheet.Cells[iRowCount, iCurrentCell + 1].Value = String.Empty;
                                    }
                                    else
                                    {
                                        // worksheet.Cells[iRowCount, iCurrentCell + 1].Value =  "T"+reader[iCurrentCell].ToString().Trim();// as  reader[iCurrentCell].getField;
                                        double d;
                                        if (double.TryParse(reader[iCurrentCell].ToString().Trim(), NumberStyles.Any, CultureInfo.InvariantCulture, out d))
                                            worksheet.Cells[iRowCount, iCurrentCell + 1].Value = d;
                                        else
                                        {
                                            DateTime dateTime;
                                            if (DateTime.TryParseExact(reader[iCurrentCell].ToString().Trim(), "MM/dd/yyyy", CultureInfo.InvariantCulture,
    DateTimeStyles.None, out dateTime))
                                            {
                                                worksheet.Cells[iRowCount, iCurrentCell + 1].Value = dateTime.ToOADate();
                                            }
                                            else
                                                worksheet.Cells[iRowCount, iCurrentCell + 1].Value = reader[iCurrentCell].ToString().Trim();
                                        }

                                    }
                                }

                                //zebra rows formatting
                                if (iRowCount % 2 == 1)
                                {
                                    worksheet.Cells[iRowCount, 1, iRowCount, iTotalCells].Style.Fill.PatternType = ExcelFillStyle.Solid;
                                    worksheet.Cells[iRowCount, 1, iRowCount, iTotalCells].Style.Fill.BackgroundColor.SetColor(Color.FromArgb(248, 248, 248));

                                }
                            }
                            iEndCellNumber = iRowCount;
                            using (ExcelRange r = worksheet.Cells[iTableHeaderRowNumber, 1, iRowCount, iTotalCells])
                            {
                                r.Style.Font.SetFromFont(new Font(DefaultFont, DefaultFontSize));//Common font to all systems
                                r.Style.Font.Color.SetColor(Color.Black);
                                r.Style.Border.BorderAround(ExcelBorderStyle.Thin, Color.Black);
                                r.Style.Border.Top.Style = ExcelBorderStyle.Thin;
                                r.Style.Border.Bottom.Style = r.Style.Border.Top.Style;
                                r.Style.Border.Left.Style = r.Style.Border.Top.Style;
                                r.Style.Border.Right.Style = r.Style.Border.Top.Style;
                            }

                            //Apply header style last on purpose
                            using (ExcelRange r = worksheet.Cells[iTableHeaderRowNumber, 1, iTableHeaderRowNumber, iTotalCells])
                            {
                                r.Style.Font.SetFromFont(new Font(DefaultFont, DefaultFontSize, FontStyle.Bold));//Common font to all systems
                                r.Style.Font.Color.SetColor(Color.Black);
                                r.Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                                r.Style.Fill.PatternType = ExcelFillStyle.Solid;
                                r.Style.Fill.BackgroundColor.SetColor(Color.FromArgb(240, 240, 240));
                                r.AutoFilter = true;


                            }


                            //Ramu Applying Code
                            if (reader.NextResult())
                            {
                                if (isGetData == true)
                                {


                                    while (reader.Read())
                                    {
                                        string ColumnNumber = string.Empty;
                                        ColumnNumber = GetColumnNumber(reader[0].ToString(), straColumnHeader);

                                        if (!string.IsNullOrEmpty(ColumnNumber))
                                        {
                                            string ExcelCellsRange = ColumnNumber + "" + iStartCellNumber.ToString() + ":" + ColumnNumber + "" + iEndCellNumber.ToString();

                                            worksheet.Cells[ExcelCellsRange].Style.Numberformat.Format = reader[2].ToString();

                                            switch (reader[1].ToString())
                                            {
                                                case "R":
                                                    worksheet.Cells[ExcelCellsRange].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                                                    break;
                                                case "C":
                                                    worksheet.Cells[ExcelCellsRange].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                                    break;
                                                default:
                                                    worksheet.Cells[ExcelCellsRange].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                                                    break;
                                            }

                                        }
                                    }
                                }
                            }

                            //End

                            //auto fit all columns
                            for (int i = 1; i <= straColumnHeader.Length; i++)
                            {
                                worksheet.Column(i).BestFit = true;
                                worksheet.Column(i).AutoFit();
                                worksheet.Column(i).Style.WrapText = true;
                            }




                            int Index = 0;
                        newSheet:
                            Index = 0;
                            if (reader.NextResult())
                            {
                                while (reader.Read()) // Get Row Collection
                                {

                                    if (Index == 0)
                                    {
                                        iTableHeaderRowNumber = 2;
                                        iRowCount = iTableHeaderRowNumber;
                                        worksheet = xlPackage.Workbook.Worksheets.Add(Convert.ToString(reader[0]));
                                        worksheet.View.ShowGridLines = false;

                                        iTotalCells = reader.FieldCount;
                                        using (DataTable schema = reader.GetSchemaTable())
                                        {
                                            straColumnHeader = GetColumnHeaders(schema);
                                        }

                                        for (int i = 1; i < iTotalCells; i++)
                                        {
                                            worksheet.Cells[iTableHeaderRowNumber, i + 1].Value = straColumnHeader[i];

                                        }
                                        iStartCellNumber = iRowCount + 1;
                                    }

                                    iRowCount++;
                                    for (int iCurrentCell = 1; iCurrentCell < iTotalCells; iCurrentCell++)
                                    {

                                        if (reader.IsDBNull(iCurrentCell) || reader[iCurrentCell] == DBNull.Value || reader[iCurrentCell] is DBNull || reader[iCurrentCell] == null)
                                        {
                                            worksheet.Cells[iRowCount, iCurrentCell + 1].Value = String.Empty;
                                        }
                                        else
                                        {
                                            // worksheet.Cells[iRowCount, iCurrentCell + 1].FormulaR1C1 = "=" + reader[iCurrentCell].ToString().Trim(); 
                                            double d;
                                            if (double.TryParse(reader[iCurrentCell].ToString().Trim(), NumberStyles.Any, CultureInfo.InvariantCulture, out d))
                                                worksheet.Cells[iRowCount, iCurrentCell + 1].Value = d;
                                            else
                                            {
                                                DateTime dateTime;
                                                if (DateTime.TryParseExact(reader[iCurrentCell].ToString().Trim(), "MM/dd/yyyy", CultureInfo.InvariantCulture,
        DateTimeStyles.None, out dateTime))
                                                {
                                                    worksheet.Cells[iRowCount, iCurrentCell + 1].Value = dateTime.ToOADate();
                                                }
                                                else
                                                    worksheet.Cells[iRowCount, iCurrentCell + 1].Value = reader[iCurrentCell].ToString().Trim();
                                            }
                                            //worksheet.Cells[iRowCount, iCurrentCell + 1].Style.Numberformat.Format="0";


                                        }


                                    }

                                    if (iRowCount % 2 == 1)
                                    {
                                        worksheet.Cells[iRowCount, 2, iRowCount, iTotalCells].Style.Fill.PatternType = ExcelFillStyle.Solid;
                                        worksheet.Cells[iRowCount, 2, iRowCount, iTotalCells].Style.Fill.BackgroundColor.SetColor(Color.FromArgb(248, 248, 248));

                                    }
                                    Index++;
                                }

                                iEndCellNumber = iRowCount;
                                using (ExcelRange r = worksheet.Cells[iTableHeaderRowNumber, 2, iRowCount, iTotalCells])
                                {
                                    r.Style.Font.SetFromFont(new Font(DefaultFont, DefaultFontSize));//Common font to all systems
                                    r.Style.Font.Color.SetColor(Color.Black);
                                    r.Style.Border.BorderAround(ExcelBorderStyle.Thin, Color.Black);
                                    r.Style.Border.Top.Style = ExcelBorderStyle.Thin;
                                    r.Style.Border.Bottom.Style = r.Style.Border.Top.Style;
                                    r.Style.Border.Left.Style = r.Style.Border.Top.Style;
                                    r.Style.Border.Right.Style = r.Style.Border.Top.Style;
                                }

                                //Apply header style last on purpose
                                using (ExcelRange r = worksheet.Cells[iTableHeaderRowNumber, 2, iTableHeaderRowNumber, iTotalCells])
                                {
                                    r.Style.Font.SetFromFont(new Font(DefaultFont, DefaultFontSize, FontStyle.Bold));//Common font to all systems
                                    r.Style.Font.Color.SetColor(Color.Black);
                                    r.Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                                    r.Style.Fill.PatternType = ExcelFillStyle.Solid;
                                    r.Style.Fill.BackgroundColor.SetColor(Color.FromArgb(240, 240, 240));
                                    r.AutoFilter = true;
                                    //r.Style.WrapText = true;


                                }

                                reader.NextResult(); // Applying format

                                while (reader.Read()) // Get Row Collection
                                {
                                    string ColumnNumber = string.Empty;
                                    ColumnNumber = GetColumnNumber(reader[0].ToString(), straColumnHeader);

                                    if (!string.IsNullOrEmpty(ColumnNumber))
                                    {
                                        string ExcelCellsRange = ColumnNumber + "" + iStartCellNumber.ToString() + ":" + ColumnNumber + "" + iEndCellNumber.ToString();

                                        worksheet.Cells[ExcelCellsRange].Style.Numberformat.Format = reader[2].ToString();
                                        worksheet.Cells[ExcelCellsRange].Style.WrapText = true;



                                        switch (reader[1].ToString())
                                        {
                                            case "R":
                                                worksheet.Cells[ExcelCellsRange].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                                                break;
                                            case "C":
                                                worksheet.Cells[ExcelCellsRange].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                                break;
                                            default:
                                                worksheet.Cells[ExcelCellsRange].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                                                break;
                                        }

                                    }
                                }


                                for (int i = 1; i <= straColumnHeader.Length; i++)
                                {
                                    worksheet.Column(i).BestFit = true;
                                    worksheet.Column(i).AutoFit();

                                }
                                goto newSheet;
                            }





                        }
                    }
                }
                // save the new spreadsheet
                xlPackage.Save();
            }

            return generatedFileName;
        }

        static string GetColumnNumber(string str, string[] ColumnHeaders)
        {
            int FindRow = 0;
            for (int i = 0; i < ColumnHeaders.Length; i++)
            {
                if (str.Trim() == ColumnHeaders[i].Trim())
                {
                    FindRow = i + 1;
                    break;
                }
            }

            return ExcelSheetColumnNo(FindRow);
        }
        static string ExcelSheetColumnNo(int RowNo)
        {
            string retValue = "";
            if (RowNo != 0)
            {
                if (RowNo > 26)
                {
                    retValue = ExcelSheetColumnNo(RowNo % 26 == 0 ? (RowNo / 26) - 1 : (RowNo / 26)).Trim() + AtoZ(RowNo % 26).Trim();
                }
                else
                {

                    retValue = AtoZ(RowNo % 26).Trim();
                }
            }
            return retValue;
        }

        static string AtoZ(int Value)
        {
            if (Value == 0)
                Value = 26;
            return ((char)(64 + (Value))).ToString();
        }
        /// <summary>
        /// Creates the excel2003 stream.
        /// Remember to close the stream
        /// </summary>
        /// <param name="reader">The reader.</param>
        /// <param name="Header">The header.</param>
        /// <param name="FileName">Name of the file.</param>
        /// <returns></returns>
        public FileStream CreateExcel2007Stream(IDataReader reader, ReportCriterion[] Header, String FileName)
        {

            generatedFileName = CreateExcel2007File(reader, Header, FileName);

            readonlyFileStream = new FileStream(generatedFileName, FileMode.Open, FileAccess.Read, FileShare.Read);

            return readonlyFileStream;
        }

        private void Cleanup()
        {

            if (!String.IsNullOrEmpty(generatedFileName) && File.Exists(generatedFileName))
            {
                File.Delete(generatedFileName);
            }

            if (!String.IsNullOrEmpty(this.strTempDirectory) && Directory.Exists(this.strTempDirectory))
            {
                Directory.Delete(this.strTempDirectory);
            }
            if (readonlyFileStream != null)
            {
                readonlyFileStream.Dispose();
                readonlyFileStream = null;
            }
        }

        private bool disposed = false; // to detect redundant calls

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




        ~ExcelWriter2007()
        {
            Dispose(false);
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }


        private void CopyStream(Stream source, Stream target)
        {
            const int bufSize = 0x1000;
            byte[] buf = new byte[bufSize];
            int bytesRead = 0;
            while ((bytesRead = source.Read(buf, 0, bufSize)) > 0)
                target.Write(buf, 0, bytesRead);
        }// end:


    }



    public class ExcelWriter2003 : IDisposable
    {
        public const String ContentType = "application/vnd.ms-excel";
        private String strTempDirectory = null;
        private String TempDirectory
        {
            get
            {
                if (String.IsNullOrEmpty(strTempDirectory))
                {
                    strTempDirectory = Path.Combine(Path.GetTempPath(), Path.GetRandomFileName());
                    // strTempDirectory = Path.Combine("C:\\", Path.GetRandomFileName());
                    Directory.CreateDirectory(TempDirectory);
                }
                return strTempDirectory;
            }
        }

        private String generatedFileName = null;
        private FileStream readonlyFileStream = null;


        private String GetColumnHeaders(DataTable ReaderSchema)
        {
            StringBuilder strbHeaderRow = new StringBuilder();
            strbHeaderRow.Append("<thead>");
            strbHeaderRow.Append("<tr>");

            foreach (DataRow row in ReaderSchema.Rows)
            {
                strbHeaderRow.AppendFormat("<th style=\"font-weight:bold;background-color:#F0F0F0; \">{0}</th>", row[0].ToString());
            }


            strbHeaderRow.Append("</tr>");
            strbHeaderRow.Append("</thead>");
            return strbHeaderRow.ToString();
        }


        public String CreateExcel2003File(IDataReader reader, String Header, String FileName)
        {

            int iRowCount = 1;
            int iTotalCells = 0;

            String strColumnHeader = String.Empty;
            generatedFileName = Path.Combine(TempDirectory, FileName.AppendIfMissing(".xls"));
            String strTD = "<td>";


            using (FileStream fs = new FileStream(generatedFileName, FileMode.Create, FileAccess.ReadWrite, FileShare.None))
            {
                using (StreamWriter outfile = new StreamWriter(fs))
                {

                    if (!String.IsNullOrEmpty(Header))
                    {
                        outfile.Write(Header);
                        outfile.Write("<table border=\"0\" cellpadding=\"1\" cellspacing=\"1\"><tr><td> </td></tr></table>");
                    }


                    outfile.Write("<table cellpadding=\"1\" cellspacing=\"1\" border=\"1\">");



                    if (reader != null && !reader.IsClosed)
                    {
                        using (reader)
                        {
                            iTotalCells = reader.FieldCount;
                            using (DataTable schema = reader.GetSchemaTable())
                            {
                                strColumnHeader = GetColumnHeaders(schema);
                            }

                            outfile.Write(strColumnHeader);
                            outfile.Write("<tbody>");
                            while (reader.Read())
                            {
                                iRowCount++;
                                outfile.Write("<tr>");


                                if (iRowCount % 2 == 1)
                                {
                                    strTD = "<td style=\"background-color: #F8F8F8; \">";
                                }
                                else
                                {

                                    strTD = "<td>";
                                }


                                for (int iCurrentCell = 0; iCurrentCell < iTotalCells; iCurrentCell++)
                                {
                                    if (reader.IsDBNull(iCurrentCell) || reader[iCurrentCell] == DBNull.Value || reader[iCurrentCell] is DBNull || reader[iCurrentCell] == null)
                                    {
                                        outfile.Write(strTD);
                                        outfile.Write("</td>");
                                    }
                                    else
                                    {
                                        outfile.Write(strTD);
                                        outfile.Write("{0}</td>", reader[iCurrentCell].ToString().Trim());
                                    }
                                }
                                outfile.Write("</tr>");
                            }
                            outfile.Write("</tbody>");

                        }
                    }
                    else
                    {
                        outfile.Write("<tr><td>No Data</td></tr>");
                    }

                    outfile.Write("</table>");
                    fs.Flush();
                }


            }


            return generatedFileName;
        }


        /// <summary>
        /// Creates the excel2003 stream.
        /// Remember to close the stream
        /// </summary>
        /// <param name="reader">The reader.</param>
        /// <param name="Header">The header.</param>
        /// <param name="FileName">Name of the file.</param>
        /// <returns></returns>
        public FileStream CreateExcel2003Stream(IDataReader reader, String Header, String FileName)
        {

            generatedFileName = CreateExcel2003File(reader, Header, FileName);

            readonlyFileStream = new FileStream(generatedFileName, FileMode.Open, FileAccess.Read, FileShare.Read);

            return readonlyFileStream;
        }

        private void Cleanup()
        {

            if (!String.IsNullOrEmpty(generatedFileName) && File.Exists(generatedFileName))
            {
                File.Delete(generatedFileName);
            }

            if (readonlyFileStream != null)
            {
                readonlyFileStream.Dispose();
                readonlyFileStream = null;
            }
        }

        private bool disposed = false; // to detect redundant calls

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




        ~ExcelWriter2003()
        {
            Dispose(false);
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }


        private void CopyStream(Stream source, Stream target)
        {
            const int bufSize = 0x1000;
            byte[] buf = new byte[bufSize];
            int bytesRead = 0;
            while ((bytesRead = source.Read(buf, 0, bufSize)) > 0)
                target.Write(buf, 0, bytesRead);
        }// end:


    }



    /// <summary>
    /// Excel Reporting Library
    /// </summary>
    public class ExcelReport
    {
        private const String strMIMEType = "application/vnd.ms-excel";

        public static ClientFileStream CreateExcelReport(IDataReader reader, ExcelReportConfiguration ReportConfiguration)
        {
            ExcelWriter2007 writer = new ExcelWriter2007();
            ClientFileStream cfs = new ClientFileStream();

            if (String.IsNullOrEmpty(ReportConfiguration.ReportTitle))
            {
                ReportConfiguration.ReportTitle.ThrowIfArgumentIsNull("ReportConfiguration.ReportTitle");
            }
            writer.Title = ReportConfiguration.ReportTitle;


            cfs.MIMEType = strMIMEType;
            cfs.FileExtension = "xlsx";
            cfs.Title = ReportConfiguration.ReportTitle.Replace(" ", "_");


            String strFileName = ReportConfiguration.ReportTitle.Replace(" ", "_");
            //String strHeader = ReportCriterion.ToHtmlTable(ReportConfiguration.ReportCriteria, ReportConfiguration.BorderWidth, ReportConfiguration.Header.HeaderText);

            cfs.StreamFileName = writer.CreateExcel2007File(reader, ReportConfiguration.ReportCriteria, strFileName);
            cfs.MIMEType = MIMEAssistant.GetMIMEType(cfs.StreamFileName);

            return cfs;
        }


        /// <summary>
        /// Creates an Excel Report from a Data Table
        /// </summary>
        /// <param name="SourceData">The source data.</param>
        /// <param name="ReportConfiguration">The report configuration.</param>
        /// <returns></returns>
        public static ClientTextFile CreateExcelReport(DataTable SourceData, ExcelReportConfiguration ReportConfiguration)
        {
            if (String.IsNullOrEmpty(ReportConfiguration.ReportTitle))
            {
                ReportConfiguration.ReportTitle.ThrowIfArgumentIsNull("ReportConfiguration.ReportTitle");
            }
            if (SourceData == null || SourceData.Rows.Count == 0)
            {
                return new ClientTextFile(ReportConfiguration.ReportTitle, "xls", strMIMEType, "<table><tr><td>No Data</td></tr></table>");
            }
            ClientTextFile ctx = new ClientTextFile();

            ctx.MIMEType = strMIMEType;
            ctx.FileExtension = "xls";
            ctx.Title = ReportConfiguration.ReportTitle.Replace(" ", "_");
            ctx.FileContents = ReportCriterion.ToHtmlTable(ReportConfiguration.ReportCriteria, ReportConfiguration.BorderWidth, ReportConfiguration.Header.HeaderText);
            ctx.FileContents += SourceData.ToHTMLTable(ReportConfiguration.BorderWidth, null, ReportConfiguration.Header.Alignment, ReportConfiguration.Footer.FooterText, ReportConfiguration.Footer.Alignment, ReportConfiguration.HiddenColumns, ReportConfiguration.DataColumnHeaders);

            return ctx;
        }

        /// <summary>
        /// Creates an Excel Report from a DataSet
        /// </summary>
        /// <param name="SourceData">The source data.</param>
        /// <param name="ReportConfiguration">The report configuration.</param>
        /// <returns></returns>
        public static ClientTextFile CreateExcelReport(DataSet SourceData, ExcelReportConfiguration ReportConfiguration)
        {
            String strMIMEType = "application/vnd.ms-excel";
            if (String.IsNullOrEmpty(ReportConfiguration.ReportTitle))
            {
                ReportConfiguration.ReportTitle.ThrowIfArgumentIsNull("ReportConfiguration.ReportTitle");
            }
            if (SourceData == null || SourceData.Tables.Count == 0 || SourceData.Tables[0].Rows.Count == 0)
            {
                return new ClientTextFile(ReportConfiguration.ReportTitle, "xls", strMIMEType, "<table><tr><td>No Data</td></tr></table>");
            }
            ClientTextFile ctx = new ClientTextFile();

            ctx.MIMEType = strMIMEType;
            ctx.FileExtension = "xls";
            ctx.Title = ReportConfiguration.ReportTitle.Replace(" ", "_");



            ctx.FileContents = ReportCriterion.ToHtmlTable(ReportConfiguration.ReportCriteria, ReportConfiguration.BorderWidth, ReportConfiguration.Header.HeaderText);
            ctx.FileContents += SourceData.ToHTMLTable(ReportConfiguration.BorderWidth, null, ReportConfiguration.Header.Alignment, ReportConfiguration.Footer.FooterText, ReportConfiguration.Footer.Alignment, ReportConfiguration.HiddenColumns, ReportConfiguration.DataColumnHeaders);

            return ctx;
        }
    }

}
