using ClearRedis.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ClearRedis
{
    public partial class ClearRedis : Form
    {
        public ClearRedis()
        {
            InitializeComponent();
        }

        private void btnQuery_Click(object sender, EventArgs e)
        {
            string key = txtKey.Text.Trim();
            if (string.IsNullOrWhiteSpace(key))
            {
                MessageBox.Show("请输入银行卡卡号!");
                return;
            }
            ThreadPool.QueueUserWorkItem((obj) =>
            {
                string value = CRedisUtility.GetValue(key);
                txtValue.Invoke((Action)delegate
                {
                    if (string.IsNullOrWhiteSpace(value))
                    {
                        MessageBox.Show("未找到相关缓存内容!");
                    }
                    txtValue.Text = value;
                });
            });


        }

        private void txtValue_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            string key = txtKey.Text.Trim();
            if (string.IsNullOrWhiteSpace(key))
            {
                MessageBox.Show("请输入银行卡卡号!");
                return;
            }
            ThreadPool.QueueUserWorkItem((obj) =>
            {
                string msg = CRedisUtility.ClearValue(key);
                txtValue.Invoke((Action)delegate
                {
                    if (!string.IsNullOrWhiteSpace(msg))
                    {
                        MessageBox.Show(msg);
                    }
                    txtValue.Text = CRedisUtility.GetValue(key);
                });
            });
        }
    }
}

