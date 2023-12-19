using Spectre.Console;

namespace AOC2023.Puzzles;

public enum Op
{
    LargerThan,
    LessThan
}

public record Rule(string field, Op op, int value, string action);

public class Workflow
{
    public string Name { get; set; } = "";
    public List<Rule> Rules { get; set; } = new();
    public string EndState { get; set; } = "";
}

public class Part
{
    public Dictionary<string, int> Properties { get; set; } = new();
}


public class Puzzle19 : PuzzleBase
{
    private Workflow startWorkflow = new();
    private Dictionary<string, Workflow> workflows = new();
    private List<Part> parts = new();

    private void LoadData()
    {
        var lines = File.ReadAllLines("Data//puzzle19.txt");
        int j = 0;
        while (lines[j].Trim() != "")
        {
            string line = lines[j++];
            int idx = line.IndexOf('{');
            string name = line.Substring(0, idx).Trim();
            Workflow workflow = new Workflow();
            workflow.Name = name;
            workflows[name] = workflow;
            line = line.Substring(idx).Trim('{').Trim('}');
            var rules = line.Split(",");
            foreach (var rule in rules)
            {
                if (rule.IndexOf(":") > 0)
                {
                    var parts = rule.Split(":");
                    var field = parts[0][0].ToString();
                    var op = parts[0][1];
                    var value = int.Parse(parts[0].Substring(2).Trim());
                    workflow.Rules.Add(new Rule(field, op == '>' ? Op.LargerThan : Op.LessThan, value, parts[1]));
                }
                else
                {
                    workflow.EndState = rule;
                }
            }
            if(j == 1) startWorkflow = workflow;
        }

        j++;
        
        while (j < lines.Length)
        {
            Part p = new Part();
            string line = lines[j++];
            line = line.Trim('{').Trim('}');
            var lineParts = line.Split(",");
            foreach (var part in lineParts)
            {
                p.Properties[part[0].ToString()] = int.Parse(part.Substring(2));
            }
            parts.Add(p);
        }
    }

    private void ProcessParts()
    {
        int totalValue = 0;
        
        foreach (var part in parts)
        {
            bool result = EvaluatePart(part);
            if(result)
                totalValue += part.Properties.Values.Sum();
        }
        
        AnsiConsole.WriteLine("Total value is " + totalValue);
    }

    private bool EvaluatePart(Part part)
    {
        Workflow workflow = workflows["in"];
        while (true)
        {
            string action = "";
            foreach (var rule in workflow.Rules)
            {
                int value = part.Properties[rule.field];
                    
                if (rule.op == Op.LargerThan)
                {
                    if (value > rule.value)
                    {
                        action = rule.action;
                        break;
                    }
                }
                else
                {
                    if (value < rule.value)
                    {
                        action = rule.action;
                        break;
                    }
                }

                if (action != "")
                {
                    break;
                }
            }

            if(action == "")
                action = workflow.EndState;
                
            if (action != "")
            {
                if (action == "A")
                {
                    AnsiConsole.WriteLine(part.Properties.ToString() + " -> A");
                    return true;
                    
                }
                else if (action == "R")
                {
                    return false;
                }
                else
                {
                    workflow = workflows[action];
                }
            }

                
        }

        
    }

    public override void Part1()
    {
        AnsiConsole.WriteLine("Puzzle 19 part 1");
        AnsiConsole.WriteLine("Reading file");
        LoadData();
        AnsiConsole.WriteLine("File read");
        
        ProcessParts();
    }

    public override void Part2()
    {
        AnsiConsole.WriteLine("Puzzle 19 part 2");
        AnsiConsole.WriteLine("Reading file");
        LoadData();
        AnsiConsole.WriteLine("File read");
        
        
    }
}