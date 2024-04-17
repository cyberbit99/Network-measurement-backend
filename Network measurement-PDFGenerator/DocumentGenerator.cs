using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System;


namespace Network_measurement_PDFGenerator
{
    public class DocumentGenerator
    {
        //private Document document;
        //private Section currentSection;
        //private Paragraph currentParagraph;
        //private readonly double maxContentHeight;

        //public DocumentGenerator(double maxContentHeight = 700)
        //{
        //    this.maxContentHeight = maxContentHeight;
        //    InitializeDocument();
        //}

        //private void InitializeDocument()
        //{
        //    // Create a new MigraDoc document
        //    document = new Document();

        //    // Initialize the first section and paragraph
        //    StartNewSection();
        //}

        //private void StartNewSection()
        //{
        //    // Add a new section to the document
        //    currentSection = document.AddSection();
        //    currentParagraph = currentSection.AddParagraph();
        //}

        //public void AddText(string text, bool bold = false)
        //{
        //    // Split the text into lines
        //    var lines = text.Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);

        //    foreach (var line in lines)
        //    {
        //        // Check if the next line will exceed the maximum content height
        //        if (currentParagraph.Format.LayoutInfo.ContentArea.Height + currentParagraph.Format.LineSpacing > maxContentHeight)
        //        {
        //            // Add a new section if the content exceeds the available space
        //            StartNewSection();
        //        }

        //        // Add the line to the current paragraph
        //        var formattedText = currentParagraph.AddFormattedText(line);
        //        if (bold)
        //        {
        //            formattedText.Bold = true;
        //        }

        //        // Add a new line
        //        currentParagraph.AddLineBreak();
        //    }
        //}

        //public void AddImage(string imagePath, double width = 300, double height = 200)
        //{
        //    // Check if the image will exceed the maximum content height
        //    if (currentParagraph.Format.LayoutInfo.ContentArea.Height + height > maxContentHeight)
        //    {
        //        // Add a new section if the content exceeds the available space
        //        StartNewSection();
        //    }

        //    // Add the image to the current paragraph
        //    var image = currentParagraph.AddImage(imagePath);
        //    image.Width = width;
        //    image.Height = height;

        //    // Add a new line
        //    currentParagraph.AddLineBreak();
        //}

        //public void Save(string outputPath)
        //{
        //    // Render the document to a PDF file
        //    PdfDocumentRenderer pdfRenderer = new PdfDocumentRenderer(true);
        //    pdfRenderer.Document = document;
        //    pdfRenderer.RenderDocument();
        //    pdfRenderer.PdfDocument.Save(outputPath);

        //    Console.WriteLine($"PDF created at: {outputPath}");
        //}
    }

    //class Program
    //{
    //    static void Main()
    //    {
    //        var generator = new DocumentGenerator();

    //        // Add content to the document
    //        generator.AddText("Hello, MigraDoc!", bold: true);
    //        generator.AddText("This is a simple document generator using MigraDoc.");
    //        generator.AddImage("path/to/your/image.jpg");

    //        // Add more content as needed...

    //        // Save the document
    //        generator.Save("output.pdf");
    //    }
    //}
}

