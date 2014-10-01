using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace POSSystem
{
    public class CommandParser
    {
        private readonly ISaleEventListener _saleEventListener;

        public CommandParser(ISaleEventListener saleEventListener)
        {
            _saleEventListener = saleEventListener;
        }

        public void Parse(string command)
        {
            var commandParts = command.Split(':');
            var commandName = commandParts[1];
            if (commandParts[0] == "Command")
            {
                if (commandName == "NewSale")
                {
                    _saleEventListener.NewSaleInitialized();
                }
                if (commandName == "EndSale")
                {
                    _saleEventListener.EndSale();
                }
            } else if (commandParts[0] == "Input")
            {
                var commandData = commandParts[1].Split(',');
                var barcodeInfo = commandData[0].Split('=');
                var quantityInfo = commandData[1].Split('=');
                _saleEventListener.AddProduct(barcodeInfo[1], int.Parse(quantityInfo[1]));
            }
            
        }
    }
}
