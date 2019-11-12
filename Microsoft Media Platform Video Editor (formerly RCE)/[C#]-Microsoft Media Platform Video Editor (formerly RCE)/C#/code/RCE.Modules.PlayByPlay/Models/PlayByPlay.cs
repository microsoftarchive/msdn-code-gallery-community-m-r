namespace RCE.Modules.PlayByPlay.Models
{
    public class PlayByPlay
    {
        private readonly long offset;

        public PlayByPlay(long offset)
        {
            this.offset = offset;
        }

        public string Text { get; set; }

        public long TimeWithOffset 
        { 
            get
            {
                return this.TimeWithoutOffset + this.offset;
            }
        }

        public long TimeWithoutOffset { get; set; }
    }
}
