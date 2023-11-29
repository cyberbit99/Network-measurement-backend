using PdfSharp.Drawing;
using PdfSharp.Pdf;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Network_measurement_PDFGenerator
{
    public class PDFGenerator
    {
        public PDFGenerator() { }

        public List<string> ReadAllPDF()
        {
            string folderPath = @"C:\Users\Admin\source\repos\Network measurement-backend\Network measurement-PDFGenerator\Reports";

            // Check if the folder exists
            if (Directory.Exists(folderPath))
            {
                // Get all file names in the folder
                List<string> fileNames = Directory.GetFiles(folderPath).ToList();

                return fileNames;
            }
            else
            {
                return null;
            }
        }
        public PdfDocument Generator() 
        {
            // Create a new PDF document
            using (PdfDocument document = new PdfDocument())
            {
                // Add a page to the document
                PdfPage page = document.AddPage();
                XGraphics gfx = XGraphics.FromPdfPage(page);

                // Set font properties
                XFont fontHeader = new XFont("Times New Roman", 20);
                XFont fontDescription = new XFont("Times New Roman", 12);
                XFont fontParagraph = new XFont("Times New Roman", 12);

                // Draw centered header
                string headerText = "Header Text";
                XSize headerSize = gfx.MeasureString(headerText, fontHeader);
                XPoint headerPosition = new XPoint((page.Width - headerSize.Width) / 2, 20);
                gfx.DrawString(headerText, fontHeader, XBrushes.Black, headerPosition);

                // Draw description under the header
                string descriptionText = "This is a description.";
                XPoint descriptionPosition = new XPoint(50, headerPosition.Y + headerSize.Height + 10);
                gfx.DrawString(descriptionText, fontDescription, XBrushes.Black, descriptionPosition);

                // Draw a solid line under the description
                XPoint lineStart = new XPoint(50, descriptionPosition.Y + 10);
                XPoint lineEnd = new XPoint(page.Width - 50, lineStart.Y);
                gfx.DrawLine(XPens.Black, lineStart, lineEnd);

                // Draw numbered paragraphs
                XPoint paragraphPosition = new XPoint(50, lineEnd.Y + 20);
                for (int i = 1; i <= 5; i++)
                {
                    string paragraphText = $" {i}: This is a sample sentence.";
                    gfx.DrawString(paragraphText, fontParagraph, XBrushes.Black, paragraphPosition);
                    paragraphPosition.Y += 20;
                }

                // Save the document to a file
                string filePath = "output.pdf";
                document.Save(filePath);

                Console.WriteLine($"PDF created successfully at: {filePath}");

                // Open the generated PDF file
                //Process.Start(filePath);
                return document;
            }
        }
    }
}
