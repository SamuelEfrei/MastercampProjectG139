using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PdfSharp.Drawing;
using PdfSharp.Pdf;
using PdfSharp.Drawing.Layout;
using System.Diagnostics;


namespace MastercampProjectG139
{
    internal class PDF
    {

        public void GeneratePDF()
        {
            PdfDocument doc = new PdfDocument();
            PdfPage page = doc.AddPage();

            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            doc.Info.Title = "ratio";
            XGraphics gfx = XGraphics.FromPdfPage(page);
            XFont font;
            font = new("Verdana", 20, XFontStyle.BoldItalic);

            XTextFormatter tf = new XTextFormatter(gfx);
            tf.DrawString("Information \n du \n Médecin", font, XBrushes.Black,
            new XRect(50, 50, page.Width, page.Height), XStringFormats.TopLeft);

            gfx.DrawString("Medicaments + instructions", font, XBrushes.Black,
            new XRect(0, 0, page.Width, page.Height), XStringFormats.Center);

            gfx.DrawString("Signature tampon etc", font, XBrushes.Black,
            new XRect(50, -50, page.Width, page.Height), XStringFormats.BottomLeft);

            gfx.DrawString("Ptit code", font, XBrushes.Black,
            new XRect(-50, -50, page.Width, page.Height), XStringFormats.BottomRight);



            string filename = "Ordonnance.pdf";

            doc.Save(filename);

            System.Diagnostics.Process.Start("explorer", filename);

        }
    }
}