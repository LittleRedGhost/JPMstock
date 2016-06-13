using System.Windows;
using System.Collections.Generic;
using System.Linq;
using System;
using System.Text;
using System.Windows.Controls;

namespace wpfStock
{
    // Simple class to hold the information for a single stock item
    class stockData 
    {
        public string symbol = "";
        public string type = "";
        public decimal lastDividend = -1;
        public int fixedDividend = -1;
        public decimal parValue  = -1;

        public stockData(string symbol, string type, decimal lastDividend, int fixedDividend, decimal parValue)
        {
            this.symbol = symbol;
            this.type = type;
            this.lastDividend = lastDividend;
            this.fixedDividend = fixedDividend;
            this.parValue = parValue;
        }
    }

    public partial class MainWindow : Window
    {
        // Store for all stock types
        private List<stockData> allStock = new List<stockData>();

        private bool ValidateGetPrice(string priceText, out decimal priceValue)
        {
            bool priceTest = decimal.TryParse(priceText, out priceValue);
            if(priceTest==false)
                MessageBox.Show("Sorry, invalid price.", "JP", MessageBoxButton.OK, MessageBoxImage.Error);
            return priceTest;
        }
        private bool ValidateGetStock(out stockData stockData)
        {
            if (listStockType.SelectedItem == null)
            {
                MessageBox.Show("Please select a stock symbol.", "JP", MessageBoxButton.OK, MessageBoxImage.Error);
                stockData = null;
                return false;
            }
            //Find the relevant stock details from our allStock list...
            string stockSymbol = listStockType.SelectedItem.ToString();
            stockData = allStock.FirstOrDefault(st => st.symbol == stockSymbol);
            if (stockData == null)
                MessageBox.Show("Sorry, unrecognised stock symbol [" + stockSymbol + "].", "JP", MessageBoxButton.OK, MessageBoxImage.Error);
            return (stockData != null);
        }
        private decimal? GetDividendYield(stockData stock, decimal stockPrice)
        {
            // Perform the correct dividend calculation, according to the information in the class
            decimal? dividendYield = null;
            switch (stock.type)
            {
                case "Common":
                    dividendYield = stock.lastDividend / stockPrice;
                    break;
                case "Preferred":
                    dividendYield = (stock.fixedDividend * stock.parValue / 100M) / stockPrice;
                    break;
                default:
                    MessageBox.Show("Sorry, unrecognised stock type [" + stock.type + "].", "JP", MessageBoxButton.OK, MessageBoxImage.Error);
                    break;
            }
            return dividendYield;
        }

        public MainWindow()
        {
            InitializeComponent();
            // Set up our 5 little stock types...
            allStock.Add(new stockData("TEA", "Common", 0M, 0, 1M));
            allStock.Add(new stockData("POP", "Common", 0.08M, 0, 1M));
            allStock.Add(new stockData("ALE", "Common", 0.23M, 0, 0.6M));
            allStock.Add(new stockData("GIN", "Preferred", 0.08M, 2, 1M));
            allStock.Add(new stockData("JOE", "Common", 0.13M, 0, 2.5M));
            // Start with a random price...
            textPrice.Text = (new Random().Next(10, 1000) / 100M).ToString("0.00");
            // Build the list for simple selection...
            foreach (var stock in allStock)
                listStockType.Items.Add(stock.symbol);
        }

        private void buttonDividendYield_Click(object sender, RoutedEventArgs e)
        {
            // Clear the display, then validate the inputs...
            labelDividendYield.Content = "£-.--";
            stockData stock = null;
            decimal stockPrice = 0.0M;
            if (ValidateGetPrice(textPrice.Text, out stockPrice) == false) return;
            if (ValidateGetStock(out stock) == false) return;
            // Call the calculation function and display...
            decimal? dividendYield = GetDividendYield(stock, stockPrice);
            if (dividendYield != null)
                labelDividendYield.Content = "£" + ((double)dividendYield).ToString("0.000");
        }

        private void buttonPERatio_Click(object sender, RoutedEventArgs e)
        {
            // Clear the display, then validate the inputs...
            labelPERatio.Content = "£-.--";
            stockData stock = null;
            decimal stockPrice = 0.0M;
            if (ValidateGetPrice(textPrice.Text, out stockPrice) == false) return;
            if (ValidateGetStock(out stock) == false) return;
            // Call the calculation function and display...
            decimal? dividendYield = GetDividendYield(stock, stockPrice);
            if (dividendYield != null)
            {
                if (dividendYield == 0)
                {
                    if (sender is Button) // Don't show messagebox if called from listbox)
                        MessageBox.Show("Sorry, cannot calculate the P/E Ration with a dividend of zero.", "JP", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else
                    labelPERatio.Content = ((decimal)(stockPrice / dividendYield)).ToString("0.000");
            }
        }

        private void buttonBuy_Click(object sender, RoutedEventArgs e)
        {
            // Simulate the addition of 50 random traces with time stamps, prices, and quantities.
            // Along the way store GeoMeanTotal, and the VWSP sums for ease of calculation at the end.
            Random myRandom = new Random();
            DateTime rightNow = DateTime.Now;
            DateTime tradeTime = DateTime.Now.AddHours(-1);
            listTrades.Items.Clear();
            double SumPriceQuantity = 0;
            double SumQuantity = 0;
            double GeoMeanTotal = 1; // Can't start the multiplication count with zero :)
            int StockCount = 50;
            for (int ii = 0; ii < StockCount; ii++)
            {
                // Make everything nice and random.
                int tradeQuantity = myRandom.Next(1,200);
                double tradePrice = myRandom.Next(100, 2000) / 200d;
                string buyOrSell = (myRandom.Next(2) == 1) ? "BUY " : "SELL";
                tradeTime = tradeTime.AddSeconds(new Random().Next(1, 100));
                // Build a fixed-width column display for quickness and clarity...
                StringBuilder aTrade = new StringBuilder();
                aTrade.Append(tradeTime.ToString("yyyy-MM-dd HH:mm:ss")).Append(" // ");
                aTrade.Append(tradeQuantity.ToString("000")).Append(" // ");
                aTrade.Append(buyOrSell).Append(" // £");
                aTrade.Append(string.Format("{0,5:#0.00}",tradePrice)).Append(" // £");
                aTrade.Append(string.Format("{0,8:####0.00}", tradePrice * tradeQuantity));
                listTrades.Items.Add(aTrade);
                // If item is within the last 15 minutes, select it and prepare for the Volume Weighted Stock Price calc
                if ((tradeTime.AddMinutes(15) > rightNow) && (tradeTime < rightNow))
                {   // Also ignore items from the future!!
                    listTrades.SelectedItems.Add(listTrades.Items.GetItemAt(ii));
                    SumPriceQuantity += (tradePrice * tradeQuantity);
                    SumQuantity += tradeQuantity;
                }
                //Build the price "total" for the GeoMean calc...
                GeoMeanTotal *= tradePrice;
            }
            // Perform the final VWSP calc
            labelVWSPresult.Content = "£-.--";
            labelCount.Content = listTrades.SelectedItems.Count + " item(s).";
            if (listTrades.SelectedItems.Count > 0) // Don't attempt if no items found in last 15 minutes.
            {
                double VWSP = SumPriceQuantity / SumQuantity;
                labelVWSPresult.Content = "£" + VWSP.ToString("0.00");
            }
            //Geometric Mean calculation...
            double GeoMean = Math.Pow(GeoMeanTotal, 1d / StockCount);
            labelGeoMean.Content = "Geometric Mean is the " + StockCount + "th root of "
                + GeoMeanTotal.ToString("E") + " = " + GeoMean.ToString("0.000");
        }

        private void listStockType_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            // Call both button clicks for speed of use.
            buttonDividendYield_Click(sender, e);
            buttonPERatio_Click(sender, e);
        }
    }
}
