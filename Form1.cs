using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using WhetherMysqlSham.plugin;
using WhetherMysqlSham.plugin.NoReflection;

namespace WhetherMysqlSham
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void Button1_Click(object sender, EventArgs e)
        {
            string host = Host.Text;
            int port = Convert.ToInt32(Port.Text);
            log.Text = "Log:\r\n";
            if (!Regex.IsMatch(host, @"^((2[0-4]\d|25[0-5]|[01]?\d\d?)\.){3}(2[0-4]\d|25[0-5]|[01]?\d\d?)$"))
            {
                MessageBox.Show("ip error!");
                log.Text += "ip error!\r\n";
                return;
            }
            Assembly asm = Assembly.GetExecutingAssembly();
            Type[] types = asm.GetTypes();
            foreach (var item in types)
            {
                if (item.Namespace== "WhetherMysqlSham.plugin")
                {
                    if (item.GetType().IsInstanceOfType(typeof(Plugin)))
                    {
                        if (item.IsPublic)
                        {
                            if (item.IsClass)
                            {
                                Plugin plugin =  (Plugin)Activator.CreateInstance(item.UnderlyingSystemType);
                                plugin.Init(host,port);
                                try
                                {
                                    log.Text += string.Format("{0} : {1}\r\n", item.Name, plugin.IsSham());
                                }
                                catch (Exception E)
                                {
                                    log.Text += string.Format("{0} : {1}\r\n", item.Name, E.Message);
                                }
                                

                            }
                        }
                    }
                }
            }
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            log.Text = "Info:\r\n";
            GetServerInfo serverInfo = new GetServerInfo();
            serverInfo.Init(Host.Text,int.Parse(Port.Text));
            log.Text += serverInfo.GetInfo();
        }

        private void LinkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start("https://github.com/BeichenDream/WhetherMysqlSham");
        }
    }
}
