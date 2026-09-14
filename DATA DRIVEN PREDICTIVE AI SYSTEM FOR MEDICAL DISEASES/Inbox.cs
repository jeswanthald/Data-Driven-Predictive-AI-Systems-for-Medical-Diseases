using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json;
using DATA_DRIVEN_PREDICTIVE_AI_SYSTEM_FOR_MEDICAL_DISEASES;


namespace DATA_DRIVEN_PREDICTIVE_AI_SYSTEM_FOR_MEDICAL_DISEASES
{
    public partial class Inbox : Form
    {
        private List<EmailItem> messages = new List<EmailItem>();
        private bool isReplyFormOpen = false;

        public Inbox()
        {
            InitializeComponent();
            this.Load += Inbox_Load;
            dgv.RowHeadersWidth = 4;

            dgv.CellContentClick += dgv_CellContentClick;

        }

        private async void Inbox_Load(object sender, EventArgs e)
        {
            ConfigureDataGridView();
            await LoadMessagesFromServer();
        }

        private void ConfigureDataGridView()
        {

            dgv.AutoGenerateColumns = false;
            dgv.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgv.MultiSelect = false;
            dgv.Columns.Clear();

            dgv.Columns.Add(new DataGridViewTextBoxColumn { Name = "Name", HeaderText = "Name", DataPropertyName = "Name", Width = 120 });
            dgv.Columns.Add(new DataGridViewTextBoxColumn { Name = "Email", HeaderText = "Email", DataPropertyName = "Email", Width = 150 });
            dgv.Columns.Add(new DataGridViewTextBoxColumn { Name = "Message", HeaderText = "Message", DataPropertyName = "Message", Width = 250 });
            dgv.Columns.Add(new DataGridViewTextBoxColumn { Name = "ReceivedAt", HeaderText = "Received", DataPropertyName = "ReceivedAt", Width = 150 });
            dgv.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "File", DataPropertyName = "FileUrl", Width = 100, Visible = false }); // hidden
          
        }


        private async Task LoadMessagesFromServer()
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    var response = await client.GetStringAsync("http://127.0.0.1:5000/api/inbox");
                    messages = JsonConvert.DeserializeObject<List<EmailItem>>(response);
                    dgv.DataSource = messages;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("❌ Failed to load messages: " + ex.Message);
            }
        }

        private void dgv_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && !isReplyFormOpen)
            {
                try
                {
                    
                    isReplyFormOpen = true;

                    var row = dgv.Rows[e.RowIndex];
                    string name = row.Cells[0].Value?.ToString();
                    string email = row.Cells[1].Value?.ToString();
                    string message = row.Cells[2].Value?.ToString();
                    string fileUrl = row.Cells[4].Value?.ToString(); // FileUrl column is 4th


                    var replyForm = new Reply(name, email, message, fileUrl);


                    
                    replyForm.ShowDialog();  // ✅ BLOCKS until Reply form is closed
                    RefreshMessages();


                }
                catch (Exception ex)
                {
                    isReplyFormOpen = false;
                    MessageBox.Show("❌ Error opening reply: " + ex.Message);
                }
            }
        }

        public async void RefreshMessages()
        {
            try
            {
                await LoadMessagesFromServer();

                // Force UI refresh
                dgv.DataSource = null;
                dgv.DataSource = messages;
          
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed to refresh inbox: " + ex.Message);
            }
        }


        private void OpenMessage(EmailItem item)
        {
            if (isReplyFormOpen) return; // Prevent multiple reply forms

            isReplyFormOpen = true;
            var replyForm = new Reply(item.Name, item.Email, item.Message, item.FileUrl);

            replyForm.FormClosed += (s, e) =>
            {
                isReplyFormOpen = false;
                dgv.ClearSelection(); // Optional: clear selection for better UX
            };

            replyForm.Show();
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            RefreshMessages();
        }

        private void BtnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;

        }
    }
}
