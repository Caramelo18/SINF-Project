using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using FirstREST.Lib_Primavera.Model;
using FirstREST.Models;


namespace FirstREST.Controllers
{
    public class SaleInvoice
    {
        public Models.Invoice invoice { get; set; }
        public List<Models.Line> lines { get; set; }
        public Models.DocumentTotals docs { get; set; }

        public SaleInvoice(Models.Invoice invoice, List<Models.Line> lines, Models.DocumentTotals docs)
        {
            this.invoice = invoice;
            this.lines = lines;
            this.docs = docs;
        }
        public SaleInvoice() { }
    }

    public class Sums : SaleInvoice
    {
        public double sumDay = 0;
        public double sumMonth = 0;
        public double sumYear = 0;
        public double sumTotal = 0;
        public Sums(double year, double month, double day, double total)
        {
            sumDay = day;
            sumMonth = month;
            sumYear = year;
            sumTotal = total;
        }
    }

    public class SalesInvoicesController : ApiController
    {
        DatabaseEntities db = new DatabaseEntities();

        [Route("api/salesInvoices")]
        public List<SaleInvoice> Get()
        {
            DateTime time = DateTime.Today;
            var year = time.Year;
            var month = time.Month;
            var day = time.Day;

            double sumYear = 0;
            double sumMonth = 0;
            double sumDay = 0;
            double sumTotal = 0;

            var docs = new List<SaleInvoice>();

            List<Models.Invoice> invoices = (from i in db.Invoice select i).ToList();
            foreach (Models.Invoice invoice in invoices)
            {
                DateTime de = DateTime.Parse( invoice.InvoiceDate);
                List<Models.Line> lines = (from i in db.Invoice
                                           join l in db.Line
                                           on i.InvoiceNo equals l.InvoiceNo
                                           where i.InvoiceNo == invoice.InvoiceNo
                                           select l).ToList();

                Models.DocumentTotals doc = (from d in db.DocumentTotals
                                             where d.InvoiceNo == invoice.InvoiceNo
                                             select d).AsQueryable().First();

                docs.Add(new SaleInvoice(invoice, lines, doc));
                sumTotal += doc.GrossTotal;
                if (de.Year >= year - 1)
                {
                    sumYear += doc.GrossTotal;
                    if (de.Month >= month)
                    {
                        sumMonth += doc.GrossTotal;
                        if (de.Day == day)
                        {
                            sumDay += doc.GrossTotal;
                        }
                    }
                }
            }
            docs.Add(new Sums(sumYear, sumMonth, sumDay, sumTotal));
            //docs = docs.OrderByDescending(x => x.Data).ToList();

            return docs;
        }

        [HttpGet]
        public List<SaleInvoice> GetByDate(string date)
        {
            DateTime time = DateTime.Today;
            var year = time.Year;
            var month = time.Month;
            var day = time.Day;

            double sumYear = 0;
            double sumMonth = 0;
            double sumDay = 0;
            double sumTotal = 0;

            var docs = new List<SaleInvoice>();

            List<Models.Invoice> invoices = (from i in db.Invoice where i.InvoiceDate.CompareTo(date) >= 0 select i).ToList();
            foreach (Models.Invoice invoice in invoices)
            {
                DateTime de = DateTime.Parse(invoice.InvoiceDate);
                List<Models.Line> lines = (from i in db.Invoice
                                           join l in db.Line
                                           on i.InvoiceNo equals l.InvoiceNo
                                           where i.InvoiceNo == invoice.InvoiceNo
                                           select l).ToList();

                Models.DocumentTotals doc = (from d in db.DocumentTotals
                                             where d.InvoiceNo == invoice.InvoiceNo
                                             select d).AsQueryable().First();

                docs.Add(new SaleInvoice(invoice, lines, doc));
            }
            //docs = docs.OrderByDescending(x => x.Data).ToList();

            return docs;
        }


        [Route("api/DocVenda/get?id={id*}")]
        public SaleInvoice Get(string id)
        {
            try
            {
                Models.Invoice invoice = (from i in db.Invoice
                                          where i.InvoiceNo == id
                                          select i).AsQueryable().First();

                List<Models.Line> lines = (from i in db.Invoice
                                           join l in db.Line
                                           on i.InvoiceNo equals l.InvoiceNo
                                           where i.InvoiceNo == invoice.InvoiceNo
                                           select l).ToList();

                Models.DocumentTotals doc = (from d in db.DocumentTotals
                                             where d.InvoiceNo == invoice.InvoiceNo
                                             select d).AsQueryable().First();

                return new SaleInvoice(invoice, lines, doc);
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine(e);
                return null;
            }
        }

        public List<SaleInvoice> GetByProduct(string id)
        {
            try
            {
                List<SaleInvoice> list = new List<SaleInvoice>();

                List<Models.Invoice> invoices = (from i in db.Invoice
                                                 join l in db.Line
                                                 on i.InvoiceNo equals l.InvoiceNo
                                                 where l.ProductCode == id
                                                 select i).ToList();

                foreach (var invoice in invoices)
                {
                    List<Models.Line> lines = (from i in db.Line
                                               where i.InvoiceNo == invoice.InvoiceNo
                                               && i.ProductCode == id
                                               select i).ToList();

                    Models.DocumentTotals doc = (from d in db.DocumentTotals
                                                 where d.InvoiceNo == invoice.InvoiceNo
                                                 select d).AsQueryable().First();

                    if (list.Find(x => x.invoice == invoice) == null)
                    {
                        list.Add(new SaleInvoice(invoice, lines, doc));
                    }
                }

                return list;

            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine(e);
                return null;
            }
        }

        public List<SaleInvoice> GetByClient(string id)
        {
            try
            {
                List<SaleInvoice> list = new List<SaleInvoice>();

                List<Models.Invoice> invoices = (from i in db.Invoice
                                                 where i.CustomerID == id
                                                 select i).ToList();

                foreach (var invoice in invoices)
                {
                    List<Models.Line> lines = (from i in db.Line
                                               where i.InvoiceNo == invoice.InvoiceNo
                                               select i).ToList();

                    Models.DocumentTotals doc = (from d in db.DocumentTotals
                                                 where d.InvoiceNo == invoice.InvoiceNo
                                                 select d).AsQueryable().First();

                    list.Add(new SaleInvoice(invoice, lines, doc));
                }

                return list;

            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine(e);
                return null;
            }
        }
    }
}
