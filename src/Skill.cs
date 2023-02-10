namespace BTFS
{
    using SFDGameScriptInterface;

    public abstract class Skill
    {
        protected Callbacks _cb;

        protected string Name { get; set; }
      
        protected string[] Description { get; set; }
        protected bool[] Conditions { get; set; }

        private Character character;
        private bool _isOwn = false;

        IPlayer _playerOwner;

        public Skill (Callbacks cb) { _cb = cb; }
        
        public void HandleCharacter(Character character) { this.character = character; }

        public IPlayer GetOwner() {  return _playerOwner; }
        public Character GetCharacter() { return this.character; }
        public PlayerModifiers GetModifiers() { return _playerOwner.GetModifiers(); }
        
        public bool IsOwn() { return _isOwn; }

        public void SetOwner(IPlayer player) {_playerOwner = player; _isOwn = true; }
        public void SetModifiers(PlayerModifiers mod) { _playerOwner.SetModifiers(mod); }
        public void SetCallbacks (Callbacks cb) { _cb = cb; }

        public abstract void Activate(IPlayer player);
    }
}
