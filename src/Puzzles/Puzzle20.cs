using Spectre.Console;

namespace AOC2023.Puzzles;


enum ModuleType
{
    FlipFlop,
    Conjunction,
    BroadCast
}

abstract class Module
{
    public static Queue<(string from, string to, bool low)> PulseQueue { get; set; } = new();
    public static long LowPulses { get; set; }
    public static long HighPulses { get; set; }

    public static Dictionary<string, Module> Modules { get; set; } = new();

    public List<string> LinkedModules { get; set; } = new();

    public void Link(string name)
    {
        if (LinkedModules.Contains(name)) return;
        LinkedModules.Add(name);
    }

    public string Name { get; set; }
    public ModuleType Type { get; set; }

    public virtual void ProcessPulse(string from, bool low)
    {
        if(low)
        {
            //AnsiConsole.WriteLine($"{from} -low-> {Name}");
            LowPulses++;
        }
        else
        {
            //AnsiConsole.WriteLine($"{from} -high-> {Name}");
            HighPulses++;
        }
    }



}

class FlipFlop : Module
{
    public FlipFlop()
    {
        Type = ModuleType.FlipFlop;
    }

    public bool State { get; set; } = false;
    public override void ProcessPulse(string from, bool low)
    {
        base.ProcessPulse(from, low);
        
        if (low)
        {
            State = !State;

            foreach (var linkedModule in LinkedModules)
            {
                PulseQueue.Enqueue((Name, linkedModule, !State));
                //Modules[linkedModule].ProcessPulse(Name, !State);
            }
        }
    }
}

class Conjunction : Module
{
    public Conjunction()
    {
        Type = ModuleType.Conjunction;
    }

    public Dictionary<string, bool> inputStates = new();

    public void AddInput(string name)
    {
        inputStates[name] = true;
    }
    
    public override void ProcessPulse(string from, bool low)
    {
        
        base.ProcessPulse(from, low);
        inputStates[from] = low;
        
        if (inputStates.Values.All(t => !t))
        {
            foreach (var linkedModule in LinkedModules)
            {
                PulseQueue.Enqueue((Name, linkedModule, true));
            }
        }
        else
        {
            foreach (var linkedModule in LinkedModules)
            {
                PulseQueue.Enqueue((Name, linkedModule, false));
            }
        }
    }
}

class BroadCaster : Module
{
    public BroadCaster()
    {
        Type = ModuleType.BroadCast;
    }

    public override void ProcessPulse(string from, bool low)
    {
        base.ProcessPulse(from, low);
        
        foreach (var linkedModule in LinkedModules)
        {
            PulseQueue.Enqueue((Name, linkedModule, low));
        }
        
    }
}


public class Puzzle20 : PuzzleBase
{

    private void LoadModules()
    {
        var lines = File.ReadAllLines("Data//puzzle20.txt");
        foreach (var line in lines)
        {
            string[] parts = line.Split(' ');
            string name = parts[0];
            Module m = new BroadCaster();
            if (name == "broadcaster")
            {
                m = new BroadCaster();
                m.Name = name;
            }
            else if (name.StartsWith("%"))
            {
                m = new FlipFlop();
                m.Name = name.Substring(1);
            }
            else if (name.StartsWith("&"))
            {
                m = new Conjunction();
                m.Name = name.Substring(1);
            }


            int j = 2;
            while (j < parts.Length)
            {
                m.Link(parts[j++].Trim(','));
            }

            Module.Modules[m.Name] = m;
            
        }

        foreach (var module in Module.Modules.Values.ToList())
        {
            foreach(var linkedModule in module.LinkedModules)
            {
                if(!Module.Modules.ContainsKey(linkedModule))
                {
                    Module.Modules[linkedModule] = new BroadCaster();
                    Module.Modules[linkedModule].Name = linkedModule;   
                }
                var lModule = Module.Modules[linkedModule];
                if(lModule.Type == ModuleType.Conjunction)
                {
                    ((Conjunction)lModule).AddInput(module.Name);
                }
            }
        }
        
    }
    
    public override void Part1()
    {
        AnsiConsole.WriteLine("Puzzle 20 part 1");
        AnsiConsole.WriteLine("Reading file");
        LoadModules();
        AnsiConsole.WriteLine("File read");

        for (int j = 0; j < 1000; j++)
        {
            AnsiConsole.WriteLine("Next cycle");
            Module.Modules["broadcaster"].ProcessPulse("button", true);
            while(Module.PulseQueue.Count > 0)
            {
                var pulse = Module.PulseQueue.Dequeue();
                Module.Modules[pulse.to].ProcessPulse(pulse.from, pulse.low);
            }
        }

        AnsiConsole.WriteLine($"Result: {Module.LowPulses} * {Module.HighPulses} = {Module.LowPulses * Module.HighPulses}");
    }

    public override void Part2()
    {
        AnsiConsole.WriteLine("Puzzle 20 part 2");
        AnsiConsole.WriteLine("Reading file");
        LoadModules();
        AnsiConsole.WriteLine("File read");

        int count = 1;
        bool finished = false;

        Dictionary<string, int> nrCounts = new Dictionary<string, int>();

        int nrInputs = ((Conjunction)Module.Modules["nr"]).inputStates.Count;
        
        while(!finished)
        {
            //AnsiConsole.WriteLine("Next cycle");
            Module.Modules["broadcaster"].ProcessPulse("button", true);
            while(Module.PulseQueue.Count > 0)
            {
                var pulse = Module.PulseQueue.Dequeue();

                if (pulse.to == "nr" && !pulse.low)
                {
                    if (!nrCounts.ContainsKey(pulse.from))
                    {
                        nrCounts[pulse.from] = count;
                    }
                    
                    
                    
                }

                if (nrInputs == nrCounts.Keys.Count)
                {
                    finished = true;
                }

                Module.Modules[pulse.to].ProcessPulse(pulse.from, pulse.low);
            }

            count++;
        }

        long total = LCM(nrCounts.Values.ToList());
        // foreach(var val in nrCounts.Values)
        // {
        //     total *= val;
        // }

        AnsiConsole.WriteLine($"Button presses: {total}");
    }
    
    public long GCD(long a, long b)
    {
        while (b != 0)
        {
            long temp = b;
            b = a % b;
            a = temp;
        }
        return a;
    }

    public long LCM(long a, long b)
    {
        return (a / GCD(a, b)) * b;
    }

    public long LCM(List<int> numbers)
    {
        long lcm = numbers[0];
        for (int i = 1; i < numbers.Count; i++)
        {
            lcm = LCM(lcm, numbers[i]);
        }
        return lcm;
    }
}