using System.Security.Cryptography;

namespace SameDelete
{
    public partial class SameDelete : Form
    {
        string FromPath = @"E:\123";
        string ToPath = @"E:\456";
        public SameDelete()
        {
            InitializeComponent();
        }

        private void Start_Click(object sender, EventArgs e)
        {
            if (From.Text!="")
            {
                FromPath = From.Text;
            }
            if (To.Text != "")
            {
                ToPath = To.Text;
            }
            //���Ŀ¼�������򴴽������ⱨ��
            if (!Directory.Exists(FromPath))
            {
                Directory.CreateDirectory(FromPath);
            }
            if (!Directory.Exists(ToPath))
            {
                Directory.CreateDirectory(ToPath);
            }

            //ʵ��MD5����ļ��Ƿ���ͬ
            var files = Directory.GetFiles(FromPath,"*.*", SearchOption.AllDirectories);
            List<string> md5List = new List<string>();
            foreach (var file in files)
            {
                var md5 = MD5.Create();
                var stream = File.OpenRead(file);
                var hash = md5.ComputeHash(stream);
                var md5String = BitConverter.ToString(hash).Replace("-", "").ToLower();
                if (md5List.Contains(md5String))
                {
                    richTextBox1.Text += md5String;
                    richTextBox1.Text += Environment.NewLine;
                    richTextBox1.Text += file;
                    richTextBox1.Text += Environment.NewLine;
                }
                else
                {
                    md5List.Add(md5String);
                }
            }
            //����ToPath��ɾ����ͬ�ļ�
            var toFiles = Directory.GetFiles(ToPath, "*.*", SearchOption.AllDirectories);
            foreach (var file in toFiles)
            {
                var md5 = MD5.Create();
                var stream = File.OpenRead(file);
                var hash = md5.ComputeHash(stream);
                //�ͷ��ļ�
                stream.Close();
                var md5String = BitConverter.ToString(hash).Replace("-", "").ToLower();
                if (md5List.Contains(md5String))
                {
                    File.Delete(file);
                    richTextBox2.Text += file;
                    richTextBox2.Text += Environment.NewLine;
                }
            }

        }
    }
}