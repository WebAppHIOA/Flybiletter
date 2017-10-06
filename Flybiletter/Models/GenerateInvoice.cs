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
        public string NewInvoise(Invoice invoice)
        {
            StringBuilder builder = new StringBuilder();
            Header(builder, invoice);
            Body(builder, invoice);
            Footer(builder);
            return builder.ToString();
        }

        public string Header(StringBuilder builder, Invoice invoice)
        {
            builder.Append("<html><head><body><h2><center><u>Order invoice</u></center></h2>");
            return "Halla";
        }

        public string Body(StringBuilder builder, Invoice invoice)
        {
            builder.Append("<body><table style='100%'><tr><td> Ordrenummer: </td><td>").Append(invoice.OrderReferance).Append("</td></tr>");
            builder.Append("<tr><td>Dato: </td><td>").Append(invoice.Date).Append("</td></tr>");
            builder.Append("<tr><td>Fra: </td><td>").Append(invoice.From).Append("</td></tr>");
            builder.Append("<tr><td>Til: </td><td>").Append(invoice.Destination).Append("</td></tr>");
            builder.Append("<tr><td>Pris: </td><td>").Append(invoice.Price).Append("</td></tr></br>");

            builder.Append("</br><p>Forfallsdator er 14 dager etter motatt faktura<p></br>");

            builder.Append("<tr><td>Account number: </td></td>1234.56.78901</td></tr>");
            builder.Append("<tr><td>KID number: </td><td>").Append(invoice.OrderReferance).Append(" </ td ></ tr ></table></body>");

            return "Test";
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

        public void SendEmail()
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
            mail.To.Add(new MailAddress("katrinealmas@gmail.com"));
            mail.Attachments.Add(new Attachment(new MemoryStream(ConvertHtmlToPDF("<h2>Test</h2></br><p>Funker det?</p>")), "Ordre.pdf"));

            //Formatted mail body
            mail.IsBodyHtml = true;
            string st = "Takk for din ordre ";

            mail.Body = st;
            smtp.Send(mail);
        }

        public string Footer(StringBuilder builder)
        {
            builder.Append("</head></html>");
            return "the end";
        }
    }
}