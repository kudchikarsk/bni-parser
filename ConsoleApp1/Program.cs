using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            var text = File.ReadAllLines(@"C:\Users\Ritesh\Desktop\New folder (2)\A.txt");
            var data = text.Where(t=>!String.IsNullOrWhiteSpace(t)).ToList();
            //Console.WriteLine(String.Join("\n",data));
            var members = new List<Member>();

            var blockCount = 0;
            var blockSize = 8;
            var member = new Member();
            foreach (var line in data)
            {
               
                
                switch (blockCount)
                {
                    case 1:
                        member.name = line;
                        break;
                    case 2:
                        member.expertise = line;
                        break;
                    case 3:
                        member.designation = line;
                        break;
                    case 6:
                        member.mobile = line.Replace("T:", "").Trim(); 
                        break;
                    case 7:
                        member.email = line.Replace("E:","").Trim();
                        break;
                    default:
                        break;
                }

                blockCount++;
                if (blockCount >= blockSize)
                {
                    member.profilePic = $"/assets/img/profile-pic/{member.name.Trim().Replace(" ","-")}.jpg";
                    members.Add(
                    member
                    );
                    member = new Member();
                    blockCount = 0;
                } 
            }
            var json = JsonConvert.SerializeObject(members,Formatting.Indented);
            File.WriteAllText(@"C:\Users\Ritesh\Desktop\New folder (2)\A.json", json.ToString());
            //Console.WriteLine(json);
            Console.WriteLine("Done");
            Console.ReadLine();
        }
    }
}
