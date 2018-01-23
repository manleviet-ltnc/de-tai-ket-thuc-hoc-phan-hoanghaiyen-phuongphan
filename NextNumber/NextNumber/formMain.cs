using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using System.Xml;
using System.Data;
namespace NextNumber
{
    public partial class formMain : Form
    {
        private BackgroundWorker backgroundWorker;
        private Timer timer;
  
        private const int DEFAULT_TIME = 60;// thời gian mặc định khi chơi một level
       
        private int _timeCounter = 0; // dùng để đếm thời lúc chơi
        private bool _won;// kiểm tra thắng thua
        static int _demLevel=1;// đếm số level hiện tại
     
        private List<Button> _listBlock;// danh sách các block
        private IList<int> _listResult;// Dùng để lưu các giá trị sau mỗi lần click vào block
        private int newScore = 0;// giá trị này dùng để tính điểm của một lần chơi
        private int  minScore = 200; // gán giá trị ban đầu cho điểm min
        private string playerName;// tên người chơi
        private List<Level> _listLevel;// lưu tất cả các level 

         XmlDocument doc = new XmlDocument();// new một tài liệu
        XmlElement root; // gốc của tài liệu đó
        string filename = System.IO.Directory.GetCurrentDirectory() + @"\DuLieu.xml";// đường dẫn của một tài liệu
        public formMain()
        {
            
           
            InitializeComponent();
            this.Text = "Level1";
            this.btnResume.Enabled = false;
            this.btnPause.Enabled = false;
            _listLevel = new List<Level>();
            progressBar.Maximum = 60; 
            AddLevel();
          
            _listResult = new List<int>();
            _listBlock = new List<Button>();
           
            backgroundWorker = new BackgroundWorker();

            // tạo một Dialog để nhập tên người chơi
            using (EnterName frmName = new EnterName())
            {
                if (frmName.ShowDialog() == System.Windows.Forms.DialogResult.OK)// nếu click vào Ok thì thực hiện
                {
                    if (frmName.playerN == "")
                    {
                        playerName = "HoangYen";
                    }
                    else
                    {
                        playerName = frmName.playerN;
                    }

                }

            }
        }

      // thêm tất cả các level vào _listLevel
        public void AddLevel()
        {
            //thêm level 1
            _listLevel.Add(new Level(4, 4));
            //thêm level 2
            _listLevel.Add(new Level(5, 6));
            //thêm level 3
            _listLevel.Add(new Level(4, 5));
            //thêm level 4
            _listLevel.Add(new Level(5, 6));
            //thêm level 5
            _listLevel.Add(new Level(4, 5));
            //thêm level 6
            _listLevel.Add(new Level(5, 6));
            //thêm level 7
            _listLevel.Add(new Level(4, 5));
            //thêm level 8
            _listLevel.Add(new Level(5, 6));
            //thêm level 9
            _listLevel.Add(new Level(5, 6));
            //thêm level 10
            _listLevel.Add(new Level(6, 7));
            

           
        }
        private void Initial()
        {
            // thực hiện việc thực thi đa luồng
            backgroundWorker.WorkerReportsProgress = true;
            backgroundWorker.WorkerSupportsCancellation = true;

            backgroundWorker.DoWork -= backgroundWorker_DoWork;
            backgroundWorker.DoWork += backgroundWorker_DoWork;
            backgroundWorker.ProgressChanged += backgroundWorker_ProgressChanged;
            backgroundWorker.RunWorkerCompleted -= backgroundWorker_RunWorkerCompleted;
            backgroundWorker.RunWorkerCompleted += backgroundWorker_RunWorkerCompleted;

            timer = new Timer();// khởi tạo timer

            timer.Interval = 1000;
            timer.Tick += timer_Tick;
            _timeCounter = 0;
           
            GameInit();
        }
        
        //Sự kiện timer
        private void timer_Tick(object sender, EventArgs e)
        {

            if (progressBar.Value != 60)
            {
                progressBar.Value++;
            }
            lb_time.Text = Math.Abs(_timeCounter - DEFAULT_TIME).ToString("00");// hàm lấy giá trị tuyệt đối
            _timeCounter++;
        }
        // hàm này được thực thi khi backgroundWorker.RunWorkerAsync() được gọi
        private void backgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
           
           GameRun();
           
        }

        private void backgroundWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            progressBar.Value = e.ProgressPercentage;
        }

        private void backgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            GameEnd();
           
        }

        private void GameInit()
        {

           
            // khởi tạo giao diện các button
            Random rnd = new Random();
           
            var w = (this.panel1.Width) / _listLevel[_demLevel-1].WidthCount; // Tính độ rộng của một block
            var h = (this.panel1.Height) / _listLevel[_demLevel - 1].HeightCount;// Tính độ cao của một block

            for (int i = 0; i < _listLevel[_demLevel - 1].HeightCount; i++)
            {
                for (int j = 0; j < _listLevel[_demLevel - 1].WidthCount; j++)
                {
                   
                        _listBlock.Add(new Button()
                        {
                            Name = i.ToString() + j,
                            TabStop = false,
                            Text = RandomNumber().ToString(),
                            Size = new System.Drawing.Size()
                            {
                                Height = h,
                                Width = w
                            },
                            Location = new System.Drawing.Point
                            {
                                X = j * w - 1,
                                Y = i * h - 1
                            }
                        });
                   
                }
            }

            foreach(var block in _listBlock)
            {
                this.panel1.Controls.Add(block);
            }
        }

        private void block_Click(object sender, EventArgs e)
        {// xữ lý khi click 1 button
            var btn = sender as Button;
            btn.Enabled = false;      
            _listResult.Add(int.Parse(btn.Text));
            btn.BackColor = Color.FromArgb(255, 232, 232);
           
         
        }

        private void GameRun()
        {
            do
            {
                if (_timeCounter == DEFAULT_TIME || GameChecker() || _won )
                {// các điều kiện để kết thúc game 
                    timer.Stop();
                    backgroundWorker.CancelAsync();
                    break;

                }

            }
            while (true);
        }

       
        private void SaveScore(int score, string name)// hàm này thực hiện chức năng thêm mới một giá trị vào file
        {
            XmlElement ds = doc.CreateElement("dsdiem");
            XmlElement ten = doc.CreateElement("Ten");
            ten.InnerText = name;
            ds.AppendChild(ten);
            XmlElement diem = doc.CreateElement("Diem");
            diem.InnerText = score.ToString();
            ds.AppendChild(diem);
            root.AppendChild(ds);
            doc.Save(filename);// lưu file sao khi thêm
        }
       
        // hàm này xữ lý khi kết thúc game
        private void GameEnd()
        {
            this.btnResume.Enabled = false;
            this.btnPause.Enabled = false;
            if (!_won)
            {

                MessageBox.Show("Thua");
                panel1.Enabled = false;
                this.btnPlay.Enabled = true;
                this.btnPlay.Text = "RePlay";

            }
            else
            {
                newScore += _timeCounter; // tính số điểm của một lần chơi theo thời gian chơi
                int _demTop = 0;// biết này dùng để xác định số lần chơi có số điểm cao nhất
                doc.Load(filename); // để load file
                root = doc.DocumentElement;
                // để đếm số phần tử trong file
                XmlNodeList elemList = root.GetElementsByTagName("Ten");
                 _demTop = elemList.Count; // số phần tử trong file
                 XmlNodeList nodes_diem = root.SelectNodes("/Score/dsdiem/Diem");// Lấy tất cả các giá trị có cột là diem
             
                if (_demTop < 10) //nếu trong file chưa đủ 10 giá trị thì thêm vào
                {
                    SaveScore(newScore, playerName); 
                }
                else if (_demTop == 10)
                {
                    
                    minScore = int.Parse(nodes_diem[0].InnerText);// giá trị điểm đầu tiên trong file
                    for (int i = 1; i < nodes_diem.Count; i++)
                    { // vòng lặp này dùng để tìm giá trị min của file
                        if (minScore > int.Parse(nodes_diem[i].InnerText))
                        {
                            minScore = int.Parse(nodes_diem[i].InnerText);
                           
                        }
                    }
                    if (newScore>minScore)// khi giá trị mới lớn hơn min thì xóa giá trị min và thêm giá trị mới vào nếu không thì ko làm gì
                    {
                        foreach (XmlNode xNode in doc.SelectNodes("Score/dsdiem"))
                        {
                            if (xNode.SelectSingleNode("Diem").InnerText == minScore.ToString())
                            {
                                xNode.ParentNode.RemoveChild(xNode);
                                break;
                            }
                        }
                        SaveScore(newScore, playerName);
                       
                    }
                    else { }
                }
              
               
                MessageBox.Show("Your score: "+newScore );
                this.btnPlay.Enabled = true;
                this.btnPlay.Text = "Level" + (_demLevel + 1).ToString();
                this.Text = "Level" + (_demLevel + 1).ToString();
                _demLevel += 1;

            }
          
        }
      
        private bool GameChecker()
        {
         // Hàm này trả về true khi click đúng số block cho phép theo mỗi level cụ thể
        // Ví dụ level 1 nếu click đủ 12 block thì trả về true
            _won = false;

            if (_listResult.Count < 1)
            {
                return false;
            }
       
            if (_demLevel == 1 || _demLevel == 2)// kiểm tra level 1, 2 ở đây
            {
                    for (int i = 0; i < _listResult.Count; i++)
                    {
                        if (_listResult[i] != i + 1) // kiểm tra số liên tiếp
                        {
                            MessageBox.Show("Rẩt tiếc bạn đã thua, lẽ ra bạn phải chọn số   " + (i + 1));
                            return true;
                        
                        }
                    }
                    if (_listResult.Count == _listBlock.Count)// nếu click đủ số block thì thực hiện
                    {
                        for (int j = 0; j < _listResult.Count; j++)
                        {
                            if (_listResult[j] == j + 1)
                            {
                                _won = true;
                            }
                            else
                            {
                                _won = false;
                                break;// nếu click sai thì thoát khỏi game mà không cần kiểm tra tiếp

                            }
                        }
                        return true;
                    }
                        
            }
            else if(_demLevel==3||_demLevel==4)
            {
                for (int i = 0; i < _listResult.Count; i++)
                {
                    if (_listResult[i] != 2*i + 1)
                    {
                        MessageBox.Show("Rẩt tiếc bạn đã thua, lẽ ra bạn phải chọn số   " + (2*i + 1));
                        return true;

                    }
                }
                // Math.Round với MidpointRounding.AwayFromZero dùng để lấy giá trị nguyên cận trên. ví dụ: 4.5= 5
                if (_listResult.Count == Math.Round((double)_listBlock.Count/2, MidpointRounding.AwayFromZero))
               {
                 
                   for (int j = 0; j < _listResult.Count; j++)
                   {
                       if (_listResult[j] == 2*j + 1)
                       {
                           _won = true;
                       }
                       else
                       {
                           _won = false;
                           break;// nếu click sai thì thoát khỏi game mà không cần kiểm tra tiếp

                       }
                   }
                   return true;
                }
             }
             else if (_demLevel == 5 || _demLevel == 6)
             {
                 for (int i = 0; i < _listResult.Count; i++)
                 {
                     if (_listResult[i] != 2*i + 2)
                     {
                         MessageBox.Show("Rẩt tiếc bạn đã thua, lẽ ra bạn phải chọn số   " + (2 * i + 2));
                         return true;

                     }
                 }
                 if (_listResult.Count == _listBlock.Count / 2) 
                 {
                     for (int j = 0; j < _listResult.Count; j++)
                     {
                         if (_listResult[j] == 2 * j + 2)// kiểm tra một số là số chắn
                         {
                             _won = true;
                         }
                         else
                         {
                             _won = false;
                             break;// nếu click sai thì thoát khỏi game mà không cần kiểm tra tiếp

                         }
                     }
                     return true;
                 }
           }
           else if (_demLevel == 7 || _demLevel == 8)
           {

               for (int i = 0; i < _listResult.Count; i++)
               {
                   if (!laSoNguyenTo(_listResult[i]))// kiểm tra một số có phải là số nguyên tố không
                   {
                       MessageBox.Show("Rẩt tiếc đây không phải lá số nguyên tố");
                       
                       return true;

                   }
                   else
                   {
                       if (_demLevel == 7)
                       {
                           if (_listResult.Count == 8)// ở đây fix cứng level 7 có 20 số từ 1->20 nên có tối đa 8 số nguyên tố  
                           {
                               _won = true;
                               return true;
                           }
                       }
                       else
                       {
                           if (_listResult.Count == 10)// ở đây fix cứng level 8 có 30 số từ 1->30 nên có tối đa 10 số nguyên tố  
                           {
                               _won = true;
                               return true;
                           }
                       }
                   }
               }
              
           }
            else if (_demLevel == 9 || _demLevel == 10)//  level 9 hoặc 10 thì kiểm tra ở đây
            {// bỏ qua giá trị 0 đầu tiên của dãy số fibonacci
                if(_listResult[0]==1){ // kiểm tra giá trị ban đầu có bằng 1 không

                }
                else{
                     MessageBox.Show("Rẩt tiếc bạn đã thua, lẽ ra bạn phải chọn số   " + 1);
                    return true;
                }
                if (_listResult.Count < 2)
                {
                    _listResult.Add(1);// thêm giá trị 1 vào dãy vì không thể click số 1 hai lần 
                }
                else
                {
                    for (int i = 2; i < _listResult.Count; i++)
                    {

                        if (_listResult[i] != _listResult[i - 1] + _listResult[i - 2])// công thức thành lập dãy số
                        {
                            MessageBox.Show("Rẩt tiếc bạn đã thua, lẽ ra bạn phải chọn số   " + (_listResult[i - 1] + _listResult[i - 2]));
                            return true;

                        }
                        else
                        {
                            if (_demLevel == 9)// ở đây fix cứng level 9 có 30 số từ 1->30 nên số  Fibonacci lớn nhất là 21  
                            {
                                if (_listResult.Count == 8)
                                {
                                    _won = true;
                                    return true;
                                }
                            }
                            else
                            {
                                if (_listResult.Count == 9)//fix cứng level 10 có 42 số từ 1->42 nên số  Fibonacci lớn nhất là 34 
                                {
                                    _won = true;
                                    return true;
                                }
                            }

                        }
                    } 
                }
     
            }
    
             return false;           
        }
        
        // game sẽ run khi click vào button này
        private void btnStart_Click(object sender, EventArgs e)
        {
           
           // dùng để thông báo luật chơi của từng level
            switch (_demLevel)
            {
                case 1:
                    MessageBox.Show("Chọn giá trị từ thấp đến cao và bắt đầu từ 1 ", "Rules of the game");
                    break;
                case 2:
                    MessageBox.Show("Chọn giá trị từ thấp đến cao và bắt đầu từ 2 ", "Rules of the game");
                    break;
                case 3: 
                     MessageBox.Show("Chọn giá trị tăng dần của số lẻ và bắt đầu từ 1 ", "Rules of the game");
                    break;
                case 4:
                    MessageBox.Show("Chọn giá trị tăng dần của số lẻ và bắt đầu từ 1(2) ", "Rules of the game");
                    break;
                case 5:
                    MessageBox.Show("Chọn giá trị tăng dần của số chẵn và bắt đầu từ 2(1) ", "Rules of the game");
                    break;
                case 6:
                    MessageBox.Show("Chọn giá trị tăng dần của số chẵn và bắt đầu từ 2(2) ", "Rules of the game");
                    break;
                case 7:
                    MessageBox.Show("Tìm tất cả các số nguyên tố(1).\n Chú ý: Số nguyên tố là số tự nhiên chỉ có hai ước số dương phân biệt là 1 và chính nó. ", "Rules of the game");
                    break;
                case 8:
                    MessageBox.Show("Tìm tất cả các  số nguyên tố(2).\n Chú ý: Số nguyên tố là số tự nhiên chỉ có hai ước số dương phân biệt là 1 và chính nó. ", "Rules of the game");
                    break;
                case 9:
                    MessageBox.Show("Tìm dãy Fibonacci và bắt đầu là số 1 (1).\n Chú ý:Dãy Fibonacci là một dãy số trong đó một con số được xác định bằng cách cộng hai con số đứng trước nó. Bắt đầu với 0 và 1, dãy số tiếp tục 0, 1, 1, 2, 3,...", "Rules of the game");
                    break;
                case 10:
                    MessageBox.Show("Tìm dãy Fibonacci và bắt đầu là số 1 (1).\n Chú ý:Dãy Fibonacci là một dãy số trong đó một con số được xác định bằng cách cộng hai con số đứng trước nó. Bắt đầu với 0 và 1, dãy số tiếp tục 0, 1, 1, 2, 3,...", "Rules of the game");
                    break;
                default:
                    MessageBox.Show("Xin chúc mừng, bạn đã hoàn thành trò chơi.");
                    break;
               
            }
            
         // xóa dữ liệu cũ
            _listBlock.Clear();
            _listResult.Clear();
            this.panel1.Controls.Clear();
            panel1.Enabled = true;
            Initial();
            progressBar.Value = 0;
            if (!backgroundWorker.IsBusy)
            {
                backgroundWorker.RunWorkerAsync();
            }
            timer.Start();

            foreach (var block in _listBlock)
            {
                block.Click += block_Click;
            }
            this.btnPlay.Enabled = false;
            this.btnPlay.Text = "Replay";
            this.btnResume.Enabled = true;
            this.btnPause.Enabled = true;
        }
        // hàm này thực hiện kiểm tra một số có phải là số nguyên tố không
        bool laSoNguyenTo(int n)
        {
            // Neu n < 2 thi khong phai la SNT
            if (n < 2)
            {
                return false;
            }

            for (int i = 2; i < (n - 1); i++)
            {
                if (n % i == 0)
                {
                    return false;
                }
            }

            return true;
        }
        // Hàm này có chức năng tìm số nguyên ngẫu nhiên cho mỗi button không bị lặp lại
        private int RandomNumber()
        {
            var rdn = new Random();
            do
            {
                var value = rdn.Next(1, _listLevel[_demLevel - 1].WidthCount * _listLevel[_demLevel - 1].HeightCount + 1);
                var res = _listBlock.Count(x => x.Text.Equals(value.ToString()));
                if(res == 0)
                {
                    return value;
                }
            } while (true);
        }


        // hàm này thực thi khi click vào button Resume và thực hiện chức năng kích hoạt lại trạng thái dừng của game
        private void btnResume_Click(object sender, EventArgs e)
        {
            timer.Enabled = true;
            panel1.Enabled = true;
        }

     
      // hàm này thực thi khi click vào button Pause và thực hiện chức năng dừng trạng thái hoạt động của game
        private void btnPause_Click(object sender, EventArgs e)
        {
            timer.Stop();
            panel1.Enabled = false;
        }

        private void btnScore_Click(object sender, EventArgs e)
        {
            HighestScore hs = new HighestScore();
            hs.Show();
         
        }
    }
}
