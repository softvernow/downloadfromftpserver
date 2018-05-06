using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace downloadfromftpserver
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
          richTextBox1.Text = Get_Data_From_FTP_Server_File();
        }

        //Method use to download and retrieve data from states.txt file in your ftp server. 
        private String Get_Data_From_FTP_Server_File()
        {
            //used to display data into rich text.box
            String result = String.Empty;
            
            //initialize FtpWebRequest with your FTP Url
            //your FTP url should start with ftp://wwww.yourftpsite.com//
            FtpWebRequest request = (FtpWebRequest)WebRequest.Create("ftp://wwww.yourftpsite.com//" + "states.txt");
            request.Method = WebRequestMethods.Ftp.DownloadFile;

            //set up credentials. 
            request.Credentials = new NetworkCredential("yourftplogin", "yourftppassword");

            //initialize Ftp response.
            FtpWebResponse response = (FtpWebResponse)request.GetResponse();

            //open readers to read data from ftp 
            Stream responsestream = response.GetResponseStream();
            StreamReader reader = new StreamReader(responsestream);

            //read data from FTP
            result = reader.ReadToEnd();

            //save file locally on your pc
            using (StreamWriter file = File.CreateText("states.txt"))
            {
                file.WriteLine(result);
                file.Close();
            }

            //close readers. 
            reader.Close();
            response.Close();

            //return data from file. 
            return result;

        }

      
    }
}




