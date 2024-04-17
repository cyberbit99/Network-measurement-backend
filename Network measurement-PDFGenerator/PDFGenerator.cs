
using QuestPDF.Infrastructure;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using System.Text.Json.Serialization;
using Newtonsoft.Json;
using Network_measurement_database.Model.Requests;
using Network_measurement_database.Model;

namespace Network_measurement_PDFGenerator
{
    //cseréld le ezt a szart egy dinktopdfre. az megoldja a bajodat
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
        public string Generator(MeasurementReportRequest reportRequest)
        {
            string folderPath = @"C:\Users\Admin\source\repos\Network measurement-backend\Network measurement-PDFGenerator\Reports\";

            List<Measurement> measurements = new List<Measurement>();
            measurements = reportRequest.Data;
            QuestPDF.Settings.License = LicenseType.Community;
            // code in your main method
            Document.Create(container =>
            {
                container.Page(page =>
                {
                    page.Size(PageSizes.A4);
                    page.Margin(2, Unit.Centimetre);
                    page.PageColor(Colors.White);
                    page.DefaultTextStyle(x => x.FontSize(12));

                    page.Header()
                        .Text(reportRequest.Name)
                        .SemiBold().FontSize(36).FontColor(Colors.Blue.Medium);

                    page.Content().PaddingVertical(1, Unit.Centimetre).Column(column =>
                    {
                        column.Item().Text(reportRequest.Description);
                        foreach (var measurement in measurements)
                        {
                            column.Spacing(20);
                            column.Item().Element(container => ComposeMeasurement(container, measurement));
                        }
                    });
                    //page.Content()
                    //    .PaddingVertical(1, Unit.Centimetre)
                    //    .Column(x =>
                    //    {
                    //        x.Spacing(20);

                    //        x.Item().Text(Placeholders.LoremIpsum());
                    //        x.Item().Text(Placeholders.LoremIpsum());
                    //        x.Item().Image(Placeholders.Image(200, 100));
                    //    });

                    page.Footer()
                        .AlignCenter()
                        .Text(x =>
                        {
                            x.Span("Page ");
                            x.CurrentPageNumber();
                        });
                });
            })
            .GeneratePdf(folderPath + reportRequest.MeasurementReportId.ToString() + ".pdf");

            return reportRequest.MeasurementReportId.ToString() + ".pdf";
        }

        //public void Compose(IDocumentContainer container)
        //{
        //    container
        //        .Page(page =>
        //        {
        //            page.Margin(25);
        //            page.Header().Text("Measurement Report");
        //            page.Content().Column(column =>
        //            {
        //                foreach (var measurement in _measurements)
        //                {
        //                    column.Item().Element(container => ComposeMeasurement(container, measurement));
        //                }
        //            });
        //            page.Footer().Text(x => x.CurrentPageNumber().ToString());
        //        });
        //}

        private void ComposeMeasurement(IContainer container, Measurement measurement)
        {
            //var measuredData = JObject.Parse(measurement.MeasuredData);

            // Itt döntünk el, hogy milyen típusú a mérés és ennek megfelelően jelenítjük meg
            switch (measurement.MeasurementTypeId)
            {
                case 1:
                    // Típus 1-es adatok megjelenítése
                    DisplayType1(container, measurement);
                    break;
                case 2:
                    // Típus 2-es adatok megjelenítése
                    DisplayType2(container, measurement);
                    break;
                case 3:
                    // Típus 3-as adatok megjelenítése
                    DisplayType3(container, measurement);
                    break;
                default:
                    container.Text("Ismeretlen méréstípus");
                    break;
            }
        }

        private void DisplayType1(IContainer container, Measurement measurement)
        {
            //Speedtest
            var list = JsonConvert.DeserializeObject<List<SpeedTestItem>>(measurement.MeasuredData);

            container.Table(table =>
            {
                // step 1
                table.ColumnsDefinition(columns =>
                {
                    columns.ConstantColumn(25);
                    columns.RelativeColumn(3);
                    columns.RelativeColumn();
                    columns.RelativeColumn();
                });

                // step 2
                table.Header(header =>
                {
                    header.Cell().Element(CellStyle).Text("#");
                    header.Cell().Element(CellStyle).Text("Up");
                    header.Cell().Element(CellStyle).Text("Down");
                    header.Cell().Element(CellStyle).Text("Ping");


                    static IContainer CellStyle(IContainer container)
                    {
                        return container.DefaultTextStyle(x => x.SemiBold()).PaddingVertical(5).BorderBottom(1).BorderColor(Colors.Black);
                    }
                });

                // step 3
                foreach (var item in list)
                {
                    table.Cell().Element(CellStyle).Text(list.IndexOf(item) + 1);
                    table.Cell().Element(CellStyle).Text(item.Up);
                    table.Cell().Element(CellStyle).Text(item.Down);
                    table.Cell().Element(CellStyle).Text(item.Ping);

                    static IContainer CellStyle(IContainer container)
                    {
                        return container.BorderBottom(1).BorderColor(Colors.Grey.Lighten2).PaddingVertical(5);
                    }
                }
            });
        }

        private void DisplayType2(IContainer container, Measurement measurement)
        {
            //Wifi signal data

            var list = JsonConvert.DeserializeObject<List<NetworkData>>(measurement.MeasuredData);
            container.Table(table =>
            {
                // step 1
                table.ColumnsDefinition(columns =>
                {
                    columns.ConstantColumn(25);
                    columns.RelativeColumn(3);
                    columns.RelativeColumn();
                    columns.RelativeColumn();

                });

                // step 2
                table.Header(header =>
                {
                    header.Cell().Element(CellStyle).Text("#");
                    header.Cell().Element(CellStyle).Text("SSID");
                    header.Cell().Element(CellStyle).AlignLeft().Text("BSSID");
                    header.Cell().Element(CellStyle).AlignRight().Text("Signal strength");

                    static IContainer CellStyle(IContainer container)
                    {
                        return container.DefaultTextStyle(x => x.SemiBold()).PaddingVertical(5).BorderBottom(1).BorderColor(Colors.Black);
                    }
                });

                // step 3
                foreach (var item in list)
                {
                    table.Cell().Element(CellStyle).Text(list.IndexOf(item) + 1);
                    table.Cell().Element(CellStyle).Text(item.SsidName);
                    table.Cell().Element(CellStyle).AlignLeft().Text(item.Bssid);
                    table.Cell().Element(CellStyle).AlignRight().Text(item.SignalStrengthDecibel + "dBm");

                    static IContainer CellStyle(IContainer container)
                    {
                        return container.BorderBottom(1).BorderColor(Colors.Grey.Lighten2).PaddingVertical(5);
                    }
                }
            });
        }

        private void DisplayType3(IContainer container, Measurement measurement)
        {
            //Device scan
            var list = JsonConvert.DeserializeObject<List<Device>>(measurement.MeasuredData);


            container.Table(table =>
            {
                // step 1
                table.ColumnsDefinition(columns =>
                {
                    columns.ConstantColumn(25);
                    columns.RelativeColumn(3);
                    columns.RelativeColumn();

                });

                // step 2
                table.Header(header =>
                {
                    header.Cell().Element(CellStyle).Text("#");
                    header.Cell().Element(CellStyle).Text("Device name");
                    header.Cell().Element(CellStyle).AlignRight().Text("Ip address");


                    static IContainer CellStyle(IContainer container)
                    {
                        return container.DefaultTextStyle(x => x.SemiBold()).PaddingVertical(5).BorderBottom(1).BorderColor(Colors.Black);
                    }
                });

                // step 3
                foreach (var item in list)
                {
                    table.Cell().Element(CellStyle).Text(list.IndexOf(item) + 1);
                    table.Cell().Element(CellStyle).Text(item.Name);
                    table.Cell().Element(CellStyle).AlignRight().Text(item.IPAddress);


                    static IContainer CellStyle(IContainer container)
                    {
                        return container.BorderBottom(1).BorderColor(Colors.Grey.Lighten2).PaddingVertical(5);
                    }
                }
            });
        }

        public DocumentMetadata GetMetadata() => DocumentMetadata.Default;



        //public PdfDocument Generator()
        //{
        //    GlobalFontSettings.FontResolver = new FontResolver();
        //    var fontCollection = new InstalledFontCollection();
        //    ;
        //    // Create a new PDF document
        //    using (PdfDocument document = new PdfDocument())
        //    {
        //        // Add a page to the document
        //        PdfPage page = document.AddPage();
        //        XGraphics gfx = XGraphics.FromPdfPage(page);
        //        XFontFamily xFont = new XFontFamily("Helvetica");
        //        // Set font properties
        //        XFont fontHeader = new XFont(xFont.Name, 20);
        //        XFont fontDescription = new XFont("Helvetica", 12);
        //        XFont fontParagraph = new XFont("Helvetica", 12);

        //        // Draw centered header
        //        string headerText = "Header Text";
        //        XSize headerSize = gfx.MeasureString(headerText, fontHeader);
        //        XPoint headerPosition = new XPoint((page.Width - headerSize.Width) / 2, 20);
        //        gfx.DrawString(headerText, fontHeader, XBrushes.Black, headerPosition);

        //        // Draw description under the header
        //        string descriptionText = "This is a description.";
        //        XPoint descriptionPosition = new XPoint(50, headerPosition.Y + headerSize.Height + 10);
        //        gfx.DrawString(descriptionText, fontDescription, XBrushes.Black, descriptionPosition);

        //        // Draw a solid line under the description
        //        XPoint lineStart = new XPoint(50, descriptionPosition.Y + 10);
        //        XPoint lineEnd = new XPoint(page.Width - 50, lineStart.Y);
        //        gfx.DrawLine(XPens.Black, lineStart, lineEnd);

        //        // Draw numbered paragraphs
        //        XPoint paragraphPosition = new XPoint(50, lineEnd.Y + 20);
        //        for (int i = 1; i <= 500; i++)
        //        {
        //            string paragraphText = $" {i}: This is a sample sentence.";
        //            gfx.DrawString(paragraphText, fontParagraph, XBrushes.Black, paragraphPosition);
        //            paragraphPosition.Y += 20;
        //        }

        //        // Save the document to a file
        //        string filePath = @"C:\Users\Admin\source\repos\Network measurement-backend\Network measurement-PDFGenerator\Reports\output.pdf";
        //        document.Save(filePath);

        //        // Open the generated PDF file
        //        //Process.Start(filePath);
        //        return document;
        //    }
        //}


    }
}
