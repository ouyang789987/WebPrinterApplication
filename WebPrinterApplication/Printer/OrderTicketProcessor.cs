using System;
using System.Collections.Generic;
using log4net;

namespace WepPrinterApplication.Printer
{
    public class OrderData
    {
        public String Caption { get; set; }
        public String Name { get; set; }
        public String Phone { get; set; }
        public String Email { get; set; }
        public String Message { get; set; }

        public OrderData()
        {
            Caption = "";
            Name = "";
            Phone = "";
            Email = "";
            Message = "";
        }
    }

    public interface IOrderTicketProcessor
    {
        void PrintTicket(OrderData data);
        void Start(String logoPath);
        void Stop();
    }

    public class OrderTicketProcessor : TicketProcessor, IOrderTicketProcessor
    {
        private static readonly ILog logger = LogManager.GetLogger(typeof(OrderTicketProcessor));

        public void PrintTicket(OrderData data)
        {
            PrinterLineInfo line = new PrinterLineInfo(true, PrinterLineFontType.Default, "---------------------------------");
                
            var infos = new List<PrinterLineInfo>
            {
                new PrinterLineInfo(true, PrinterLineFontType.Caption, data.Caption),
                line,
                new PrinterLineInfo(false, PrinterLineFontType.Default, AddColon("���"), DateTime.Now.ToString()),
                new PrinterLineInfo(false, PrinterLineFontType.Default, AddColon("�������"), data.Phone),
                new PrinterLineInfo(false, PrinterLineFontType.Default, AddColon("E-mail"), data.Email),
                new PrinterLineInfo(true, PrinterLineFontType.Default, "\n" + CleanMessage(data.Message)),
                line
            };


            logger.Info("Printing a ticket with data:");
            foreach (PrinterLineInfo info in infos) {
                logger.Info(info.ToString());
            }

            ThreadStart(infos);
        }

        public static string CleanMessage(string source)
        {
            return source.Replace("\r\n", "").Replace("\\n", "\n");
        }
    }
}
