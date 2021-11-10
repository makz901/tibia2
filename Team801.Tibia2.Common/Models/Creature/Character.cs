namespace Team801.Tibia2.Common.Models.Creature
{
    public class Character : Creature
    {
        public int Level { get; set; }

        public override string ToString()
        {
            return $"[{Name} lv:{Level}]";
        }
    }
}