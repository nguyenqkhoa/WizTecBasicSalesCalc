using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Xml.Serialization;


//1 - An interface showing products, the transaction,
//subtotal amount, tax amount, the final amount,
//a cancel/void button, and a save/export button.

namespace WizTecBasicSalesCalc
{
    public partial class Form1 : Form
    {
        //Set up base variables and the object class for output
        //Output will consist for each button and will have:
        //a price, type if it's taxable, and item type to output on the textbox.
        List<Product> output = new List<Product>();
        double VAT = 0.05;
        double subTotal = 0.0;
        double Tax = 0.0;
        double Total = 0.0;
        List<string> salesText = new List<string>();

        //2- There must be at least 10 different products,
        //each with their own price, option to apply a 10% VAT tax,
        //and belonging to 1 of 3 groups.
        //You are free to decide what the products, groups, and prices are.
        public class Product
        {
            public double price { get; set; }
            public string type { get; set; }
            public string item { get; set; }
        }

        public Form1()
        {
            InitializeComponent();
            
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            salesText.Add("Subtotal: ");
            salesText.Add("Tax: ");
            salesText.Add("Total: ");
            changeSalesText();

        }

        //For each of the twelve buttons when pressed:
        //Adds to the output list with the three variables
        //Add the selected product to the textbox
        //Update/calculate the current subtotal, tax, and total

        //Start of product buttons
        private void Apple_Click(object sender, EventArgs e)
        {
            output.Add(new Product() 
            { price = 1.05, type = "Produce", item = "Apple - 1.05" });
            changeText();
        }

        private void Orange_Click(object sender, EventArgs e)
        {
            output.Add(new Product()
            { price = 0.80, type = "Produce", item = "Orange - 0.80" });
            changeText();
        }

        private void Banana_Click(object sender, EventArgs e)
        {
            output.Add(new Product()
            { price = 0.75, type = "Produce", item = "Banana - 0.75" });
            changeText();
        }

        private void WindshieldFluid_Click(object sender, EventArgs e)
        {
            output.Add(new Product()
            { price = 15.50, type = "Vehicle", item = "Windshield Fluid - 15.50" });
            changeText();
        }

        private void Lube_Click(object sender, EventArgs e)
        {
            output.Add(new Product()
            { price = 12.28, type = "Vehicle", item = "Lube - 12.28" });
            changeText();
        }

        private void Pump_Click(object sender, EventArgs e)
        {
            output.Add(new Product()
            { price = 50.99, type = "Vehicle", item = "Pump - 50.99" });
            changeText();
        }

        private void Keyboard_Click(object sender, EventArgs e)
        {
            output.Add(new Product()
            { price = 20.00, type = "Electronics", item = "Keyboard - 20.00" });
            changeText();
        }

        private void Mouse_Click(object sender, EventArgs e)
        {
            output.Add(new Product()
            { price = 15.00, type = "Electronics", item = "Mouse - 15.00" });
            changeText();
        }

        private void Mousepad_Click(object sender, EventArgs e)
        {
            output.Add(new Product()
            { price = 10.00, type = "Electronics", item = "Mousepad - 10.00" });
            changeText();
        }

        private void LipBalm_Click(object sender, EventArgs e)
        {
            output.Add(new Product()
            { price = 2.55, type = "Healthcare", item = "Lip Balm - 2.55" });
            changeText();
        }

        private void Ointment_Click(object sender, EventArgs e)
        {
            output.Add(new Product()
            { price = 10.55, type = "Healthcare", item = "Ointment - 10.55" });
            changeText();
        }

        private void Lotion_Click(object sender, EventArgs e)
        {
            output.Add(new Product()
            { price = 5.55, type = "Healthcare", item = "Lotion - 5.55" });
            changeText();
        }
        //End of product buttons

        //4 - A cancel or void button which,
        //when pressed, clears and restarts the sale
        private void Void_Click(object sender, EventArgs e)
        {  
            output.Clear();
            subTotal = 0.0;
            Tax = 0.0;
            Total = 0.0;
            this.Transaction.Text = "";
            this.Sales.Text = "";
        }

        //Todo - Added
        //5 - A save or export button which, when pressed,
        //saves or exports the current transaction in a JSON, XML, or text format.
        //Exports to WizTecBasicSalesCalc\bin\Build\netcoreapp3.1\"ExportedText"
        private void Export_Click(object sender, EventArgs e)
        {
            //Text
            TextWriter exportedText = new StreamWriter("ExportedText.txt");

            foreach (var p in output)
            {
                exportedText.WriteLine(p.item);
            }
            exportedText.Close();

            //XML
            XmlSerializer serialiser = new XmlSerializer(typeof(List<Product>));
            TextWriter exportedXml = new StreamWriter("ExportedText.xml");
            serialiser.Serialize(exportedXml, output);
            exportedXml.Close();
        }

        //3 - When a product is selected, it is added to the current transaction.
        //The transaction must support up to 10 different products,
        //listing product and price, and calculating subtotal, VAT, and total

        //Change the transaction text box
        private void changeText()
        {
            subTotal = 0.0;
            Tax = 0.0;
            Total = 0.0;
            var listString = new StringBuilder();
            foreach (var p in output)
            {
                listString.Append(p.item);
                listString.Append(Environment.NewLine);
                if (p.type == "Healthcare")
                {
                    subTotal += p.price;
                    subTotal = Math.Round(subTotal, 2, MidpointRounding.ToZero);
                }
                else
                {
                    subTotal += p.price;
                    Tax += (p.price * VAT);
                    subTotal = Math.Round(subTotal, 2, MidpointRounding.ToZero);
                    Tax = Math.Round(Tax, 2, MidpointRounding.ToZero);
                }
            }
            Total += subTotal + Tax;
            Total = Math.Round(Total, 2, MidpointRounding.ToZero);
            Transaction.Text = listString.ToString();
            changeSalesText();
        }

        //Change the sales text box
        private void changeSalesText()
        {
            var salesString = new StringBuilder();
            List<string> salesText = new List<string>();
            salesText.Add("Subtotal: ");
            salesText.Add("Tax: ");
            salesText.Add("Total: ");
            for(int i = 0; i < salesText.Count; i++)
            {
                if (i == 0) {
                    salesString.Append(salesText[i] + subTotal);
                    salesString.Append(Environment.NewLine);
                }
                if (i == 1)
                {
                    salesString.Append(salesText[i] + Tax);
                    salesString.Append(Environment.NewLine);
                }
                if (i == 2)
                {
                    salesString.Append(salesText[i] + Total);
                    salesString.Append(Environment.NewLine);
                }
            }
            Sales.Text = salesString.ToString();
        }
        private void Transaction_TextChanged(object sender, EventArgs e)
        {

        }

        private void Sales_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
