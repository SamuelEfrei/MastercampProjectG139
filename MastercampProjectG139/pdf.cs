using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PdfSharp.Drawing;
using PdfSharp.Pdf;
using PdfSharp.Drawing.Layout;
using System.Diagnostics;
using MastercampProjectG139.Models;
using MastercampProjectG139.Stores;
using MastercampProjectG139.ViewModels;
using MastercampProjectG139.Services;


namespace MastercampProjectG139
{
    internal class PDF
    {
        private Medecin medecin;
        private ModelOrdonnance ordo;


        public void GeneratePDF(Medecin medecin, ModelOrdonnance ordo)
        {
            PdfDocument doc = new PdfDocument();
            PdfPage page = doc.AddPage();
            this.medecin = medecin;
            this.ordo = ordo;

            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            doc.Info.Title = "ratio";
            XGraphics gfx = XGraphics.FromPdfPage(page);
            XFont font;
            font = new("Verdana", 15, XFontStyle.Italic);



            XTextFormatter tf = new XTextFormatter(gfx);
            tf.DrawString(medecin.getNom().ToUpper() + "\n" + medecin.getPrenom().ToUpper() + "\n" + "44 avenue des hôpitaux", font, XBrushes.Black,
            new XRect(50, 50, page.Width, page.Height), XStringFormats.TopLeft);
            int marge = 0;

            for (int i = 0; i < ordo.GetAllMedicaments().Count(); i++)
            {
                marge -= 15;
            }

            foreach (ModelMedicament med in ordo.GetAllMedicaments())
            {
                gfx.DrawString(med.Name.ToUpper() + " " + med.Frequence + " pendant " + med.Duration , font, XBrushes.Black,
                new XRect(0, marge, page.Width, page.Height), XStringFormats.Center);                
                marge+=30;
            }



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