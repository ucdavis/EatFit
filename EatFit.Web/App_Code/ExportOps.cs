using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

using System.Collections;
using System.Collections.Specialized;
using System.Text;
using ceTe.DynamicPDF;
using ceTe.DynamicPDF.PageElements;

/// <summary>
/// Current Version : Beta
/// 
/// Written By : Alan Lai
///              College Of Agricultural and Environmental Sciences Dean's Office, UC Davis
/// Date : 10/27/2006
/// 
/// A generic class to export data to various formats including : Word, Excel, PDF
/// 
/// Notes :
/// 
///     To export pdf files the ExportToPDF.aspx must be placed in the same directory of the file
///     that will be calling the export to pdf function.
/// 
/// 
/// Updates :
/// 
///     [2/20/2007]
///         PDF
///         - Updated to work with the new version of PDF Creator
///         - Generalized the PDF Creation data structures so there is one arraylist
///             for all the objects on a specific page.
///             The structure for the arraylist is documented below in the Structures section under _PDFDocumentPages declaration
///             - Not all the structures are complete for the types of things that can be added
///         - Also added the ability to use templates (Which has not been fully tested)
///         - Enums exist so that a user can define font options, alignments, styles and colors
///             this allows them to define these properties without having to use the the DynamicPDF references
/// 
/// </summary>

namespace CAESDO
{
    public class ExportOps
    {
        public ExportOps()
        {
            this._PDFTemplate = new ArrayList();
            this._PDFDocumentPages = new ArrayList();

            this._PDFDefaultHeight = 700;  // Default height of a page using TimesNewRoman with 12pt font
            this._PDFDefaultWidth = 500;   // Default width of a page using TimesNewRoman with 12 pt font
            this._PDFDefaultXCoordinate = 1;
            this._PDFDefaultYCoordinate = 1;
            this._PDFDefaultTextSize = 12;
            this._PDFDefaultFont = PDFFonts.TimesRoman;
            this._PDFDefaultColor = PDFColor.Black;
            this._PDFDefaultTextAlign = PDFTextAlign.Left;
            this._PDFDefaultLineStyle = PDFLineStyle.Solid;
            this._PDFDefaultFontFamily = PDFFontFamily.Times;

            this._PDFDocumentAuthor = "";
            this._PDFDocumentTitle = "";

            this._ExcelColumns = new ArrayList();
        }

        #region PDF

        #region Private Members
        private ArrayList _PDFTemplate;         // Containing the controls for the Template Page
        private ArrayList _PDFDocumentPages;    // Containing the controls for individual pages in the document
        // ============== Structures ============== //
        //  The follow are the structures of the sub-arrays for objects in the _PDFTemplate and _PDFDocuemtnPages arrays
        //
        //  Image : [0] PDFAreaType [1] Image Path [2] x-Coordinate [3] y-Coordinate [4] scale
        //  Background Image : [0] PDFAreaType [1] Image Path [2] Use within page margins
        //  Label : [0] PDFAreaType [1] Text [2] x-Coordinate [3] y-Coordinate [4] width [5] height [6] Font (Font.Fontname) [7] Text Size [8] Text Align (Textalign.TextAlign) [9] Color (RgbColor.Color)
        //  Line : [0] PDFAreaType [1] x-Coordinate1 [2] y-Coordinate1 [3] x-Coordinate2 [4] y-Coordinate2 [5] Width [6] Color [7] Style
        //  Rectangle : [0] PDFAreaType [1]
        //  Link :
        //  FormattedTextArea : [0] PDFAreaType [1] Text [2] x-Coordinate [3] y-Coordinate [4] width [5] height [6] Font (Font.Fontname) [7] Text Size [8] Preserve White Space
        //  OrderList :
        //  UnorderedList :
        //  Table :
        // === Template only ===
        //  PageNumbering :
        // ======================================== //
        private string _PDFDocumentAuthor;      // The Document's Author
        private string _PDFDocumentTitle;       // The Document's Title
        private Boolean _PDFUsingTemplate;      // Boolean to determine if the template should be used
        private float _PDFDefaultXCoordinate;   // Default X-Coordinate for a text box if one is not specified
        private float _PDFDefaultYCoordinate;   // Default Y-Coordinate for a text box if one is not specified
        private float _PDFDefaultHeight;        // Default Height for a text box if one is not specified
        private float _PDFDefaultWidth;         // Default Width for a text box if one is not specified
        private PDFFonts _PDFDefaultFont;       // Default Font for a text box if one is not specified
        private float _PDFDefaultTextSize;      // Default text size
        private PDFColor _PDFDefaultColor;
        private PDFTextAlign _PDFDefaultTextAlign;
        private PDFLineStyle _PDFDefaultLineStyle;
        private PDFFontFamily _PDFDefaultFontFamily;
        
        #endregion

        #region Enums
        public enum PDFObjectType { Image, BackgroundImage, Label, Line, PageNumbering, Rectangle, Link, FormattedTextArea, OrderedList, UnorderedList, Table, TextArea };
        public enum PDFFonts { TimesRoman, TimesBold, TimesItalic, Courier, CourierBold, CourierOblique, Helvetica, HelveticaBold, HelveticaBoldOblique };
        public enum PDFFontFamily { Times, Helvetica, Courier };
        public enum PDFTextAlign { Center, FullJustify, Justify, Right, Left };
        public enum PDFLineStyle { Solid, Dots, Dash, DashLarge, DashSmall };
        public enum PDFColor { Black, Gray, DarkGray, White, Red, Green, Purple, Blue, Yellow ,Beige, Brown, DarkBlue, DarkGreen, DarkRed, Gold, Navy, Olive, Pink, Teal };
        #endregion

        #region Accessors
        public ArrayList PDFDocumentPages { get { return this._PDFDocumentPages; } }
        public ArrayList PDFTemplate { get { return this._PDFTemplate; } }
        public string PDFDocumentAuthor 
        { 
            get{ return this._PDFDocumentAuthor; }
            set { this._PDFDocumentAuthor = value; } 
        }
        public string PDFDocumentTitle 
        {
            get { return this._PDFDocumentTitle; }
            set { this._PDFDocumentTitle = value; }
        }
        public Boolean PDFUsingTemplate
        {
            get { return this._PDFUsingTemplate; }
            set { this._PDFUsingTemplate = value; }
        }
        public float PDFDefaultXCoordinate
        {
            get { return this._PDFDefaultXCoordinate; }
            set { this._PDFDefaultXCoordinate = value; }
        }
        public float PDFDefaultYCoordinate
        {
            get { return this._PDFDefaultYCoordinate; }
            set { this._PDFDefaultYCoordinate = value; }
        }
        public float PDFDefaultHeight
        {
            get { return this._PDFDefaultHeight; }
            set { this._PDFDefaultHeight = value; }
        }
        public float PDFDefaultWidth
        {
            get { return this._PDFDefaultWidth; }
            set { this._PDFDefaultWidth = value; }
        }
        public PDFFonts PDFDefaultFont
        {
            get { return this._PDFDefaultFont; }
            set { this._PDFDefaultFont = value; }
        }
        #endregion

        #region Public Methods
        public void ExportToPDF()
        {
            HttpContext.Current.Session.Add("PDFExport", this);

            HttpContext.Current.Response.Redirect("ExportToPDF.aspx");
        }

        /// <summary>
        /// Adds a page to the document so objects can be added.  This needs to be run atleast once so that there
        /// is a page to add controls to.
        /// </summary>
        public void PDFAddPage()
        {
            this._PDFDocumentPages.Add(new ArrayList());
        }

        /// <summary>
        /// Adds an image object to the specified page.
        /// </summary>
        /// <param name="imagePath">Relative path to the image file.</param>
        /// <param name="xCoordinate">X-Coordinate image should be located.</param>
        /// <param name="yCoordinate">Y-Coordinate image should be located.</param>
        /// <param name="scale">Scale of the image.</param>
        /// <param name="pageNumber">Page number you would like image placed.</param>
        /// <returns>True if addition was successful, False if page number of out of bounds.</returns>
        public Boolean PDFAddImage(string imagePath, float xCoordinate, float yCoordinate, float scale, int pageNumber)
        {
            if (pageNumber > this._PDFDocumentPages.Count)
                return false;

            ArrayList tempArray = new ArrayList();
            tempArray.Add(PDFObjectType.Image);
            tempArray.Add(imagePath);
            tempArray.Add(xCoordinate);
            tempArray.Add(yCoordinate);
            tempArray.Add(scale);

            pageNumber--;   // Decrement the number, because the arrray starts at 0 not 1

            // Add teh object to the array
            ArrayList tempArray2 = (ArrayList)this._PDFDocumentPages[pageNumber];
            tempArray2.Add(tempArray);

            return true;
        }
        /// <summary>
        /// Adds an image object to the specified page.
        /// </summary>
        /// <param name="imagePath">Relative path to the image file.</param>
        /// <param name="xCoordinate">X-Coordinate image should be located.</param>
        /// <param name="yCoordinate">Y-Coordinate image should be located.</param>
        /// <param name="pageNumber">Page number you would like image placed.</param>
        /// <returns>True if addition was successful, False if page number of out of bounds.</returns>
        public Boolean PDFAddImage(string imagePath, float xCoordinate, float yCoordinate, int pageNumber)
        {
            return this.PDFAddImage(imagePath, xCoordinate, yCoordinate, 1, pageNumber);
        }

        /// <summary>
        /// Adds a background image to the specified page.
        /// </summary>
        /// <param name="imagePath">Relative path to the image file.</param>
        /// <param name="withinMargin">Place the image within the margins?</param>
        /// <param name="pageNumber">Page number you would like the background image placed.</param>
        /// <returns>True if addition was successful, False if page number if out of bounds.</returns>
        public Boolean PDFAddBackgroundImage(string imagePath, Boolean withinMargin, int pageNumber)
        {
            if (pageNumber > this._PDFDocumentPages.Count)
                return false;

            ArrayList tempArray = new ArrayList();
            tempArray.Add(PDFObjectType.BackgroundImage);
            tempArray.Add(imagePath);
            tempArray.Add(withinMargin);

            pageNumber--;   // Decrement the number, because the arrray starts at 0 not 1

            // Add teh object to the array
            ArrayList tempArray2 = (ArrayList)this._PDFDocumentPages[pageNumber];
            tempArray2.Add(tempArray);

            return true;
        }
        /// <summary>
        /// Adds a background image to the specified page.
        /// </summary>
        /// <param name="imagePath">Relative path to the image file.</param>
        /// <param name="pageNumber">Page number you would like the background image placed.</param>
        /// <returns>True if addition was successful, False if page number if out of bounds.</returns>
        public Boolean PDFAddBackgroundImage(string imagePath, int pageNumber)
        {
            return this.PDFAddBackgroundImage(imagePath, true, pageNumber);
        }

        /// <summary>
        /// Adds a text label object to the specified page.
        /// </summary>
        /// <param name="text">The text that you want to be displayed in the label.</param>
        /// <param name="xCoordinate">X-Coordinate label should be located.</param>
        /// <param name="yCoordinate">Y-Coordinate label should be located.</param>
        /// <param name="width">Width of the label.</param>
        /// <param name="height">Height of the label.</param>
        /// <param name="font">The font to be used for the label.</param>
        /// <param name="textSize">The text size.</param>
        /// <param name="txtAlign">Alignment within the label (left, right, center).</param>
        /// <param name="txtColor">Color of the text.</param>
        /// <param name="pageNumber">Page number you would like image placed.</param>
        /// <returns>True if addition was successful, False if page number of out of bounds.</returns>
        public Boolean PDFAddLabel(string text, float xCoordinate, float yCoordinate, float width, float height, PDFFonts font, float textSize, PDFTextAlign txtAlign, PDFColor txtColor, int pageNumber)
        {
            if (pageNumber > this._PDFDocumentPages.Count)
                return false;

            ArrayList tempArray = new ArrayList();
            tempArray.Add(PDFObjectType.Label);
            tempArray.Add(text);
            tempArray.Add(xCoordinate);
            tempArray.Add(yCoordinate);
            tempArray.Add(width);
            tempArray.Add(height);
            tempArray.Add(this.ConvertPDFFonts(font));      // Insert the preferred type fron the ceTe library
            tempArray.Add(textSize);
            tempArray.Add(this.ConvertTextAlign(txtAlign)); // Insert the preferred type from the ceTe library
            tempArray.Add(this.ConvertPDFColor(txtColor));  // Insert the preferred type from the ceTe library
            
            pageNumber--;   // Decrement the number, because the arrray starts at 0 not 1

            // Add teh object to the array
            ArrayList tempArray2 = (ArrayList)this._PDFDocumentPages[pageNumber];
            tempArray2.Add(tempArray);

            return true;
        }
        /// <summary>
        /// Adds a text label object to the specified page.
        /// </summary>
        /// <param name="text">The text that you want to be displayed in the label.</param>
        /// <param name="xCoordinate">X-Coordinate label should be located.</param>
        /// <param name="yCoordinate">Y-Coordinate label should be located.</param>
        /// <param name="width">Width of the label.</param>
        /// <param name="height">Height of the label.</param>
        /// <param name="font">The font to be used for the label.</param>
        /// <param name="textSize">The text size.</param>
        /// <param name="txtAlign">Alignment within the label (left, right, center).</param>
        /// <param name="pageNumber">Page number you would like image placed.</param>
        /// <returns>True if addition was successful, False if page number of out of bounds.</returns>
        public Boolean PDFAddLabel(string text, float xCoordinate, float yCoordinate, float width, float height, PDFFonts font, float textSize, PDFTextAlign txtAlign, int pageNumber)
        {
            return this.PDFAddLabel(text, xCoordinate, yCoordinate, width, height, font, textSize, txtAlign, this._PDFDefaultColor, pageNumber);
        }
        /// <summary>
        /// Adds a text label object to the specified page.
        /// </summary>
        /// <param name="text">The text that you want to be displayed in the label.</param>
        /// <param name="xCoordinate">X-Coordinate label should be located.</param>
        /// <param name="yCoordinate">Y-Coordinate label should be located.</param>
        /// <param name="width">Width of the label.</param>
        /// <param name="height">Height of the label.</param>
        /// <param name="font">The font to be used for the label.</param>
        /// <param name="textSize">The text size.</param>
        /// <param name="pageNumber">Page number you would like image placed.</param>
        /// <returns>True if addition was successful, False if page number of out of bounds.</returns>
        public Boolean PDFAddLabel(string text, float xCoordinate, float yCoordinate, float width, float height, PDFFonts font, float textSize, int pageNumber)
        {
            return this.PDFAddLabel(text, xCoordinate, yCoordinate, width, height, font, textSize, this._PDFDefaultTextAlign, pageNumber);
        }
        /// <summary>
        /// Adds a text label object to the specified page.
        /// </summary>
        /// <param name="text">The text that you want to be displayed in the label.</param>
        /// <param name="xCoordinate">X-Coordinate label should be located.</param>
        /// <param name="yCoordinate">Y-Coordinate label should be located.</param>
        /// <param name="width">Width of the label.</param>
        /// <param name="height">Height of the label.</param>
        /// <param name="font">The font to be used for the label.</param>
        /// <param name="pageNumber">Page number you would like image placed.</param>
        /// <returns>True if addition was successful, False if page number of out of bounds.</returns>
        public Boolean PDFAddLabel(string text, float xCoordinate, float yCoordinate, float width, float height, PDFFonts font, int pageNumber)
        {
            return this.PDFAddLabel(text, xCoordinate, yCoordinate, width, height, font, this._PDFDefaultTextSize, pageNumber);
        }
        /// <summary>
        /// Adds a text label object to the specified page.
        /// </summary>
        /// <param name="text">The text that you want to be displayed in the label.</param>
        /// <param name="xCoordinate">X-Coordinate label should be located.</param>
        /// <param name="yCoordinate">Y-Coordinate label should be located.</param>
        /// <param name="width">Width of the label.</param>
        /// <param name="height">Height of the label.</param>
        /// <param name="pageNumber">Page number you would like image placed.</param>
        /// <returns>True if addition was successful, False if page number of out of bounds.</returns>
        public Boolean PDFAddLabel(string text, float xCoordinate, float yCoordinate, float width, float height, int pageNumber)
        {
            return this.PDFAddLabel(text, xCoordinate, yCoordinate, width, height, this._PDFDefaultFont, pageNumber );
        }
        /// <summary>
        /// Adds a text label object to the specified page.
        /// </summary>
        /// <param name="text">The text that you want to be displayed in the label.</param>
        /// <param name="xCoordinate">X-Coordinate label should be located.</param>
        /// <param name="yCoordinate">Y-Coordinate label should be located.</param>
        /// <param name="width">Width of the label.</param>
        /// <param name="pageNumber">Page number you would like image placed.</param>
        /// <returns>True if addition was successful, False if page number of out of bounds.</returns>
        public Boolean PDFAddLabel(string text, float xCoordinate, float yCoordinate, float width, int pageNumber)
        {
            return this.PDFAddLabel(text, xCoordinate, yCoordinate, width, this._PDFDefaultHeight, pageNumber);
        }
        /// <summary>
        /// Adds a text label object to the specified page.
        /// </summary>
        /// <param name="text">The text that you want to be displayed in the label.</param>
        /// <param name="xCoordinate">X-Coordinate label should be located.</param>
        /// <param name="yCoordinate">Y-Coordinate label should be located.</param>
        /// <param name="pageNumber">Page number you would like image placed.</param>
        /// <returns>True if addition was successful, False if page number of out of bounds.</returns>
        public Boolean PDFAddLabel(string text, float xCoordinate, float yCoordinate, int pageNumber)
        {
            return this.PDFAddLabel(text, xCoordinate, yCoordinate, this._PDFDefaultWidth, pageNumber);
        }
        /// <summary>
        /// Adds a text label object to the specified page.
        /// </summary>
        /// <param name="text">The text that you want to be displayed in the label.</param>
        /// <param name="pageNumber">Page number you would like image placed.</param>
        /// <returns>True if addition was successful, False if page number of out of bounds.</returns>
        public Boolean PDFAddLabel(string text, int pageNumber)
        {
            return this.PDFAddLabel(text, this._PDFDefaultXCoordinate, this._PDFDefaultYCoordinate, pageNumber);
        }

        /// <summary>
        /// Adds a line shape to the specified page.
        /// </summary>
        /// <param name="xCoordinate1">X-Coordinate 1 of the Line object.</param>
        /// <param name="yCoordinate1">Y-Coordinate 1 of the Line object.</param>
        /// <param name="xCoordinate2">X-Coordinate 2 of the Line object.</param>
        /// <param name="yCoordinate2">Y-Coordinate 2 of the Line object.</param>
        /// <param name="width">Width of hte line (the length?).</param>
        /// <param name="lineColor">Color of the line.</param>
        /// <param name="lineStyle">Style of the line (solid, dotted, dash, dash-large, dash-small)</param>
        public void PDFAddLine(float xCoordinate1, float yCoordinate1, float xCoordinate2, float yCoordinate2, float width, PDFColor lineColor, PDFLineStyle lineStyle)
        {
            throw new System.NotImplementedException();
        }
        /// <summary>
        /// Adds a rectangle shape to the specified page.
        /// </summary>
        public void PDFAddRectangle()
        {
            throw new System.NotImplementedException();
        }
        /// <summary>
        /// Adds a link to the specified page.
        /// </summary>
        public void PDFAddLink()
        {
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// Adds a formatted text area to the specified page.
        /// </summary>
        /// <param name="text">The text that you want to be displayed in the text area.</param>
        /// <param name="xCoordinate">X-Coordinate text area should be located.</param>
        /// <param name="yCoordinate">Y-Coordinate text area should be located.</param>
        /// <param name="width">Width of the text area.</param>
        /// <param name="height">Height of the text area.</param>
        /// <param name="fontFamily">Font family that should be used on the text area.</param>
        /// <param name="textSize">The text size.</param>
        /// <param name="preserveWhiteSpace">Whether to preserve the white space, or treat it like html.</param>
        /// <param name="pageNumber">Page number you would like image placed.</param>
        /// <returns>True if addition was successful, False if page number of out of bounds.</returns>
        public Boolean PDFAddFormattedTextArea(string text, float xCoordinate, float yCoordinate, float width, float height, PDFFontFamily fontFamily, float textSize, Boolean preserveWhiteSpace, int pageNumber)
        {
            if (pageNumber > this._PDFDocumentPages.Count)
                return false;

            ArrayList tempArray = new ArrayList();
            tempArray.Add(PDFObjectType.FormattedTextArea);
            tempArray.Add(text);
            tempArray.Add(xCoordinate);
            tempArray.Add(yCoordinate);
            tempArray.Add(width);
            tempArray.Add(height);
            tempArray.Add(this.ConvertPDFFontFamily(fontFamily));      // Insert the preferred type fron the ceTe library
            tempArray.Add(textSize);
            tempArray.Add(preserveWhiteSpace);

            pageNumber--;   // Decrement the number, because the arrray starts at 0 not 1

            // Add teh object to the array
            ArrayList tempArray2 = (ArrayList)this._PDFDocumentPages[pageNumber];
            tempArray2.Add(tempArray);

            return true;
        }
        /// <summary>
        /// Adds a formatted text area to the specified page.
        /// </summary>
        /// <param name="text">The text that you want to be displayed in the text area.</param>
        /// <param name="xCoordinate">X-Coordinate text area should be located.</param>
        /// <param name="yCoordinate">Y-Coordinate text area should be located.</param>
        /// <param name="width">Width of the text area.</param>
        /// <param name="height">Height of the text area.</param>
        /// <param name="fontFamily">Font family that should be used on the text area.</param>
        /// <param name="textSize">The text size.</param>
        /// <param name="pageNumber">Page number you would like image placed.</param>
        /// <returns>True if addition was successful, False if page number of out of bounds.</returns>
        public Boolean PDFAddFormattedTextArea(string text, float xCoordinate, float yCoordinate, float width, float height, PDFFontFamily fontFamily, float textSize, int pageNumber)
        {
            return this.PDFAddFormattedTextArea(text, xCoordinate, yCoordinate, width, height, fontFamily, textSize, true, pageNumber);
        }
        /// <summary>
        /// Adds a formatted text area to the specified page.
        /// </summary>
        /// <param name="text">The text that you want to be displayed in the text area.</param>
        /// <param name="xCoordinate">X-Coordinate text area should be located.</param>
        /// <param name="yCoordinate">Y-Coordinate text area should be located.</param>
        /// <param name="width">Width of the text area.</param>
        /// <param name="height">Height of the text area.</param>
        /// <param name="fontFamily">Font family that should be used on the text area.</param>
        /// <param name="pageNumber">Page number you would like image placed.</param>
        /// <returns>True if addition was successful, False if page number of out of bounds.</returns>
        public Boolean PDFAddFormattedTextArea(string text, float xCoordinate, float yCoordinate, float width, float height, PDFFontFamily fontFamily, int pageNumber)
        {
            return this.PDFAddFormattedTextArea(text, xCoordinate, yCoordinate, width, height, fontFamily, this._PDFDefaultTextSize, pageNumber);
        }
        /// <summary>
        /// Adds a formatted text area to the specified page.
        /// </summary>
        /// <param name="text">The text that you want to be displayed in the text area.</param>
        /// <param name="xCoordinate">X-Coordinate text area should be located.</param>
        /// <param name="yCoordinate">Y-Coordinate text area should be located.</param>
        /// <param name="width">Width of the text area.</param>
        /// <param name="height">Height of the text area.</param>
        /// <param name="pageNumber">Page number you would like image placed.</param>
        /// <returns>True if addition was successful, False if page number of out of bounds.</returns>
        public Boolean PDFAddFormattedTextArea(string text, float xCoordinate, float yCoordinate, float width, float height, int pageNumber)
        {
            return this.PDFAddFormattedTextArea(text, xCoordinate, yCoordinate, width, height, this._PDFDefaultFontFamily, pageNumber);
        }
        /// <summary>
        /// Adds a formatted text area to the specified page.
        /// </summary>
        /// <param name="text">The text that you want to be displayed in the text area.</param>
        /// <param name="xCoordinate">X-Coordinate text area should be located.</param>
        /// <param name="yCoordinate">Y-Coordinate text area should be located.</param>
        /// <param name="width">Width of the text area.</param>
        /// <param name="pageNumber">Page number you would like image placed.</param>
        /// <returns>True if addition was successful, False if page number of out of bounds.</returns>
        public Boolean PDFAddFormattedTextArea(string text, float xCoordinate, float yCoordinate, float width, int pageNumber)
        {
            return this.PDFAddFormattedTextArea(text, xCoordinate, yCoordinate, width, this._PDFDefaultHeight, pageNumber);
        }
        /// <summary>
        /// Adds a formatted text area to the specified page.
        /// </summary>
        /// <param name="text">The text that you want to be displayed in the text area.</param>
        /// <param name="xCoordinate">X-Coordinate text area should be located.</param>
        /// <param name="yCoordinate">Y-Coordinate text area should be located.</param>
        /// <param name="pageNumber">Page number you would like image placed.</param>
        /// <returns>True if addition was successful, False if page number of out of bounds.</returns>
        public Boolean PDFAddFormattedTextArea(string text, float xCoordinate, float yCoordinate, int pageNumber)
        {
            return this.PDFAddFormattedTextArea(text, xCoordinate, yCoordinate, this._PDFDefaultWidth, pageNumber);
        }
        /// <summary>
        /// Adds a formatted text area to the specified page.
        /// </summary>
        /// <param name="text">The text that you want to be displayed in the text area.</param>
        /// <param name="pageNumber">Page number you would like image placed.</param>
        /// <returns>True if addition was successful, False if page number of out of bounds.</returns>
        public Boolean PDFAddFormattedTextArea(string text, int pageNumber)
        {
            return this.PDFAddFormattedTextArea(text, this._PDFDefaultXCoordinate, this._PDFDefaultYCoordinate, pageNumber);
        }

        /// <summary>
        /// Adds an ordered list to the specified page.
        /// </summary>
        public void PDFAddOrderedList()
        {
            throw new System.NotImplementedException();
        }
        /// <summary>
        /// Adds an unordered list to the specified page.
        /// </summary>
        public void PDFAddUnorderedList()
        {
            throw new System.NotImplementedException();
        }
        /// <summary>
        /// Adds a table to the specified page.
        /// </summary>
        public void PDFAddTable()
        {
            throw new System.NotImplementedException();
        }
        /// <summary>
        /// Adds a page numbering label to the specified page.
        /// </summary>
        public void PDFAddPageNumbering()
        {
            throw new System.NotImplementedException();
        }
        #endregion

        #region Private Methods
        private LineStyle ConvertPDFLineStyle (PDFLineStyle lineStyle)
        {
            switch(lineStyle)
            {
                case PDFLineStyle.Solid:    return LineStyle.Solid;
                case PDFLineStyle.Dash:     return LineStyle.Dash;
                case PDFLineStyle.DashLarge:return LineStyle.DashLarge;
                case PDFLineStyle.DashSmall:return LineStyle.DashSmall;
                case PDFLineStyle.Dots:     return LineStyle.Dots;
                default:                    return LineStyle.Solid;
            }
        }
        private RgbColor ConvertPDFColor(PDFColor color)
        {
            switch(color)
            {
                case PDFColor.Beige:    return RgbColor.Beige;
                case PDFColor.Black:    return RgbColor.Black;
                case PDFColor.Blue:     return RgbColor.Blue;
                case PDFColor.Brown:    return RgbColor.Brown;
                case PDFColor.DarkBlue: return RgbColor.DarkBlue;
                case PDFColor.DarkGray: return RgbColor.DarkGray;
                case PDFColor.DarkGreen:return RgbColor.DarkGreen;
                case PDFColor.DarkRed:  return RgbColor.DarkRed;
                case PDFColor.Gold:     return RgbColor.Gold;
                case PDFColor.Gray:     return RgbColor.Gray;
                case PDFColor.Green:    return RgbColor.Green;
                case PDFColor.Navy:     return RgbColor.Navy;
                case PDFColor.Olive:    return RgbColor.Olive;
                case PDFColor.Pink :    return RgbColor.Pink;
                case PDFColor.Purple:   return RgbColor.Purple;
                case PDFColor.Red:      return RgbColor.Red;
                case PDFColor.Teal:     return RgbColor.Teal;
                case PDFColor.White:    return RgbColor.White;
                case PDFColor.Yellow:   return RgbColor.Yellow;
                default:                return RgbColor.Black;

            }
        }
        private ceTe.DynamicPDF.TextAlign ConvertTextAlign(PDFTextAlign textAlign)
        {
            switch(textAlign)
            {
                case PDFTextAlign.Center:       return ceTe.DynamicPDF.TextAlign.Center;
                case PDFTextAlign.FullJustify:  return ceTe.DynamicPDF.TextAlign.FullJustify;
                case PDFTextAlign.Justify:      return ceTe.DynamicPDF.TextAlign.Justify;
                case PDFTextAlign.Left:         return ceTe.DynamicPDF.TextAlign.Left;
                case PDFTextAlign.Right:        return ceTe.DynamicPDF.TextAlign.Right;
                default:                        return ceTe.DynamicPDF.TextAlign.Left;
            }
        }
        private Font ConvertPDFFonts(PDFFonts textFont)
        {
            switch (textFont)
            {
                case PDFFonts.TimesRoman:       return Font.TimesRoman;
                case PDFFonts.TimesBold:        return Font.TimesBold;
                case PDFFonts.TimesItalic:      return Font.TimesItalic;
                case PDFFonts.Courier:          return Font.Courier;
                case PDFFonts.CourierBold:      return Font.CourierBold;
                case PDFFonts.CourierOblique:   return Font.CourierOblique;
                case PDFFonts.Helvetica:        return Font.Helvetica;
                case PDFFonts.HelveticaBold:    return Font.HelveticaBold;
                case PDFFonts.HelveticaBoldOblique:return Font.HelveticaBoldOblique;
                default: return Font.TimesRoman;
            }
        }
        private FontFamily ConvertPDFFontFamily(PDFFontFamily fontFamily)
        {
            switch (fontFamily)
            {
                case PDFFontFamily.Times: return FontFamily.Times;
                case PDFFontFamily.Helvetica: return FontFamily.Helvetica;
                case PDFFontFamily.Courier: return FontFamily.Courier;
                default: return FontFamily.Times;
            }
        }
        #endregion

        #endregion

        #region Word Documents
        /// <summary>
        /// Export content out to a word document.
        /// </summary>
        /// <param name="filename">Filename of the file to be produced. (Do not put an extension)</param>
        /// <param name="content">Formated string using ASCII characters to be exported. EX. '\n' = new line</param>
        public void ExportToWord(string fileName, string content)
        {
            HttpContext.Current.Response.Clear();
            HttpContext.Current.Response.Charset = "";

            HttpContext.Current.Response.ContentType = "application/msword";
            HttpContext.Current.Response.AddHeader("Content-Disposition", "inline;filename=" + fileName + ".doc");

            StringBuilder strContent = new StringBuilder();

            strContent.Append(content);

            HttpContext.Current.Response.Write(strContent);
            HttpContext.Current.Response.End();
            HttpContext.Current.Response.Flush();
        }
        #endregion

        #region Excel Documents
        private ArrayList _ExcelColumns;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="ds"></param>
        public void ExportToExcel(string fileName, DataSet ds)
        {
            // The following algorithm to export to excel was obtained from
            // http://forums.asp.net/thread/1289366.aspx

            foreach (ArrayList al in this._ExcelColumns)
            {
                if (ds.Tables[0].Columns.Contains(al[0].ToString()))
                    al[2] = ds.Tables[0].Columns[al[0].ToString()].Ordinal;
            }

            HttpContext.Current.Response.ContentType = "Application/vnd.ms-excel";
            HttpContext.Current.Response.AddHeader("Content-Disposition", "inline;filename=" + fileName + ".xls");

            HttpContext.Current.Response.Write("<table border='1px'><tr>");

            // Put the column headers in place
            foreach (ArrayList al in this._ExcelColumns)
            {
                if (Convert.ToInt32(al[2]) != -1)
                    if (al[1] == null) // No friendly name present
                        HttpContext.Current.Response.Write("<th>" + al[0].ToString() + "</th>");
                    else // Friendly name present
                        HttpContext.Current.Response.Write("<th>" + al[1].ToString() + "</th>");
            }

            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                HttpContext.Current.Response.Write("<tr>");
                foreach (ArrayList al in this._ExcelColumns)
                {
                    if (Convert.ToInt32(al[2]) != -1)
                        HttpContext.Current.Response.Write("<td>" + dr[Convert.ToInt32(al[2])].ToString() + "</td>");
                }
                HttpContext.Current.Response.Write("</tr>");
            }

            HttpContext.Current.Response.Write("</table>");
            HttpContext.Current.Response.Flush();
            HttpContext.Current.Response.Close();
            HttpContext.Current.Response.End();
        }

        /// <summary>
        /// Specify a column to be exported in the Excel document.
        /// </summary>
        /// <param name="dbColumn">The db column name to be exported.</param>
        public void ExcelAddColumn(string dbColumn)
        {
            ArrayList temp = new ArrayList();
            temp.Add(dbColumn);
            temp.Add(null);
            temp.Add(-1);

            this._ExcelColumns.Add(temp);
        }

        /// <summary>
        /// Specify a column to be exported in the Excel document.
        /// </summary>
        /// <param name="dbColumn">The db column name to be exported.</param>
        /// <param name="friendlyName">The prefered column name to use as the header in the exported file.</param>
        public void ExcelAddColumn(string dbColumn, string friendlyName)
        {
            ArrayList temp = new ArrayList();
            temp.Add(dbColumn);
            temp.Add(friendlyName);
            temp.Add(-1);

            this._ExcelColumns.Add(temp);
        }
        #endregion
    }
}