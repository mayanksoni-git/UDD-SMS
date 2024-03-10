using System;
using System.Data;
using System.Configuration;
using System.IO;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Collections.Generic;
using System.Text;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using NPOI.HSSF.UserModel;

/// <summary>
/// 
/// </summary>
/// 
public enum ExcelFileType
{
    xls,
    xlsx
}
public class GridViewExportUtil
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="fileName">Name Of The Excel File Which Will Be Downloaded</param>
    /// <param name="gv">Name Of Grid View Control</param>
    public static void Export(string fileName, GridView gv)
    {
        Table table = new Table();
        string exportContent = PartialPrepareForExport(gv, table, null);
        ExportFinal(fileName, exportContent, false);
    }

    private static string PartialPrepareForExport(GridView gv, Table table, List<int> ColNosAsText)
    {
        List<int> objLiFalse = new List<int>();
        for (int i = 0; i < gv.Columns.Count; i++)
        {
            if (gv.Columns[i].Visible == false)
            {
                objLiFalse.Add(i);
            }
        }
        StringWriter sw = null;
        HtmlTextWriter htw = null;
        using (sw = new StringWriter())
        {
            using (htw = new HtmlTextWriter(sw))
            {
                TableRow rowFinal = new TableRow();
                //  Create a form to contain the grid

                //  add the header row to the table
                if (gv.HeaderRow != null)
                {
                    GridViewExportUtil.PrepareControlForExport(gv.HeaderRow);
                    rowFinal = RemoveHiddenCells(gv.HeaderRow, objLiFalse, "H", ColNosAsText);
                    table.Rows.Add(rowFinal);
                }

                //  add each of the data rows to the table
                foreach (GridViewRow row in gv.Rows)
                {
                    GridViewExportUtil.PrepareControlForExport(row);
                    rowFinal = RemoveHiddenCells(row, objLiFalse, "R", ColNosAsText);
                    table.Rows.Add(rowFinal);
                }

                //  add the footer row to the table
                if (gv.FooterRow != null)
                {
                    GridViewExportUtil.PrepareControlForExport(gv.FooterRow);
                    rowFinal = RemoveHiddenCells(gv.FooterRow, objLiFalse, "F", ColNosAsText);
                    table.Rows.Add(rowFinal);
                }
                table.GridLines = GridLines.Both;
                //  render the table into the htmlwriter
                table.RenderControl(htw);
            }
        }
        return sw.ToString();
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="fileName">Name Of The Excel File Which Will Be Downloaded</param>
    /// <param name="gv">Name Of Grid View Control</param>
    /// <param name="ColNosAsText"></param>
    public static void Export(string fileName, GridView gv, List<int> ColNosAsText)
    {
        Table table = new Table();
        string exportContent = PartialPrepareForExport(gv, table, ColNosAsText);
        ExportFinal(fileName, exportContent, false);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="fileName">Name Of The Excel File Which Will Be Downloaded</param>
    /// <param name="InnerHTML">Content To Be Written In Excel File.</param>
    public static void Export(string fileName, string InnerHTML)
    {
        ExportFinal(fileName, InnerHTML, false);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="fileName">Name Of The Excel File Which Will Be Downloaded</param>
    /// <param name="InnerHTML">Content To Be Written In Excel File.</param>
    public static void ExportSpecial(string fileName, string InnerHTML)
    {
        ExportFinal(fileName, InnerHTML, true);
    }

    private static void ExportFinal(string fileName, string InnerHTML, bool isSpecial)
    {
        var excelTemplate = "";
        if (1 == 2)
        {
            InnerHTML = InnerHTML.Replace("<table rules=\"all\" border=\"1\">", "\r\n<Table>");
            InnerHTML = InnerHTML.Replace("</table>", "\r\n</Table>");

            InnerHTML = InnerHTML.Replace("<tr>", "\r\n<Row>");
            InnerHTML = InnerHTML.Replace("</tr>", "</Row>");

            InnerHTML = InnerHTML.Replace("<td>", "<Cell ss:StyleID=\"s62\"><Data ss:Type=\"String\">");
            InnerHTML = InnerHTML.Replace("</td>", "</Data></Cell>");
            excelTemplate = getWorkbookTemplate();
        }
        //var excelXml = string.Format(excelTemplate, InnerHTML);

        string aa = @"<style> .text{ mso-number-format:0;} </style>";
        HttpContext.Current.Response.Clear();
        HttpContext.Current.Response.Buffer = true;
        HttpContext.Current.Response.ContentType = "application/vnd.ms-excel";
        HttpContext.Current.Response.AddHeader("content-disposition", "attachment;filename=" + fileName);
        HttpContext.Current.Response.Charset = "";
        if (isSpecial)
        {
            HttpContext.Current.Response.Write(aa);
        }
        HttpContext.Current.Response.Write(InnerHTML);
        HttpContext.Current.Response.End();
    }

    private static TableRow RemoveHiddenCells(GridViewRow gridViewRow, List<int> objLiFalse, string RowType, List<int> ColNosAsText)
    {
        TableRow gr = new TableRow();
        if (RowType == "H" || RowType == "F")
        {
            //gr.Style.Value = "background-color:#cacaca;font-size:small;border:1;color:black;font-family:Verdana;";
        }
        TableCell cel;
        for (int i = 0; i < gridViewRow.Cells.Count; i++)
        {
            if (!objLiFalse.Contains(i))
            {
                cel = new TableCell();
                if (gridViewRow.Cells[i].Controls.Count > 0)
                {
                    string tmpText = "";
                    Control current = null;
                    for (int j = 0; j < gridViewRow.Cells[i].Controls.Count; j++)
                    {
                        current = gridViewRow.Cells[i].Controls[j];
                        if (current is LiteralControl)
                        {
                            try
                            {
                                tmpText += (current as LiteralControl).Text.Trim();
                            }
                            catch
                            {
                                tmpText += "";
                            }
                        }
                        else if (current is DataBoundLiteralControl)
                        {
                            try
                            {
                                tmpText += (current as DataBoundLiteralControl).Text.Trim();
                            }
                            catch
                            {
                                tmpText += "";
                            }
                        }
                    }
                    if (ColNosAsText != null && ColNosAsText.Contains(i))
                        cel.Text = tmpText.Trim() + ",";
                    else
                        cel.Text = tmpText.Trim();
                }
                else
                {
                    if (ColNosAsText != null && ColNosAsText.Contains(i))
                        cel.Text = gridViewRow.Cells[i].Text + ",";
                    else
                        cel.Text = gridViewRow.Cells[i].Text;
                }
                gr.Cells.Add(cel);
            }
        }
        return gr;
    }

    /// <summary>
    /// Replace any of the contained controls with literals
    /// </summary>
    /// <param name="control"></param>
    private static void PrepareControlForExport(Control control)
    {
        for (int i = 0; i < control.Controls.Count; i++)
        {
            Control current = control.Controls[i];
            if (current is LinkButton)
            {
                control.Controls.Remove(current);
                control.Controls.AddAt(i, new LiteralControl((current as LinkButton).Text));
            }
            else if (current is ImageButton)
            {
                control.Controls.Remove(current);
                control.Controls.AddAt(i, new LiteralControl((current as ImageButton).AlternateText));
            }
            else if (current is HyperLink)
            {
                control.Controls.Remove(current);
                control.Controls.AddAt(i, new LiteralControl((current as HyperLink).Text));
            }
            else if (current is TextBox)
            {
                control.Controls.Remove(current);
                control.Controls.AddAt(i, new LiteralControl((current as TextBox).Text.Trim()));
            }
            else if (current is DropDownList)
            {
                control.Controls.Remove(current);
                control.Controls.AddAt(i, new LiteralControl((current as DropDownList).SelectedItem.Text));
            }
            else if (current is CheckBox)
            {
                control.Controls.Remove(current);
                control.Controls.AddAt(i, new LiteralControl((current as CheckBox).Checked ? "True" : "False"));
            }
            else if (current is Label)
            {
                control.Controls.Remove(current);
                control.Controls.AddAt(i, new LiteralControl((current as Label).Text));
            }
            if (current.HasControls())
            {
                GridViewExportUtil.PrepareControlForExport(current);
            }
        }
    }

    public static void Export(string fileName, List<GridView> gvLi)
    {
        string exportContent = string.Empty;
        Table table = new Table();
        GridView gv = null;
        for (int i = 0; i < gvLi.Count; i++)
        {
            gv = gvLi[i];
            exportContent += "<Worksheet ss:Name=\"SheetTest" + (i + 1).ToString() + "\">\r\n";
            exportContent += PartialPrepareForExport(gv, table, null);
            exportContent += "\r\n</Worksheet>";

        }
        ExportFinal(fileName, exportContent, false);
    }

    private static string getWorkbookTemplate()
    {
        var sb = new StringBuilder(818);
        sb.AppendFormat(@"<?xml version=""1.0""?>{0}", Environment.NewLine);
        sb.AppendFormat(@"<?mso-application progid=""Excel.Sheet""?>{0}", Environment.NewLine);
        sb.AppendFormat(@"<Workbook xmlns=""urn:schemas-microsoft-com:office:spreadsheet""{0}", Environment.NewLine);
        sb.AppendFormat(@" xmlns:o=""urn:schemas-microsoft-com:office:office""{0}", Environment.NewLine);
        sb.AppendFormat(@" xmlns:x=""urn:schemas-microsoft-com:office:excel""{0}", Environment.NewLine);
        sb.AppendFormat(@" xmlns:ss=""urn:schemas-microsoft-com:office:spreadsheet""{0}", Environment.NewLine);
        sb.AppendFormat(@" xmlns:html=""http://www.w3.org/TR/REC-html40"">{0}", Environment.NewLine);
        sb.AppendFormat(@" <Styles>{0}", Environment.NewLine);
        sb.AppendFormat(@"  <Style ss:ID=""Default"" ss:Name=""Normal"">{0}", Environment.NewLine);
        sb.AppendFormat(@"   <Alignment ss:Vertical=""Bottom""/>{0}", Environment.NewLine);
        sb.AppendFormat(@"   <Borders/>{0}", Environment.NewLine);
        sb.AppendFormat(@"   <Font ss:FontName=""Calibri"" x:Family=""Swiss"" ss:Size=""10"" ss:Color=""#000000""/>{0}", Environment.NewLine);
        sb.AppendFormat(@"   <Interior/>{0}", Environment.NewLine);
        sb.AppendFormat(@"   <NumberFormat/>{0}", Environment.NewLine);
        sb.AppendFormat(@"   <Protection/>{0}", Environment.NewLine);
        sb.AppendFormat(@"  </Style>{0}", Environment.NewLine);
        sb.AppendFormat(@"  <Style ss:ID=""s62"">{0}", Environment.NewLine);
        sb.AppendFormat(@"   <Font ss:FontName=""Calibri"" x:Family=""Swiss"" ss:Size=""10"" ss:Color=""#000000""{0}", Environment.NewLine);
        sb.AppendFormat(@"    ss:Bold=""0""/>{0}", Environment.NewLine);
        sb.AppendFormat(@"  </Style>{0}", Environment.NewLine);
        sb.AppendFormat(@"  <Style ss:ID=""s63"">{0}", Environment.NewLine);
        sb.AppendFormat(@"   <NumberFormat ss:Format=""Short Date""/>{0}", Environment.NewLine);
        sb.AppendFormat(@"  </Style>{0}", Environment.NewLine);
        sb.AppendFormat(@" </Styles>{0}", Environment.NewLine);
        sb.Append(@"{0}\r\n</Workbook>");
        return sb.ToString();
    }

    public static void WriteExcelWithNPOI(ExcelFileType extension, DataSet ds, string fileName_Without_Extention, HttpResponse context)
    {
        // dll refered NPOI.dll and NPOI.OOXML

        IWorkbook workbook;

        if (extension == ExcelFileType.xlsx)
        {
            workbook = new XSSFWorkbook();
        }
        else if (extension == ExcelFileType.xls)
        {
            workbook = new HSSFWorkbook();
        }
        else
        {
            throw new Exception("This format is not supported");
        }
        ISheet sheet1;
        DataTable dt;
        for (int k = 0; k < ds.Tables.Count; k++)
        {
            dt = ds.Tables[k];
            sheet1 = workbook.CreateSheet("Sheet " + (k + 1).ToString());

            //make a header row
            IRow row1 = sheet1.CreateRow(0);

            for (int j = 0; j < dt.Columns.Count; j++)
            {
                ICell cell = row1.CreateCell(j);

                String columnName = dt.Columns[j].ToString();
                cell.SetCellValue(columnName);
            }

            //loops through data
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                IRow row = sheet1.CreateRow(i + 1);
                for (int j = 0; j < dt.Columns.Count; j++)
                {
                    ICell cell = row.CreateCell(j);
                    String columnName = dt.Columns[j].ToString();
                    cell.SetCellValue(dt.Rows[i][columnName].ToString());
                }
            }
        }

        using (var exportData = new MemoryStream())
        {
            context.Clear();
            workbook.Write(exportData);
            if (extension == ExcelFileType.xlsx) //xlsx file format
            {
                context.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                context.AddHeader("Content-Disposition", string.Format("attachment;filename={0}", fileName_Without_Extention + ".xlsx"));
                context.BinaryWrite(exportData.ToArray());
            }
            else if (extension == ExcelFileType.xls)  //xls file format
            {
                context.ContentType = "application/vnd.ms-excel";
                context.AddHeader("Content-Disposition", string.Format("attachment;filename={0}", fileName_Without_Extention + ".xls"));
                context.BinaryWrite(exportData.GetBuffer());
            }
            context.End();
        }
    }
}
