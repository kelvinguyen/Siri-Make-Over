using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Capstone1.Model;

namespace Capstone1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        string str = "https://www.google.com/search?q=";
        //string str = "https://duckduckgo.com/?q=";

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            WorkerClass wc = new WorkerClass();
            //ContentData cd = new ContentData();

            //RichText.Text = wc.getSourceCode("www.google.com");
            //.RichText.RichText.t = wc.getSourceCode("www.google.com");
            //RichText.Document.Blocks.Clear();
            //RichText.Document.Blocks.Add(new Paragraph(new Run(wc.getSourceCode("http://www.bongda.com.vn"))));

            if(EnterBox.Text != "")
            {
                Dictionary<string, string> contentList = new Dictionary<string, string>();
               // RichText.Document.Blocks.Clear();
                contentList["address"] = EnterBox.Text;
                RichText.Document.Blocks.Clear();
                //string content = wc.getContentOnly(str + EnterBox.Text);
                //string content = wc.getAllConnectingUrl2(str + EnterBox.Text);
                wc.Question = EnterBox.Text;
                string content = wc.analysisTheContentDiv(str + EnterBox.Text);
               // string content = cd.CollectDataPerLink("http://www.encyclopedia.com/topic/Bill_Gates.aspx");
                Paragraph p = new Paragraph(new Run(content));
                RichText.Document.Blocks.Add(p);

            }
        }
    }
}
