using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Web;
using iTextSharp.text;
using iTextSharp.text.html.simpleparser;
using iTextSharp.text.pdf;
using System.Net.Mime;
using System.IO;
using System.Net;

namespace Flybiletter.Models
{
    public class Invoice
    {
        public string InvoiceId { get; set; }
        public string OrderReferance { get; set; }
        public string Date { get; set; }
        public string From { get; set; }
        public string Destination { get; set; }
        public string Price { get; set; }
        public string Email { get; set; }
    }
    public class GenerateInvoice
    {
        /* DB og modeller må oppdateres før dette evt vil fungere da departur per dags dato ikke har en
         * direkte relasjon til order
         * 
         */
        public string NewInvoice(Invoice invoice)
        {
            StringBuilder builder = new StringBuilder();
            Header(builder, invoice);
            Body(builder, invoice);
            Footer(builder);
            return builder.ToString();
        }

        public static void Header(StringBuilder builder, Invoice invoice)
        {
            builder.Append("<html><head><body><h3><b>Faktura for din ordre</b></h3></br>");
        }

        public static void Body(StringBuilder builder, Invoice invoice)
        {
            builder.Append("</br><table style='100%'><tr><td><p>Ordrenummer:</p></td><td><p>").Append(invoice.OrderReferance).Append("</p></td></tr>");
            builder.Append("<tr><td><p>Dato: </p></td><td><p>").Append(invoice.Date).Append("</p></td></tr>");
            builder.Append("<tr><td><p>Fra: </p></td><td><p>").Append(invoice.From).Append("</p></td></tr>");
            builder.Append("<tr><td><p>Til: </p></td><td><p>").Append(invoice.Destination).Append("</p></td></tr>");
            builder.Append("<tr><td><p>Pris: </p></td><td><p>").Append(invoice.Price).Append("</p></td></tr></table></br>");

            builder.Append("<table style='100%'><tr><td><b><p>Kontonummer: </p></b></td><td><b><p>1234.56.78911</p></b></td>");
            builder.Append("<td><b><p>KID number: </p></b></td><td><b><p>").Append(invoice.OrderReferance).Append("</p></b></td></tr></table></body>");
        }

        private static Byte[] ConvertHtmlToPDF(string emailContent)
        {
            //Create a MemoryStream which will hold the data
            var ms = new MemoryStream();

            //Create an iTextSharp Document
            using (var doc = new Document())
            {
                //Create a pdf writer that will hold the instance of PDF abstraction which is doc and the memory stream
                var writer = PdfWriter.GetInstance(doc, ms);

                //Open the document for writing
                doc.Open();

                //Create a new HTMLWorker bound to our document
                using (var htmlWorker = new HTMLWorker(doc))
                {
                    //HTMLWorker needs a TextReader (in this case a StringReader) to read string 
                    using (var sr = new StringReader(emailContent))
                    {
                        //Parse the HTML
                        htmlWorker.Parse(sr);
                    }
                }

                writer.CloseStream = false;

                //close the document
                doc.Close();

                byte[] bytes = ms.ToArray();
                ms.Close();

                //return the stream
                return bytes;
            }
        }

        public void SendEmail(byte[] streamResult, Invoice invoice)
        {
            MailMessage mail = new MailMessage();
            mail.From = new System.Net.Mail.MailAddress("katrinealmastest@gmail.com");

            // The important part -- configuring the SMTP client
            SmtpClient smtp = new SmtpClient();
            smtp.Port = 587;   // [1] You can try with 465 also, I always used 587 and got success
            smtp.EnableSsl = true;
            smtp.DeliveryMethod = SmtpDeliveryMethod.Network; // [2] Added this
            smtp.UseDefaultCredentials = false; // [3] Changed this
            smtp.Credentials = new NetworkCredential("katrinealmastest@gmail.com", "K2s0G1a7");  // [4] Added this. Note, first parameter is NOT string.
            smtp.Host = "smtp.gmail.com";

            //recipient address
            mail.To.Add(new MailAddress(invoice.Email));
            mail.Attachments.Add(new Attachment(new MemoryStream(streamResult), "Faktura.pdf"));
            mail.Subject = "Ordrebekreftelse";

            mail.IsBodyHtml = true;
            mail.Body = "Takk for din ordre";
            smtp.Send(mail); ;
        }

        public static void Footer(StringBuilder builder)
        {
            builder.Append("</head></html>");
        }
    }
}