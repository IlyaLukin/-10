using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;

namespace ConsoleApp48
{
    class Program
    {
        static void Main(string[] args)
        {
            
                string nStr;
                int n;
                string mStr;
                int m;
                DoublyLinkedList<int> List = new DoublyLinkedList<int>();

                Console.Write("Введите число игроков n=");
                nStr = Console.ReadLine();

                Console.Write("Введите номер выбывающего игрока из круга m=");
                mStr = Console.ReadLine();

                if (int.TryParse(nStr, out n) && int.TryParse(mStr, out m))
                {
                   
                    List.AddAllPlayers(1, n);

                    ListElement<int> curEl = List.Head;
                   
                    while (List.Count > 1)
                    {
                        ListElement<int> lastFoundEl = List.RemoveWithPosition(curEl, 1, m);
                        Console.WriteLine("Игрок номер " + lastFoundEl.Data + " выбыл.");
                        curEl = lastFoundEl.Next;
                    }
                    Console.WriteLine("Остался игрок с номером " + curEl.Next.Data);
                }
                else
                {
                    Console.WriteLine("Введено не число!");
                }

                Console.Write("Для закрытия нажмите любую клавишу");
                string key = Console.ReadLine();
                string upperKey = key.ToUpper();
           
        }
    }

    public class ListElement<Int>
    {
        public ListElement(int data)
        {
            Data = data;
        }
        public int Data { get; set; }
        public ListElement<int> Previous { get; set; }
        public ListElement<int> Next { get; set; }
    }

    public class DoublyLinkedList<Int> : IEnumerable<int>  
    {
        ListElement<int> head; 
        ListElement<int> tail; 
        int count;  

        public ListElement<int> Head { get { return head; } }
        public int Count { get { return count; } }

        public void AddAllPlayers(int currElNumber, int maxCount)
        {
            if (currElNumber == maxCount + 1) return;
            else
            {
                ListElement<int> node = new ListElement<int>(currElNumber);
                node.Data = currElNumber;

                if (head == null)
                    head = node;
                else
                {
                    tail.Next = node;
                    node.Previous = tail;
                }
                tail = node;

                
                if (currElNumber == maxCount)
                {
                    tail.Next = head;
                    head.Previous = tail;
                }
                currElNumber++;
                count++;

                AddAllPlayers(currElNumber, maxCount);
            }
        }

        public ListElement<int> RemoveWithPosition(ListElement<int> currEl, int currPosition, int positionM)
        {
            
            if (currPosition == positionM)
            {
                currEl.Next.Previous = currEl.Previous;
                currEl.Previous.Next = currEl.Next;

                count--;
                return currEl;
            }
            else
            {
                currPosition++;
                return RemoveWithPosition(currEl.Next, currPosition, positionM);
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable)this).GetEnumerator();
        }

        IEnumerator<int> IEnumerable<int>.GetEnumerator()
        {
            ListElement<int> current = head;
            while (current != null)
            {
                yield return current.Data;
                current = current.Next;
            }
        }
    }
}
        
    

