using System;
using System.Collections;
using System.Collections.Specialized;
using ceTe.DynamicPDF;
using ceTe.DynamicPDF.PageElements;

namespace CAESDO
{
    public partial class SNAAPExportOps_ExportToPDF : System.Web.UI.Page
    {
        //protected ExportOpsBeta xops;

        protected ExportOps xops;

        protected void Page_Load(object sender, EventArgs e)
        {
            //this.xops = (ExportOpsBeta)Session["PDFExport"];
            this.xops = (ExportOps)Session["PDFExport"];

            //ArrayList Pages = xops.Pages;
            //ArrayList Headers = xops.Headers;
            //ArrayList Pictures = xops.Pictures;

            ArrayList Pages = xops.PDFDocumentPages;

            ArrayList OverFlowPages = new ArrayList();

            Document pdfDoc = new Document();
            pdfDoc.Author = xops.PDFDocumentAuthor;
            pdfDoc.Title = xops.PDFDocumentTitle;

            ceTe.DynamicPDF.Page tempPage;

            int j;

            foreach (ArrayList al in Pages) // Iterate through the collection of pages, each iteration contains the objects for the pages
            {
                tempPage = new ceTe.DynamicPDF.Page();
                if (xops.PDFUsingTemplate) // If true, assign the template to the page, but not yet implmented
                {
                }
                foreach (ArrayList obj in al)   // This iterates through the objects, each iteration will handle an object on the page
                {
                    switch ((ExportOps.PDFObjectType)obj[0]) // Tells us which type of object we have and the structure of the arraylist
                    {
                        case ExportOps.PDFObjectType.Image:
                            Image tempImage = new Image(MapPath(obj[1].ToString()), (float)obj[2], (float)obj[3], (float)obj[4]);
                            tempPage.Elements.Add(tempImage);
                            break;
                        case ExportOps.PDFObjectType.BackgroundImage:
                            BackgroundImage tempBackImage = new BackgroundImage(MapPath(obj[1].ToString()));
                            tempBackImage.UseMargins = Convert.ToBoolean(obj[2]);
                            tempPage.Elements.Add(tempBackImage);
                            break;
                        case ExportOps.PDFObjectType.Label:
                            tempPage.Elements.Add(new Label(obj[1].ToString(), (float)obj[2], (float)obj[3], (float)obj[4], (float)obj[5], (Font)obj[6], (float)obj[7], (TextAlign)obj[8], (RgbColor)obj[9]));
                            break;
                        case ExportOps.PDFObjectType.Line:
                            break;
                        case ExportOps.PDFObjectType.Rectangle:
                            break;
                        case ExportOps.PDFObjectType.Link:
                            break;
                        case ExportOps.PDFObjectType.FormattedTextArea:
                            FormattedTextAreaStyle style = new FormattedTextAreaStyle((FontFamily)obj[6], (float)obj[7], Convert.ToBoolean(obj[8]));
                            FormattedTextArea textArea = new FormattedTextArea(Convert.ToString(obj[1]), (float)obj[2], (float)obj[3], (float)obj[4], (float)obj[5], style);
                            tempPage.Elements.Add(textArea);

                            // Check for over flow text
                            if (textArea.GetOverflowFormattedTextArea() != null)
                            {
                                // Set up extra variables
                                ceTe.DynamicPDF.Page overFlowPage;

                                // There is more text than can be displayed, create a new page
                                do
                                {
                                    overFlowPage = new ceTe.DynamicPDF.Page();
                                    textArea = textArea.GetOverflowFormattedTextArea();

                                    // Add the text element
                                    overFlowPage.Elements.Add(textArea);

                                    // Add the page to the overflow array
                                    OverFlowPages.Add(overFlowPage);

                                } while (textArea.GetOverflowFormattedTextArea() != null);
                            }

                            break;
                        case ExportOps.PDFObjectType.OrderedList:
                            break;
                        case ExportOps.PDFObjectType.UnorderedList:
                            break;
                        case ExportOps.PDFObjectType.Table:
                            break;
                        case ExportOps.PDFObjectType.PageNumbering:
                            break;
                        default:
                            // do nothing
                            break;
                    }

                }
                pdfDoc.Pages.Add(tempPage);

                if (OverFlowPages.Count > 0)
                {
                    foreach (ceTe.DynamicPDF.Page p in OverFlowPages)
                    {
                        pdfDoc.Pages.Add(p);
                    }

                    // Clear the array
                    OverFlowPages.Clear();
                }
            }

            #region Old Code
            //for (int i = 0; i < Pages.Count; i++)
            //{
            //    tempPage = new ceTe.DynamicPDF.Page();

            //    // Insert the Header
            //    if (Headers[i] != null)
            //    {
            //        headerTemp = (ArrayList)Headers[i];
            //        tempImage = new Image(MapPath(headerTemp[0].ToString()), (float)headerTemp[1], (float)headerTemp[2], (float) headerTemp[3]);
            //        tempPage.Elements.Add(tempImage);
            //    }

            //    tempText = (ArrayList)Pages[i];

            //    foreach (ArrayList al in tempText)
            //    {
            //        tempString = al[0].ToString();
            //        tempPage.Elements.Add(new Label(al[0].ToString(), (float)al[1], (float)al[2], (float)al[3], (float)al[4], Font.TimesRoman, 12));
            //    }

            //    foreach (ArrayList al in Pictures)
            //    {
            //        j = i + 1;
            //        if (Convert.ToInt32(al[4]) == j)
            //        {
            //            tempImage = new Image(MapPath(al[0].ToString()), (float)al[1], (float)al[2], (float)al[3]);
            //            tempPage.Elements.Add(tempImage);
            //        }
                    
            //    }

            //    pdfDoc.Pages.Add(tempPage);
            //}
            #endregion
            pdfDoc.DrawToWeb(this);
        }
    }
}