using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrainingSystem
{
    // =====================================================
    // ABSTRACT CLASS : User
    // =====================================================
    // abstract class = คลาสต้นแบบ
    // ไม่สามารถสร้าง object จากคลาสนี้โดยตรงได้
    // ใช้เป็นคลาสแม่ให้ Student, Teacher, Guest สืบทอด
    public abstract class User
    {
        // ตัวแปรเก็บข้อมูลพื้นฐานของทุกคน
        protected string firstName;
        protected string lastName;
        protected string phone;
        protected string email;

        // Constructor ใช้กำหนดค่าเริ่มต้นตอนสร้าง object
        public User(string firstName, string lastName, string phone, string email)
        {
            this.firstName = firstName;
            this.lastName = lastName;
            this.phone = phone;
            this.email = email;
        }

        // Method สำหรับแสดงข้อมูลพื้นฐาน
        // virtual = อนุญาตให้ class ลูก override ได้
        public virtual void ShowInfo()
        {
            Console.WriteLine($"ชื่อ: {firstName} {lastName}");
            Console.WriteLine($"เบอร์โทรศัพท์: {phone}");
            Console.WriteLine($"อีเมล: {email}");
        }
    }

    // =====================================================
    // INTERFACE : IMember
    // =====================================================
    // Interface สำหรับ "ผู้เข้าอบรม"
    // ใคร implement interface นี้ จะต้องมี method RegisterCourse()
    public interface IMember
    {
        void RegisterCourse(); // เมธอดลงทะเบียนอบรม
    }

    // =====================================================
    // INTERFACE : ITrainer
    // =====================================================
    // Interface สำหรับ "วิทยากร"
    // ใคร implement ต้องสามารถสอนและอนุมัติผลได้
    public interface ITrainer
    {
        void Teach();           // สอนอบรม
        void ApproveResult();   // อนุมัติผล
    }

    // =====================================================
    // CLASS : Student (นักศึกษา)
    // =====================================================
    // สืบทอดจาก User และเป็น IMember
    // หมายความว่า นักศึกษาลงทะเบียนอบรมได้
    public class Student : User, IMember
    {
        private string studentId;  // รหัสนักศึกษา
        private string major;      // สาขา

        public Student(string firstName, string lastName, string phone, string email,
                       string studentId, string major)
            : base(firstName, lastName, phone, email) // เรียก constructor ของ User
        {
            this.studentId = studentId;
            this.major = major;
        }

        // override เพื่อแสดงข้อมูลเพิ่มจากคลาสแม่
        public override void ShowInfo()
        {
            base.ShowInfo(); // เรียกแสดงข้อมูลพื้นฐานก่อน
            Console.WriteLine($"รหัสนักศึกษา: {studentId}");
            Console.WriteLine($"สาขา: {major}");
        }

        // implement method จาก IMember
        public void RegisterCourse()
        {
            Console.WriteLine(">> นักศึกษาลงทะเบียนอบรมเรียบร้อยแล้ว");
        }
    }

    // =====================================================
    // CLASS : Teacher (อาจารย์)
    // =====================================================
    // อาจารย์สามารถเป็นผู้เข้าอบรม และเป็นวิทยากรได้
    // จึง implement ทั้ง IMember และ ITrainer
    public class Teacher : User, IMember, ITrainer
    {
        private string major;              // สาขา
        private string academicPosition;   // ตำแหน่งวิชาการ

        public Teacher(string firstName, string lastName, string phone, string email,
                       string major, string academicPosition)
            : base(firstName, lastName, phone, email)
        {
            this.major = major;
            this.academicPosition = academicPosition;
        }

        public override void ShowInfo()
        {
            base.ShowInfo();
            Console.WriteLine($"สาขา: {major}");
            Console.WriteLine($"ตำแหน่งทางวิชาการ: {academicPosition}");
        }

        public void RegisterCourse()
        {
            Console.WriteLine(">> อาจารย์ลงทะเบียนอบรมเรียบร้อยแล้ว");
        }

        public void Teach()
        {
            Console.WriteLine(">> อาจารย์กำลังทำการอบรม");
        }

        public void ApproveResult()
        {
            Console.WriteLine(">> อาจารย์อนุมัติผลการอบรมแล้ว");
        }
    }

    // =====================================================
    // CLASS : Guest (บุคคลทั่วไป)
    // =====================================================
    // บุคคลทั่วไปสามารถลงทะเบียน และสามารถเป็นวิทยากรได้
    public class Guest : User, IMember, ITrainer
    {
        private string workplace;  // สถานที่ทำงาน
        private string position;   // ตำแหน่ง

        public Guest(string firstName, string lastName, string phone, string email,
                     string workplace, string position)
            : base(firstName, lastName, phone, email)
        {
            this.workplace = workplace;
            this.position = position;
        }

        public override void ShowInfo()
        {
            base.ShowInfo();
            Console.WriteLine($"สถานที่ทำงาน: {workplace}");
            Console.WriteLine($"ตำแหน่ง: {position}");
        }

        public void RegisterCourse()
        {
            Console.WriteLine(">> บุคคลทั่วไปลงทะเบียนอบรมเรียบร้อยแล้ว");
        }

        public void Teach()
        {
            Console.WriteLine(">> วิทยากรกำลังอบรม");
        }

        public void ApproveResult()
        {
            Console.WriteLine(">> วิทยากรอนุมัติผลการอบรมแล้ว");
        }
    }

    // =====================================================
    // PROGRAM MAIN
    // =====================================================
    class Program
    {
        static void Main(string[] args)
        {
            // List เก็บผู้เข้าอบรม (Student, Teacher, Guest)
            List<IMember> members = new List<IMember>();

            // List เก็บวิทยากร (Teacher, Guest)
            List<ITrainer> trainers = new List<ITrainer>();

            while (true)
            {
                Console.WriteLine("\n===== ระบบรับสมัครฝึกอบรม =====");
                Console.WriteLine("1. เพิ่มนักศึกษา");
                Console.WriteLine("2. เพิ่มอาจารย์");
                Console.WriteLine("3. เพิ่มบุคคลทั่วไป");
                Console.WriteLine("4. แสดงรายชื่อผู้เข้าอบรม");
                Console.WriteLine("5. แสดงรายชื่อวิทยากร");
                Console.WriteLine("0. ออกจากระบบ");
                Console.Write("เลือกเมนู: ");

                string choice = Console.ReadLine();

                if (choice == "0")
                    break;

                switch (choice)
                {
                    case "1":
                        // รับข้อมูลนักศึกษา
                        Console.Write("ชื่อ: ");
                        string sfn = Console.ReadLine();
                        Console.Write("นามสกุล: ");
                        string sln = Console.ReadLine();
                        Console.Write("เบอร์โทร: ");
                        string sphone = Console.ReadLine();
                        Console.Write("Email: ");
                        string semail = Console.ReadLine();
                        Console.Write("รหัสนักศึกษา: ");
                        string sid = Console.ReadLine();
                        Console.Write("สาขา: ");
                        string smajor = Console.ReadLine();

                        Student s = new Student(sfn, sln, sphone, semail, sid, smajor);
                        members.Add(s);     // เพิ่มเข้า list ผู้เข้าอบรม
                        s.RegisterCourse();
                        break;

                    case "2":
                        // รับข้อมูลอาจารย์
                        Console.Write("ชื่อ: ");
                        string tfn = Console.ReadLine();
                        Console.Write("นามสกุล: ");
                        string tln = Console.ReadLine();
                        Console.Write("เบอร์โทร: ");
                        string tphone = Console.ReadLine();
                        Console.Write("Email: ");
                        string temail = Console.ReadLine();
                        Console.Write("สาขา: ");
                        string tmajor = Console.ReadLine();
                        Console.Write("ตำแหน่งวิชาการ: ");
                        string tpos = Console.ReadLine();

                        Teacher t = new Teacher(tfn, tln, tphone, temail, tmajor, tpos);
                        members.Add(t);

                        // ถามว่าจะให้เป็นวิทยากรหรือไม่
                        Console.Write("ต้องการให้เป็นวิทยากรด้วยหรือไม่ (y/n): ");
                        if (Console.ReadLine().ToLower() == "y")
                        {
                            trainers.Add(t);
                        }

                        t.RegisterCourse();
                        break;

                    case "3":
                        // รับข้อมูลบุคคลทั่วไป
                        Console.Write("ชื่อ: ");
                        string gfn = Console.ReadLine();
                        Console.Write("นามสกุล: ");
                        string gln = Console.ReadLine();
                        Console.Write("เบอร์โทร: ");
                        string gphone = Console.ReadLine();
                        Console.Write("Email: ");
                        string gemail = Console.ReadLine();
                        Console.Write("สถานที่ทำงาน: ");
                        string gwork = Console.ReadLine();
                        Console.Write("ตำแหน่ง: ");
                        string gpos = Console.ReadLine();

                        Guest g = new Guest(gfn, gln, gphone, gemail, gwork, gpos);
                        members.Add(g);

                        Console.Write("ต้องการให้เป็นวิทยากรด้วยหรือไม่ (y/n): ");
                        if (Console.ReadLine().ToLower() == "y")
                        {
                            trainers.Add(g);
                        }

                        g.RegisterCourse();
                        break;

                    case "4":
                        // แสดงรายชื่อผู้เข้าอบรม
                        Console.WriteLine("\n===== รายชื่อผู้เข้าอบรม =====");
                        foreach (var m in members)
                        {
                            Console.WriteLine("-----------------------");
                            ((User)m).ShowInfo();
                        }
                        break;

                    case "5":
                        // แสดงรายชื่อวิทยากร
                        Console.WriteLine("\n===== รายชื่อวิทยากร =====");
                        foreach (var tr in trainers)
                        {
                            Console.WriteLine("-----------------------");
                            ((User)tr).ShowInfo();
                        }
                        break;
                }
            }
        }
    }
}

