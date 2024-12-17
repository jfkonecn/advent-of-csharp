namespace AdventOfCSharp.Console.Year2024.Day17;

public static class Solution202417
{
    private enum Operand
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

    public static int Solution1(string[] fileContents)
    {
        var cpu = Parse(fileContents);
        System.Console.WriteLine(cpu);
        foreach (var instruction in cpu.Instructions)
        {
            System.Console.WriteLine(instruction);
        }
        throw new NotImplementedException();
    }

    public static int Solution2(string[] fileContents)
    {
        throw new NotImplementedException();
    }
}
