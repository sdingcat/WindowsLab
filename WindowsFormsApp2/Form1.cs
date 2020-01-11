using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Collections;

namespace WindowsFormsApp2
{
    // 这个队列存放生成的token序列，numOrOpe表示数字或操作符，true为数字，false为操作符。
    struct calculElems
    {
        public bool numOrOpe;
        public double numuber;
        public char operater;
    }

    public partial class Form1 : Form
    {
        public int i = 0;

        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (((Button)sender).Text == "Re")
            {
                textBox1.Text = "";
            }
            else if (((Button)sender).Text == "Del")
            {
                if (textBox1.Text.Length != 0)
                {
                    textBox1.Text = textBox1.Text.Substring(0, textBox1.Text.Length - 1);
                }
            }
            else if (((Button)sender).Text == "=")
            {
                // 使用队列存放每一个获取到的token，使用token保存当前读取到的字符，使用符号栈检测括号
                // "124+2.45-(4.78-3)-1278"
                string inputString = textBox1.Text;
                Queue repreQueue = new Queue();
                Stack opeStack = new Stack();
                string token = "";
                // 循环读取输入字符序列
                for (int i = 0; i < inputString.Length;)
                {
                    // 先检测是否为数字，包括检测小数点，若小数点超过一个则报错。
                    if (inputString[i] <= '9' && inputString[i] >= '0')
                    {
                        token += inputString[i];
                        i++;
                        bool hasPoint = false;
                        while (i < inputString.Length && ((inputString[i] <= '9' && inputString[i] >= '0') || inputString[i] == '.'))
                        {
                            // 若是小数点，开始检测
                            if (inputString[i] == '.')
                            {
                                // 小数点超过一个则报错。
                                if (hasPoint == true)
                                {
                                    Console.WriteLine("error");
                                    textBox2.Text = "Error";
                                    return;
                                }
                                // 否则将小数点添加进token
                                else
                                {
                                    hasPoint = true;
                                }
                                token += inputString[i];
                                i++;
                                if (i >= inputString.Length)
                                {
                                    Console.WriteLine("Error");
                                    textBox2.Text = "Error";
                                    return;
                                }
                            }
                            //如果遇到数字字符，就直接读入
                            else
                            {
                                token += inputString[i];
                                i++;
                                if (i >= inputString.Length)
                                {
                                    break;
                                }
                            }
                        }
                        Console.WriteLine(token);
                        // 数字直接送入队列
                        calculElems newElem;
                        newElem.numOrOpe = true;
                        newElem.numuber = Double.Parse(token);
                        newElem.operater = ' ';
                        repreQueue.Enqueue(newElem);
                        // 清空token，准备下次读入
                        token = "";
                        hasPoint = false;
                    }
                    // 以下检测运算符号，若符合入符号栈
                    else if (inputString[i] == '+')
                    {
                        Console.WriteLine('+');
                        calculElems newElem;
                        newElem.numOrOpe = false;
                        newElem.numuber = 0;
                        newElem.operater = '+';
                        // 若是空栈或者栈顶为左括号，则直接入栈
                        if (opeStack.Count == 0 || ((calculElems)opeStack.Peek()).operater == '(')
                        {
                            opeStack.Push(newElem);
                        }
                        // 若栈顶检测到同运算优先级的符号，就弹栈直到栈空或遇到左括号，再入栈
                        else if (((calculElems)opeStack.Peek()).operater == '+' || ((calculElems)opeStack.Peek()).operater == '-')
                        {
                            while (0 != opeStack.Count && '(' != ((calculElems)opeStack.Peek()).operater)
                            {
                                repreQueue.Enqueue(opeStack.Pop());
                            }
                            opeStack.Push(newElem);
                        }
                        // 若栈顶检测到高运算优先级的符号，就弹栈直到栈空或遇到左括号，再入栈
                        else if (((calculElems)opeStack.Peek()).operater == '+' || ((calculElems)opeStack.Peek()).operater == '-')
                        {
                            while (0 != opeStack.Count && '(' != ((calculElems)opeStack.Peek()).operater)
                            {
                                repreQueue.Enqueue(opeStack.Pop());
                            }
                            opeStack.Push(newElem);
                        }
                        i++;
                    }
                    else if (inputString[i] == '-')
                    {
                        Console.WriteLine('-');
                        calculElems newElem;
                        newElem.numOrOpe = false;
                        newElem.numuber = 0;
                        newElem.operater = '-';
                        // 若是空栈或者栈顶为左括号，则直接入栈
                        if (opeStack.Count == 0 || ((calculElems)opeStack.Peek()).operater == '(')
                        {
                            opeStack.Push(newElem);
                        }
                        // 若栈顶检测到同运算优先级的符号，就弹栈直到栈空或遇到左括号，再入栈
                        else if (((calculElems)opeStack.Peek()).operater == '+' || ((calculElems)opeStack.Peek()).operater == '-')
                        {
                            while (0 != opeStack.Count && '(' != ((calculElems)opeStack.Peek()).operater)
                            {
                                repreQueue.Enqueue(opeStack.Pop());
                            }
                            opeStack.Push(newElem);
                        }
                        // 若栈顶检测到高运算优先级的符号，就弹栈直到栈空或遇到左括号，再入栈
                        else if (((calculElems)opeStack.Peek()).operater == '+' || ((calculElems)opeStack.Peek()).operater == '-')
                        {
                            while (0 != opeStack.Count && '(' != ((calculElems)opeStack.Peek()).operater)
                            {
                                repreQueue.Enqueue(opeStack.Pop());
                            }
                            opeStack.Push(newElem);
                        }
                        i++;
                    }
                    else if (inputString[i] == '*')
                    {
                        Console.WriteLine('*');
                        calculElems newElem;
                        newElem.numOrOpe = false;
                        newElem.numuber = 0;
                        newElem.operater = '*';
                        // 若是空栈或者栈顶为左括号，则直接入栈
                        if (opeStack.Count == 0 || ((calculElems)opeStack.Peek()).operater == '(')
                        {
                            opeStack.Push(newElem);
                        }
                        // 若栈顶检测到同运算优先级的符号，就弹栈直到栈空或遇到左括号或遇到低优先级符号，再入栈
                        else if (((calculElems)opeStack.Peek()).operater == '*' || ((calculElems)opeStack.Peek()).operater == '/')
                        {
                            while ((0 != opeStack.Count && '(' != ((calculElems)opeStack.Peek()).operater)
                                || '+' == ((calculElems)opeStack.Peek()).operater
                                || '-' == ((calculElems)opeStack.Peek()).operater)
                            {
                                repreQueue.Enqueue(opeStack.Pop());
                            }
                            opeStack.Push(newElem);
                        }
                        // 若栈顶检测到低运算优先级的符号，就入栈
                        else if (((calculElems)opeStack.Peek()).operater == '+'
                            || ((calculElems)opeStack.Peek()).operater == '-')
                        {
                            opeStack.Push(newElem);
                        }
                        i++;
                    }
                    else if (inputString[i] == '/')
                    {
                        Console.WriteLine('/');
                        calculElems newElem;
                        newElem.numOrOpe = false;
                        newElem.numuber = 0;
                        newElem.operater = '/';
                        // 若是空栈或者栈顶为左括号，则直接入栈
                        if (opeStack.Count == 0 || ((calculElems)opeStack.Peek()).operater == '(')
                        {
                            opeStack.Push(newElem);
                        }
                        // 若栈顶检测到同运算优先级的符号，就弹栈直到栈空或遇到左括号或遇到低优先级符号，再入栈
                        else if (((calculElems)opeStack.Peek()).operater == '*' || ((calculElems)opeStack.Peek()).operater == '/')
                        {
                            while ((0 != opeStack.Count && '(' != ((calculElems)opeStack.Peek()).operater)
                                || '+' == ((calculElems)opeStack.Peek()).operater
                                || '-' == ((calculElems)opeStack.Peek()).operater)
                            {
                                repreQueue.Enqueue(opeStack.Pop());
                            }
                            opeStack.Push(newElem);
                        }
                        // 若栈顶检测到低运算优先级的符号，就入栈
                        else if (((calculElems)opeStack.Peek()).operater == '+'
                            || ((calculElems)opeStack.Peek()).operater == '-')
                        {
                            opeStack.Push(newElem);
                        }
                        i++;
                    }
                    // 以下检测括号，左括号直接入栈，右括号则弹栈直到遇到左括号，若没有则报错。
                    else if (inputString[i] == '(')
                    {
                        Console.WriteLine('(');
                        calculElems newElem;
                        newElem.numOrOpe = false;
                        newElem.numuber = 0;
                        newElem.operater = '(';
                        opeStack.Push(newElem);
                        i++;
                    }
                    else if (inputString[i] == ')')
                    {
                        Console.WriteLine(')');
                        calculElems newElem;
                        newElem.numOrOpe = false;
                        newElem.numuber = 0;
                        newElem.operater = ')';
                        while (0 != opeStack.Count && '(' != ((calculElems)opeStack.Peek()).operater)
                        {
                            repreQueue.Enqueue(opeStack.Pop());
                            if (0 == opeStack.Count)
                            {
                                Console.WriteLine("Error");
                                textBox2.Text = "Error";
                                return;
                            }
                        }
                        opeStack.Pop();
                        i++;
                    }
                    // 如果遇到其他字符，就直接跳过
                    else
                    {
                        i++;
                    }
                }
                // 读取完成后检测括号栈是否为空
                while (0 != opeStack.Count)
                {
                    if (((calculElems)opeStack.Peek()).operater == '(')
                    {
                        Console.WriteLine("Error:缺少右括号");
                        textBox2.Text = "Error";
                        return;
                    }
                    else
                    {
                        repreQueue.Enqueue(opeStack.Pop());
                    }
                }
                // 依次出队列计算结果
                Console.WriteLine("***********************************");
                Stack calculStack = new Stack();
                while (repreQueue.Count != 0)
                {
                    // 检测顶部是否是符号，若是符号，弹栈运算并压栈
                    if (((calculElems)repreQueue.Peek()).numOrOpe == false)
                    {
                        // 先检测是否可以弹栈
                        if (calculStack.Count >= 2)
                        {
                            // 根据顶部符号开始运算
                            double firstNum, secondNum;
                            double tempNum = 0;
                            if (((calculElems)repreQueue.Peek()).operater == '+')
                            {
                                repreQueue.Dequeue();
                                firstNum = ((calculElems)calculStack.Pop()).numuber;
                                secondNum = ((calculElems)calculStack.Pop()).numuber;
                                tempNum = secondNum + firstNum;
                            }
                            else if (((calculElems)repreQueue.Peek()).operater == '-')
                            {
                                repreQueue.Dequeue();
                                firstNum = ((calculElems)calculStack.Pop()).numuber;
                                secondNum = ((calculElems)calculStack.Pop()).numuber;
                                tempNum = secondNum - firstNum;
                            }
                            else if (((calculElems)repreQueue.Peek()).operater == '*')
                            {
                                repreQueue.Dequeue();
                                firstNum = ((calculElems)calculStack.Pop()).numuber;
                                secondNum = ((calculElems)calculStack.Pop()).numuber;
                                tempNum = secondNum * firstNum;
                            }
                            // 除法做除0检测
                            else if (((calculElems)repreQueue.Peek()).operater == '/')
                            {
                                repreQueue.Dequeue();
                                firstNum = ((calculElems)calculStack.Pop()).numuber;
                                secondNum = ((calculElems)calculStack.Pop()).numuber;
                                if (secondNum == 0)
                                {
                                    Console.WriteLine("Error");
                                    textBox2.Text = "Error";
                                    return;
                                }
                                tempNum = secondNum / firstNum;
                            }
                            calculElems newElem;
                            newElem.numOrOpe = true;
                            newElem.numuber = tempNum;
                            newElem.operater = ' ';
                            calculStack.Push(newElem);
                        }
                        // 若不可以弹栈
                        else
                        {
                            Console.WriteLine("Error:no enough number");
                            textBox2.Text = "Error";
                            return;
                        }
                    }
                    // 若是数字，则压入运算栈
                    else
                    {
                        calculStack.Push(repreQueue.Dequeue());
                    }
                }
                // 运算结束后输出结果，若数字未弹栈完成则报错
                if (calculStack.Count == 1)
                {
                    Console.WriteLine(((calculElems)calculStack.Peek()).numuber);
                    textBox2.Text = ((calculElems)calculStack.Peek()).numuber.ToString();
                    i = 1;
                }
                else
                {
                    Console.WriteLine("Error,too much number");
                    textBox2.Text = "Error";
                    return;
                }
            }
            else
            {
                if (i == 0)
                {
                    textBox1.Text += ((Button)sender).Text;
                }
                else
                {
                    textBox1.Text = ((Button)sender).Text;
                    i = 0;
                }
            }
        }
    }
}
