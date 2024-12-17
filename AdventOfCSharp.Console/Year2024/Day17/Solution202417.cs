namespace AdventOfCSharp.Console.Year2024.Day17;

public static class Solution202417
{
    private enum Instruction
    {
        /*
         * The adv instruction (opcode 0) performs division. The numerator is
         * the value in the A register. The denominator is found by raising 2
         * to the power of the instruction's combo operand. (So, an operand of
         * 2 would divide A by 4 (2^2); an operand of 5 would divide A by 2^B.)
         * The result of the division operation is truncated to an integer and
         * then written to the A register.
         * */
        Adv,

        /*
         * The bxl instruction (opcode 1) calculates the bitwise XOR of
         * register B and the instruction's literal operand, then stores the
         * result in register B.
         * */
        Bxl,

        /*
         * The bst instruction (opcode 2) calculates the value of its combo
         * operand modulo 8 (thereby keeping only its lowest 3 bits), then
         * writes that value to the B register.
         * */
        Bst,

        /*
         * The jnz instruction (opcode 3) does nothing if the A register is 0.
         * However, if the A register is not zero, it jumps by setting the
         * instruction pointer to the value of its literal operand; if this
         * instruction jumps, the instruction pointer is not increased by 2
         * after this instruction.
         * */
        Jnz,

        /*
         * The bxc instruction (opcode 4) calculates the bitwise XOR of
         * register B and register C, then stores the result in register B.
         * (For legacy reasons, this instruction reads an operand but ignores it.)
         * */
        Bxc,

        /*
         * The out instruction (opcode 5) calculates the value of its combo
         * operand modulo 8, then outputs that value.
         * (If a program outputs multiple values, they are separated by commas.)
         * */
        Out,

        /*
         * The bdv instruction (opcode 6) works exactly like the adv instruction
         * except that the result is stored in the B register.
         * (The numerator is still read from the A register.)
         * */
        Bdv,

        /*
         * The cdv instruction (opcode 7) works exactly like the adv instruction
         * except that the result is stored in the C register.
         * (The numerator is still read from the A register.)
         * */
        Cdv,
    }

    private record Cpu
    {
        public long ARegister { get; set; }
        public long BRegister { get; set; }
        public long CRegister { get; set; }
        public long InstructionPointer { get; set; }
        public required short[] Instructions { get; init; }
    }

    private static Cpu Parse(string[] fileContents)
    {
        return new()
        {
            ARegister = GetRegisterValue(0),
            BRegister = GetRegisterValue(1),
            CRegister = GetRegisterValue(2),
            InstructionPointer = 0,
            Instructions = fileContents[4]
                .Split(":")[1]
                .Trim()
                .Split(",")
                .Select(short.Parse)
                .ToArray(),
        };
        long GetRegisterValue(int i)
        {
            return long.Parse(fileContents[i].Split(":")[1].Trim());
        }
    }

    public static string Solution1(string[] fileContents)
    {
        var output = new List<string>();
        var cpu = Parse(fileContents);
        while (cpu.InstructionPointer < cpu.Instructions.Length)
        {
            var instruction = (Instruction)cpu.Instructions[cpu.InstructionPointer];
            var literalOperand = cpu.Instructions[cpu.InstructionPointer + 1];
            //Combo operands 0 through 3 represent literal values 0 through 3.
            //Combo operand 4 represents the value of register A.
            //Combo operand 5 represents the value of register B.
            //Combo operand 6 represents the value of register C.
            //Combo operand 7 is reserved and will not appear in valid programs.
            var comboOperand = literalOperand switch
            {
                0 or 1 or 2 or 3 => literalOperand,
                4 => cpu.ARegister,
                5 => cpu.BRegister,
                6 => cpu.CRegister,
                7 => throw new Exception("7 is not supported"),
                _ => throw new Exception($"{literalOperand} is not a known operand"),
            };

            if (instruction == Instruction.Adv)
            {
                var numerator = cpu.ARegister;
                var denominator = (long)Math.Pow(2, comboOperand);
                cpu.ARegister = numerator / denominator;
                cpu.InstructionPointer += 2;
            }
            else if (instruction == Instruction.Bxl)
            {
                cpu.BRegister ^= literalOperand;
                cpu.InstructionPointer += 2;
            }
            else if (instruction == Instruction.Bst)
            {
                cpu.BRegister = comboOperand % 8;
                cpu.InstructionPointer += 2;
            }
            else if (instruction == Instruction.Jnz)
            {
                if (cpu.ARegister != 0)
                {
                    cpu.InstructionPointer = literalOperand;
                }
                else
                {
                    cpu.InstructionPointer += 2;
                }
            }
            else if (instruction == Instruction.Bxc)
            {
                cpu.BRegister ^= cpu.CRegister;
                cpu.InstructionPointer += 2;
            }
            else if (instruction == Instruction.Out)
            {
                output.Add((comboOperand % 8).ToString());
                cpu.InstructionPointer += 2;
            }
            else if (instruction == Instruction.Bdv)
            {
                var numerator = cpu.ARegister;
                var denominator = (long)Math.Pow(2, comboOperand);
                cpu.BRegister = numerator / denominator;
                cpu.InstructionPointer += 2;
            }
            else if (instruction == Instruction.Cdv)
            {
                var numerator = cpu.ARegister;
                var denominator = (long)Math.Pow(2, comboOperand);
                cpu.CRegister = numerator / denominator;
                cpu.InstructionPointer += 2;
            }
            else
            {
                throw new Exception($"Unknown Op {instruction}");
            }
        }
        return string.Join(",", output);
    }

    public static int Solution2(string[] fileContents)
    {
        throw new NotImplementedException();
    }
}
