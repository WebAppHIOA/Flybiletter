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
        public string OrderReferance { get; set; }
        public string Date { get; set; }
        public string From { get; set; }
        public string Destination { get; set; }
        public string Price { get; set; }
        public string Email { get; set; }
    }
    public static class GenerateInvoice
    {
        /* DB og modeller må oppdateres før dette evt vil fungere da departur per dags dato ikke har en
         * direkte relasjon til order
         */

        public static string NewInvoice(Invoice newInvoice)
        {
            StringBuilder builder = new StringBuilder();
            Header(builder);
            Body(builder, newInvoice);
            Footer(builder);

            return builder.ToString();
        }

        public static void Header(StringBuilder builder)
        {
            builder.Append("<html><head><body><h3><b>Faktura for din ordre</b></h3></br>");
        }

        public static void Body(StringBuilder builder, Invoice invoice)
        {
            builder.Append("</br><table style='100%'><tr><td><p>Ordrenummer:</p></td><td><p>").Append(invoice.OrderReferance).Append("</p></td></tr>");
            builder.Append("<tr><td><p>Dato: </p></td><td><p>").Append(invoice.Date).Append("</p></td></tr>");
            builder.Append("<tr><td><p>Fra: </p></td><td><p>").Append(invoice.From).Append("</p></td></tr>");
            builder.Append("<tr><td><p>Til: </p></td><td><p>").Append(invoice.Destination).Append("</p></td></tr>");
            builder.Append("<tr><td><p>Pris: </p></td><td><p>").Append(invoice.Price).Append(" kr</p></td></tr></table></br>");

            builder.Append("<table style='100%'><tr><td><b><p>Kontonummer: </p></b></td><td><b><p>1234.56.78911</p></b></td>");
            builder.Append("<td><b><p>KID number: </p></b></td><td><b><p>").Append(invoice.OrderReferance).Append("</p></b></td></tr></table></body>");
        }

        public static Byte[] ConvertHtmlToPDF(string emailContent)
        {
           // string emailContent = builder.ToString();
            var ms = new MemoryStream();

            using (var doc = new Document())
            {
                var writer = PdfWriter.GetInstance(doc, ms);
                doc.Open();

                using (var htmlWorker = new HTMLWorker(doc))
                {
                    using (var sr = new StringReader(emailContent))
                    {
                        htmlWorker.Parse(sr);
                    }
                }

                writer.CloseStream = false;
                doc.Close();

                byte[] bytes = ms.ToArray();
                ms.Close();

                return bytes;
            }
        }


        public static void SendEmail(Invoice invoice)
        {
            var emailContent = NewInvoice(invoice);
            var streamResult = ConvertHtmlToPDF(emailContent);
            MailMessage mail = new MailMessage();
            mail.From = new System.Net.Mail.MailAddress("katrinealmastest@gmail.com");

            // configuring the SMTP client
            SmtpClient smtp = new SmtpClient();
            smtp.Port = 587;
            smtp.EnableSsl = true;
            smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
            smtp.UseDefaultCredentials = false;
            smtp.Credentials = new NetworkCredential("katrinealmastest@gmail.com", "K2s0G1a7");
            smtp.Host = "smtp.gmail.com";

            //recipient address
            mail.To.Add(new MailAddress(invoice.Email));
            mail.Attachments.Add(new Attachment(new MemoryStream(streamResult), "Faktura.pdf"));
            mail.Subject = "Ordrebekreftelse";

            mail.IsBodyHtml = true;
            mail.Body = "Takk for din ordre";
            smtp.Send(mail);
        }

        public static void Footer(StringBuilder builder)
        {
            builder.Append("</head></html>");
        }

        public static string UniqueReference()
        {
            var guid = System.Guid.NewGuid().ToString();

            return guid;
        }


    }
}