using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net.Mail;
using System.Net.Http;
using Newtonsoft.Json;
using System.IO;
using NAudio.Wave;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;
using System.Xml.Linq;
using System.Threading;


namespace DATA_DRIVEN_PREDICTIVE_AI_SYSTEM_FOR_MEDICAL_DISEASES
{
    public partial class Reply : Form
    {

        private string replyFilePath = "";
        private WaveInEvent waveIn;
        private WaveFileWriter writer;
        private TcpListener server = null;
        private CancellationTokenSource cts = new CancellationTokenSource();




        public Reply(string name, string email, string message, string fileUrl)
        {
            InitializeComponent();

            txtName.Text = name ?? "";
            txtEmail.Text = email ?? "";
            txtMessage.Text = message ?? "";
            txtReply.Text = "";

            this.Shown += Reply_Shown;

            if (!string.IsNullOrWhiteSpace(fileUrl))
            {
                linkAttachment.Text = "📎 Open Attachment";
                linkAttachment.Links.Clear();
                linkAttachment.Links.Add(0, linkAttachment.Text.Length, fileUrl);
                linkAttachment.Visible = true;
            }
            else
            {
                linkAttachment.Visible = false;
            }

        }

        private void StartServer()
        {
            if (server != null) return;
            try
            {
                server = new TcpListener(IPAddress.Loopback, 9000);
                server.Start();

                Task.Run(() =>
                {
                    while (!cts.Token.IsCancellationRequested)
                    {
                        TcpClient client = null;
                        try
                        {
                            if (!server.Pending())
                            {
                                Thread.Sleep(100);
                                continue;
                            }

                            client = server.AcceptTcpClient();
                            using (NetworkStream stream = client.GetStream())
                            {
                                byte[] buffer = new byte[client.ReceiveBufferSize];
                                int bytesRead = stream.Read(buffer, 0, buffer.Length);
                                string message = Encoding.UTF8.GetString(buffer, 0, bytesRead);

                                var parts = message.Split(new[] { "|||" }, StringSplitOptions.None);
                                if (parts.Length >= 3)
                                {
                                    string name = parts[0];
                                    string email = parts[1];
                                    string userMessage = parts[2];
                                    string fileUrl = parts.Length >= 4 ? parts[3] : "";

                                    if (!IsDisposed && IsHandleCreated)
                                    {
                                        BeginInvoke((MethodInvoker)(() =>
                                        {
                                            txtName.Text = name;
                                            txtEmail.Text = email;
                                            txtMessage.Text = userMessage;
                                            txtReply.Clear();

                                            if (!string.IsNullOrWhiteSpace(fileUrl))
                                            {
                                                linkAttachment.Text = "📎 Open Attachment";
                                                linkAttachment.Links.Clear();
                                                linkAttachment.Links.Add(0, linkAttachment.Text.Length, fileUrl);
                                                linkAttachment.Visible = true;
                                            }
                                            else
                                            {
                                                linkAttachment.Visible = false;
                                            }
                                        }));
                                    }
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            if (!IsDisposed && IsHandleCreated)
                            {
                                BeginInvoke((MethodInvoker)(() =>
                                    MessageBox.Show("❌ Server error: " + ex.Message)
                                ));
                            }
                        }
                        finally
                        {
                            client?.Close();
                        }
                    }
                }, cts.Token);
            }
            catch (SocketException ex)
            {
                MessageBox.Show("❌ Server failed to start: " + ex.Message);
            }
        }

        

             
        private void txtName_TextChanged(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {
        }

        private async void btnReply_Click(object sender, EventArgs e)
        {
            string email = txtEmail.Text.Trim();
            string reply = txtReply.Text.Trim();

            if (string.IsNullOrWhiteSpace(email))
            {
                MessageBox.Show("❌ No email found for this message. Cannot send reply.");
                return;
            }
            if (string.IsNullOrWhiteSpace(reply))
            {
                MessageBox.Show("Reply cannot be empty.");
                return;
            }
            if (!string.IsNullOrEmpty(replyFilePath) && !File.Exists(replyFilePath))
            {
                MessageBox.Show("Selected file was not found. Please re-upload.");
                replyFilePath = "";
                lblFileName.Text = "";
                return;
            }

            btnReply.Enabled = false;

            try
            {
                using (HttpClient client = new HttpClient())
                {
                    var content = new MultipartFormDataContent();
                    content.Add(new StringContent(email), "email");
                    content.Add(new StringContent(reply), "reply");

                    if (!string.IsNullOrEmpty(replyFilePath))
                    {
                        var fileContent = new ByteArrayContent(File.ReadAllBytes(replyFilePath));
                        fileContent.Headers.ContentType =
                            new System.Net.Http.Headers.MediaTypeHeaderValue("application/octet-stream");
                        content.Add(fileContent, "file", Path.GetFileName(replyFilePath));
                    }

                    var response = await client.PostAsync("http://127.0.0.1:5000/api/reply", content);
                    if (response.IsSuccessStatusCode)
                    {
                        MessageBox.Show("✅ Reply sent successfully.");
                        replyFilePath = "";
                        lblFileName.Text = "";
                        this.Close();
                    }
                    else
                    {
                        string err = await response.Content.ReadAsStringAsync();
                        MessageBox.Show("❌ Server rejected reply: " + err);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("❌ Failed: " + ex.Message);
            }
            finally
            {
                btnReply.Enabled = true;
            }


        }
        

        private void linkAttachment_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (e.Link.LinkData != null)
            {
                string fileUrl = e.Link.LinkData.ToString();
                try
                {
                    // ✅ Convert relative path to HTTP URL
                    if (!fileUrl.StartsWith("http", StringComparison.OrdinalIgnoreCase))
                    {
                        fileUrl = fileUrl.Replace("\\", "/");
                        fileUrl = "http://127.0.0.1:5000/" + fileUrl.TrimStart('/');
                    }

                    System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo
                    {
                        FileName = fileUrl,
                        UseShellExecute = true
                    });
                }
                catch (Exception ex)
                {
                    MessageBox.Show("❌ Failed to open attachment: " + ex.Message);
                }
            }
        }

        private void btnUpload_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog dialog = new OpenFileDialog())
            {
                dialog.Filter = "All Supported|*.jpg;*.jpeg;*.png;*.pdf;*.webm;*.mp3;*.wav;*.mp4|Audio|*.webm;*.mp3;*.wav|Video|*.mp4|All Files|*.*";

                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    replyFilePath = dialog.FileName;
                    lblFileName.Text = Path.GetFileName(replyFilePath);

                    string ext = Path.GetExtension(replyFilePath).ToLower();
                    if (ext == ".mp3" || ext == ".wav" || ext == ".webm" || ext == ".mp4")
                    {
                        try
                        {
                            System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo
                            {
                                FileName = replyFilePath,
                                UseShellExecute = true
                            });
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("❌ Failed to open media file: " + ex.Message);
                        }
                    }
                }
            }
        }

        private void btnRecord_Click(object sender, EventArgs e)
        {
            if (waveIn == null)
            {
                waveIn = new WaveInEvent();
                waveIn.WaveFormat = new WaveFormat(44100, 1);
                string tempPath = Path.Combine(Path.GetTempPath(), $"record_{DateTime.Now.Ticks}.wav");
                writer = new WaveFileWriter(tempPath, waveIn.WaveFormat);
                replyFilePath = tempPath;
                lblFileName.Text = Path.GetFileName(tempPath);

                waveIn.DataAvailable += (s, a) => writer.Write(a.Buffer, 0, a.BytesRecorded);

                waveIn.RecordingStopped += (s, a) =>
                {
                    writer?.Dispose();
                    writer = null;
                    waveIn.Dispose();
                    waveIn = null;

                    try
                    {
                        System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo
                        {
                            FileName = replyFilePath,
                            UseShellExecute = true
                        });
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("❌ Failed to open media player: " + ex.Message);
                    }
                };

                waveIn.StartRecording();
                btnRecord.Text = "Stop Recording";
            }
            else
            {
                waveIn.StopRecording();
                btnRecord.Text = "Record Audio";
            }
        }

        private void Reply_Shown(object sender, EventArgs e)
        {
            if (server == null)
                StartServer();
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            cts.Cancel();
            server?.Stop();
            waveIn?.Dispose();
            writer?.Dispose();
            base.OnFormClosing(e);
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}



    


