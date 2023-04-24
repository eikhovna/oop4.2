using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace oop4._2
{
    public partial class Form1 : Form
    {
        Model model;//объявление модели
        public Form1()
        {
            InitializeComponent();
            model = new Model();//инициализация модели
            model.observers += new System.EventHandler(this.UpdateFromModel);//подписка на об-новления модели

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            UpdateFromModel(this, e);
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)//метод смены значения numeric1
        {
            model.SetA(Decimal.ToInt32(numericUpDown1.Value));
        }//конец метода

        private void numericUpDown2_ValueChanged(object sender, EventArgs e)//метод смены значения numeric2
        {
            model.SetB(Decimal.ToInt32(numericUpDown2.Value));
        }//конец метода

        private void numericUpDown3_ValueChanged(object sender, EventArgs e)//метод смены значения numeric3
        {
            model.SetC(Decimal.ToInt32(numericUpDown3.Value));
        }//конец метода

        private void trackBar1_Scroll(object sender, EventArgs e)//метод смены значения trackBar1
        {
            model.SetA(trackBar1.Value);
        }//конец метода

        private void Form1_Scroll(object sender, ScrollEventArgs e)//метод смены значения trackBar2
        {
            model.SetB(trackBar2.Value);
        }//конец метода

        private void trackBar3_Scroll(object sender, EventArgs e)//метод смены значения trackBar3
        {
            model.SetC(trackBar3.Value);
        }//конец метода
        private void UpdateFromModel(object sender, EventArgs e)//метод обновления модели
        {
            textBox1.Text = model.GetA().ToString();
            textBox2.Text = model.GetB().ToString();
            textBox3.Text = model.GetC().ToString();
            numericUpDown1.Value = model.GetA();
            numericUpDown2.Value = model.GetB();
            numericUpDown3.Value = model.GetC();
            trackBar1.Value = model.GetA();
            trackBar2.Value = model.GetB();
            trackBar3.Value = model.GetC();
        }//конец метода

        private void textBox1_KeyDown(object sender, KeyEventArgs e)//метод смены значения textBox1
        {
            if (e.KeyCode == Keys.Enter)
                model.SetA(Int32.Parse(textBox1.Text));
        }//конец метода

        private void textBox2_KeyDown(object sender, KeyEventArgs e)//метод смены значения textBox2
        {
            if (e.KeyCode == Keys.Enter)
                model.SetB(Int32.Parse(textBox2.Text));
        }//конец метода

        private void textBox3_KeyDown(object sender, KeyEventArgs e)//метод смены значения textBox3
        {
            if (e.KeyCode == Keys.Enter)
                model.SetC(Int32.Parse(textBox3.Text));
        }//конец метода

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)//метод сохранения значений А и С при закрытии формы
        {
            Properties.Settings.Default.value_A = trackBar1.Value;
            Properties.Settings.Default.value_C = trackBar3.Value;
            Properties.Settings.Default.Save();
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }
    }
    class Model//модель
    {
        private int A, B, C;//объявление элементов модели
        public System.EventHandler observers;
        public Model()
        {
            A = Properties.Settings.Default.value_A;
            B = 0;
            C = Properties.Settings.Default.value_C;

        }
        public int GetA()//геттер(возвращает данные) для А
        {
            return A;
        }//конец метода
        public int GetB()//геттер для B
        {
            return B;
        }//конец метода
        public int GetC()//геттер для С
        {
            return C;
        }//конец метода
        public void SetA(int a)//сеттер(устанавливает данные) для А
        {
            if (a > C)
            {
                observers.Invoke(this, null);
                return;
            }
            if (a > B && a <= C)
                B = a;
            A = a;
            observers.Invoke(this, null);
        }//конец метода
        public void SetB(int b)//сеттер для В
        {
            if (b > C || b < A)
            {
                observers.Invoke(this, null);
                return;
            }
            B = b;
            observers.Invoke(this, null);
        }//конец метода
        public void SetC(int c)//сеттер для С
        {
            if (c < A)
            {
                observers.Invoke(this, null);
                return;
            }
            if (c < B)
                B = c;
            C = c;
            observers.Invoke(this, null);
        }//конец метода
    }

}
