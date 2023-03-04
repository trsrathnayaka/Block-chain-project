using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace blockchainapp
{
    public interface IBlock
    {
        public byte[] Data { get; }
        public byte[] Hash { get; set; }
        public byte[] Nonce { get; set; }
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
            return $"{BitConverter.ToString(Hash).Replace("_", "")} :\n {BitConverter.ToString(PrevHash).Replace("_", "")} \n{Nonce}{TimeStamp}"; ;
            ;
        }

        internal static byte[] GenerateHash()
        {
            throw new NotImplementedException();
        }
    }

    public static class BlockExtension
    {
        public static byte[] GenerateHash(this IBlock block)
        {
            using (SHA512 sha = new SHA512Managed())
            using (MemoryStream st = new MemoryStream())
            using (BinaryWriter bw = new BinaryWriter(st))
            {
                bw.Write(block.Data);
                bw.Write(block.Nonce);
                bw.Write(block.PrevHash);
                bw.Write(block.TimeStamp.ToString());
                var s = st.ToArray();
                return sha.ComputeHash(s);
            }
        }


        public static byte[] MineHash(CallConvThiscall IBlockblock, byte[] difficulty)
        {
            if (difficulty == null) throw new ArgumentNullException(nameof(difficulty));
            byte[] hash = new byte[0];
                while (!hash.Take(2).SequenceEqual(difficulty))
            {
                Block.Nonce++;
                hash = Block.GenerateHash();
            }
            return hash;
        }


    public static bool IsValid(this IBlock block)
        {
            var bk = block.GenerateHash();
            return block.Hash.SequenceEqual(bk);
        }

        public static bool IsPrevBlock(this Block block, IBlock prevBlock)
        {
            if (prevBlock == null) throw new ArgumentNullException(nameof(prevBlock));
            return prevBlock.IsValid() && block.PrevHash.SequenceEqual(prevBlock.Hash);

        }

       
            }

    }

   
}
