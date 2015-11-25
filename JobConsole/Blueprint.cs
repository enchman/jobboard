using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobConsole
{
    class Blueprint
    {
        public enum Instruction { Punching = 4, Welding = 10, Bending = 6, LaserCutter = 10, Milling = 10, Shears = 6, Assembling = 10 }
        public enum DefaultItem { Customized, Normal, Bread, Toast, Press }

        public int Id;
        public DefaultItem Type;
        public Dictionary<Instruction, int> WorkLines = new Dictionary<Instruction, int> { };


        /// <summary>
        /// Initiate Customized Makeup (Dynamic)
        /// </summary>
        /// <param name="instructions">List of Requirements</param>
        public Blueprint(List<Instruction> instructions)
        {
            foreach (Instruction item in instructions)
            {
                if (WorkLines.ContainsKey(item))
                {
                    WorkLines[item] += 1;
                }
                else
                {
                    WorkLines.Add(item, 1);
                }
            }
        }

        /// <summary>
        /// Initiate Customized Makeup
        /// </summary>
        /// <param name="instructions">Dictionary List of Requirements</param>
        public Blueprint(Dictionary<Instruction, int> instructions)
        {
            this.WorkLines = instructions;
        }

        private void SetDefault(DefaultItem choice)
        {
            if (choice != DefaultItem.Customized)
            {
                if (choice == DefaultItem.Normal)
                {
                    this.WorkLines.Add(Instruction.LaserCutter, 1);
                    this.WorkLines.Add(Instruction.Bending, 1);
                    this.WorkLines.Add(Instruction.Assembling, 1);
                }
                else if (choice == DefaultItem.Bread)
                {
                    this.WorkLines.Add(Instruction.Milling, 1);
                    this.WorkLines.Add(Instruction.LaserCutter, 1);
                    this.WorkLines.Add(Instruction.Bending, 1);
                    this.WorkLines.Add(Instruction.Assembling, 1);
                }
                else if (choice == DefaultItem.Toast)
                {
                    this.WorkLines.Add(Instruction.Milling, 1);
                    this.WorkLines.Add(Instruction.Punching, 1);
                    this.WorkLines.Add(Instruction.LaserCutter, 1);
                    this.WorkLines.Add(Instruction.Shears, 1);
                    this.WorkLines.Add(Instruction.Bending, 1);
                    this.WorkLines.Add(Instruction.Assembling, 1);
                }
                else if (choice == DefaultItem.Press)
                {
                    this.WorkLines.Add(Instruction.Milling, 1);
                    this.WorkLines.Add(Instruction.Bending, 1);
                    this.WorkLines.Add(Instruction.Assembling, 1);
                }
            }
        }
    }
}
