using System;

namespace Lab2_2
{
    public class Message
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Text { get; set; }

        public override string ToString()
        {
            return $"{Name}: {Text}";
        }
    }
}
