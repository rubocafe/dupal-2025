using DUPALPayroll.Library;
using DUPALPayroll.Library.Date;
using DUPALPayroll.UI.Common.AnalyzeBean;
using MigraDoc.DocumentObjectModel;
using MigraDoc.DocumentObjectModel.Shapes;
using MigraDoc.DocumentObjectModel.Tables;
using MigraDoc.Rendering;
using System;
using System.Diagnostics;
using System.IO;

// Harshan Nishantha
// 2013-09-23

namespace DUPALPayroll.UI.Common.SalarySlip
{
  /// <summary>
  /// Creates the invoice form.
  /// </summary>
  public class TcSalarySlipsCreator <T> where T : TcSalaryAnalyzedRow
  {
    private Document    document;
    private Table       table;
    private TextFrame   signatureFrame;

    public string finalSalaryString = "Bank Transfer Amount";

    readonly static Color TableBorder = new Color(81, 125, 192);

    private string companyName = "DU PAL PRIVATE LIMITED";

    public TcYearMonth WorkingYearMonth { get; set; }
    public string Code { get; set; }

    public TcSalarySlipsCreator(TcYearMonth workingYearMonth, string code)
    {
        WorkingYearMonth = workingYearMonth;
        Code = code;
    }

    public void CreateAndSave(string path, TcBindingList<T> list)
    {
        File.Delete(path);

        Document document = CreateDocument(list);
        document.UseCmykColor = true;

        // Create a renderer for PDF that uses Unicode font encoding
        PdfDocumentRenderer pdfRenderer = new PdfDocumentRenderer(true);

        // Set the MigraDoc document
        pdfRenderer.Document = document;

        // Create the PDF document
        pdfRenderer.RenderDocument();

        pdfRenderer.Save(path);
    }

    public void ShowFile(string path)
    {
        Process.Start(path);
    }

    private Document CreateDocument(TcBindingList<T> list)
    {
      // Create a new MigraDoc document
      this.document = new Document();

      this.document.DefaultPageSetup.PageFormat = PageFormat.A5;

      this.document.DefaultPageSetup.PageWidth = new Unit(148, UnitType.Millimeter);
      this.document.DefaultPageSetup.PageHeight = new Unit(210, UnitType.Millimeter);

      this.document.DefaultPageSetup.TopMargin = new Unit(15, UnitType.Millimeter);
      this.document.DefaultPageSetup.LeftMargin = new Unit(15, UnitType.Millimeter);
      this.document.DefaultPageSetup.RightMargin = new Unit(15, UnitType.Millimeter);
      this.document.DefaultPageSetup.BottomMargin = new Unit(5, UnitType.Millimeter);

      this.document.DefaultPageSetup.FooterDistance = new Unit(10, UnitType.Millimeter);

      string title = string.Format("{0} - {1}", WorkingYearMonth.ToDate().ToString("yyyy"), WorkingYearMonth.ToDate().ToString("MMMM"));
      this.document.Info.Title      = title;
      this.document.Info.Subject    = title;
      this.document.Info.Author     = companyName;

      DefineStyles();

      for (int i = 0; i < list.Count; i++)
      {
          CreateSalarySlip(i + 1, list[i]);
      }

      return this.document;
    }

    private void CreateSalarySlip(int pageNumber, T data)
    {
        CreatePage(pageNumber, data);

        FillContent(data);
    }

    void DefineStyles()
    {
      Style style = this.document.Styles["Normal"];
      style.Font.Name = "Verdana";

      // Create a new style called Table based on style Normal
      style = this.document.Styles.AddStyle("Table", "Normal");
      style.Font.Name = "Verdana";
      style.Font.Size = 9;
    }

    void CreatePage(int pageNumber, T data)
    {
      // Each MigraDoc document needs at least one section.
      Section section = this.document.AddSection();

      // Put a logo in the header
      string path = TcPaths.GetDuPalLogoPath();
      Image image = section.Headers.Primary.AddImage(path);
      image.Height = "0.7cm";
      image.LockAspectRatio = true;
      image.RelativeVertical = RelativeVertical.Line;
      image.RelativeHorizontal = RelativeHorizontal.Margin;
      image.Top = ShapePosition.Top;
      image.Left = "7.7cm"; ;
      image.WrapFormat.Style = WrapStyle.None;

      AddCompanyAddress(section);
      AddAddress(section, data);

      // Title
      Paragraph paragraph = section.AddParagraph();
      paragraph.AddText(string.Format("SALARY SLIP FOR\t: {0} {1}", WorkingYearMonth.ToDate().ToString("MMM").ToUpper(), WorkingYearMonth.ToDate().ToString("yyyy")));
      paragraph.AddLineBreak();
      paragraph.AddText(string.Format("DEPARTMENT\t: {0}", Code));
      paragraph.AddLineBreak();
      paragraph.AddText(string.Format("EMPLOYEE NO\t: {0}", GetEmployeeNumber(data)));
      paragraph.AddLineBreak();
      paragraph.AddText(string.Format("NIC\t\t: {0}", GetNIC(data)));
      paragraph.Format.SpaceBefore = "3cm";
      paragraph.Format.Alignment = ParagraphAlignment.Left;
      SetSmallFontToParagraph(paragraph);
      paragraph.Format.SpaceAfter = "1.5cm";
      paragraph.Format.LeftIndent = "0.1cm";

      // Create the item table
      this.table = section.AddTable();
      this.table.Style = "Table";
      this.table.Borders.Style = BorderStyle.Single;
      this.table.Borders.Color = TableBorder;
      this.table.Borders.Width = 0.10;
      this.table.Borders.Left.Width = 0.5;
      this.table.Borders.Right.Width = 0.5;
      this.table.Rows.LeftIndent = 0;

      // Before you can add a row, you must define the columns
      Column column = this.table.AddColumn("8cm");
      column.Format.Alignment = ParagraphAlignment.Left;

      column = this.table.AddColumn("4cm");
      column.Format.Alignment = ParagraphAlignment.Right;

      this.signatureFrame = section.AddTextFrame();
      this.signatureFrame.Height = "3.0cm";
      this.signatureFrame.Width = this.document.DefaultPageSetup.PageWidth;
      this.signatureFrame.Left = ShapePosition.Left;
      this.signatureFrame.Top = "18.0cm";
      this.signatureFrame.RelativeHorizontal = RelativeHorizontal.Page;
      this.signatureFrame.RelativeVertical = RelativeVertical.Page;

      // Create footer
      paragraph = section.Footers.Primary.AddParagraph();
      paragraph.AddText("THIS IS A COMPUTER GENERATED DOCUMENT. NO SIGNATURE IS REQUIRED");
      paragraph.Format.Font.Size = 7;
      paragraph.Format.Alignment = ParagraphAlignment.Center;
    }

    private void AddCompanyAddress(Section section)
    {
        TextFrame   companyAddressFrame = section.AddTextFrame();

        // Company
        Paragraph paragraph = companyAddressFrame.AddParagraph();
       // paragraph.Format.SpaceBefore = "1cm";
        paragraph.AddText(companyName);
        SetNormalFontToParagraph(paragraph);
        paragraph.Format.Alignment = ParagraphAlignment.Left;

        // Address
        paragraph = companyAddressFrame.AddParagraph("33 1/1, Skelton Gardens,");
        paragraph.AddLineBreak();
        paragraph.AddText("Colombo 5, Sri Lanka");
        SetSmallFontToParagraph(paragraph);
        paragraph.Format.Alignment = ParagraphAlignment.Left;

        paragraph = companyAddressFrame.AddParagraph();
        paragraph.AddText(string.Format("Tel:"));
        paragraph.AddSpace(2);
        paragraph.AddText("+94 112 507907");
        paragraph.AddLineBreak();
        paragraph.AddText(string.Format("Fax: +94 112 507906"));
        SetSmallFontToParagraph(paragraph);
        paragraph.Format.SpaceBefore = "0.2cm";
        paragraph.Format.Alignment = ParagraphAlignment.Left;

        companyAddressFrame.Height = "3.0cm";
        companyAddressFrame.Width = "5.6cm";
        companyAddressFrame.Left = ShapePosition.Right;
        companyAddressFrame.Top = "2.1cm";
        companyAddressFrame.RelativeHorizontal = RelativeHorizontal.Page;
        companyAddressFrame.RelativeVertical = RelativeVertical.Page;
    }

    private void AddAddress(Section section, T data)
    {
        TextFrame addressFrame = section.AddTextFrame();

        // Name
        Paragraph paragraph = addressFrame.AddParagraph();
        paragraph.AddText(data.Name);
        paragraph.AddLineBreak();
        paragraph.AddLineBreak();
        SetNormalFontToParagraph(paragraph);
        paragraph.Format.Alignment = ParagraphAlignment.Left;

        // Address
        paragraph = addressFrame.AddParagraph();
        if (!string.IsNullOrEmpty(data.AddressLine1))
        {
            paragraph.AddText(data.AddressLine1);
            paragraph.AddLineBreak();
        }

        if (!string.IsNullOrEmpty(data.AddressLine2))
        {
            paragraph.AddText(data.AddressLine2);
            paragraph.AddLineBreak();
        }

        if (!string.IsNullOrEmpty(data.City))
        {
            paragraph.AddText(data.City);
        }

        SetSmallFontToParagraph(paragraph);
        paragraph.Format.Alignment = ParagraphAlignment.Left;

        addressFrame.Height = "3.0cm";
        addressFrame.Width = "13.2cm";
        addressFrame.Left = ShapePosition.Right;
        addressFrame.Top = "1.3cm";
        addressFrame.RelativeHorizontal = RelativeHorizontal.Page;
        addressFrame.RelativeVertical = RelativeVertical.Page;
    }

    private void SetNormalFontToParagraph(Paragraph paragraph)
    {
        paragraph.Format.Font.Name = "Verdana";
        paragraph.Format.Font.Size = 9;
    }

    private void SetSmallFontToParagraph(Paragraph paragraph)
    {
        paragraph.Format.Font.Name = "Verdana";
        paragraph.Format.Font.Size = 7;
        paragraph.Format.SpaceAfter = 3;
    }

    public virtual string GetEmployeeName(T data)
    {
        return data.Name;
    }

    public virtual string GetEmployeeNumber(T data)
    {
        return data.EmployeeNumber;
    }

    public virtual string GetNIC(T data)
    {
        return data.NIC;
    }

    public virtual void FillContent(T data)
    {
        AddRow("Basic Salary", 14000);
        AddRow("Budgetary Relief Allowance", 1000);
        AddRow("No Pay", 0);
        AddEmptyRow();

        AddRow("Gross Salary", 15000);
        AddRow("OT Normal", (decimal)1523.44);
        AddRow("OT Double", (decimal)3062.50);
        AddRow("Travelling Allowance", 0);
        AddTotalRow("", (decimal)19585.94);
        AddEmptyRow();

        AddBoldHeadingRow("Deductions");
        AddRow("EPF 8%", 1200);
        AddRow("Others", 0);
        AddEmptyRow();

        AddTotalRow("Net Salary", (decimal)18385.94);
        AddEmptyRow();

        AddRow("EPF 12%", 1800);
        AddRow("ETF 3%", 450);
    }

    public decimal ZeroIfNegative(decimal value)
    {
        return value < 0 ? 0 : value;
    }

    protected void AddTotalRow(string description, decimal amount)
    {
        Row row = this.table.AddRow();

        Paragraph paragraph = row.Cells[0].AddParagraph();
        paragraph.AddFormattedText(description, TextFormat.Bold);

        paragraph = row.Cells[1].AddParagraph();
        paragraph.AddFormattedText(amount.ToString("N2"), TextFormat.Bold);

        row.Borders.Visible = false;
        row.Height = 15;
        row.VerticalAlignment = VerticalAlignment.Center;
    }

    protected void AddRow(string description, decimal amount)
    {
        Row row = this.table.AddRow();

        row.Cells[0].AddParagraph(description);
        row.Cells[1].AddParagraph(amount.ToString("N2"));

        row.Borders.Visible = false;
        row.Height = 15;
        row.VerticalAlignment = VerticalAlignment.Center;
    }

    protected void AddNegativePayeRow(string description, decimal amount)
    {
        if (amount > 0)
        {
            AddNegativeRow(description, amount);
        }
    }

    protected void AddNegativeRow(string description, decimal amount)
    {
        Row row = this.table.AddRow();

        row.Cells[0].AddParagraph(description);
        row.Cells[1].AddParagraph(string.Format("({0})",amount.ToString("N2")));

        row.Borders.Visible = false;
        row.Height = 15;
        row.VerticalAlignment = VerticalAlignment.Center;
    }

    protected void AddBoldHeadingRow(string heading)
    {
        Row row = this.table.AddRow();

        Paragraph paragraph = row.Cells[0].AddParagraph();
        paragraph.AddFormattedText(heading, TextFormat.Bold);

        row.Borders.Visible = false;
        row.Height = 15;
        row.VerticalAlignment = VerticalAlignment.Center;
    }

    protected void AddEmptyRow()
    {
        Row row = this.table.AddRow();
        row.HeightRule = RowHeightRule.Exactly;
        row.Height = 7;
        row.Borders.Visible = false;
    }
  }
}
