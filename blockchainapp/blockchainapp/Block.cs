using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace blockchainapp
{
    public interface IBlock
    {
       public  byte[] Data { get; }
       public  byte[] Hash { get; set; }
       public  byte[] Nonce { get; set; }
       public byte[] PrevHash { get; set; }
       public DateTime TimeStamp { get; set; }

    }
    public class Block : IBlock 
    {
        public Block(byte[] data)
        {
            Data = data ?? throw new ArgumentNullException(nameof(data));
            Nonce = 0;
            PrevHash = new byte[] { 0 * 00 };
            TimeStamp = DateTime.Now;
        }
        public byte[] Data { get; }
        public byte[] Hash { get; set; }
        public int Nonce { get; set; }
        public byte[] PrevHash { get; set; }
        public DateTime TimeStamp { get; set; }
    
        public override string ToString()
        {
            return $"{BitConverter.ToString(Hash).Replace("_","")} :\n { BitConverter.ToString(PrevHash).Replace("_", "")} \n{ Nonce}{ TimeStamp}"; ;
                    ;
        }
    }
}
