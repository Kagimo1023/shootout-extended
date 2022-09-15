namespace Shootout
{
    using SFDGameScriptInterface;

    public abstract class Effect
    {
        protected Callbacks _cb;

        protected string Name { get; set; }
        protected string[] Description { get; set; }

        private Character character;
        private bool _isOwn = false;

        IPlayer _playerOwner;

        public Effect(Callbacks cb) => _cb = cb;
        
        public void HandleCharacter(Character character) => this.character = character;

        public IPlayer GetOwner() => _playerOwner;
        public Character GetCharacter() => this.character;
        public PlayerModifiers GetModifiers() => _playerOwner.GetModifiers();
        
        public bool IsOwn() => _isOwn;

        public void SetOwner(IPlayer player) {_playerOwner = player; _isOwn = true; }
        public void SetModifiers(PlayerModifiers mod) => _playerOwner.SetModifiers(mod);

        public abstract void Activate(IPlayer player);
    }
}
